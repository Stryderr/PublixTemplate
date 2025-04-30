using AutoMapper;
using Repository.Entities;
using Service.Models;

namespace Service.Mappers
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {

            // ENTITY TO DOAMIN

            CreateMap<Generic, GenericSM>();

            // SERVICE TO ENTITY

            CreateMap<GenericSM, Generic>();

        }
    }
}

