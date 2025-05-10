using EnhancedVehicules.Settings;
using EnhancedVehicules.Settings.Vehicule;
using HarmonyLib;
using MelonLoader;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace EnhancedVehicules.Modules.Storage;

public static class StorageEntityPatch
{
    private static Type storageEntityType;

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

            var harmony = new HarmonyLib.Harmony("com.enhancedvehiculesmod.patch");
            harmony.Patch(
                storageEntityType.GetMethod("Awake", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic),
                new HarmonyMethod(typeof(StorageEntityPatch), nameof(StorageEntityInitialize_Prefix))
            );

            harmony.PatchAll(typeof(StorageMenuPatch));

            MelonLogger.Msg("[Enhanced Vehicules] StorageEntity and StorageMenu patched successfully.");
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"[Enhanced Vehicules] Failed to patch: {ex.Message}");
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
                refreshMethod?.Invoke(__instance, null);

                MelonLogger.Msg($"[Enhanced Vehicules] Patched {val.gameObject.name} (Slots: {slots}, Rows: {rows})");
                break;
            }
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"[Enhanced Vehicules] StorageEntity Prefix error: {ex.Message}");
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

    [HarmonyPatch(typeof(Il2CppScheduleOne.UI.StorageMenu), "Awake")]
    private static class StorageMenuPatch
    {
        [HarmonyPrefix]
        private static void Prefix(Il2CppScheduleOne.UI.StorageMenu __instance)
        {
            try
            {
                int maxSlots = SettingsManager.prefGameMaxSlots.Value;
                if (maxSlots <= 20) return;

                var slotsTransform = __instance.Container?.Find("Slots");
                if (slotsTransform == null) return;

                var slotsUIsList = new List<Il2CppScheduleOne.UI.ItemSlotUI>(__instance.SlotsUIs);

                while (slotsTransform.childCount < maxSlots)
                {
                    var firstChild = slotsTransform.GetChild(0).gameObject;
                    var clonedSlot = Object.Instantiate(firstChild, slotsTransform, true);
                    clonedSlot.name = clonedSlot.name.Replace("Clone", $"Extra-[{slotsTransform.childCount - 1}]");

                    clonedSlot.SetActive(true);
                    clonedSlot.transform.localScale = Vector3.one;

                    var itemSlotUI = clonedSlot.GetComponent<Il2CppScheduleOne.UI.ItemSlotUI>()
                        ?? clonedSlot.AddComponent<Il2CppScheduleOne.UI.ItemSlotUI>();
                    slotsUIsList.Add(itemSlotUI);
                }

                __instance.SlotsUIs = slotsUIsList.ToArray();

                // Ajustement visuel automatique du container
                var gridLayout = __instance.SlotGridLayout;
                if (gridLayout != null && __instance.Container != null)
                {
                    int columns = gridLayout.constraintCount;
                    int currentRows = Mathf.CeilToInt((float)slotsUIsList.Count / columns);

                    var rect = __instance.Container.GetComponent<RectTransform>();

                    // Point d'ancrage centré
                    // rect.anchorMin = new Vector2(0.5f, 0.5f);
                    // rect.anchorMax = new Vector2(0.5f, 0.5f);
                    // rect.pivot = new Vector2(0.5f, 0.5f);

                    // Décalage vers le haut pour chaque rangée au-delà de 3
                    // int extraRows = Mathf.Max(0, currentRows - 3);
                    // float offsetY = extraRows * 0.2f;

                    rect.anchoredPosition += new Vector2(0f, offsetY);

                    MelonLogger.Msg($"[Enhanced Vehicules] Adjusted StorageMenu for {currentRows} rows (Offset Y: {offsetY}).");
                }

                MelonLogger.Msg($"[Enhanced Vehicules] StorageMenu slots expanded to {slotsTransform.childCount}.");
            }
            catch (System.Exception ex)
            {
                MelonLogger.Error($"[Enhanced Vehicules] StorageMenu Patch error: {ex.Message}");
            }
        }
    }

}
