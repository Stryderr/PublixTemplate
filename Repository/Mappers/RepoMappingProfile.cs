using AutoMapper;
using Repository.Entities;

namespace Repository.Mappers
{
    public class RepoMappingProfile : Profile
    {
        public RepoMappingProfile()
        {
            // ENTITY TO ENTITY
            CreateMap<Generic, Generic>();
        }
    }
}
