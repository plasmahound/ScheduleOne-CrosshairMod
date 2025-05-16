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
			Core.enableToggle = Core.category.CreateEntry<bool>("EnableToggle", true, null, "Do you want to manually toggle the crosshair? (true/false)", false, false, null, null);
			Core.toggleKey = Core.category.CreateEntry<KeyCode>("ToggleKey", KeyCode.Y, null, "Crosshair toggle hotkey:", false, false, null, null);
			this.showCrosshair = false;
		}

		public override void OnUpdate()
		{
			bool flag = Core.enableToggle.Value && Input.GetKeyDown(Core.toggleKey.Value);
			if (flag)
			{
				this.showCrosshair = !this.showCrosshair;
			}
		}

		public override void OnGUI()
		{
			if (Core.enableToggle.Value && this.showCrosshair)
			{
				float screenWidth = (float)(Screen.width / 2);
				float screenHeight = (float)(Screen.height / 2);
				float lineLength = 10f;
				float lineThickness = 1f;

				GUI.DrawTexture(new Rect(screenWidth - lineLength / 2f, screenHeight - lineThickness, lineLength, 2f), Texture2D.whiteTexture);
				GUI.DrawTexture(new Rect(screenWidth - lineThickness, screenHeight - lineLength / 2f, 2f, lineLength), Texture2D.whiteTexture);
			}
		}

		private static void Log(string msg)
		{
			MelonLogger.Msg(msg);
		}

		private bool showCrosshair;

		public static MelonPreferences_Category category;

		public static MelonPreferences_Entry<bool> enableToggle;

		public static MelonPreferences_Entry<KeyCode> toggleKey;

	}
}