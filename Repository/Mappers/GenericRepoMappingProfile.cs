using AutoMapper;
using Repository.Entities;

namespace Repository.Mappers
{
    public class GenericRepoMappingProfile : Profile
    {
        public GenericRepoMappingProfile()
        {
            // ENTITY TO ENTITY
            CreateMap<Generic, Generic>();
        }
    }
}
