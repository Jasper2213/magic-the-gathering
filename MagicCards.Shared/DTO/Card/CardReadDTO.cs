namespace MagicCards.Shared.DTO.Card
{
    public record CardReadDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ManaCost { get; set; }
        public string Type { get; set; }
        public string RarityCode { get; set; }
        public string SetCode { get; set; }
        public string Text { get; set; }
        public string Flavor { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string Layout { get; set; }
        public string OriginalImageUrl { get; set; }
        public string Image { get; set; }
        public virtual ArtistReadDTO Artist { get; set; }
        public virtual RarityReadDTO RarityCodeNavigation { get; set; }
        public virtual SetReadDTO SetCodeNavigation { get; set; }
        public virtual ICollection<CardColorReadDTO> CardColors { get; set; }
        public virtual ICollection<CardTypeReadDTO> CardTypes { get; set; }
    }
}
