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
["EnhancedVehicules"]
EnableMod = true
UseDefaultSettings = false
GameMaxSlots = 20

["EnhancedVehicules/Shitbox"]
Slots = 5
Rows = 1

["EnhancedVehicules/Bruiser"]
Slots = 5
Rows = 1

["EnhancedVehicules/Cheetah"]
Slots = 4
Rows = 1

["EnhancedVehicules/Dinkler"]
Slots = 8
Rows = 2

["EnhancedVehicules/Veeper"]
Slots = 16
Rows = 2

["EnhancedVehicules/Hounddog"]
Slots = 5
Rows = 1
```