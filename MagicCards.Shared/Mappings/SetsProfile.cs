namespace MagicCards.Shared.Mappings
{
    public class SetsProfile : Profile
    {
        public SetsProfile()
        {
            CreateMap<Set, SetReadDTO>();
        }
    }
}
