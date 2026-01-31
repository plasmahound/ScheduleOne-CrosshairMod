using HarmonyLib;
using Il2CppScheduleOne.DevUtilities;
using Il2CppScheduleOne.PlayerScripts;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(EZCrosshair.Core), "EZCrosshair", "1.1.3", "plasmahound", null)]
[assembly: MelonGame("TVGS", "Schedule I")]
[assembly: MelonColor(255, 255, 130, 30)]
[assembly: MelonAuthorColor(200, 180, 130, 255)]

namespace EZCrosshair
{
	public class Core : MelonMod
	{
		/* List of ranged weapon IDs to compare with the hotbar item ID */
		private static readonly string[] defaultIdList = { "revolver", "m1911", "pumpshotgun", "ak47", "minigun" };

		private const bool debugMode = false;

		private string[] crosshairIdList = defaultIdList;

		private bool gameLoaded;

		private bool showCrosshair;

		private Texture2D whiteTexture;
		private Texture2D whiteDotTexture;
		private Texture2D redDotTexture;

		public static MelonPreferences_Category category;

		public static MelonPreferences_Entry<string> crosshairMode;

		public static MelonPreferences_Entry<KeyCode> toggleKey;

		public static MelonPreferences_Entry<string> crosshairIds;

		public static MelonPreferences_Entry<int> crosshairLength;

		public static MelonPreferences_Entry<int> crosshairWidth;

		public override void OnInitializeMelon()
		{
			LoggerInstance.Msg("Aimed & ready to fire!");
			Core.category = MelonPreferences.CreateCategory("EZCrosshair", "EZ Crosshair");
			Core.crosshairMode = Core.category.CreateEntry<string>("CrosshairMode", "auto", null, "Automatic vs. Manual Toggle (\"auto\" / \"manual\")", false, false, null, null);
			Core.crosshairIds = Core.category.CreateEntry<string>("CrosshairIDs", string.Join(", ", defaultIdList), null, "Item IDs (separated by comma) for \"auto\" crosshair", false, false, null, null);
			Core.toggleKey = Core.category.CreateEntry<KeyCode>("ToggleKey", KeyCode.Y, null, "Toggle hotkey for \"manual\" crosshair (NOTE: This does *nothing* when CrosshairMode = \"auto\"!)", false, false, null, null);
			Core.crosshairLength = Core.category.CreateEntry<int>("CrosshairSize", 8, null, "Crosshair line length (DEFAULT: 10)", false, false, null, null);
			Core.crosshairWidth = Core.category.CreateEntry<int>("CrosshairThickness", 2, null, "Crosshair line thickness (DEFAULT: 2)", false, false, null, null);
		}

