using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.ListingOwned.HomeElectronics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.ListingOwned
{
    public class ElectronicsDetails
    {
        [Key, ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        public virtual Listing? Listing { get; set; }

        public TVBrand? TVBrand { get; set; }
        public TVType? TVType { get; set; }
        public ElectronicsCondition? Condition { get; set; }
        public ScreenSize? ScreenSize { get; set; }
        public DisplayTechnology? DisplayTechnology { get; set; }


        public AudioBrand? AudioBrand { get; set; }

        public KitchenApplianceType? KitchenApplianceType { get; set; }

        public CoolingHeatingType? CoolingHeatingType { get; set; }

        public CleaningApplianceType? CleaningApplianceType { get; set; }

        public WashingMachineBrand? WashingMachineBrand { get; set; }

        public ComputerBrand? ComputerBrand { get; set; }
        public ComputerType? ComputerType { get; set; }
        public Processor? Processor { get; set; }
        public RamSize? RamSize { get; set; }
        public ComputerScreenSize? ComputerScreenSize { get; set; }
        public ComputerStorage? ComputerStorage { get; set; }
        public StorageType? StorageType { get; set; }
        public ComputerColor? ComputerColor { get; set; }

        public ComputerAccessoryType? ComputerAccessoryType { get; set; }

        public CameraBrand? CameraBrand { get; set; }
        public CameraType? CameraType { get; set; }

        public GamingBrand? GamingBrand { get; set; }
        public GamingType? GamingType { get; set; }

        public CompatibleConsole? CompatibleConsole { get; set; }
        public GameCondition? GameCondition { get; set; }
        public GameGenre? GameGenre { get; set; }
    }
}
