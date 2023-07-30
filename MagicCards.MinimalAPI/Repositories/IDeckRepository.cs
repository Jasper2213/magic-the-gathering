namespace MagicCards.MinimalAPI.Repositories
{
    public interface IDeckRepository
    {
        Task<IEnumerable<DeckItem>> GetAllCards();
        Task AddCardToDeckAsync(CardReadDTO card);
        Task DeleteCardAsync(string id);
        Task ClearDeck();
    }
}
