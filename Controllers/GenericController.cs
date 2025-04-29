using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S0WISRXX.SharedExternal.Logger;
using Service.Interfaces;

namespace TemplateCSharp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController : BaseController
    {
        private readonly IGenericService _dm;

        public GenericController(IUtilityLogger logger, IGenericService dm) : base(logger)
        {
            _dm = dm ?? throw new ArgumentNullException(nameof(dm));
        }


    }
}
