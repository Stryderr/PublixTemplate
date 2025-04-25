using AutoMapper;
using Domain.Mappers;
using S0WISRXX.SharedExternal.Logger;

namespace Domain.DomainObjects
{
    public class BaseDomain
    {
        public readonly IUtilityLogger _logger;
        public readonly IMapper _mapper;

        public BaseDomain(IMapper mapper, IUtilityLogger logger) : base()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<GenericDomainMappingProfile>()).CreateMapper();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
