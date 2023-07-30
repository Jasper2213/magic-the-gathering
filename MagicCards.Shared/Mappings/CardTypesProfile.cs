namespace MagicCards.Shared.Mappings
{
    public class CardTypesProfile : Profile
    {
        public CardTypesProfile() 
        {
            CreateMap<CardType, CardTypeReadDTO>();
        }
    }
}
