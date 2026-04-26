using Arzly.Shared.Enums.ListingOwned.LeisureAndInterests;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Shared.ListingOwned
{
    [Owned]
    public class HobbiesDetails
    {
      
        public CollectibleType? CollectibleType { get; set; }
        public HobbiesCondition? Condition { get; set; }


        public InstrumentType? InstrumentType { get; set; }

 
        public BookType? BookType { get; set; }
        public BookLanguage? BookLanguage { get; set; }


        public MovieGenre? MovieGenre { get; set; }


        public HobbyGameType? HobbyGameType { get; set; }
    }
}