using Arzly.Api.Domain.ListingOwned;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Api.Infrastructure.Data.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Helpers
{
    public static class ListingOwnedSeedHelper
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (context != null)
            {
                VehiclesDetailsSeed.Data.ForEach(async x => await context.VehiclesDetails.AddRangeAsync(x));
                RealEstateDetailsSeed.Data.ForEach(async x => await context.RealEstateDetails.AddRangeAsync(x));
                ElectronicsDetailsSeed.Data.ForEach(async x => await context.ElectronicsDetails.AddRangeAsync(x));
                FurnitureDetailsSeed.Data.ForEach(async x => await context.FurnitureDetails.AddRangeAsync(x));
                PhonesDetailsSeed.Data.ForEach(async x => await context.PhonesDetails.AddRangeAsync(x));
                ServicesDetailsSeed.Data.ForEach(async x => await context.ServicesDetails.AddRangeAsync(x));
                PetsDetailsSeed.Data.ForEach(async x => await context.PetsDetails.AddRangeAsync(x));
                FashionDetailsSeed.Data.ForEach(async x => await context.FashionDetails.AddRangeAsync(x));
                BabyChildDetailsSeed.Data.ForEach(async x => await context.BabyChildDetails.AddRangeAsync(x));
                HobbiesDetailsSeed.Data.ForEach(async x => await context.HobbiesDetails.AddRangeAsync(x));
                SportsDetailsSeed.Data.ForEach(async x => await context.SportsDetails.AddRangeAsync(x));

                foreach (var seedListing in ListingSeed.Data)
                {
                    var exists = await context.Listings.AnyAsync(x => x.Id == seedListing.Id);
                    if (!exists)
                    {
                        await context.Listings.AddAsync(seedListing);
                    }
                }
                await context.SaveChangesAsync();

            }

        }
    }
}
