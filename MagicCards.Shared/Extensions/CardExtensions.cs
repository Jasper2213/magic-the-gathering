namespace MagicCards.Shared.Extensions
{
    public static class CardExtensions
    {
        public static IQueryable<Card> ToFilteredList(this IQueryable<Card> cards, string name, string type, string text, string artistFullName, string rarityName, string setCode)
        {
            return cards
                .Where(c =>
                        c.Name.ToLower().Contains(name.ToLower() ?? "") &&
                        c.Type.ToLower().Contains(type.ToLower() ?? "") &&
                        c.Text.ToLower().Contains(text.ToLower() ?? "") &&
                        c.Artist.FullName.ToLower().Contains(artistFullName.ToLower() ?? "") &&
                        c.RarityCodeNavigation.Name.ToLower().Contains(rarityName.ToLower() ?? "") &&
                        c.SetCodeNavigation.Code.ToLower().Contains(setCode.ToLower() ?? ""));
        }

        public static IQueryable<Card> ToOrderedList(this IQueryable<Card> cards, string order)
        {
            if (!string.IsNullOrEmpty(order))
            {
                if (order.ToLower().Equals("asc"))
                    cards = cards.OrderBy(c => c.Name);
                else if (order.ToLower().Equals("desc"))
                    cards = cards.OrderByDescending(c => c.Name);
            }

            return cards;
        }
    }
}
