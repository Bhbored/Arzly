//using Arzly.Api.Application.Contracts;
//using Arzly.Api.Domain.Contracts;
//using Arzly.Api.Domain.Entities;
//using Arzly.Api.Mappings;
//using Arzly.Shared.DTOs.Request.Notification;
//using Arzly.Shared.DTOs.Response.Notification;

//namespace Arzly.Api.Application.Services
//{
//    public class NotificationService : BaseService<Notification, NotificationResponse, NotificationAddRequest, NotificationUpdateRequest, Guid>, INotificationService
//    {
//        public NotificationService(INotificationRepository repository) : base(repository)
//        {
//        }

//        protected override NotificationResponse MapToDto(Notification entity) => entity.ToResponse();
//        protected override Notification MapToEntity(NotificationAddRequest createDto) => createDto.ToEntity();
//        protected override Notification MapToEntity(NotificationUpdateRequest updateDto) => updateDto.ToEntity();
//    }
//}
