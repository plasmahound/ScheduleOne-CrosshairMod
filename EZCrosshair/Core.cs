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
					this.crosshairIdList = convertStringListToArray(Core.crosshairIds.Value);
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
				float screenCenterX = Mathf.Round(Screen.width * 0.5f);
				float screenCenterY = Mathf.Round(Screen.height * 0.5f);

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

				//DrawCrosshairPlus(screenCenterX, screenCenterY, this.whiteTexture, lineLength, lineThickness, centerGap);
				DrawCrosshairDot(screenCenterX, screenCenterY, this.whiteDotTexture, 6f);
				//DrawCrosshairSquare(screenCenterX, screenCenterY, this.whiteTexture, 10f, 2f, 0f);
				//DrawCrosshairSquareWithDot(screenCenterX, screenCenterY, this.whiteTexture, this.whiteDotTexture, 10f, 2f, 1f, 2f);
			}
		}

		// EXAMPLE CROSSHAIR VALUES:
		// Dot:					DrawCrosshairDot(screenCenterX, screenCenterY, this.whiteDotTexture, 6f);
		// Plus:				DrawCrosshairPlus(screenCenterX, screenCenterY, this.whiteTexture, 8f, 2f, 0f);
		// Plus (gap):			DrawCrosshairPlus(screenCenterX, screenCenterY, this.whiteTexture, 8f, 2f, 3f);
		// Square:				DrawCrosshairSquare(screenCenterX, screenCenterY, this.whiteTexture, 10f, 2f, 0f);
		// Square (gap):		DrawCrosshairSquare(screenCenterX, screenCenterY, this.whiteTexture, 10f, 2f, 1f);
		// Plus-Dot:			DrawCrosshairPlusWithDot(screenCenterX, screenCenterY, this.whiteTexture, this.whiteDotTexture, 8f, 2f, 3f, 2f);
		// Square-Dot:			DrawCrosshairSquareWithDot(screenCenterX, screenCenterY, this.whiteTexture, this.whiteDotTexture, 10f, 2f, 0f, 2f);
		// Square-Dot (gap):	DrawCrosshairSquareWithDot(screenCenterX, screenCenterY, this.whiteTexture, this.whiteDotTexture, 10f, 2f, 1f, 2f);

		private void DrawCrosshairDot(float centerX, float centerY, Texture2D texture, float size)
		{
			GUI.DrawTexture(new Rect(centerX - size * 0.5f, centerY - size * 0.5f, size, size), texture);
		}

		private void DrawCrosshairPlus(float centerX, float centerY, Texture2D texture, float length, float thickness, float gap)
		{
			// Horizontal line - LEFT of center
			GUI.DrawTexture(new Rect(centerX - gap - length, centerY - thickness * 0.5f, length, thickness), texture);
			// Horizontal line - RIGHT of center
			GUI.DrawTexture(new Rect(centerX + gap, centerY - thickness * 0.5f, length, thickness), texture);
			// Vertical line - ABOVE center
			GUI.DrawTexture(new Rect(centerX - thickness * 0.5f, centerY - gap - length, thickness, length), texture);
			// Vertical line - BELOW center
			GUI.DrawTexture(new Rect(centerX - thickness * 0.5f, centerY + gap, thickness, length), texture);
		}

		private void DrawCrosshairSquare(float centerX, float centerY, Texture2D texture, float size, float thickness, float gap)
		{
			// The distance from the screen center to any edge of the square
			float halfLength = size * 0.5f;
			// Clamp the gap
			gap = Mathf.Clamp(gap, 0f, halfLength - thickness);

			// (N) TOP edge - horizontal line
			GUI.DrawTexture(new Rect(centerX - halfLength, centerY - halfLength, size, thickness), texture);
			// (S) BOTTOM edge - horizontal line
			GUI.DrawTexture(new Rect(centerX - halfLength, centerY + halfLength - thickness, size, thickness), texture);

			float verticalHeight = halfLength - gap;

			// (NW) LEFT edge - vertical line (TOP half)
			GUI.DrawTexture(new Rect(centerX - halfLength, centerY - halfLength, thickness, verticalHeight), texture);
			// (SW) LEFT edge - vertical line (BOTTOM half)
			GUI.DrawTexture(new Rect(centerX - halfLength, centerY + gap, thickness, verticalHeight), texture);

			// (NE) RIGHT edge - vertical line (TOP half)
			GUI.DrawTexture(new Rect(centerX + halfLength - thickness, centerY - halfLength, thickness, verticalHeight), texture);
			// (SE) RIGHT edge - vertical line (BOTTOM half)
			GUI.DrawTexture(new Rect(centerX + halfLength - thickness, centerY + gap, thickness, verticalHeight), texture);
		}

		private void DrawCrosshairPlusWithDot(float centerX, float centerY, Texture2D texturePlus, Texture2D textureDot, float length, float thickness, float gap, float dotSize)
		{
			DrawCrosshairPlus(centerX, centerY, texturePlus, length, thickness, gap);
			DrawCrosshairDot(centerX, centerY, textureDot, dotSize);
		}

		private void DrawCrosshairSquareWithDot(float centerX, float centerY, Texture2D textureSquare, Texture2D textureDot, float size, float thickness, float gap, float dotSize)
		{
			DrawCrosshairSquare(centerX, centerY, textureSquare, size, thickness, gap);
			DrawCrosshairDot(centerX, centerY, textureDot, dotSize);
		}

		private Texture2D CreateCircleTexture(int size, Color color)
		{
			Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
			texture.filterMode = FilterMode.Bilinear;
			texture.wrapMode = TextureWrapMode.Clamp;

			float radius = size * 0.5f;
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

		private Texture2D CreateOutlinedCircleTexture(int size, float borderThickness, Color color, Color outline, float edgeSoftness = 1.25f)
		{
			Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
			texture.filterMode = FilterMode.Bilinear;
			texture.wrapMode = TextureWrapMode.Clamp;

			float radius = size * 0.5f;
			float innerRadius = radius - borderThickness;
			Vector2 center = new Vector2(radius, radius);

			// Outer Transparency -> Soft Outer Edge -> Border Color -> Fill Color
			for (int y = 0; y < size; y++)
			{
				for (int x = 0; x < size; x++)
				{
					float distance = Vector2.Distance(new Vector2(x + 0.5f, y + 0.5f), center);

					// Outer Transparency - Draw nothing outside of the circle
					if (distance > radius + edgeSoftness)
					{
						texture.SetPixel(x, y, Color.clear);
						continue;
					}

					// Soft Outer Edge - Alpha fading ANTI-ALIASING between outside the circle & the border outline
					if (distance > radius)
					{
						float alpha = Mathf.Clamp01((radius + edgeSoftness - distance) / edgeSoftness);
						texture.SetPixel(x, y, new Color(0f, 0f, 0f, alpha));
						continue;
					}

					// Border Color - Solid border ring with alpha-blended inner edge
					if (distance >= innerRadius)
					{
						// Anti-alias inner border edge
						float innerAlpha = Mathf.Clamp01((distance - innerRadius) / edgeSoftness);
						Color c = Color.Lerp(color, outline, innerAlpha);
						texture.SetPixel(x, y, c);
						continue;
					}

					// Fill Color - Opaque inner fill of the given color
					texture.SetPixel(x, y, color);
				}
			}

			texture.Apply();
			return texture;
		}

		private string[] convertStringListToArray(string idList)
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