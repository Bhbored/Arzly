using Arzly.Api.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class SavedListing
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Listing ID is required.")]
        public Guid ListingId { get; set; }

        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; } = null!;

        [ForeignKey(nameof(ListingId))]
        public virtual Listing Listing { get; set; } = null!;
    }



}

// Get all users who saved this listing
//var listing = await _context.Listings
//    .Include(l => l.SavedByUsers)
//    .ThenInclude(s => s.User)
//    .FirstOrDefaultAsync(l => l.Id == id);

//var count = listing.SavedByUsers?.Count(s => s.DeletedAt == null); // active saves only