using Arzly.Api.Application.Contracts;
using Arzly.Shared.DTOs.Request.SubCategory;
using Arzly.Shared.DTOs.Response.SubCategory;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _service;
        private readonly ILogger<SubCategoryController> _logger;

        public SubCategoryController(ISubCategoryService service, ILogger<SubCategoryController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubCategoryResponse>>> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SubCategoryController.GetAll");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SubCategoryResponse>> GetById(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SubCategoryController.GetById");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SubCategoryResponse>> Create([FromBody] SubCategoryAddRequest createDto)
        {
            try
            {
                var result = await _service.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SubCategoryController.Create");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<SubCategoryResponse>> Update([FromBody] SubCategoryUpdateRequest updateDto)
        {
            try
            {
                var result = await _service.UpdateAsync(updateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SubCategoryController.Update");
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
                _logger.LogError(ex, "Error in SubCategoryController.Delete");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
