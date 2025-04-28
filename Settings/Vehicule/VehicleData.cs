using MelonLoader;

namespace EnhancedVehicules.Settings.Vehicule;

public class VehicleData
{
    public string Key;
    public string Label;
    public int Slots;
    public int Rows;
    public float Price;
    public MelonPreferences_Entry<int> PrefSlots;
    public MelonPreferences_Entry<int> PrefRows;
    public MelonPreferences_Entry<float> PrefPrice;
}
