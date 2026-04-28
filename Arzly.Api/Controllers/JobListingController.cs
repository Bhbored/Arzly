using Arzly.Api.Application.Contracts;
using Arzly.Api.Filters.ActionFilters;
using Arzly.Shared.DTOs.Request.JobListing;
using Arzly.Shared.DTOs.Response.JobListing;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public class JobListingController : ControllerBase
    {
        private readonly ILogger<JobListingController> _logger;
        private readonly IJobListingService _service;

        public JobListingController(IJobListingService service, ILogger<JobListingController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<JobListingResponse>>> GetAll()
        {
            try
            {
                _logger.LogInformation("{Controller}.GetAll - Before", GetType().Name);
                var result = await _service.GetAllAsync();
                _logger.LogInformation("{Controller}.GetAll - After", GetType().Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in JobListingController.GetAll");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<JobListingResponse>> GetById(Guid? id)
        {
            try
            {
                _logger.LogInformation("{Controller}.GetById({Id}) - Before", GetType().Name, id);
                var result = await _service.GetByIdAsync(id.Value);
                _logger.LogInformation("{Controller}.GetById({Id}) - After", GetType().Name, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in JobListingController.GetById");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("[action]")]
        [TypeFilter(typeof(ModelBindingFilter), Arguments = new object[] { typeof(JobListingController) })]
        public async Task<ActionResult<JobListingResponse>> Create([FromBody] JobListingAddRequest? request)
        {
            try
            {
                _logger.LogInformation("{Controller}.Create - Before", GetType().Name);
                var result = await _service.CreateAsync(request);
                _logger.LogInformation("{Controller}.Create - After", GetType().Name);
                return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in JobListingController.Create");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("[action]/{id:guid}")]
        [TypeFilter(typeof(ModelBindingFilter), Arguments = new object[] { typeof(JobListingController) })]
        public async Task<ActionResult<JobListingResponse?>> Update([FromBody] JobListingUpdateRequest? request)
        {
            try
            {
                _logger.LogInformation("{Controller}.Update({Id}) - Before", GetType().Name, request);
                var result = await _service.UpdateAsync(request);
                _logger.LogInformation("{Controller}.Update({Id}) - After", GetType().Name, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in JobListingController.Update");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("[action]/{id:guid}")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            try
            {
                _logger.LogInformation("{Controller}.Delete({Id}) - Before", GetType().Name, id);
                await _service.DeleteAsync(id.Value);
                _logger.LogInformation("{Controller}.Delete({Id}) - After", GetType().Name, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in JobListingController.Delete");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
