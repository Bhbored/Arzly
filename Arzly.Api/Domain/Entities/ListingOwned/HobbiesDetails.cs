using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.ListingOwned.LeisureAndInterests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Arzly.Api.Domain.ListingOwned
{
    public class HobbiesDetails
    {
        [Key, ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        [JsonIgnore]
        public virtual Listing? Listing { get; set; }

        public CollectibleType? CollectibleType { get; set; }
        public HobbiesCondition? Condition { get; set; }


        public InstrumentType? InstrumentType { get; set; }


        public BookType? BookType { get; set; }
        public BookLanguage? BookLanguage { get; set; }


        public MovieGenre? MovieGenre { get; set; }


        public HobbyGameType? HobbyGameType { get; set; }
    }
}
