//using Arzly.Api.Application.Contracts;
//using Arzly.Api.Filters.ActionFilters;
//using Arzly.Shared.DTOs.Request.Notification;
//using Arzly.Shared.DTOs.Response.Notification;
//using Microsoft.AspNetCore.Mvc;

//namespace Arzly.Api.Controllers
//{
//    [ApiController]
//    [Route("arzly/[controller]")]
//    public class NotificationController : ControllerBase
//    {
//        private readonly ILogger<NotificationController> _logger;
//        private readonly INotificationService _service;

//        public NotificationController(INotificationService service, ILogger<NotificationController> logger)
//        {
//            _logger = logger;
//            _service = service;
//        }

//        [HttpGet]
//        public virtual async Task<ActionResult<List<NotificationResponse>>> GetAll()
//        {
//            try
//            {
//                _logger.LogInformation("{Controller}.GetAll - Before", GetType().Name);
//                var result = await _service.GetAllAsync();
//                _logger.LogInformation("{Controller}.GetAll - After", GetType().Name);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error in NotificationController.GetAll");
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpGet("{id:guid}")]
//        public async Task<ActionResult<NotificationResponse>> GetById(Guid? id)
//        {
//            try
//            {
//                _logger.LogInformation("{Controller}.GetById({Id}) - Before", GetType().Name, id);
//                var result = await _service.GetByIdAsync(id.Value);
//                _logger.LogInformation("{Controller}.GetById({Id}) - After", GetType().Name, id);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error in NotificationController.GetById");
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpPost("[action]")]
//        [TypeFilter(typeof(ModelBindingFilter), Arguments = new object[] { typeof(NotificationController) })]
//        public async Task<ActionResult<NotificationResponse>> Create([FromBody] NotificationAddRequest? request)
//        {
//            try
//            {
//                _logger.LogInformation("{Controller}.Create - Before", GetType().Name);
//                var result = await _service.CreateAsync(request);
//                _logger.LogInformation("{Controller}.Create - After", GetType().Name);
//                return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error in NotificationController.Create");
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpPut("[action]/{id:guid}")]
//        [TypeFilter(typeof(ModelBindingFilter), Arguments = new object[] { typeof(NotificationController) })]
//        public async Task<ActionResult<NotificationResponse?>> Update([FromBody] NotificationUpdateRequest? request)
//        {
//            try
//            {
//                _logger.LogInformation("{Controller}.Update({Id}) - Before", GetType().Name, request?.Id);
//                var result = await _service.UpdateAsync(request);
//                _logger.LogInformation("{Controller}.Update({Id}) - After", GetType().Name, request?.Id);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error in NotificationController.Update");
//                return StatusCode(500, "Internal server error");
//            }
//        }

//        [HttpDelete("[action]/{id:guid}")]
//        public async Task<ActionResult> Delete(Guid? id)
//        {
//            try
//            {
//                _logger.LogInformation("{Controller}.Delete({Id}) - Before", GetType().Name, id);
//                await _service.DeleteAsync(id.Value);
//                _logger.LogInformation("{Controller}.Delete({Id}) - After", GetType().Name, id);
//                return NoContent();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error in NotificationController.Delete");
//                return StatusCode(500, "Internal server error");
//            }
//        }
//    }
//}
