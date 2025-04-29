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
    public class GenericController : BaseController<IGenericService>
    {

        public GenericController(IGenericService dm, IUtilityLogger logger) : base(dm, logger)
        {
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all items.");
            try
            {
                var result = await _service.GetAll();
                _logger.LogInformation($"Successfully fetched {result.Count} items.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all items.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            _logger.LogInformation($"Fetching item with ID {id}.");
            try
            {
                var result = await _service.GetById(id);
                if (result == null)
                {
                    _logger.LogInformation($"Item with ID {id} not found.");
                    return NoContent();
                }
                _logger.LogInformation($"Successfully fetched item with ID {id}.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the item with ID {id}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GenericDM newItem)
        {
            _logger.LogInformation("Creating a new item.");

            if (newItem == null) return BadRequest("Invalid data.");

            try
            {
                var result = await _service.Add(newItem);
                _logger.LogInformation($"Successfully created item with ID {result.Id}.");
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new item.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GenericDM updatedItem)
        {
            _logger.LogInformation($"Updating item with ID {updatedItem.Id}.");

            if (updatedItem == null) return BadRequest("Invalid data.");

            try
            {
                var result = await _service.Update(updatedItem);
                _logger.LogInformation($"Successfully updated item with ID {result.Id}.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the item with ID {updatedItem.Id}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting item with ID {id}.");

            if (id <= 0) return BadRequest("Invalid ID.");

            try
            {
                var result = await _service.Delete(id);
                if (!result)
                {
                    _logger.LogInformation($"Item with ID {id} not found or could not be deleted.");
                    return NoContent();
                }
                _logger.LogInformation($"Successfully deleted item with ID {id}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the item with ID {id}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
