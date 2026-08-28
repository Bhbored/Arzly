using Arzly.Api.Application.Contracts.Support;
using Arzly.Api.Domain.Contracts.Support;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.TicketAttachment;
using Arzly.Shared.DTOs.Response.TicketAttachment;

namespace Arzly.Api.Application.Services.Support
{
    public class TicketAttachmentService : ITicketAttachmentService
    {
        private readonly ITicketAttachmentRepository _repository;

        public TicketAttachmentService(ITicketAttachmentRepository repository) => _repository = repository;

        public async Task<List<TicketAttachmentResponse>> GetAllAsync() =>
            (await _repository.GetAllAsync()).Select(entity => entity.ToResponse()).ToList();

        public async Task<TicketAttachmentResponse?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            var entity = await _repository.GetByIdAsync(id)
                ?? throw new ArgumentException($"No Object with this ID {id} Found");
            return entity.ToResponse();
        }

        public async Task<TicketAttachmentResponse?> CreateAsync(TicketAttachmentAddRequest? request, Guid userId)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), ExceptionMessages.EmptyAddRequest);
            var entity = request.ToEntity();
            await _repository.AddAsync(entity);
            return entity.ToResponse();
        }

        public async Task<TicketAttachmentResponse?> UpdateAsync(TicketAttachmentUpdateRequest? request, Guid userId)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), ExceptionMessages.EmptyUpdateRequest);
            return (await _repository.Update(request.ToEntity())).ToResponse();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            var entity = await _repository.GetByIdAsync(id);
            return entity is not null && await _repository.Delete(entity);
        }
    }
}
