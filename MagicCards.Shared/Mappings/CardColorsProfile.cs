namespace MagicCards.Shared.Mappings
{
    public class CardColorsProfile : Profile
    {
        public CardColorsProfile() 
        {
            CreateMap<CardColor, CardColorReadDTO>();
        }
    }
}
