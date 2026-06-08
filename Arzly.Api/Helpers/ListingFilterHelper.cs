using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.ListingOwned;
using Arzly.Api.Helpers.ListingFilters;

namespace Arzly.Api.Helpers
{
    public static class ListingFilterHelper
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, object details)
        {
            return details switch
            {
                VehiclesDetails v => VehiclesDetailsFilter.Apply(query, v),
                RealEstateDetails d => RealEstateDetailsFilter.Apply(query, d),
                PhonesDetails d => PhonesDetailsFilter.Apply(query, d),
                ElectronicsDetails d => ElectronicsDetailsFilter.Apply(query, d),
                FurnitureDetails d => FurnitureDetailsFilter.Apply(query, d),
                PetsDetails d => PetsDetailsFilter.Apply(query, d),
                BabyChildDetails d => BabyChildDetailsFilter.Apply(query, d),
                SportsDetails d => SportsDetailsFilter.Apply(query, d),
                HobbiesDetails d => HobbiesDetailsFilter.Apply(query, d),
                FashionDetails d => FashionDetailsFilter.Apply(query, d),
                ServicesDetails d => ServicesDetailsFilter.Apply(query, d),
                _ => query
            };
        }
    }
}
