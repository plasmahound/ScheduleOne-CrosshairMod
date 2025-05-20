# EZ Crosshair Mod
Improve guns in **Schedule I** with a configurable crosshair.

https://nexusmods.com/schedule1/mods/984

---

## Features

#### [NEW] Automatic Crosshair Visibility!!!

- No more blindly inaccurate shooting in **Schedule I**!
- Adds a crosshair to the HUD to make aiming with guns easier.
- Choice of either manual crosshair toggle *(configurable)* **OR** automatic visibility when a gun is held!

### Planned Features / Bugfixes
- Adjust MelonPreferences to make clearer what each setting does.
  - *(`EnableToggle` is a little confusing after **v1.1.0**)*
- Create a new preference that disables the auto-mode. *(Then, the toggle could still be used in a TBD way **together** with the new feature!)**
- Customizable crosshair:
	- Color
	- Shape
	- Sizing Option *(for each shape)*
	- Configure & enable each shape separately *(so that you can combine multiple shapes!)*
- ~~Dynamically toggle the crosshair when a gun is equipped/unequipped in the hotbar.~~ &rarr; **[ADDED v1.1.0]**
- ~~Add support for added weapon(s) from MoreGuns.  *(Not necessarily required, **assuming** that the broader resolution detailed right underneath is implemented!)*~~ &rarr; **[AK-47 ADDED v1.1.0]**
- Identify a ranged weapon **category** type that can be checked for the automatic functionality.
  - (*Theoretically*, **ALL** ranged weapons **(modded or not)** should then be supported & "future-proofed")

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
  - `true` &rarr; **Manual** Mode
  - `false` &rarr; **Auto** Mode **(NEW!)**
- `ToggleKey =` [KeyCode property](https://docs.unity3d.com/ScriptReference/KeyCode.html)
  - Only used for **Manual Mode** *(as of now)*

## Usage

#### Auto Mode:

- **No action needed!** When you hold a gun, the crosshair will be visible.

#### Manual Mode:

- Press the **toggle hotkey** (default `Y`) to enable/disable the crosshair.

## Demo

![Crosshair-Toggle-Demo](https://raw.githubusercontent.com/plasmahound/ScheduleOne-CrosshairMod/master/Demo.gif)

<br>

## License

[MIT](https://choosealicense.com/licenses/mit/)
