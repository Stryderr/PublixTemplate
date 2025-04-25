using Microsoft.AspNetCore.Mvc;
using S0WISRXX.SharedExternal.Logger;

namespace TemplateCSharp.Controllers
{

    public abstract class BaseController : ControllerBase
    {
        public readonly IUtilityLogger _logger;

        public BaseController(IUtilityLogger logger) : base()
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
