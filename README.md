# EZ Crosshair Mod
Improve guns in **Schedule I** with a configurable crosshair.

<!-- https://nexusmods.com/schedule1/mods/ -->

---

## Features

- Add a crosshair to the HUD.
- Toggle crosshair with a configurable hotkey.

### Planned Features / Bugfixes
- Optionally preserve toggle state between game sessions *(via another config value)*
	- Plus, another config value to control whether this value is overriden or not on exit!
- Disable crosshair upon exiting game (to prevent it from rendering in the main menu)
	- (???) Also, prevent hotkey from toggling while in main menu.
	- (Combined, this should help further preserve state + fix a visual bug!)
- Customizable crosshair:
	- Color
	- Size
	- Shape
- Dynamically toggle the crosshair when a gun is equipped/unequipped in the hotbar.
- Add support for MoreGuns additions.  *(Not necessarily required, assuming that the broader solution detailed below is implemented!)*
- Identify a ranged weapon category type for use with the dynamic toggle feature. That way, all ranged weapons (modded or not) should be supported, theoretically.

## Installation

1.  **Prerequisites:** Make sure you have [MelonLoader](https://melonwiki.xyz/) (**v0.7.0**) installed.
2.  **Download:** Get the latest version of the mod from [releases](https://github.com/plasmahound/ScheduleOne-CrosshairMod/releases/latest).
3.  **Installation:** Extract the Mods folder into your game's root directory.

## Configuration

<sub>(**IMPORTANT:** First, generate the config entry by opening the game at least once!)</sub>

1.  Edit `MelonPreferences.cfg`, located in the UserData folder.
2.  Find the `[EZCrosshair]` section.
3.  Replace the contents after ` = ` for each config option.


- `EnableToggle =` ( `true`/`false` )
- `ToggleKey =` [KeyCode property](https://docs.unity3d.com/ScriptReference/KeyCode.html)

## Usage

- No more blindly inaccurate shooting in **Schedule I**!
- Press the toggle hotkey (default `Y`) to enable/disable a crosshair.
- While enabled, this crosshair will be visible, even while wielding a gun!

## Demo

![Crosshair Toggle Demo](https://raw.githubusercontent.com/plasmahound/ScheduleOne-CrosshairMod/blob/master/Demo.gif)

<br>

## License

[MIT](https://choosealicense.com/licenses/mit/)