# EZ Crosshair Mod
Improve guns in **Schedule I** with a configurable crosshair.

https://nexusmods.com/schedule1/mods/984

---

## Features

- No more blindly inaccurate shooting in **Schedule I**!
- Adds a crosshair to the HUD to make aiming with guns easier.
- Configure which key you want to toggle the crosshair.

### Planned Features / Bugfixes
- Optionally preserve toggle state between game sessions *(via another config value)*
	- Plus, another config value to control whether this value is overridden or not on exit!
- Customizable crosshair:
	- Color
	- Shape
	- Sizing Option *(for each shape)*
	- Configure & enable each shape separately (so that you can combine multiple shapes!)
- Dynamically toggle the crosshair when a gun is equipped/unequipped in the hotbar.
- Add support for added weapon(s) from MoreGuns.  *(Not necessarily required, **assuming** that the broader resolution detailed right underneath is implemented!)*
- Identify a ranged weapon category type for use with the dynamic toggle feature. That way, all ranged weapons **(modded or not)** should be supported, *theoretically*.

## Installation

1.  **Prerequisites:** Make sure you have [MelonLoader](https://melonwiki.xyz/) (**v0.7.0**) installed.
2.  **Download:** Get the latest version of the mod from [releases](https://github.com/plasmahound/ScheduleOne-CrosshairMod/releases/latest).
3.  **Installation:** Extract the Mods folder into your game's root directory.

## Configuration

<sub>(**IMPORTANT:** First, you must <b>open the game</b> at least <u>once</u> to generate preferences!)</sub>

1.  Edit `MelonPreferences.cfg`, located in the **UserData** folder.
2.  Find the `[EZCrosshair]` section.
3.  Replace the contents after ` = ` for each config option.


- `EnableToggle =` ( `true`/`false` )
- `ToggleKey =` [KeyCode property](https://docs.unity3d.com/ScriptReference/KeyCode.html)

## Usage

- Press the toggle hotkey (default `Y`) to enable/disable the crosshair.
- While enabled, the crosshair will be visible, even when wielding a gun!

## Demo

![Crosshair-Toggle-Demo](https://raw.githubusercontent.com/plasmahound/ScheduleOne-CrosshairMod/master/Demo.gif)

<br>

## License

[MIT](https://choosealicense.com/licenses/mit/)
