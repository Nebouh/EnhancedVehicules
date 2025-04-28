using System.Collections.Generic;

namespace EnhancedVehicules.Settings.Vehicule;

public static class VehicleDatabase
{
    public static readonly List<VehicleData> Vehicles = new()
    {
        new VehicleData { Key = "Shitbox", Label = "Shitbox", Slots = 5, Rows = 1, Price=5000 },
        new VehicleData { Key = "Veeper", Label = "Van", Slots = 16, Rows = 2, Price=9000 },
        new VehicleData { Key = "Bruiser", Label = "SUV", Slots = 5, Rows = 1, Price=12000 },
        new VehicleData { Key = "Dinkler", Label = "Pickup", Slots = 8, Rows = 2, Price=15000 },
        new VehicleData { Key = "Hounddog", Label = "Sedan", Slots = 5, Rows = 1, Price=25000 },
        new VehicleData { Key = "Cheetah", Label = "Coupe", Slots = 4, Rows = 1, Price=40000 },
    };
}
