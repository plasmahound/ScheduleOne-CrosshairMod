# EZ Crosshair Mod
Improve guns in **Schedule I** with a configurable crosshair.

https://nexusmods.com/schedule1/mods/984

---

## Features

#### Automatic Crosshair Visibility!

- No more blindly inaccurate shooting in **Schedule I**!
- Adds a crosshair to the HUD to make aiming with guns easier.
- Choice of either manual crosshair toggle *(configurable)* **OR** automatic visibility when a gun is held!

### Planned Features / Bugfixes
- Add proper support for the [Mod Manager - Phone App](https://www.nexusmods.com/schedule1/mods/397).
- Add a configurable option to disable the default white dot reticle.
- Customizable crosshair:
	- Color
	- Shape
	- Sizing Option *(for each shape)*
	- Configure & enable each shape separately *(so that you can combine multiple shapes!)*
- Identify a ranged weapon **category** type that can be checked for the automatic functionality.
  - *Theoretically*, **ALL** ranged weapons **(modded or not)** should then be supported & "future-proofed".
- ~~Dynamically toggle the crosshair when a gun is equipped/unequipped in the hotbar.~~ &rarr; **[ADDED v1.1.0]**
- ~~Add support for added weapon(s) from MoreGuns.  *(Not necessarily required, **assuming** that the broader resolution detailed right underneath is implemented!)*~~ &rarr; **[AK-47 ADDED v1.1.0]**
- ~~Adjust MelonPreferences to make clearer what each setting does.~~ **[ADDED v1.1.1]**
- ~~Add support for new Shotgun *(Basegame)* and Minigun *(MoreGuns)*.~~ **[ADDED v1.1.2]**

## Installation

1.  **Prerequisites:** Make sure you have [MelonLoader](https://melonwiki.xyz/) (**v0.7.0**) installed.
2.  **Download:** Get the latest version of the mod from [releases](https://github.com/plasmahound/ScheduleOne-CrosshairMod/releases/latest).
3.  **Installation:** Extract the Mods folder into your game's root directory.

## Configuration

<sub>(**IMPORTANT:** First, you must <b>open the game</b> at least <u>once</u> to generate preferences!)</sub>

1.  Edit `MelonPreferences.cfg`, located in the **UserData** folder.
2.  Find the `[EZCrosshair]` section.
3.  Replace the contents after ` = ` for each config option.


- `CrosshairMode =` ("auto" / "manual")
  - `"auto"` &rarr; **Auto** Mode
  - `"manual"` &rarr; **Manual** Mode
- `CrosshairIDs =` [List of gun IDs for crosshair]
  - Only used for **Auto Mode**.
- `ToggleKey =` [KeyCode property](https://docs.unity3d.com/ScriptReference/KeyCode.html)
  - Only used for **Manual Mode**.

## Usage

#### Auto Mode:

- **No action needed!** When you hold a gun, the crosshair will become visible.

#### Manual Mode:

- Press the **toggle hotkey** (default `Y`) to enable/disable the crosshair.

## Demo

![Crosshair-Toggle-Demo](https://raw.githubusercontent.com/plasmahound/ScheduleOne-CrosshairMod/master/Demo.gif)

<br>

## License

[MIT](https://choosealicense.com/licenses/mit/)
