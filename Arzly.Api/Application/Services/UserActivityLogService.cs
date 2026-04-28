//using Arzly.Api.Application.Contracts;
//using Arzly.Api.Domain.Contracts;
//using Arzly.Api.Domain.Entities;
//using Arzly.Api.Mappings;
//using Arzly.Shared.DTOs.Request.UserActivityLog;
//using Arzly.Shared.DTOs.Response.UserActivityLog;

//namespace Arzly.Api.Application.Services
//{
//    public class UserActivityLogService : IUserActivityLogService
//    {
//        private readonly IUserActivityLogRepository _repository;

//        public UserActivityLogService(IUserActivityLogRepository repository)
//        {
//            _repository = repository;
//        }

//        public Task<UserActivityLogResponse?> CreateAsync(UserActivityLogAddRequest request)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<UserActivityLogResponse>> GetAllAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<UserActivityLogResponse?> GetByIdAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
