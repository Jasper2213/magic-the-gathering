namespace MagicCards.MinimalAPI.Models
{
    public class DeckItem
    {
        public long Id { get; set; }
        public CardReadDTO Card { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
