namespace MagicCards.Shared.Filters
{
    public class CardFilter : PaginationFilter
    {
        public string Type { get; set; } = "";
        public string Name { get; set; } = "";
        public string Text { get; set; } = "";
        public string ArtistFullName { get; set; } = "";
        public string RarityName { get; set; } = "";
        public string SetCode { get; set; } = "";
        public string OrderName { get; set; } = "";

        public override bool Equals(object obj)
        {
            return obj is CardFilter filter &&
                   PageNumber == filter.PageNumber &&
                   PageSize == filter.PageSize &&
                   Type == filter.Type &&
                   Name == filter.Name &&
                   Text == filter.Text &&
                   ArtistFullName == filter.ArtistFullName &&
                   RarityName == filter.RarityName &&
                   SetCode == filter.SetCode &&
                   OrderName == filter.OrderName;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(PageNumber);
            hash.Add(PageSize);
            hash.Add(Type);
            hash.Add(Name);
            hash.Add(Text);
            hash.Add(ArtistFullName);
            hash.Add(RarityName);
            hash.Add(SetCode);
            hash.Add(OrderName);
            return hash.ToHashCode();
        }
    }
}
