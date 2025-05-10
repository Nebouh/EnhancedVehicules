using EnhancedVehicules.Settings.Vehicule;
using MelonLoader;
using System.Collections.Generic;

namespace EnhancedVehicules.Settings;

public static class SettingsManager
{
    public static MelonPreferences_Category category;
    public static MelonPreferences_Entry<bool> prefEnableMod;
    public static MelonPreferences_Entry<int> prefGameMaxSlots;
    public static readonly Dictionary<string, MelonPreferences_Category> RackCategories = new();

    public static void CreateSettings()
    {
        category = MelonPreferences.CreateCategory("EnhancedVehicules", "Enhanced Vehicules Settings");
        prefEnableMod = category.CreateEntry("EnableMod", true, "Enable This Mod");
        prefGameMaxSlots = category.CreateEntry("GameMaxSlots", 20, "Maximum Total Slots in Game");

        foreach (var vehicle in VehicleDatabase.Vehicles)
        {
            var vehicleCategory = MelonPreferences.CreateCategory(
                $"EnhancedVehicules/{vehicle.Key}",
                $"{vehicle.Label} Settings"
            );

            RackCategories[vehicle.Key] = vehicleCategory;

            vehicle.PrefSlots = vehicleCategory.CreateEntry("Slots", vehicle.Slots, $"{vehicle.Label} - Number of Slots");
            vehicle.PrefRows = vehicleCategory.CreateEntry("Rows", vehicle.Rows, $"{vehicle.Label} - Number of Rows");
            vehicle.PrefPrice = vehicleCategory.CreateEntry("Price", vehicle.Price, $"{vehicle.Label} - Purchase Price");
            vehicle.PrefDiffGear = vehicleCategory.CreateEntry("DiffGear", vehicle.DiffGear, $"{vehicle.Label} - Acceleration");
            vehicle.PrefTopSpeed = vehicleCategory.CreateEntry("TopSpeed", vehicle.TopSpeed, $"{vehicle.Label} - Top Speed");
        }
    }

    public static void LoadSettings()
    {
        foreach (var vehicle in VehicleDatabase.Vehicles)
        {
            if (vehicle.PrefSlots != null)
                vehicle.Slots = vehicle.PrefSlots.Value;

            if (vehicle.PrefRows != null)
                vehicle.Rows = vehicle.PrefRows.Value;

            if (vehicle.PrefPrice != null)
                vehicle.Price = vehicle.PrefPrice.Value;

            if (vehicle.PrefDiffGear != null)
                vehicle.DiffGear = vehicle.PrefDiffGear.Value;

            if (vehicle.PrefTopSpeed != null)
                vehicle.TopSpeed = vehicle.PrefTopSpeed.Value;

        }
    }
}
