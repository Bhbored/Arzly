using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.DTOs.Request.User;
using Arzly.Shared.DTOs.Response.User;

namespace Arzly.Api.Mappings
{
    public static class UserMapping
    {
        public static AppUserResponse ToResponse(this AppUser entity)
        {
            return new AppUserResponse
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Email = entity.Email,
                AuthMethod = entity.AuthMethod,
                FirebaseUid = entity.FirebaseUid,
                PublicName = entity.PublicName,
                PublicLocation = entity.PublicLocation,
                ProfileImageUrl = entity.ProfileImageUrl,
                CreatedAt = entity.CreatedAt,
                LastActiveAt = entity.LastActiveAt,
                IsDeleted = entity.IsDeleted,
                DeletedAt = entity.DeletedAt,
                IsBanned = entity.IsBanned,
                BanReason = entity.BanReason,
                BanExpiresAt = entity.BanExpiresAt,
                IsVerified = entity.IsVerified
            };
        }

        public static AppUser ToEntity(this AppUserAddRequest request)
        {
            return new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                AuthMethod = request.AuthMethod,
                FirebaseUid = request.FirebaseUid,
                PublicName = request.PublicName,
                PublicLocation = request.PublicLocation,
                ProfileImageUrl = request.ProfileImageUrl
            };
        }

        public static AppUser ToEntity(this AppUserUpdateRequest request)
        {
            return new AppUser
            {
                Id = request.Id,
                UserName = request.UserName,
                PublicName = request.PublicName,
                PublicLocation = request.PublicLocation,
                ProfileImageUrl = request.ProfileImageUrl,
                LastActiveAt = request.LastActiveAt,
                IsBanned = request.IsBanned,
                BanReason = request.BanReason,
                BanExpiresAt = request.BanExpiresAt,
                IsVerified = request.IsVerified
            };
        }

        public static AppUserUpdateRequest ToUpdateRequest(this AppUserResponse response)
        {
            return new AppUserUpdateRequest
            {
                Id = response.Id,
                UserName = response.UserName,
                PublicName = response.PublicName,
                PublicLocation = response.PublicLocation,
                ProfileImageUrl = response.ProfileImageUrl,
                LastActiveAt = response.LastActiveAt,
                IsBanned = response.IsBanned,
                BanReason = response.BanReason,
                BanExpiresAt = response.BanExpiresAt,
                IsVerified = response.IsVerified
            };
        }
    }
}
