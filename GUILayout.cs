using System;

namespace UnityEngine
{
	// Token: 0x02000067 RID: 103
	public sealed class GUILayout
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		public static void Label(string text, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(text), GUI.skin.label, options);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000C100 File Offset: 0x0000A300
		private static void DoLabel(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Label(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000C114 File Offset: 0x0000A314
		public static bool Button(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000C12C File Offset: 0x0000A32C
		private static bool DoButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Button(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000C140 File Offset: 0x0000A340
		public static string TextField(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, false, GUI.skin.textField, options);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000C158 File Offset: 0x0000A358
		public static string TextField(string text, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, false, GUI.skin.textField, options);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000C170 File Offset: 0x0000A370
		private static string DoTextField(string text, int maxLength, bool multiline, GUIStyle style, GUILayoutOption[] options)
		{
			int controlID = GUIUtility.GetControlID(FocusType.Keyboard);
			GUIContent guicontent = GUIContent.Temp(text);
			if (GUIUtility.keyboardControl != controlID)
			{
				guicontent = GUIContent.Temp(text);
			}
			else
			{
				guicontent = GUIContent.Temp(text + Input.compositionString);
			}
			Rect rect = GUILayoutUtility.GetRect(guicontent, style, options);
			if (GUIUtility.keyboardControl == controlID)
			{
				guicontent = GUIContent.Temp(text);
			}
			GUI.DoTextField(rect, controlID, guicontent, multiline, maxLength, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		public static float HorizontalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.DoHorizontalSlider(value, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, options);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000C204 File Offset: 0x0000A404
		private static float DoHorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUILayoutOption[] options)
		{
			return GUI.HorizontalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000C224 File Offset: 0x0000A424
		public static void Space(float pixels)
		{
			GUIUtility.CheckOnGUI();
			if (GUILayoutUtility.current.topLevel.isVertical)
			{
				GUILayoutUtility.GetRect(0f, pixels, GUILayoutUtility.spaceStyle, new GUILayoutOption[] { GUILayout.Height(pixels) });
			}
			else
			{
				GUILayoutUtility.GetRect(pixels, 0f, GUILayoutUtility.spaceStyle, new GUILayoutOption[] { GUILayout.Width(pixels) });
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000C290 File Offset: 0x0000A490
		public static void BeginHorizontal(params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		public static void BeginHorizontal(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = false;
			if (style != GUIStyle.none || content != GUIContent.none)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		public static void EndHorizontal()
		{
			GUILayoutUtility.EndGroup("GUILayout.EndHorizontal");
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000C304 File Offset: 0x0000A504
		public static void BeginVertical(params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000C318 File Offset: 0x0000A518
		public static void BeginVertical(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.none, style, options);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000C328 File Offset: 0x0000A528
		public static void BeginVertical(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = true;
			if (style != GUIStyle.none)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000C368 File Offset: 0x0000A568
		public static void EndVertical()
		{
			GUILayoutUtility.EndGroup("GUILayout.EndVertical");
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000C37C File Offset: 0x0000A57C
		public static void BeginArea(Rect screenRect)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000C390 File Offset: 0x0000A590
		public static void BeginArea(Rect screenRect, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutArea(style, typeof(GUILayoutGroup));
			if (Event.current.type == EventType.Layout)
			{
				guilayoutGroup.resetCoords = true;
				guilayoutGroup.minWidth = (guilayoutGroup.maxWidth = screenRect.width);
				guilayoutGroup.minHeight = (guilayoutGroup.maxHeight = screenRect.height);
				guilayoutGroup.rect = Rect.MinMaxRect(screenRect.xMin, screenRect.yMin, guilayoutGroup.rect.xMax, guilayoutGroup.rect.yMax);
			}
			GUI.BeginGroup(guilayoutGroup.rect, content, style);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000C434 File Offset: 0x0000A634
		public static void EndArea()
		{
			GUIUtility.CheckOnGUI();
			if (Event.current.type == EventType.Used)
			{
				return;
			}
			GUILayoutUtility.current.layoutGroups.Pop();
			GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
			GUI.EndGroup();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000C48C File Offset: 0x0000A68C
		public static GUILayoutOption Width(float width)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedWidth, width);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000C49C File Offset: 0x0000A69C
		public static GUILayoutOption MaxWidth(float maxWidth)
		{
			return new GUILayoutOption(GUILayoutOption.Type.maxWidth, maxWidth);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000C4AC File Offset: 0x0000A6AC
		public static GUILayoutOption Height(float height)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedHeight, height);
		}
	}
}
