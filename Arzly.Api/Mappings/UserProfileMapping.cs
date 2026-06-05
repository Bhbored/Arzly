using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.UserProfile;
using Arzly.Shared.DTOs.Response.UserProfile;

namespace Arzly.Api.Mappings
{
    public static class UserProfileMapping
    {
        public static UserProfileResponse ToResponse(this UserProfile entity)
        {
            return new UserProfileResponse
            {
                UserId = entity.UserId,
                FullName = entity.FullName,
                PublicName = entity.PublicName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                ProfileImageUrl = entity.ProfileImageUrl,
                StoreDescription = entity.StoreDescription,
                IsStore = entity.IsStore,
                PublicLocation = entity.PublicLocation,
                IsVerified = entity.IsVerified,
                UpdatedAt = entity.UpdateddAt,
                LastActiveAt = entity.LastActiveAt
            };
        }

        public static UserProfile ToEntity(this UserProfileUpdateRequest request)
        {
            return new UserProfile
            {
                UserId = request.UserId,
                FullName = request.FullName,
                PublicName = request.PublicName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                ProfileImageUrl = request.ProfileImageUrl,
                StoreDescription = request.StoreDescription,
                IsStore = request.IsStore,
                PublicLocation = request.PublicLocation,
            };
        }
    }
}
