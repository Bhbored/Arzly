using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.ListingOwned.BabyAndChild;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Arzly.Api.Domain.ListingOwned
{
    public class BabyChildDetails
    {
        [Key, ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        [JsonIgnore]
        public virtual Listing? Listing { get; set; }
        public KidsAgeRange? AgeRange { get; set; }
        public BabyChildCondition? Condition { get; set; }


        public StrollerSeatType? StrollerSeatType { get; set; }

        public KidGender? Gender { get; set; }

        public CribFurnitureType? CribFurnitureType { get; set; }


        public FeedingType? FeedingType { get; set; }
    }
}