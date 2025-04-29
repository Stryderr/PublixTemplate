using API.Models;
using AutoMapper;
using Service.Models;

namespace SolutionName.Mappers
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            // SERVICE TO DTO
            CreateMap<GenericDM, GenericDTO>();


            // DTO TO SERVICE 
            CreateMap<GenericDTO, GenericDM>();
        }
    }
}

