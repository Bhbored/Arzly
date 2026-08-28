using Arzly.Api.Domain.Entities.Support;
using Arzly.Shared.DTOs.Request.TicketMessage;
using Arzly.Shared.DTOs.Response.TicketMessage;

namespace Arzly.Api.Application.Contracts.Support
{
    public interface ITicketMessageService
    {
        Task<List<TicketMessageResponse>> GetAllAsync();
        Task<TicketMessageResponse?> GetByIdAsync(Guid id);
        Task<TicketMessageResponse?> CreateAsync(TicketMessageAddRequest? request, Guid userId);
        Task<TicketMessageResponse?> UpdateAsync(TicketMessageUpdateRequest? request, Guid userId);
        Task<bool> DeleteAsync(Guid id);
    }
}
