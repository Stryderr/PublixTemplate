using Microsoft.AspNetCore.Mvc;
using S0WISRXX.SharedExternal.Logger;

namespace API.Controllers
{
    public class BaseController<TService> : ControllerBase
    {
        protected readonly TService _service;
        protected readonly IUtilityLogger _logger;

        public BaseController(TService service, IUtilityLogger logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
