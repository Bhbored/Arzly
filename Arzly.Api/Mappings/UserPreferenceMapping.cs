using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.UserPreference;
using Arzly.Shared.DTOs.Response.UserPreference;

namespace Arzly.Api.Mappings
{
    public static class UserPreferenceMapping
    {
        public static UserPreferenceResponse ToResponse(this UserPreference entity)
        {
            return new UserPreferenceResponse
            {
                UserId = entity.UserId,
                Theme = entity.Theme,
                Language = entity.Language,
                PushNotifications = entity.PushNotifications,
                EmailNotifications = entity.EmailNotifications,
                UpdatedAt = entity.UpdatedAt
            };
        }


        public static UserPreference ToEntity(this UserPreferenceUpdateRequest request)
        {
            return new UserPreference
            {
                UserId = request.UserId,
                Theme = request.Theme,
                Language = request.Language,
                PushNotifications = request.PushNotifications,
                EmailNotifications = request.EmailNotifications
            };
        }

        public static UserPreferenceUpdateRequest ToUpdateRequest(this UserPreferenceResponse response)
        {
            return new UserPreferenceUpdateRequest
            {
                UserId = response.UserId,
                Theme = response.Theme,
                Language = response.Language,
                PushNotifications = response.PushNotifications,
                EmailNotifications = response.EmailNotifications
            };
        }
    }
}
