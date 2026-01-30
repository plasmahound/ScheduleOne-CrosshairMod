# Changelog

## [v1.1.3] 2026-01-30
- Crosshair size is now configurable in a `MelonPreferences.cfg` entry.
- Updated `GUI.DrawTexture()` lines which render the crosshair.
  - Fixed flawed coordinate position maths.
  - Change from 2 lines drawn to 4 lines on the N/S/E/W direction of the screen center.
	- This allows for a center crosshair gap!
- Refactored `OnGUI()` method in preparation of new crosshair shape options.

## [v1.1.2] 2026-01-19
- Resolves Issue [[#4](https://github.com/plasmahound/ScheduleOne-CrosshairMod/issues/4)].
- Added the `pumpshotgun` and `minigun` weapons to the Gun ID list.
- Changed the list of Gun IDs to generate as a `MelonPreferences.cfg` entry.
  - This allows the user to add any new gun ID from a game update or a mod, without having to wait!
  - Also, by removing an ID from the list, that specific gun won't have a crosshair.

## [v1.1.1] 2025-05-28
- Refactored code for easier maintenance & readability.
- Changed `MelonPreferences.cfg` to make clearer what each preference does.
- `EnableToggle` -> `CrosshairMode` (`"auto"` / `"manual"`)

## [v1.1.0] 2025-05-19
### Automatic Crosshair Visibility!
The crosshair automatically appears when holding a gun & disappears when the gun is put away!

<sup><strong>(NEW!)</strong> For <strong>Auto Mode</strong>, make sure `EnableToggle` is set to `false` in `MelonPreferences.cfg`.</sup>

- The `EnableToggle` preference now controls which crosshair mode you want:
  - `EnableToggle` = `true` &rarr; **Manual Mode** (**toggle** crosshair via hotkey)
  - `EnableToggle` = `false` &rarr; **Auto Mode** (automatic crosshair visibility)

#### Additional Notes:

- When `EnableToggle` = `false`, **the hotkey is completely disabled**!
- The description for `EnableToggle` was modified to make a little more sense.  *(NOTE: names/descriptions still need more improvement!)*

## [v1.0.1] 2025-05-16
- Changed the description for the `EnableToggle` preference. *(**No action required!*** `MelonPreferences.cfg` *should update automatically)*
- The crosshair is no longer rendered in the main menu.
- The toggle hotkey no longer activates in the main menu.

## [v1.0.0] 2025-05-16
- (Optionally) disable the ability to toggle the crosshair with the `EnableToggle` config.
- Set the toggle crosshair hotkey with `ToggleKey`. (Find valid hotkey names [here](https://docs.unity3d.com/ScriptReference/KeyCode.html))
- If `EnableToggle` is set to `false`, the toggle key won't do anything! (as of now, this effectively **disables** the mod)
- Each line that the crosshair is comprised of is 10 pixels long and 1px wide. (NOT YET CONFIGURABLE)
