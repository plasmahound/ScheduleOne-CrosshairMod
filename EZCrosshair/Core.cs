using HarmonyLib;
using Il2CppScheduleOne.DevUtilities;
using Il2CppScheduleOne.PlayerScripts;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(EZCrosshair.Core), "EZCrosshair", "1.1.0", "plasmahound", null)]
[assembly: MelonGame("TVGS", "Schedule I")]
[assembly: MelonColor(255, 255, 130, 30)]
[assembly: MelonAuthorColor(200, 180, 130, 255)]

namespace EZCrosshair
{
	public class Core : MelonMod
	{
		/* List of ranged weapon IDs to compare with the hotbar item ID */
		private static readonly string[] crosshairIdList = { "revolver", "m1911", "ak47" };

		private const bool debugMode = false;

		private bool gameLoaded;

		private bool showCrosshair;

		public static MelonPreferences_Category category;

		public static MelonPreferences_Entry<bool> enableToggle;

		public static MelonPreferences_Entry<KeyCode> toggleKey;

		public override void OnInitializeMelon()
		{
			LoggerInstance.Msg("Aimed & ready to fire!");
			Core.category = MelonPreferences.CreateCategory("EZCrosshair", "EZ Crosshair");
			Core.enableToggle = Core.category.CreateEntry<bool>("EnableToggle", false, null, "Enable manual toggle? (NEW!) `false` for AUTOMATIC crosshair visibility! (true/false)", false, false, null, null);
			Core.toggleKey = Core.category.CreateEntry<KeyCode>("ToggleKey", KeyCode.Y, null, "Manual toggle hotkey:", false, false, null, null);
		}

		public override void OnSceneWasInitialized(int buildIndex, string sceneName)
		{
			switch (sceneName)
			{
				case "Main":
					DebugLog("(Scene) \"Main\" Initialized!");
					break;

				case "Menu":
					DebugLog("(Scene) \"Menu\" Initialized!");
					break;
			}
		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			switch (sceneName)
			{
				case "Main":
					DebugLog("(Scene) \"Main\" Loaded!");
					this.gameLoaded = true;
					break;
			}
		}

		public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
		{
			switch (sceneName)
			{
				case "Main":
					DebugLog("(Scene) \"Main\" Unloaded!");
					this.gameLoaded = false;
					break;
			}
		}

		public override void OnUpdate()
		{
			PlayerInventory playerInv = PlayerSingleton<PlayerInventory>.Instance;

			if (this.gameLoaded && Core.enableToggle.Value && Input.GetKeyDown(Core.toggleKey.Value))
			{
				DebugLog(String.Format("[Toggle: {0}]", !this.showCrosshair ? "ON" : "OFF"));

				this.showCrosshair = !this.showCrosshair;
			}
			else if (this.gameLoaded && !Core.enableToggle.Value && playerInv != null)
			{
				/* Only true if a hotbar slot is selected AND that slot has an item in it */
				if (playerInv.isAnythingEquipped)
				{
					DebugLog(String.Format("\nSlot[{0}]:", playerInv.EquippedSlotIndex));  // hotbar slot index

					if (playerInv.equippedSlot.ItemInstance != null)
					{
						DebugLog(String.Format("equippedSlot.ItemInstance.Name: {0}  (ID: {1})", playerInv.equippedSlot.ItemInstance.Name, playerInv.equippedSlot.ItemInstance.ID));

						/* If crosshair is not already visible & the item ID in the hotbar matches a listed ID, then enable the crosshair! */
						if (!this.showCrosshair && crosshairIdList.Contains(playerInv.equippedSlot.ItemInstance.ID))
						{
							DebugLog("[Toggle: ON]");
							this.showCrosshair = true;
						}
						/* Otherwise, if the crosshair is visible & the equipped ID doesn't match, then disable the crosshair! */
						else if (this.showCrosshair && !crosshairIdList.Contains(playerInv.equippedSlot.ItemInstance.ID))
						{
							DebugLog("[Toggle: OFF]");
							this.showCrosshair = false;
						}
					}
				}
				/* If an item is not selected in the hotbar & the crosshair was previously enabled, then disable the crosshair! */
				else if (this.showCrosshair)
				{
					this.showCrosshair = false;
				}
			}
		}

		public override void OnGUI()
		{
			if (this.gameLoaded && this.showCrosshair)
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
	}
}