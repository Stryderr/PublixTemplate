using AutoMapper;
using Service.Models;
using Repository.Entities;

namespace Service.Mappers
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {

            // ENTITY TO DOAMIN

            CreateMap<Generic, GenericDM>();

            // SERVICE TO ENTITY

            CreateMap<GenericDM, Generic>();

        }
    }
}

