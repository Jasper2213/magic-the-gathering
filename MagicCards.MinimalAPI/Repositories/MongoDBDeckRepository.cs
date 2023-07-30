namespace MagicCards.MinimalAPI.Repositories
{
    public class MongoDBDeckRepository : IDeckRepository
    {
        private readonly IMongoCollection<DeckItem> _cardCollection;

        public MongoDBDeckRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _cardCollection = database.GetCollection<DeckItem>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<IEnumerable<DeckItem>> GetAllCards()
        {
            return await _cardCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddCardToDeckAsync(CardReadDTO card)
        {
            IEnumerable<DeckItem> allCards = await GetAllCards();

            if (allCards?.Sum(i => i.Quantity) < 60)
            {
                DeckItem itemToUpdate = new() { Card = card, Id = card.Id };

                FilterDefinition<DeckItem> filter = Builders<DeckItem>.Filter.Eq(d => d.Id, itemToUpdate.Id);
                DeckItem existingItem = _cardCollection.Find(filter).FirstOrDefault();

                if (existingItem != null)
                {
                    UpdateDefinition<DeckItem> update = Builders<DeckItem>.Update.Inc(d => d.Quantity, 1);
                    await _cardCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    await _cardCollection.InsertOneAsync(itemToUpdate);
                }
            }

            return;
        }

        public async Task DeleteCardAsync(string id)
        {
            FilterDefinition<DeckItem> filter = Builders<DeckItem>.Filter.Eq("Id", id);

            DeckItem existingItem = _cardCollection.Find(filter).FirstOrDefault();

            if (existingItem != null)
            {
                if (existingItem.Quantity > 1)
                {
                    UpdateDefinition<DeckItem> update = Builders<DeckItem>.Update.Inc(d => d.Quantity, -1);
                    await _cardCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    await _cardCollection.DeleteOneAsync(filter);
                }
            }

            return;
        }

        public async Task ClearDeck()
        {
            await _cardCollection.DeleteManyAsync("{}");
        }
    }
}
