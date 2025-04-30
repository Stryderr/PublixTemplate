using Microsoft.AspNetCore.Mvc;
using S0WISRXX.SharedExternal.Logger;
using Service.Interfaces;
using Shared.Responses;

namespace API.Controllers
{
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        protected readonly IBaseService<T> _service;
        protected readonly IUtilityLogger _logger;

        public BaseController(IBaseService<T> service, IUtilityLogger logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<Response<T>>> GetAll() => Ok(Response<List<T>>.Ok(await _service.GetAllAsync()));

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Response<T>>> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null
                ? NoContent()
                : Ok(Response<T>.Ok(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T newItem)
        {
            if (newItem == null) return BadRequest("Invalid data.");
            await _service.AddAsync(newItem);
            return CreatedAtAction(nameof(GetById), Response<string>.Ok("Created"));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return Ok(Response<string>.Ok("Deleted"));
        }
    }
}
