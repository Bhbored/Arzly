using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.ListingOwned;

namespace Arzly.Api.Helpers.ListingFilters
{
    public static class HobbiesDetailsFilter
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, HobbiesDetails d)
        {
            if (d.CollectibleType.HasValue)
                query = query.Where(x => x.HobbiesDetails!.CollectibleType == d.CollectibleType);
            if (d.Condition.HasValue)
                query = query.Where(x => x.HobbiesDetails!.Condition == d.Condition);
            if (d.InstrumentType.HasValue)
                query = query.Where(x => x.HobbiesDetails!.InstrumentType == d.InstrumentType);
            if (d.BookType.HasValue)
                query = query.Where(x => x.HobbiesDetails!.BookType == d.BookType);
            if (d.BookLanguage.HasValue)
                query = query.Where(x => x.HobbiesDetails!.BookLanguage == d.BookLanguage);
            if (d.MovieGenre.HasValue)
                query = query.Where(x => x.HobbiesDetails!.MovieGenre == d.MovieGenre);
            if (d.HobbyGameType.HasValue)
                query = query.Where(x => x.HobbiesDetails!.HobbyGameType == d.HobbyGameType);

            return query;
        }
    }
}
