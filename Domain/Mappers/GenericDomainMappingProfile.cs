using AutoMapper;
using Domain.Models;
using Repository.Entities;

namespace Domain.Mappers
{
    public class GenericDomainMappingProfile : Profile
    {
        public GenericDomainMappingProfile()
        {

            // ENTITY TO DOAMIN

            CreateMap<Generic, GenericDM>();

            // DOMAIN TO ENTITY

            CreateMap<GenericDM, Generic>();

        }
    }
}

