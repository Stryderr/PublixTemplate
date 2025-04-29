using AutoMapper;
using S0WISRXX.SharedExternal.Logger;
using Service.Mappers;

namespace Service.Services
{
    public class BaseService
    {
        public readonly IUtilityLogger _logger;
        public readonly IMapper _mapper;

        public BaseService(IMapper mapper, IUtilityLogger logger) : base()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ServiceMappingProfile>()).CreateMapper();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
