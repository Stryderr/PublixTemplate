using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S0WISRXX.SharedExternal.Logger;
using Service.Interfaces;
using Service.Models;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController : BaseController<GenericSM>
    {

        public GenericController(IBaseService<GenericSM> baseService, IGenericService service, IUtilityLogger logger) : base(baseService, logger)
        {
        }
    }
}