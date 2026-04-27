using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Notification
{
    public class NotificationUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
