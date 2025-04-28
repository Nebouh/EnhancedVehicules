using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[assembly: MelonColor()]
[assembly: MelonInfo(typeof(EnhancedVehiculesMod.EnhancedVehicules), "Enhanced Vehicules", "1.0.0", "Nebouh")]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace EnhancedVehiculesMod;

public class EnhancedVehicules : MelonMod
{
    private static bool isPatched = false;

    public override void OnInitializeMelon()
    {
        MelonLogger.Msg("[Enhanced Vehicules] Loading settings...");

        SettingsManager.CreateSettings();
        SettingsManager.LoadSettings();

        if (!SettingsManager.prefEnableMod.Value)
        {
            MelonLogger.Msg("[Enhanced Vehicules] Mod disabled via config.");
            return;
        }

        SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>)OnSceneLoaded);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!scene.name.Equals("Main", StringComparison.OrdinalIgnoreCase))
            return;
        if (isPatched) return;
        MelonCoroutines.Start(DelayedAfterLoad());

        MelonLogger.Msg($"[Enhanced Vehicules] Scene loaded: {scene.name}, now patching...");
        StorageEntityPatch.Patch();
        MelonCoroutines.Start(VehiclePriceManager.ApplyPrices());
        
        isPatched = true;
    }
    private IEnumerator DelayedAfterLoad()
    {
        yield return new WaitForSeconds(2f);
    }
}
