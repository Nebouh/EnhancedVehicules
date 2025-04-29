# Enhanced Vehicules

A mod for **Schedule I** that enhances and customizes the storage capacity of various vehicles in the game.

Created by **Nebouh** — powered by **MelonLoader** and **Harmony**.

---

## 🚗 Features

- **Customize storage**: Adjust the slot count and number of display rows for each vehicle.
- **Customize pricing**: Modify the in-game purchase price of vehicles.
- **Per-vehicle settings**: Each vehicle has independent configuration for storage and price.
- **Increase maximum storage slots**: Raise the slot limit beyond the default 20 slots.

---

## 🔥 Latest Updates

- New **vehicle price management** feature: patch vehicle prefab prices at runtime.
- **Optimized patching**: Only applies after the `"Main"` scene is loaded.

---

## 🔮 Planned Features

Future updates will expand the mod to include performance customization for each vehicle, allowing users to tweak:

- **Speed** – Adjust how fast each vehicle can go.
- **Handling** – Fine-tune steering responsiveness and control.
- **Drift Behavior** – Modify how vehicles slide and grip while turning.

---

## 🛠️ Technical Details

This mod dynamically patches the :
- `SlotCount`
- `DisplayRowCount`
- `GameMaxSlots`
- `vehiclePrice`

Values are applied based on user configuration and capped using the global `GameMaxSlots` setting.

Supported vehicle types and their default settings:

| Vehicle     | Internal Key | Slots | Rows | Configurable |
|-------------|--------------|-------|------|--------------|
| Shitbox     | Shitbox      | 5     | 1    | ✅            |
| SUV         | Bruiser      | 5     | 1    | ✅            |
| Coupe       | Cheetah      | 4     | 1    | ✅            |
| Pickup      | Dinkler      | 8     | 2    | ✅            |
| Van         | Veeper       | 16    | 2    | ✅            |
| Sedan       | Hounddog     | 5     | 1    | ✅            |

---

## ⚙️ Configuration

Settings are stored in the following file: `./UserData/MelonPreferences.cfg`

Example default configuration:
```ini
[EnhancedVehicules]
EnableMod = true
UseDefaultSettings = false
GameMaxSlots = 120

["EnhancedVehicules/Shitbox"]
Slots = 32
Rows = 4
Price = 1500.0

["EnhancedVehicules/Bruiser"]
Slots = 32
Rows = 4
Price = 2250.0

["EnhancedVehicules/Cheetah"]
Slots = 32
Rows = 4
Price = 2500.0

["EnhancedVehicules/Dinkler"]
Slots = 32
Rows = 4
Price = 2750.0

["EnhancedVehicules/Veeper"]
Slots = 32
Rows = 4
Price = 2950.0

["EnhancedVehicules/Hounddog"]
Slots = 32
Rows = 4
Price = 3000.0
```
