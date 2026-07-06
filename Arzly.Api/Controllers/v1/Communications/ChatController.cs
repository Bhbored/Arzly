using Arzly.Api.Application.Contracts.Communications;
using Arzly.Api.Filters.ResultFilters;
using Arzly.Shared.DTOs.Request.Chat;
using Arzly.Shared.DTOs.Response.Chat;
using Arzly.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers.v1.Communications
{
    [JsonFormatter(UsePascalCase = true)]

    public class ChatController : CustomeControllerBase
    {
        private readonly IChatService _service;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IChatService service, ILogger<ChatController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatResponse>>> GetUserChats(
            [FromQuery] bool IsArchived =false,
            [FromQuery] bool IsDiscontinued=false,
            [FromQuery] int pageSize = 10,
            [FromQuery] int currentPage = 0)
        {
            _logger.LogInformation("{Controller}.GetUserChats - Before",
                GetType().Name);

            var result = await _service.GetUserChatsAsync(User.GetUserId(), IsArchived, IsDiscontinued, pageSize, currentPage);

            _logger.LogInformation("{Controller}.GetUserChats - After",
                GetType().Name);
            return Ok(result);
        }


        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ChatResponse>> GetByIdWithMessages(
            Guid id,
            [FromQuery] int pageSize = 10,
            [FromQuery] int currentPage = 0)
        {
            _logger.LogInformation("{Controller}.GetByIdWithMessages({Id}) - Before",
                GetType().Name, id);

            var result = await _service.GetByIdWithMessagesAsync(id, pageSize, currentPage);

            _logger.LogInformation("{Controller}.GetByIdWithMessages({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ChatResponse>> StartNewChat([FromBody] ChatAddRequest createDto)
        {
            _logger.LogInformation("{Controller}.StartNewChat - Before",
                GetType().Name);

            var result = await _service.StartNewChatAsync(createDto, User.GetUserId());

            _logger.LogInformation("{Controller}.StartNewChat - After",
                GetType().Name);
            return CreatedAtAction(nameof(GetByIdWithMessages), new { id = result?.Id }, result);
        }

        [HttpPut("[action]/{id:guid}")]
        public async Task<ActionResult<ChatResponse>> ToggleArchive(Guid id)
        {
            _logger.LogInformation("{Controller}.ToggleArchive({Id}) - Before",
                GetType().Name, id);

            var result = await _service.ToggleArchiveAsync(id, User.GetUserId());

            _logger.LogInformation("{Controller}.ToggleArchive({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpPut("[action]/{id:guid}")]
        public async Task<ActionResult<ChatResponse>> MarkDiscontinued(Guid id)
        {
            _logger.LogInformation("{Controller}.MarkDiscontinued({Id}) - Before",
                GetType().Name, id);

            var result = await _service.MarkDiscontinuedAsync(id, User.GetUserId());

            _logger.LogInformation("{Controller}.MarkDiscontinued({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            _logger.LogInformation("{Controller}.Delete({Id}) - Before",
                GetType().Name, id);

            await _service.DeleteAsync(id);

            _logger.LogInformation("{Controller}.Delete({Id}) - After",
                GetType().Name, id);
            return NoContent();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest sendDto)
        {
            _logger.LogInformation("{Controller}.SendMessage - Before",
                GetType().Name);

            var result = await _service.SendMessageAsync(sendDto.ChatId, sendDto.Text, User.GetUserId());

            _logger.LogInformation("{Controller}.SendMessage - After",
                GetType().Name);
            return CreatedAtAction(nameof(GetByIdWithMessages), new { id = sendDto.ChatId }, result);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> MarkMessageAsRead([FromBody] MarkMessageAsReadRequest readDto)
        {
            _logger.LogInformation("{Controller}.MarkMessageAsRead({MessageId}) - Before",
                GetType().Name, readDto.MessageId);

            await _service.MarkMessageAsReadAsync(readDto.MessageId, User.GetUserId());

            _logger.LogInformation("{Controller}.MarkMessageAsRead({MessageId}) - After",
                GetType().Name, readDto.MessageId);
            return NoContent();
        }
    }
}
