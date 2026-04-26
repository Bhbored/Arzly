using Arzly.Shared.Enums.ListingOwned.AnimalsAndPets;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Shared.ListingOwned
{
    [Owned]
    public class PetsDetails
    {

        public PetFoodType? PetFoodType { get; set; }


        public PetToyType? PetToyType { get; set; }


        public GroomingType? GroomingType { get; set; }


        public PetAccessoryType? PetAccessoryType { get; set; }


        public DogBreed? DogBreed { get; set; }
        public PetGender? Gender { get; set; }
        public DogAgeRange? DogAgeRange { get; set; }
        public bool? IsVaccinated { get; set; }


        public CatBreed? CatBreed { get; set; }
        public CatAgeRange? CatAgeRange { get; set; }

        public BirdSpecies? BirdSpecies { get; set; }
        public BirdAgeGroup? BirdAgeGroup { get; set; }


        public string? AnimalType { get; set; }  


        public PetServiceType? PetServiceType { get; set; }
    }
}