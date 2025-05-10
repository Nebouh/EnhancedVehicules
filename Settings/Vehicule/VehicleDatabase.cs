using System.Collections.Generic;

namespace EnhancedVehicules.Settings.Vehicule;

public static class VehicleDatabase
{
    public static readonly List<VehicleData> Vehicles = new()
    {
        new VehicleData { Key = "Shitbox", Label = "Shitbox", Slots = 5, Rows = 1, Price=5000, DiffGear=5, TopSpeed=35 },
        new VehicleData { Key = "Veeper", Label = "Van", Slots = 16, Rows = 2, Price=9000, DiffGear=5, TopSpeed=40 },
        new VehicleData { Key = "Bruiser", Label = "SUV", Slots = 5, Rows = 1, Price=12000, DiffGear=5, TopSpeed=40 },
        new VehicleData { Key = "Dinkler", Label = "Pickup", Slots = 8, Rows = 2, Price=15000, DiffGear=5, TopSpeed=45 },
        new VehicleData { Key = "Hounddog", Label = "Sedan", Slots = 5, Rows = 1, Price=25000, DiffGear=5, TopSpeed=50 },
        new VehicleData { Key = "Cheetah", Label = "Coupe", Slots = 4, Rows = 1, Price=40000, DiffGear=5, TopSpeed=60 },
    };
}
