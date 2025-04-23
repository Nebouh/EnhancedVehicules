using System.Reflection;
using EnhancedVehiculesMod;
using HarmonyLib;
using Il2CppFishNet.Object;
using Il2CppScheduleOne.DevUtilities;
using MelonLoader;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

[assembly: MelonColor()]
[assembly: MelonInfo(typeof(EnhancedVehicules), "Enhanced Vehicules", "1.0.0", "Nebouh")]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace EnhancedVehiculesMod;

public class EnhancedVehicules : MelonMod
{
    private static MelonPreferences_Category category;
    private static MelonPreferences_Entry<bool> prefEnableMod;
    private static MelonPreferences_Entry<bool> prefUseDefaultSettings;
    private static MelonPreferences_Entry<int> prefGameMaxSlots;
    private static readonly Dictionary<string, MelonPreferences_Category> RackCategories = new();

    private static Type storageEntityType;

    private struct RackSettings
    {
        public string Key;
        public string Label;
        public int DefaultSlots;
        public int DefaultRows;
        public int Slots;
        public int Rows;
        public MelonPreferences_Entry<int> PrefSlots;
        public MelonPreferences_Entry<int> PrefRows;
    }

    private static readonly Dictionary<string, RackSettings> RackConfigs = new()
    {
        ["Shitbox"] = new RackSettings { Key = "Shitbox", Label = "Shitbox", DefaultSlots = 5, DefaultRows = 1 }, // Shitbox
        ["Bruiser"] = new RackSettings { Key = "Bruiser", Label = "SUV", DefaultSlots = 5, DefaultRows = 1 }, // Bruiser
        ["Cheetah"] = new RackSettings { Key = "Cheetah", Label = "Coupe", DefaultSlots = 4, DefaultRows = 1 }, // Cheetah
        ["Dinkler"] = new RackSettings { Key = "Dinkler", Label = "Pickup", DefaultSlots = 8, DefaultRows = 2 }, // Dinkler
        ["Veeper"] = new RackSettings { Key = "Veeper", Label = "Van", DefaultSlots = 16, DefaultRows = 2 }, // Van
        ["Hounddog"] = new RackSettings { Key = "Hounddog", Label = "Sedan", DefaultSlots = 5, DefaultRows = 1 }, // Sedan
    };

    public override void OnInitializeMelon()
    {
        MelonLogger.Msg("[Enhanced Vehicules] Loading...");

        CreateSettings();
        LoadSettings();

        if (!prefEnableMod.Value)
        { 
            MelonLogger.Msg("[Enhanced Vehicules] Mod disabled via config.");
            return;
        }

        PatchStorage();
        MelonLogger.Msg("[Enhanced Vehicules] Successfully loaded.");
    }

    private static void CreateSettings()
    {
        var generalCategory = MelonPreferences.CreateCategory("EnhancedVehicules", "Enhanced Vehicules Settings");
        prefEnableMod = generalCategory.CreateEntry("EnableMod", true, "Enable This Mod");
        prefUseDefaultSettings = generalCategory.CreateEntry("UseDefaultSettings", false, "Use Default Rack Settings");
        prefGameMaxSlots = generalCategory.CreateEntry("GameMaxSlots", 20, "Maximum Total Slots in Game");

        foreach (var key in RackConfigs.Keys.ToList())
        {
            var cfg = RackConfigs[key];

            var vehicleCategory = MelonPreferences.CreateCategory(
                $"EnhancedVehicules/{cfg.Key}",
                $"{cfg.Label} Settings"
            );

            RackCategories[cfg.Key] = vehicleCategory;

            cfg.PrefSlots = vehicleCategory.CreateEntry("Slots", cfg.DefaultSlots, $"{cfg.Label} - Number of Slots");
            cfg.PrefRows = vehicleCategory.CreateEntry("Rows", cfg.DefaultRows, $"{cfg.Label} - Number of Rows");

            RackConfigs[key] = cfg;
        }

    }


    private static void LoadSettings()
    {
        foreach (var key in RackConfigs.Keys.ToList())
        {
            var cfg = RackConfigs[key];

            if (prefUseDefaultSettings.Value)
            {
                cfg.Slots = cfg.DefaultSlots;
                cfg.Rows = cfg.DefaultRows;
            }
            else
            {
                cfg.Slots = cfg.PrefSlots.Value;
                cfg.Rows = cfg.PrefRows.Value;
            }

            RackConfigs[key] = cfg;
        }
    }

    private static void PatchStorage()
    {
        try
        {
            storageEntityType = Type.GetType("Il2CppScheduleOne.Storage.StorageEntity, Assembly-CSharp");
            if (storageEntityType == null)
                throw new Exception("StorageEntity type not found");

            var method = storageEntityType.GetMethod("Awake", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var prefix = typeof(EnhancedVehicules).GetMethod(nameof(StorageEntityInitialize_Prefix), BindingFlags.Static | BindingFlags.NonPublic);
            new HarmonyLib.Harmony("com.mod.enhancedvehicules").Patch(method, new HarmonyMethod(prefix));
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"[Enhanced Vehicules] Patch error: {ex.Message}");
        }
    }

    private static bool StorageEntityInitialize_Prefix(object __instance)
    {
        try
        {
            if (__instance is not MonoBehaviour val) return true;
            // MelonLogger.Msg($"[Enhanced Vehicules] Found object: {val.gameObject.name}");

            foreach (var entry in RackConfigs)
            {
                if (!val.gameObject.name.Contains(entry.Value.Label)) continue;

                int slots = Mathf.Min(entry.Value.Slots, prefGameMaxSlots.Value);
                int rows = entry.Value.Rows;

                SetFieldOrProperty(__instance, "SlotCount", slots);
                SetFieldOrProperty(__instance, "DisplayRowCount", rows);

                break;
            }

            return true;
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"[Enhanced Vehicules] Prefix error: {ex.Message}");
            return true;
        }
    }

    private static void SetFieldOrProperty(object target, string name, object value)
    {
        var prop = storageEntityType.GetProperty(name);
        if (prop != null) { prop.SetValue(target, value); return; }

        var field = storageEntityType.GetField(name);
        field?.SetValue(target, value);
    }
}
