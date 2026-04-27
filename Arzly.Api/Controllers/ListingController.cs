using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Filters.ActionFilters;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public class ListingController : ControllerBase
    {
        private readonly ILogger<ListingController> _logger;
        private readonly IListingService _service;
        public ListingController(IListingService service, ILogger<ListingController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<ListingResponse>>> GetAll()
        {
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetAllAsync();

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public  async Task<ActionResult<ListingResponse>> GetById(Guid? id)
        {
            _logger.LogInformation("{Controller}.GetById({Id}) - Before",
                GetType().Name, id);

            var result = await _service.GetByIdAsync(id.Value);

            _logger.LogInformation("{Controller}.GetById({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [TypeFilter(typeof(ModelBindingFilter),Arguments =new object []{typeof(ListingController), "createDto" })]
        public  async Task<ActionResult<ListingResponse>> Create([FromBody] ListingAddRequest? createDto)
        {
            _logger.LogInformation("{Controller}.Create - Before",
                GetType().Name);

            var result = await _service.CreateAsync(createDto);

            _logger.LogInformation("{Controller}.Create - After",
                GetType().Name);
            return CreatedAtAction(nameof(GetById), new
            {
                id = result?
                .GetType()
                .GetProperty("Id")?
                .GetValue(result)
            },
                result);
        }

        [HttpPut("[action]/{id:guid}")]
        public  async Task<ActionResult<ListingResponse?>> Update([FromBody] ListingUpdateRequest? updateDto)
        {
            _logger.LogInformation("{Controller}.Update({Id}) - Before",
                GetType().Name, updateDto);

            var result = await _service.UpdateAsync(updateDto);

            _logger.LogInformation("{Controller}.Update({Id}) - After",
                GetType().Name, updateDto);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public  async Task<ActionResult> Delete(Guid? id)
        {
            _logger.LogInformation("{Controller}.Delete({Id}) - Before",
                GetType().Name, id);

            await _service.DeleteAsync(id.Value);

            _logger.LogInformation("{Controller}.Delete({Id}) - After",
                GetType().Name, id);
            return NoContent();
        }

    }
}
