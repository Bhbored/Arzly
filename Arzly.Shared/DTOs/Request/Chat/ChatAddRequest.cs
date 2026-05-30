using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Chat
{
    public class ChatAddRequest
    {
        [Required(ErrorMessage = "Context role is required.")]
        public ChatRole ContextRole { get; set; }

        [Required(ErrorMessage = "Initiator ID is required.")]
        public Guid InitiatorId { get; set; }

        [Required(ErrorMessage = "Receiver ID is required.")]
        public Guid ReceiverId { get; set; }

        public Guid? ListingId { get; set; }
    }
}
