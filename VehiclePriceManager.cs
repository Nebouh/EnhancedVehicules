using Il2CppScheduleOne.Vehicles;
using MelonLoader;
using System.Collections;
using UnityEngine;
using Il2CppTMPro;
using Il2CppScheduleOne.Tools;

namespace EnhancedVehiculesMod;

public static class VehiclePriceManager
{
    private static VehicleManager vehicleManager;

    public static IEnumerator ApplyPrices()
    {
        yield return null;

        vehicleManager = UnityEngine.Object.FindObjectOfType<VehicleManager>();

        if (vehicleManager == null)
        {
            MelonLogger.Warning("[Enhanced Vehicules] VehicleManager not found. Cannot apply prices to vehicles.");
            yield break;
        }

        MelonLogger.Msg("[Enhanced Vehicules] Applying vehicle prices...");

        foreach (LandVehicle vehicle in vehicleManager.VehiclePrefabs)
        {
            if (vehicle == null) continue;
            ApplyPriceToVehicle(vehicle.name, price => vehicle.vehiclePrice = price);
        }

        foreach (VehicleSaleSign sign in UnityEngine.Object.FindObjectsOfType<VehicleSaleSign>())
        {
            if (sign == null) continue;
            if (sign.NameLabel == null || sign.PriceLabel == null) continue;

            ApplyPriceToVehicle(sign.NameLabel.text, price => sign.PriceLabel.text = $"${price}");
        }
    }

    private static void ApplyPriceToVehicle(string objectName, Action<float> applyAction)
    {
        foreach (var data in VehicleDatabase.Vehicles)
        {
            if (objectName.Contains(data.Label, StringComparison.OrdinalIgnoreCase))
            {
                applyAction(data.Price);
                MelonLogger.Msg($"[Enhanced Vehicules] Applied price ${data.Price} to {objectName}");
                break;
            }
        }
    }
}
