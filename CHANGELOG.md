# Changelog

## [v1.0.0] 2025-05-16
- (Optionally) disable the ability to toggle the crosshair with the `EnableToggle` config.
- Set the toggle crosshair hotkey with `ToggleKey`. (Find valid hotkey names [here](https://docs.unity3d.com/ScriptReference/KeyCode.html))
- If `EnableToggle` is set to `false`, the toggle key won't do anything! (as of now, this effectively **disables** the mod)
- Each line that the crosshair is comprised of is 10 pixels long and 1px wide. (NOT YET CONFIGURABLE)

## [v1.0.1] 2025-05-16
- Changed the description for the `EnableToggle` preference. *(**No action required!*** `MelonPreferences.cfg` *should update automatically)*
- The crosshair is no longer rendered in the main menu.
- The toggle hotkey no longer activates in the main menu.