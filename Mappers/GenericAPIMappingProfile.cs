using AutoMapper;
using Domain.Models;
using TemplateCSharp.Models;

namespace TemplateCSharp.Mappers
{
    public class GenericAPIMappingProfile : Profile
    {
        public GenericAPIMappingProfile()
        {
            // DOMAIN TO DTO
            CreateMap<GenericDM, GenericDTO>();


            // DTO TO DOMAIN 
            CreateMap<GenericDTO, GenericDM>();
        }
    }
}

