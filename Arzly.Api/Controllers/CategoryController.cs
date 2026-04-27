using Arzly.Api.Application.Contracts;
using Arzly.Shared.DTOs.Request.Category;
using Arzly.Shared.DTOs.Response.Category;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService service, ILogger<CategoryController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponse>>> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryController.GetAll");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryResponse>> GetById(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryController.GetById");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<CategoryResponse>> Create([FromBody] CategoryAddRequest createDto)
        {
            try
            {
                var result = await _service.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryController.Create");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<CategoryResponse>> Update([FromBody] CategoryUpdateRequest updateDto)
        {
            try
            {
                var result = await _service.UpdateAsync(updateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryController.Update");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CategoryController.Delete");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
