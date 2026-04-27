using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Chat
{
    public class ChatAddRequest
    {
        [Required(ErrorMessage = "Context role is required.")]
        public ChatRole ContextRole { get; set; }

        [Required(ErrorMessage = "Initiator ID is required.")]
        public string InitiatorId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Receiver ID is required.")]
        public string ReceiverId { get; set; } = string.Empty;

        public Guid? ListingId { get; set; }
    }
}
