namespace MagicCards.Shared.Mappings
{
    public class TypesProfile : Profile
    {
        public TypesProfile() 
        {
            CreateMap<DAL.Models.Type, TypeReadDTO>();
        }
    }
}
