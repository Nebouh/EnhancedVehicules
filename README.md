# Enhanced Vehicules

A mod for **Schedule I** that enhances and customizes the storage capacity of various vehicles in the game.

Created by **Nebouh** — powered by **MelonLoader** and **Harmony**.

---

## 🚗 Features

- **Customize storage**: Adjust the slot count and number of display rows for each vehicle.
- **Customize pricing**: Modify the in-game purchase price of vehicles.
- **Customize acceleration**: Modify differential gearing of vehicles to change their acceleration.
- **Customize top speed**: Modify the top speed of vehicles.
- **Per-vehicle settings**: Each vehicle has independent configuration for storage and price.
- **Increase maximum storage slots**: Raise the slot limit beyond the default 20 slots.
- Ability to modify the **acceleration** and **top speed** of vehicles.

---

## 🔥 Latest Updates

- Ability to modify the **acceleration** and **top speed** of vehicles.

---

## 🔮 Planned Features

Future updates will expand the mod to include performance customization for each vehicle, allowing users to tweak:

- **Handling** – Fine-tune steering responsiveness and control.
- **Drift Behavior** – Modify how vehicles slide and grip while turning.
- **New Vehicles** – Add support for van deliverys

---

## 🛠️ Technical Details

This mod dynamically patches the :
- `SlotCount`
- `DisplayRowCount`
- `GameMaxSlots`
- `vehiclePrice`
- `diffGearing`
- `TopSpeed`

Values are applied based on user configuration and capped using the global `GameMaxSlots` setting.

Supported vehicle types and their default settings:

| Vehicle     | Internal Key | Slots | Rows | Configurable | Price |  Diff Gear | Top Speed |
|-------------|--------------|-------|------|--------------|-------|------------|------------|
| Shitbox     | Shitbox      | 5     | 1    | ✅           | 5000  | 5          | 35         |
| SUV         | Bruiser      | 5     | 1    | ✅           | 12000 | 5          | 40         |
| Coupe       | Cheetah      | 4     | 1    | ✅           | 40000 | 5          | 60         |
| Pickup      | Dinkler      | 8     | 2    | ✅           | 15000 | 5          | 45         |
| Van         | Veeper       | 16    | 2    | ✅           | 9000  | 5          | 40         |
| Sedan       | Hounddog     | 5     | 1    | ✅           | 25000 | 5          | 50         |

---

## ⚙️ Configuration

Add the file `*.dll` in your Mods folder of your game
Settings are stored in the following file: `./UserData/MelonPreferences.cfg`

Example default configuration:
```ini
[EnhancedVehicules]
EnableMod = true
GameMaxSlots = 20

["EnhancedVehicules/Shitbox"]
Slots = 5
Rows = 1
Price = 5000.0
DiffGear = 5.0
TopSpeed = 35.0

["EnhancedVehicules/Veeper"]
Slots = 16
Rows = 2
Price = 9000.0
DiffGear = 5.0
TopSpeed = 40.0

["EnhancedVehicules/Bruiser"]
Slots = 5
Rows = 1
Price = 12000.0
DiffGear = 5.0
TopSpeed = 40.0

["EnhancedVehicules/Dinkler"]
Slots = 8
Rows = 2
Price = 15000.0
DiffGear = 5.0
TopSpeed = 45.0

["EnhancedVehicules/Hounddog"]
Slots = 5
Rows = 1
Price = 25000.0
DiffGear = 5.0
TopSpeed = 50.0

["EnhancedVehicules/Cheetah"]
Slots = 4
Rows = 1
Price = 40000.0
DiffGear = 5.0
TopSpeed = 60.0
```

---

## 🙌 Credits

Special thanks to:
- [JumbleBumble](https://github.com/JumbleBumble) for implementing the **differential gearing** and **top speed** features.
- [BreadCh4n](https://github.com/BreadCh4n) for assisting with managing the **maximum storage limit** beyond the game's default slot restrictions.
