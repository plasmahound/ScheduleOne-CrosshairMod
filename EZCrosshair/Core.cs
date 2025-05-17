using HarmonyLib;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(EZCrosshair.Core), "EZCrosshair", "1.0.0", "plasmahound", null)]
[assembly: MelonGame("TVGS", "Schedule I")]
[assembly: MelonColor(255, 255, 130, 30)]
[assembly: MelonAuthorColor(200, 180, 130, 255)]

namespace EZCrosshair
{
	public class Core : MelonMod
	{
		public override void OnInitializeMelon()
		{
			LoggerInstance.Msg("Aimed & ready to fire!");
			Core.category = MelonPreferences.CreateCategory("EZCrosshair", "EZ Crosshair");
			Core.enableToggle = Core.category.CreateEntry<bool>("EnableToggle", true, null, "Enable manual crosshair toggle? (true/false)", false, false, null, null);
			Core.toggleKey = Core.category.CreateEntry<KeyCode>("ToggleKey", KeyCode.Y, null, "Crosshair toggle hotkey:", false, false, null, null);
		}

		public override void OnSceneWasInitialized(int buildIndex, string sceneName)
		{
			if (sceneName == "Main")
			{
				DebugLog("{Scene} Main Initialized!");
			}
			if (sceneName == "Menu")
			{
				DebugLog("{Scene} Menu Initialized!");
			}
		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			if (sceneName == "Main")
			{
				DebugLog("{Scene} Main Loaded!");

				this.gameLoaded = true;
			}
		}

		public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
		{
			if (sceneName == "Main")
			{
				DebugLog("{Scene} Main Unloaded!");

				this.gameLoaded = false;
			}
		}

		public override void OnUpdate()
		{
			if (this.gameLoaded && Core.enableToggle.Value && Input.GetKeyDown(Core.toggleKey.Value))
			{
				DebugLog("[Toggle: " + (!this.showCrosshair ? "ON" : "OFF") + "]");

				this.showCrosshair = !this.showCrosshair;
			}
		}

		public override void OnGUI()
		{
			if (this.gameLoaded && Core.enableToggle.Value && this.showCrosshair)
			{
				float screenWidth = (float)(Screen.width / 2);
				float screenHeight = (float)(Screen.height / 2);
				float lineLength = 10f;
				float lineThickness = 1f;

				GUI.DrawTexture(new Rect(screenWidth - lineLength / 2f, screenHeight - lineThickness, lineLength, 2f), Texture2D.whiteTexture);
				GUI.DrawTexture(new Rect(screenWidth - lineThickness, screenHeight - lineLength / 2f, 2f, lineLength), Texture2D.whiteTexture);
			}
		}

		public static void DebugLog(string msg)
		{
			if (Core.debugMode)
			{
				MelonLogger.Msg(msg);
			}
		}

		private const bool debugMode = false;

		private bool gameLoaded;

		private bool showCrosshair;

		public static MelonPreferences_Category category;

		public static MelonPreferences_Entry<bool> enableToggle;

		public static MelonPreferences_Entry<KeyCode> toggleKey;
	}
}