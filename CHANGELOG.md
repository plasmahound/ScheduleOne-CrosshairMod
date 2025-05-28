# Changelog

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
