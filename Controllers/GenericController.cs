using Domain;
using Microsoft.AspNetCore.Mvc;

namespace TemplateCSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenericController : ControllerBase
    {
        private readonly ILogger<GenericController> _logger;
        private readonly IGenericDM _dm;

        public GenericController(ILogger<GenericController> logger, IGenericDM dm)
        {
            _logger = logger;
            _dm = dm ?? throw new ArgumentNullException(nameof(dm));
        }

        [HttpGet]
        [Route("/all")]
        public ActionResult GetAll()
        {
            var result = _dm.GetAll();
            return Ok(result + "Hello from Generic Get");
        }

        [HttpGet]
        [Route("/{id}")]
        public ActionResult GetById(long id)
        {
            var result = _dm.GetById(id);
            return Ok(result + "Hello from Generic Get");
        }

        [HttpPost]
        // [Route("")]
        public IActionResult Create([FromBody] GenericDM newItem)
        {
            if (newItem == null)
            {
                return BadRequest("Invalid data.");
            }
            var result = _dm.Add(newItem);
            return Ok(result + "Hello from Generic Post");
        }

        [HttpPut]
        // [Route("")]
        public IActionResult Update([FromBody] GenericDM updatedItem)
        {
            if (updatedItem == null)
            {
                return BadRequest("Invalid data.");
            }
            var result = _dm.Update(updatedItem);
            return Ok(result + "Hello from Generic Put");
        }

        [HttpDelete]
        // [Route("")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }
            var result = _dm.Delete(id);
            return Ok(result + "Hello from Generic Delete");
        }

    }
}
