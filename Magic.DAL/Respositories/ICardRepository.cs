namespace MagicCards.DAL.Respositories
{
    public interface ICardRepository
    {
        IQueryable<Card> GetCards();
        Card? GetCardById(int id);
    }
}
