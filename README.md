# Enhanced Vehicules

A mod for **Schedule I** that enhances and customizes the storage capacity of various vehicles in the game.

Created by **Nebouh** — powered by **MelonLoader** and **Harmony**.

---

## 🚗 Features

- Increases the slot count and number of display rows for in-game vehicles.
- Fully customizable per vehicle type via configuration.
- Includes a global slot cap to maintain balance.
- Uses MelonPreferences for user-configurable settings.

---

## 🔮 Planned Features

Future updates will expand the mod to include performance customization for each vehicle, allowing users to tweak:

- **Speed** – Adjust how fast each vehicle can go.
- **Handling** – Fine-tune steering responsiveness and control.
- **Drift Behavior** – Modify how vehicles slide and grip while turning.

---

## 🛠️ Technical Details

This mod dynamically patches the `Awake` method of the `StorageEntity` class using Harmony to modify internal values:

- `SlotCount`
- `DisplayRowCount`

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
