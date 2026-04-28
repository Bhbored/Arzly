using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.UserActivityLog;
using Arzly.Shared.DTOs.Response.UserActivityLog;

namespace Arzly.Api.Application.Services
{
    public class UserActivityLogService : IUserActivityLogService
    {
        private readonly IUserActivityLogRepository _repository;

        public UserActivityLogService(IUserActivityLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserActivityLogResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.ConvertAll(x => x.ToResponse());
        }

        public async Task<UserActivityLogResponse?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity?.ToResponse();
        }

        public async Task<UserActivityLogResponse?> CreateAsync(UserActivityLogAddRequest request)
        {
            var entity = request.ToEntity();
            await _repository.AddAsync(entity);
            return entity.ToResponse();
        }
    }
}
