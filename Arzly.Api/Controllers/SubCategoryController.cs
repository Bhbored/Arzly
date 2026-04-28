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
                _logger.LogInformation("{Controller}.GetAll - Before",
                    GetType().Name);

                var result = await _service.GetAllAsync();

                _logger.LogInformation("{Controller}.GetAll - After",
                    GetType().Name);
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
                _logger.LogError(ex, "Error in SubCategoryController.GetById");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SubCategoryResponse>> Create([FromBody] SubCategoryAddRequest createDto)
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
                _logger.LogError(ex, "Error in SubCategoryController.Create");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<SubCategoryResponse>> Update([FromBody] SubCategoryUpdateRequest updateDto)
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
                _logger.LogError(ex, "Error in SubCategoryController.Update");
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
                _logger.LogError(ex, "Error in SubCategoryController.Delete");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
