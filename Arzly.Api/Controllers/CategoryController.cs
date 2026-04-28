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
                _logger.LogInformation("{Controller}.GetAll - Before",
                    GetType().Name);

                var result = await _service.GetAllAsync();

                _logger.LogInformation("{Controller}.GetAll - After",
                    GetType().Name);
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
                _logger.LogInformation("{Controller}.GetById({Id}) - Before",
                    GetType().Name, id);

                var result = await _service.GetByIdAsync(id);

                _logger.LogInformation("{Controller}.GetById({Id}) - After",
                    GetType().Name, id);
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
                _logger.LogInformation("{Controller}.Create - Before",
                    GetType().Name);

                var result = await _service.CreateAsync(createDto);

                _logger.LogInformation("{Controller}.Create - After",
                    GetType().Name);
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
                _logger.LogInformation("{Controller}.Update({Id}) - Before",
                    GetType().Name, updateDto);

                var result = await _service.UpdateAsync(updateDto);

                _logger.LogInformation("{Controller}.Update({Id}) - After",
                    GetType().Name, updateDto);
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
                _logger.LogInformation("{Controller}.Delete({Id}) - Before",
                    GetType().Name, id);

                var success = await _service.DeleteAsync(id);

                _logger.LogInformation("{Controller}.Delete({Id}) - After",
                    GetType().Name, id);
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
