using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.ListingOwned;

namespace Arzly.Api.Helpers.ListingFilters
{
    public static class ElectronicsDetailsFilter
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, ElectronicsDetails d)
        {
            if (d.TVBrand.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.TVBrand == d.TVBrand);
            if (d.TVType.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.TVType == d.TVType);
            if (d.Condition.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.Condition == d.Condition);
            if (d.ScreenSize.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.ScreenSize == d.ScreenSize);
            if (d.DisplayTechnology.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.DisplayTechnology == d.DisplayTechnology);
            if (d.AudioBrand.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.AudioBrand == d.AudioBrand);
            if (d.KitchenApplianceType.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.KitchenApplianceType == d.KitchenApplianceType);
            if (d.CoolingHeatingType.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.CoolingHeatingType == d.CoolingHeatingType);
            if (d.CleaningApplianceType.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.CleaningApplianceType == d.CleaningApplianceType);
            if (d.WashingMachineBrand.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.WashingMachineBrand == d.WashingMachineBrand);
            if (d.ComputerBrand.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.ComputerBrand == d.ComputerBrand);
            if (d.ComputerType.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.ComputerType == d.ComputerType);
            if (d.Processor.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.Processor == d.Processor);
            if (d.RamSize.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.RamSize == d.RamSize);
            if (d.ComputerScreenSize.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.ComputerScreenSize == d.ComputerScreenSize);
            if (d.ComputerStorage.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.ComputerStorage == d.ComputerStorage);
            if (d.StorageType.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.StorageType == d.StorageType);
            if (d.ComputerColor.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.ComputerColor == d.ComputerColor);
            if (d.ComputerAccessoryType.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.ComputerAccessoryType == d.ComputerAccessoryType);
            if (d.CameraBrand.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.CameraBrand == d.CameraBrand);
            if (d.CameraType.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.CameraType == d.CameraType);
            if (d.GamingBrand.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.GamingBrand == d.GamingBrand);
            if (d.GamingType.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.GamingType == d.GamingType);
            if (d.CompatibleConsole.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.CompatibleConsole == d.CompatibleConsole);
            if (d.GameCondition.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.GameCondition == d.GameCondition);
            if (d.GameGenre.HasValue)
                query = query.Where(x => x.ElectronicsDetails!.GameGenre == d.GameGenre);

            return query;
        }
    }
}
