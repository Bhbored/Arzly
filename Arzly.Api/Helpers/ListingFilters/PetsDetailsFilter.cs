using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;

namespace Arzly.Api.Helpers.ListingFilters
{
    public static class PetsDetailsFilter
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, PetsDetails d)
        {
            if (d.PetFoodType.HasValue)
                query = query.Where(x => x.PetsDetails!.PetFoodType == d.PetFoodType);
            if (d.PetToyType.HasValue)
                query = query.Where(x => x.PetsDetails!.PetToyType == d.PetToyType);
            if (d.GroomingType.HasValue)
                query = query.Where(x => x.PetsDetails!.GroomingType == d.GroomingType);
            if (d.PetAccessoryType.HasValue)
                query = query.Where(x => x.PetsDetails!.PetAccessoryType == d.PetAccessoryType);
            if (d.DogBreed.HasValue)
                query = query.Where(x => x.PetsDetails!.DogBreed == d.DogBreed);
            if (d.Gender.HasValue)
                query = query.Where(x => x.PetsDetails!.Gender == d.Gender);
            if (d.DogAgeRange.HasValue)
                query = query.Where(x => x.PetsDetails!.DogAgeRange == d.DogAgeRange);
            if (d.IsVaccinated.HasValue)
                query = query.Where(x => x.PetsDetails!.IsVaccinated == d.IsVaccinated);
            if (d.CatBreed.HasValue)
                query = query.Where(x => x.PetsDetails!.CatBreed == d.CatBreed);
            if (d.CatAgeRange.HasValue)
                query = query.Where(x => x.PetsDetails!.CatAgeRange == d.CatAgeRange);
            if (d.BirdSpecies.HasValue)
                query = query.Where(x => x.PetsDetails!.BirdSpecies == d.BirdSpecies);
            if (d.BirdAgeGroup.HasValue)
                query = query.Where(x => x.PetsDetails!.BirdAgeGroup == d.BirdAgeGroup);
            if (!string.IsNullOrWhiteSpace(d.AnimalType))
                query = query.Where(x => x.PetsDetails!.AnimalType == d.AnimalType);
            if (d.PetServiceType.HasValue)
                query = query.Where(x => x.PetsDetails!.PetServiceType == d.PetServiceType);

            return query;
        }
    }
}
