using Arzly.Api.Application.Contracts.Categories;
using Arzly.Api.Application.Contracts;
using Arzly.Api.Filters.ResultFilters;
using Arzly.Shared.DTOs.Request.SubCategory;
using Arzly.Shared.DTOs.Response.SubCategory;
using Microsoft.AspNetCore.Mvc;
using Arzly.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Arzly.Api.Controllers.v1.Categories
{
    [JsonFormatter(UsePascalCase = true)]

    public class SubCategoryController : CustomeControllerBase
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
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetAllAsync();

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return Ok(result);
        }

        [HttpGet("category/{categoryId:guid}")]
        public async Task<ActionResult<List<SubCategoryResponse>>> GetByCategoryId(Guid categoryId)
        {
            _logger.LogInformation("{Controller}.GetByCategoryId({CategoryId}) - Before",
                GetType().Name, categoryId);

            var result = await _service.GetByCategoryIdAsync(categoryId);

            _logger.LogInformation("{Controller}.GetByCategoryId({CategoryId}) - After",
                GetType().Name, categoryId);
            return Ok(result);
        }

        [HttpGet("by-title/{title}")]
        public async Task<ActionResult<SubCategoryResponse>> GetByTitle(string title)
        {
            _logger.LogInformation("{Controller}.GetByTitle({Title}) - Before",
                GetType().Name, title);

            var result = await _service.GetByTitleAsync(title);

            _logger.LogInformation("{Controller}.GetByTitle({Title}) - After",
                GetType().Name, title);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SubCategoryResponse>> GetById(Guid id)
        {
            _logger.LogInformation("{Controller}.GetById({Id}) - Before",
                GetType().Name, id);

            var result = await _service.GetByIdAsync(id);

            _logger.LogInformation("{Controller}.GetById({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<SubCategoryResponse>> Create([FromBody] SubCategoryAddRequest createDto)
        {
            _logger.LogInformation("{Controller}.Create - Before",
                GetType().Name);

            var result = await _service.CreateAsync(createDto,User.GetUserId());

            _logger.LogInformation("{Controller}.Create - After",
                GetType().Name);
            return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<SubCategoryResponse>> Update([FromBody] SubCategoryUpdateRequest updateDto)
        {
            _logger.LogInformation("{Controller}.Update({Id}) - Before",
                GetType().Name, updateDto);

            var result = await _service.UpdateAsync(updateDto,User.GetUserId());

            _logger.LogInformation("{Controller}.Update({Id}) - After",
                GetType().Name, updateDto);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            _logger.LogInformation("{Controller}.Delete({Id}) - Before",
                GetType().Name, id);

            await _service.DeleteAsync(id);

            _logger.LogInformation("{Controller}.Delete({Id}) - After",
                GetType().Name, id);
            return NoContent();
        }
    }
}
