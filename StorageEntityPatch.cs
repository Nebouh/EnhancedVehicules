using HarmonyLib;
using MelonLoader;
using System.Reflection;
using UnityEngine;

namespace EnhancedVehiculesMod;

public static class StorageEntityPatch
{
    private static System.Type storageEntityType;

    public static void Patch()
    {
        try
        {
            storageEntityType = Type.GetType("Il2CppScheduleOne.Storage.StorageEntity, Assembly-CSharp");
            if (storageEntityType == null)
            {
                MelonLogger.Error("[Enhanced Vehicules] StorageEntity type not found!");
                return;
            }

            var method = storageEntityType.GetMethod("Awake", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var prefix = typeof(StorageEntityPatch).GetMethod(nameof(StorageEntityInitialize_Prefix), BindingFlags.Static | BindingFlags.NonPublic);

            new HarmonyLib.Harmony("com.enhancedvehiculesmod.patch").Patch(method, new HarmonyMethod(prefix));

            MelonLogger.Msg("[Enhanced Vehicules] StorageEntity patched successfully after scene load.");
        }
        catch (System.Exception ex)
        {
            MelonLogger.Error($"[Enhanced Vehicules] Failed to patch StorageEntity: {ex.Message}");
        }
    }

    private static bool StorageEntityInitialize_Prefix(object __instance)
    {
        try
        {
            if (__instance is not MonoBehaviour val)
                return true;

            foreach (var vehicle in VehicleDatabase.Vehicles)
            {
                if (!val.gameObject.name.Contains(vehicle.Label))
                    continue;

                int slots = Mathf.Min(vehicle.Slots, SettingsManager.prefGameMaxSlots.Value);
                int rows = vehicle.Rows;

                SetFieldOrProperty(__instance, "SlotCount", slots);
                SetFieldOrProperty(__instance, "DisplayRowCount", rows);

                var refreshMethod = __instance.GetType().GetMethod("SetupSlots", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (refreshMethod != null)
                {
                    refreshMethod.Invoke(__instance, null);
                    MelonLogger.Msg($"[Enhanced Vehicules] SetupSlots called on {val.gameObject.name}");
                }

                MelonLogger.Msg($"[Enhanced Vehicules] Patched {val.gameObject.name} (Slots: {slots}, Rows: {rows})");
                break;
            }
        }
        catch (System.Exception ex)
        {
            MelonLogger.Error($"[Enhanced Vehicules] Prefix error: {ex.Message}");
        }

        return true;
    }

    private static void SetFieldOrProperty(object target, string name, object value)
    {
        var type = storageEntityType;

        var prop = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (prop != null)
        {
            prop.SetValue(target, value);
            return;
        }

        var field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (field != null)
        {
            field.SetValue(target, value);
        }
    }
}
