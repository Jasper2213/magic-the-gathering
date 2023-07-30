namespace MagicCards.DAL.Respositories
{
    public class SqlCardRepository : ICardRepository
    {
        private readonly mtgContext _db;

        public SqlCardRepository(mtgContext mtgContext)
        {
            _db = mtgContext;
        }

        public IQueryable<Card> GetCards()
        {
            IQueryable<Card> allCards = _db.Cards
                .Select(c => new Card
                {
                    Id = c.Id,
                    Name = c.Name,
                    ManaCost = c.ManaCost,
                    Type = c.Type,
                    RarityCode = c.RarityCode,
                    SetCode = c.SetCode,
                    Text = c.Text,
                    Flavor = c.Flavor,
                    Power = c.Power,
                    Toughness = c.Toughness,
                    Layout = c.Layout,
                    OriginalImageUrl = c.OriginalImageUrl,
                    Image = c.Image,
                    Artist = new Artist
                    {
                        FullName = c.Artist.FullName
                    },
                    RarityCodeNavigation = new Rarity
                    {
                        Code = c.RarityCodeNavigation.Code,
                        Name = c.RarityCodeNavigation.Name
                    },
                    SetCodeNavigation = new Set
                    {
                        Code = c.SetCodeNavigation.Code,
                        Name = c.SetCodeNavigation.Name
                    },
                    CardColors = c.CardColors.Select(cc => new CardColor
                    {
                        Color = cc.Color
                    }).ToList(),
                    CardTypes = c.CardTypes.Select(ct => new CardType
                    {
                        Type = ct.Type
                    }).ToList()
                });

            return allCards;
        }

        public Card GetCardById(int id)
        {
            Card? singleCard = _db.Cards
                .Select(c => new Card
                {
                    Id = c.Id,
                    Name = c.Name,
                    ManaCost = c.ManaCost,
                    ConvertedManaCost = c.ConvertedManaCost,
                    Type = c.Type,
                    RarityCode = c.RarityCode,
                    SetCode = c.SetCode,
                    Text = c.Text,
                    Flavor = c.Flavor,
                    ArtistId = c.ArtistId,
                    Number = c.Number,
                    Power = c.Power,
                    Toughness = c.Toughness,
                    Layout = c.Layout,
                    MultiverseId = c.MultiverseId,
                    OriginalImageUrl = c.OriginalImageUrl,
                    Image = c.Image,
                    OriginalText = c.OriginalText,
                    OriginalType = c.OriginalType,
                    MtgId = c.MtgId,
                    Variations = c.Variations,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Artist = new Artist
                    {
                        FullName = c.Artist.FullName
                    },
                    RarityCodeNavigation = new Rarity
                    {
                        Code = c.RarityCodeNavigation.Code,
                        Name = c.RarityCodeNavigation.Name
                    },
                    SetCodeNavigation = new Set
                    {
                        Code = c.SetCodeNavigation.Code,
                        Name = c.SetCodeNavigation.Name
                    },
                    CardColors = c.CardColors.Select(cc => new CardColor
                    {
                        Color = cc.Color
                    }).ToList(),
                    CardTypes = c.CardTypes.Select(ct => new CardType
                    {
                        Type = ct.Type
                    }).ToList()
                })
                .SingleOrDefault(c =>  c.Id == id);

            return singleCard;
        }
    }
}
