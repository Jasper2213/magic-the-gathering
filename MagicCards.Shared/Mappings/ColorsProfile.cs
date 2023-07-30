namespace MagicCards.Shared.Mappings
{
    public class ColorsProfile : Profile
    {
        public ColorsProfile() 
        {
            CreateMap<Color, ColorReadDTO>();
        }
    }
}
