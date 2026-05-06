using Arzly.Api.Application.Contracts;
using Arzly.Api.Filters.ActionFilters;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
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

        #region fetch
        [HttpGet]
        public async Task<ActionResult<List<ListingResponse>>> GetAll()
        {
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetAllAsync();

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return Ok(result);
        }
        [HttpGet("indexed")]
        public async Task<ActionResult<List<ListingResponse>>> IndexedListings([FromHeader] int pageSize = 10, [FromHeader] int currentPage = 0)
        {
            _logger.LogInformation("{Controller}.GetByPage - Before",
                GetType().Name);

            var result = await _service.GetIndexedListings(pageSize, currentPage);

            _logger.LogInformation("{Controller}.GetByPage - After",
                GetType().Name);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<ListingResponse>>> GetFilteredListing(string searchBy, string searchString,
            [FromHeader] int pageSize = 10, [FromHeader] int currentPage = 0)
        {
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetFilteredListing(searchBy, searchString, pageSize, currentPage);

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ListingResponse>> GetByListingId(Guid? id)
        {
            _logger.LogInformation("{Controller}.GetById({Id}) - Before",
                GetType().Name, id);

            var result = await _service.GetByIdAsync(id.Value);

            _logger.LogInformation("{Controller}.GetById({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }


        [HttpGet("user-listings")]
        public async Task<ActionResult<ListingResponse>> GetByUserId([FromHeader] string? firebaseId, [FromHeader] int pageSize = 10,
            [FromHeader] int currentPage = 0)
        {
            _logger.LogInformation("{Controller}.GetByUserId({Id}) - Before",
                GetType().Name, firebaseId);

            var result = await _service.GetListingByUserId(firebaseId, pageSize, currentPage);

            _logger.LogInformation("{Controller}.GetByUserId({Id}) - After",
                GetType().Name, firebaseId);
            return Ok(result);
        }
        [HttpGet("initial-listings")]
        public async Task<ActionResult<ListingResponse>> GetInitialListings([FromBody] List<Guid> categoriesId) 
        {
            _logger.LogInformation("{Controller}.GetInitialListings() - Before",
                GetType().Name);

            var result = await _service.GetInitialListings(categoriesId);

            _logger.LogInformation("{Controller}.GetInitialListings() - After",
                GetType().Name);
            return Ok(result);
        }

        [HttpGet("category-listing")]
        public async Task<ActionResult<ListingResponse>> GetByCategoryId(Guid? CategoryId, [FromHeader] int pageSize = 10,
            [FromHeader] int currentPage = 0)
        {
            _logger.LogInformation("{Controller}.GetByCategoryId({CategoryId}) - Before",
                GetType().Name, CategoryId);

            var result = await _service.GetListingByCategoryId(CategoryId.Value, pageSize, currentPage);

            _logger.LogInformation("{Controller}.GetByCategoryId({CategoryId}) - After",
                GetType().Name, CategoryId);
            return Ok(result);
        }
        #endregion


        [HttpPost("[action]")]
        [TypeFilter(typeof(ModelBindingFilter), Arguments = new object[] { typeof(ListingController) })]
        public async Task<ActionResult<ListingResponse>> Create([FromBody] ListingAddRequest? request, [FromHeader] string? firebaseId)
        {
            _logger.LogInformation("{Controller}.Create - Before",
                GetType().Name);

            var result = await _service.CreateAsync(request, firebaseId);

            _logger.LogInformation("{Controller}.Create - After",
                GetType().Name);
            return CreatedAtAction(nameof(Create), new
            {
                id = result?.Id
            },
                result);
        }

        [HttpPut("[action]")]
        [TypeFilter(typeof(ModelBindingFilter), Arguments = new object[] { typeof(ListingController) })]
        public async Task<ActionResult<ListingResponse?>> Update([FromBody] ListingUpdateRequest? request, [FromHeader] string? firebaseId)
        {
            _logger.LogInformation("{Controller}.Update({Id}) - Before",
                GetType().Name, request);

            var result = await _service.UpdateAsync(request, firebaseId);

            _logger.LogInformation("{Controller}.Update({Id}) - After",
                GetType().Name, request);
            return Ok(result);
        }

        [HttpDelete("[action]/{id:guid}")]
        public async Task<ActionResult> Delete(Guid? id)
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