		public override void OnSceneWasInitialized(int buildIndex, string sceneName)
		{
			switch (sceneName)
			{
				case "Main":
					DebugLog("(Scene) \"Main\" Initialized!");
					this.crosshairIdList = convertIdsToArray(Core.crosshairIds.Value);
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

			if (this.gameLoaded)
			{
				if (Core.crosshairMode.Value.ToLower().Contains("man") && Input.GetKeyDown(Core.toggleKey.Value))
				{
					DebugLog(String.Format("[Toggle: {0}]", !this.showCrosshair ? "ON" : "OFF"));

					this.showCrosshair = !this.showCrosshair;
				}
				else if (Core.crosshairMode.Value.ToLower().Contains("aut") && playerInv != null)
				{
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
		}

		public override void OnGUI()
		{
			if (this.gameLoaded && this.showCrosshair)
			{
				float screenCenterX = (float)(Screen.width / 2);
				float screenCenterY = (float)(Screen.height / 2);

				float lineLength = (float)Core.crosshairLength.Value; // 8f
				float lineThickness = (float)Core.crosshairWidth.Value; // 2f
				float centerGap = 0f;

				if (this.whiteTexture == null)
				{
					this.whiteTexture = Texture2D.whiteTexture;
				}
				if (this.whiteDotTexture == null)
				{
					this.whiteDotTexture = this.CreateOutlinedCircleTexture(16, 2f, Color.white, Color.black);
			}
				if (this.redDotTexture == null)
				{
					this.redDotTexture = this.CreateOutlinedCircleTexture(16, 2f, Color.red, Color.black);
		}

				DrawCrosshairPlus(screenCenterX, screenCenterY, this.whiteTexture, lineLength, lineThickness, centerGap);
				//DrawCrosshairDot(screenCenterX, screenCenterY, this.whiteDotTexture, 6f);
			}
		}

		// EXAMPLE CROSSHAIR VALUES:
		// Dot:			DrawCrosshairDot(screenCenterX, screenCenterY, this.whiteDotTexture, 6f);
		// Plus:		DrawCrosshairPlus(screenCenterX, screenCenterY, this.whiteTexture, 8f, 2f, 0f);
		// Plus (gap):	DrawCrosshairPlus(screenCenterX, screenCenterY, this.whiteTexture, 8f, 2f, 3f);
		// Plus-Dot:	DrawCrosshairPlusWithDot(screenCenterX, screenCenterY, this.whiteTexture, this.whiteDotTexture, 8f, 2f, 3f, 2f);

		private void DrawCrosshairDot(float centerX, float centerY, Texture2D texture, float size)
		{
			GUI.DrawTexture(new Rect(centerX - size / 2f, centerY - size / 2f, size, size), texture);
		}

		private void DrawCrosshairPlus(float centerX, float centerY, Texture2D texture, float length, float thickness, float gap)
		{
			// Horizontal line - LEFT of center
			GUI.DrawTexture(new Rect(centerX - gap - length, centerY - thickness / 2f, length, thickness), texture);
			// Horizontal line - RIGHT of center
			GUI.DrawTexture(new Rect(centerX + gap, centerY - thickness / 2f, length, thickness), texture);
			// Vertical line - ABOVE center
			GUI.DrawTexture(new Rect(centerX - thickness / 2f, centerY - gap - length, thickness, length), texture);
			// Vertical line - BELOW center
			GUI.DrawTexture(new Rect(centerX - thickness / 2f, centerY + gap, thickness, length), texture);
		}

		private void DrawCrosshairSquare(float centerX, float centerY, Texture2D texture, float size, float thickness, float gap)
		{
			float halfLength = size / 2f; // The distance from the screen center to any edge of the square
			// (-) TOP edge
			GUI.DrawTexture(new Rect(centerX - halfLength, centerY - halfLength, size, thickness), texture);
			// (-) BOTTOM edge
			GUI.DrawTexture(new Rect(centerX - halfLength, centerY + halfLength - thickness, size, thickness), texture);
			// (|) LEFT edge
			GUI.DrawTexture(new Rect(centerX - halfLength, centerY - halfLength, thickness, halfLength - gap), texture);
			// (|) RIGHT edge
			GUI.DrawTexture(new Rect(centerX + halfLength - thickness, centerY - halfLength, thickness, halfLength - gap), texture);
		}

		private void DrawCrosshairPlusWithDot(float centerX, float centerY, Texture2D texturePlus, Texture2D textureDot, float length, float thickness, float gap, float dotSize)
		{
			DrawCrosshairPlus(centerX, centerY, texturePlus, length, thickness, gap);
			DrawCrosshairDot(centerX, centerY, textureDot, dotSize);
		}

		private void DrawCrosshairSquareWithDot(float centerX, float centerY, Texture2D texture, float size, float thickness, float gap, float dotSize)
		{
			DrawCrosshairSquare(centerX, centerY, texture, size, thickness, gap);
			DrawCrosshairDot(centerX, centerY, texture, dotSize);
		}

		private Texture2D CreateCircleTexture(int size, Color color)
		{
			Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
			texture.filterMode = FilterMode.Bilinear;
			texture.wrapMode = TextureWrapMode.Clamp;

			float radius = size / 2f;
			Vector2 center = new Vector2(radius, radius);

			for (int y = 0; y < size; y++)
			{
				for (int x = 0; x < size; x++)
				{
					float distance = Vector2.Distance(new Vector2(x, y), center);

					// If inside dot's radius, draw a colored pixel
					if (distance <= radius)
					{
						texture.SetPixel(x, y, color);
					}
					// If outside the dot's radius, draw exterior transparency to prevent artifacting
					else
					{
						texture.SetPixel(x, y, Color.clear);
					}
				}
			}

			texture.Apply();
			return texture;
		}

		private Texture2D CreateOutlinedCircleTexture(int size, float borderThickness, Color color, Color outline)
		{
			Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
			texture.filterMode= FilterMode.Bilinear;
			texture.wrapMode= TextureWrapMode.Clamp;

			float radius = size / 2f;
			float innerRadius = radius - borderThickness;
			Vector2 center = new Vector2(radius, radius);

			for (int y = 0; y < size; y++)
			{
				for (int x = 0; x < size; x++)
				{
					float distance = Vector2.Distance(new Vector2(x, y), center);

					// If inside the dot's radius, draw a colored pixel
					if (distance <= radius)
					{
						// If outside the inner radius, draw the border outline color
						if (distance >= innerRadius)
						{
							texture.SetPixel(x, y, outline);
						}
						// Otherwise, draw the interior dot color
						else
						{
							// Inside circle
							texture.SetPixel(x, y, color);
						}
					}
					// Otherwise, draw exterior transparency to prevent square artifacting
					else
					{
						texture.SetPixel(x, y, Color.clear);
					}
				}
			}

			texture.Apply();
			return texture;
		}

		private string[] convertIdsToArray(string idList)
		{
			// Remove all whitespace characters & split each array item by comma
			return string.Concat(idList.ToString().Where(c => !char.IsWhiteSpace(c))).Split(",");
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