using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.ListingOwned.AnimalsAndPets;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class PetsDetailsSeed
    {
        public static readonly List<PetsDetails> Data = new()
        {
            new PetsDetails
            {
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                DogBreed = DogBreed.GoldenRetriever,
                Gender = PetGender.Male,
                DogAgeRange = DogAgeRange.Puppy,
                IsVaccinated = true,
            },
        };
    }
}
