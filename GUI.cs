using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000060 RID: 96
	public class GUI
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00009508 File Offset: 0x00007708
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x00009510 File Offset: 0x00007710
		internal static DateTime nextScrollStepTime { get; set; } = DateTime.Now;

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00009518 File Offset: 0x00007718
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x00009520 File Offset: 0x00007720
		internal static int scrollTroughSide { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00009550 File Offset: 0x00007750
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00009528 File Offset: 0x00007728
		public static GUISkin skin
		{
			get
			{
				GUIUtility.CheckOnGUI();
				return GUI.s_Skin;
			}
			set
			{
				GUIUtility.CheckOnGUI();
				if (!value)
				{
					value = GUIUtility.GetDefaultSkin();
				}
				GUI.s_Skin = value;
				value.MakeCurrent();
			}
		}

		// Token: 0x0600042A RID: 1066
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_color(out Color value);

		// Token: 0x0600042B RID: 1067
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_color(ref Color value);

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000955C File Offset: 0x0000775C
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x00009574 File Offset: 0x00007774
		public static Color color
		{
			get
			{
				Color color;
				GUI.INTERNAL_get_color(out color);
				return color;
			}
			set
			{
				GUI.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x0600042E RID: 1070
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_backgroundColor(out Color value);

		// Token: 0x0600042F RID: 1071
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_backgroundColor(ref Color value);

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00009580 File Offset: 0x00007780
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x00009598 File Offset: 0x00007798
		public static Color backgroundColor
		{
			get
			{
				Color color;
				GUI.INTERNAL_get_backgroundColor(out color);
				return color;
			}
			set
			{
				GUI.INTERNAL_set_backgroundColor(ref value);
			}
		}

		// Token: 0x06000432 RID: 1074
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_contentColor(out Color value);

		// Token: 0x06000433 RID: 1075
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_contentColor(ref Color value);

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x000095A4 File Offset: 0x000077A4
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x000095BC File Offset: 0x000077BC
		public static Color contentColor
		{
			get
			{
				Color color;
				GUI.INTERNAL_get_contentColor(out color);
				return color;
			}
			set
			{
				GUI.INTERNAL_set_contentColor(ref value);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000436 RID: 1078
		// (set) Token: 0x06000437 RID: 1079
		public static extern bool changed
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000438 RID: 1080
		// (set) Token: 0x06000439 RID: 1081
		public static extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x000095C8 File Offset: 0x000077C8
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x000095D0 File Offset: 0x000077D0
		public static Matrix4x4 matrix
		{
			get
			{
				return GUIClip.GetMatrix();
			}
			set
			{
				GUIClip.SetMatrix(value);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x000095D8 File Offset: 0x000077D8
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x000095F8 File Offset: 0x000077F8
		public static string tooltip
		{
			get
			{
				string text = GUI.Internal_GetTooltip();
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				GUI.Internal_SetTooltip(value);
			}
		}

		// Token: 0x0600043E RID: 1086
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern string Internal_GetTooltip();

		// Token: 0x0600043F RID: 1087
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetTooltip(string value);

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00009600 File Offset: 0x00007800
		protected static string mouseTooltip
		{
			get
			{
				return GUI.Internal_GetMouseTooltip();
			}
		}

		// Token: 0x06000441 RID: 1089
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern string Internal_GetMouseTooltip();

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00009608 File Offset: 0x00007808
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x00009610 File Offset: 0x00007810
		protected static Rect tooltipRect
		{
			get
			{
				return GUI.s_ToolTipRect;
			}
			set
			{
				GUI.s_ToolTipRect = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000444 RID: 1092
		// (set) Token: 0x06000445 RID: 1093
		public static extern int depth
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00009618 File Offset: 0x00007818
		public static void Label(Rect position, string text)
		{
			GUI.Label(position, GUIContent.Temp(text), GUI.s_Skin.label);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00009630 File Offset: 0x00007830
		public static void Label(Rect position, Texture image)
		{
			GUI.Label(position, GUIContent.Temp(image), GUI.s_Skin.label);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00009648 File Offset: 0x00007848
		public static void Label(Rect position, GUIContent content)
		{
			GUI.Label(position, content, GUI.s_Skin.label);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000965C File Offset: 0x0000785C
		public static void Label(Rect position, string text, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(text), style);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000966C File Offset: 0x0000786C
		public static void Label(Rect position, Texture image, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(image), style);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000967C File Offset: 0x0000787C
		public static void Label(Rect position, GUIContent content, GUIStyle style)
		{
			GUI.DoLabel(position, content, style.m_Ptr);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000968C File Offset: 0x0000788C
		private static void DoLabel(Rect position, GUIContent content, IntPtr style)
		{
			GUI.INTERNAL_CALL_DoLabel(ref position, content, style);
		}

		// Token: 0x0600044D RID: 1101
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DoLabel(ref Rect position, GUIContent content, IntPtr style);

		// Token: 0x0600044E RID: 1102
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void InitializeGUIClipTexture();

		// Token: 0x0600044F RID: 1103 RVA: 0x00009698 File Offset: 0x00007898
		[ExcludeFromDocs]
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend)
		{
			float num = 0f;
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, num);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000096B8 File Offset: 0x000078B8
		[ExcludeFromDocs]
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode)
		{
			float num = 0f;
			bool flag = true;
			GUI.DrawTexture(position, image, scaleMode, flag, num);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000096D8 File Offset: 0x000078D8
		[ExcludeFromDocs]
		public static void DrawTexture(Rect position, Texture image)
		{
			float num = 0f;
			bool flag = true;
			ScaleMode scaleMode = ScaleMode.StretchToFill;
			GUI.DrawTexture(position, image, scaleMode, flag, num);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000096FC File Offset: 0x000078FC
		public static void DrawTexture(Rect position, Texture image, [DefaultValue("ScaleMode.StretchToFill")] ScaleMode scaleMode, [DefaultValue("true")] bool alphaBlend, [DefaultValue("0")] float imageAspect)
		{
			if (Event.current.type == EventType.Repaint)
			{
				if (image == null)
				{
					Debug.LogWarning("null texture passed to GUI.DrawTexture");
					return;
				}
				if (imageAspect == 0f)
				{
					imageAspect = (float)image.width / (float)image.height;
				}
				Material material = ((!alphaBlend) ? GUI.blitMaterial : GUI.blendMaterial);
				float num = position.width / position.height;
				InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
				internalDrawTextureArguments.texture = image;
				internalDrawTextureArguments.leftBorder = 0;
				internalDrawTextureArguments.rightBorder = 0;
				internalDrawTextureArguments.topBorder = 0;
				internalDrawTextureArguments.bottomBorder = 0;
				internalDrawTextureArguments.color = GUI.color;
				internalDrawTextureArguments.mat = material;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
					internalDrawTextureArguments.screenRect = position;
					internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
					Graphics.DrawTexture(ref internalDrawTextureArguments);
					break;
				case ScaleMode.ScaleAndCrop:
					if (num > imageAspect)
					{
						float num2 = imageAspect / num;
						internalDrawTextureArguments.screenRect = position;
						internalDrawTextureArguments.sourceRect = new Rect(0f, (1f - num2) * 0.5f, 1f, num2);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					else
					{
						float num3 = num / imageAspect;
						internalDrawTextureArguments.screenRect = position;
						internalDrawTextureArguments.sourceRect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					break;
				case ScaleMode.ScaleToFit:
					if (num > imageAspect)
					{
						float num4 = imageAspect / num;
						internalDrawTextureArguments.screenRect = new Rect(position.xMin + position.width * (1f - num4) * 0.5f, position.yMin, num4 * position.width, position.height);
						internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					else
					{
						float num5 = num / imageAspect;
						internalDrawTextureArguments.screenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num5) * 0.5f, position.width, num5 * position.height);
						internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					break;
				}
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00009980 File Offset: 0x00007B80
		internal static bool CalculateScaledTextureRects(Rect position, ScaleMode scaleMode, float imageAspect, ref Rect outScreenRect, ref Rect outSourceRect)
		{
			float num = position.width / position.height;
			bool flag = false;
			switch (scaleMode)
			{
			case ScaleMode.StretchToFill:
				outScreenRect = position;
				outSourceRect = new Rect(0f, 0f, 1f, 1f);
				flag = true;
				break;
			case ScaleMode.ScaleAndCrop:
				if (num > imageAspect)
				{
					float num2 = imageAspect / num;
					outScreenRect = position;
					outSourceRect = new Rect(0f, (1f - num2) * 0.5f, 1f, num2);
					flag = true;
				}
				else
				{
					float num3 = num / imageAspect;
					outScreenRect = position;
					outSourceRect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
					flag = true;
				}
				break;
			case ScaleMode.ScaleToFit:
				if (num > imageAspect)
				{
					float num4 = imageAspect / num;
					outScreenRect = new Rect(position.xMin + position.width * (1f - num4) * 0.5f, position.yMin, num4 * position.width, position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					flag = true;
				}
				else
				{
					float num5 = num / imageAspect;
					outScreenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num5) * 0.5f, position.width, num5 * position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					flag = true;
				}
				break;
			}
			return flag;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00009B1C File Offset: 0x00007D1C
		[ExcludeFromDocs]
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords)
		{
			bool flag = true;
			GUI.DrawTextureWithTexCoords(position, image, texCoords, flag);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00009B34 File Offset: 0x00007D34
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords, [DefaultValue("true")] bool alphaBlend)
		{
			if (Event.current.type == EventType.Repaint)
			{
				Material material = ((!alphaBlend) ? GUI.blitMaterial : GUI.blendMaterial);
				InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
				internalDrawTextureArguments.texture = image;
				internalDrawTextureArguments.leftBorder = 0;
				internalDrawTextureArguments.rightBorder = 0;
				internalDrawTextureArguments.topBorder = 0;
				internalDrawTextureArguments.bottomBorder = 0;
				internalDrawTextureArguments.color = GUI.color;
				internalDrawTextureArguments.mat = material;
				internalDrawTextureArguments.screenRect = position;
				internalDrawTextureArguments.sourceRect = texCoords;
				Graphics.DrawTexture(ref internalDrawTextureArguments);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000456 RID: 1110
		private static extern Material blendMaterial
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000457 RID: 1111
		private static extern Material blitMaterial
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public static void Box(Rect position, string text)
		{
			GUI.Box(position, GUIContent.Temp(text), GUI.s_Skin.box);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00009BE0 File Offset: 0x00007DE0
		public static void Box(Rect position, Texture image)
		{
			GUI.Box(position, GUIContent.Temp(image), GUI.s_Skin.box);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00009BF8 File Offset: 0x00007DF8
		public static void Box(Rect position, GUIContent content)
		{
			GUI.Box(position, content, GUI.s_Skin.box);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00009C0C File Offset: 0x00007E0C
		public static void Box(Rect position, string text, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(text), style);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00009C1C File Offset: 0x00007E1C
		public static void Box(Rect position, Texture image, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(image), style);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00009C2C File Offset: 0x00007E2C
		public static void Box(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.boxHash, FocusType.Passive);
			if (Event.current.type == EventType.Repaint)
			{
				style.Draw(position, content, controlID);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00009C64 File Offset: 0x00007E64
		public static bool Button(Rect position, string text)
		{
			return GUI.DoButton(position, GUIContent.Temp(text), GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00009C84 File Offset: 0x00007E84
		public static bool Button(Rect position, Texture image)
		{
			return GUI.DoButton(position, GUIContent.Temp(image), GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00009CA4 File Offset: 0x00007EA4
		public static bool Button(Rect position, GUIContent content)
		{
			return GUI.DoButton(position, content, GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00009CBC File Offset: 0x00007EBC
		public static bool Button(Rect position, string text, GUIStyle style)
		{
			return GUI.DoButton(position, GUIContent.Temp(text), style.m_Ptr);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00009CD0 File Offset: 0x00007ED0
		public static bool Button(Rect position, Texture image, GUIStyle style)
		{
			return GUI.DoButton(position, GUIContent.Temp(image), style.m_Ptr);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00009CE4 File Offset: 0x00007EE4
		public static bool Button(Rect position, GUIContent content, GUIStyle style)
		{
			return GUI.DoButton(position, content, style.m_Ptr);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00009CF4 File Offset: 0x00007EF4
		private static bool DoButton(Rect position, GUIContent content, IntPtr style)
		{
			return GUI.INTERNAL_CALL_DoButton(ref position, content, style);
		}

		// Token: 0x06000465 RID: 1125
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_DoButton(ref Rect position, GUIContent content, IntPtr style);

		// Token: 0x06000466 RID: 1126 RVA: 0x00009D00 File Offset: 0x00007F00
		public static bool RepeatButton(Rect position, string text)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00009D1C File Offset: 0x00007F1C
		public static bool RepeatButton(Rect position, Texture image)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00009D38 File Offset: 0x00007F38
		public static bool RepeatButton(Rect position, GUIContent content)
		{
			return GUI.DoRepeatButton(position, content, GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00009D4C File Offset: 0x00007F4C
		public static bool RepeatButton(Rect position, string text, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), style, FocusType.Native);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00009D5C File Offset: 0x00007F5C
		public static bool RepeatButton(Rect position, Texture image, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), style, FocusType.Native);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00009D6C File Offset: 0x00007F6C
		public static bool RepeatButton(Rect position, GUIContent content, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, content, style, FocusType.Native);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00009D78 File Offset: 0x00007F78
		private static bool DoRepeatButton(Rect position, GUIContent content, GUIStyle style, FocusType focusType)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.repeatButtonHash, focusType, position);
			EventType typeForControl = Event.current.GetTypeForControl(controlID);
			if (typeForControl == EventType.MouseDown)
			{
				if (position.Contains(Event.current.mousePosition))
				{
					GUIUtility.hotControl = controlID;
					Event.current.Use();
				}
				return false;
			}
			if (typeForControl != EventType.MouseUp)
			{
				if (typeForControl != EventType.Repaint)
				{
					return false;
				}
				style.Draw(position, content, controlID);
				return controlID == GUIUtility.hotControl && position.Contains(Event.current.mousePosition);
			}
			else
			{
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					return position.Contains(Event.current.mousePosition);
				}
				return false;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00009E40 File Offset: 0x00008040
		public static string TextField(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, GUI.skin.textField, null, '\0');
			return guicontent.text;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00009E78 File Offset: 0x00008078
		public static string TextField(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, GUI.skin.textField, null, '\0');
			return guicontent.text;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00009EB0 File Offset: 0x000080B0
		public static string TextField(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00009EE0 File Offset: 0x000080E0
		public static string TextField(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, maxLength, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00009F10 File Offset: 0x00008110
		public static string PasswordField(Rect position, string password, char maskChar)
		{
			return GUI.PasswordField(position, password, maskChar, -1, GUI.skin.textField);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00009F28 File Offset: 0x00008128
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength)
		{
			return GUI.PasswordField(position, password, maskChar, maxLength, GUI.skin.textField);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00009F40 File Offset: 0x00008140
		public static string PasswordField(Rect position, string password, char maskChar, GUIStyle style)
		{
			return GUI.PasswordField(position, password, maskChar, -1, style);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00009F4C File Offset: 0x0000814C
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength, GUIStyle style)
		{
			string text = GUI.PasswordFieldGetStrToShow(password, maskChar);
			GUIContent guicontent = GUIContent.Temp(text);
			bool changed = GUI.changed;
			GUI.changed = false;
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard), guicontent, false, maxLength, style, password, maskChar);
			text = ((!GUI.changed) ? password : guicontent.text);
			GUI.changed = GUI.changed || changed;
			return text;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00009FAC File Offset: 0x000081AC
		internal static string PasswordFieldGetStrToShow(string password, char maskChar)
		{
			return (Event.current.type != EventType.Repaint && Event.current.type != EventType.MouseDown) ? password : string.Empty.PadRight(password.Length, maskChar);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00009FE4 File Offset: 0x000081E4
		public static string TextArea(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, GUI.skin.textArea, null, '\0');
			return guicontent.text;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000A01C File Offset: 0x0000821C
		public static string TextArea(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, maxLength, GUI.skin.textArea, null, '\0');
			return guicontent.text;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000A054 File Offset: 0x00008254
		public static string TextArea(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000A084 File Offset: 0x00008284
		public static string TextArea(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000A0B4 File Offset: 0x000082B4
		private static string TextArea(Rect position, GUIContent content, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(content.text, content.image);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style, null, '\0');
			return guicontent.text;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000A0EC File Offset: 0x000082EC
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, string secureText = null, char maskChar = '\0')
		{
			if (maxLength >= 0 && content.text.Length > maxLength)
			{
				content.text = content.text.Substring(0, maxLength);
			}
			GUIUtility.CheckOnGUI();
			TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), id);
			textEditor.content.text = content.text;
			textEditor.SaveBackup();
			textEditor.position = position;
			textEditor.style = style;
			textEditor.multiline = multiline;
			textEditor.controlID = id;
			textEditor.ClampPos();
			Event current = Event.current;
			EventType type = current.type;
			if (type != EventType.MouseDown)
			{
				if (type == EventType.Repaint)
				{
					if (textEditor.keyboardOnScreen != null)
					{
						content.text = textEditor.keyboardOnScreen.text;
						if (maxLength >= 0 && content.text.Length > maxLength)
						{
							content.text = content.text.Substring(0, maxLength);
						}
						if (textEditor.keyboardOnScreen.done)
						{
							textEditor.keyboardOnScreen = null;
							GUI.changed = true;
						}
					}
					string text = content.text;
					if (secureText != null)
					{
						content.text = GUI.PasswordFieldGetStrToShow(text, maskChar);
					}
					style.Draw(position, content, id, false);
					content.text = text;
				}
			}
			else if (position.Contains(current.mousePosition))
			{
				GUIUtility.hotControl = id;
				if (GUI.hotTextField != -1 && GUI.hotTextField != id)
				{
					TextEditor textEditor2 = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUI.hotTextField);
					textEditor2.keyboardOnScreen = null;
				}
				GUI.hotTextField = id;
				if (GUIUtility.keyboardControl != id)
				{
					GUIUtility.keyboardControl = id;
				}
				textEditor.keyboardOnScreen = TouchScreenKeyboard.Open((secureText == null) ? content.text : secureText, TouchScreenKeyboardType.Default, true, multiline, secureText != null);
				current.Use();
			}
		}

		// Token: 0x0600047C RID: 1148
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetNextControlName(string name);

		// Token: 0x0600047D RID: 1149
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern string GetNameOfFocusedControl();

		// Token: 0x0600047E RID: 1150
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void FocusControl(string name);

		// Token: 0x0600047F RID: 1151 RVA: 0x0000A2D4 File Offset: 0x000084D4
		public static bool Toggle(Rect position, bool value, string text)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), GUI.s_Skin.toggle);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000A2F0 File Offset: 0x000084F0
		public static bool Toggle(Rect position, bool value, Texture image)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), GUI.s_Skin.toggle);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000A30C File Offset: 0x0000850C
		public static bool Toggle(Rect position, bool value, GUIContent content)
		{
			return GUI.Toggle(position, value, content, GUI.s_Skin.toggle);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000A320 File Offset: 0x00008520
		public static bool Toggle(Rect position, bool value, string text, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), style);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000A330 File Offset: 0x00008530
		public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), style);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000A340 File Offset: 0x00008540
		public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)
		{
			return GUI.DoToggle(position, GUIUtility.GetControlID(GUI.toggleHash, FocusType.Native, position), value, content, style.m_Ptr);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000A35C File Offset: 0x0000855C
		public static bool Toggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
		{
			return GUI.DoToggle(position, id, value, content, style.m_Ptr);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000A370 File Offset: 0x00008570
		internal static bool DoToggle(Rect position, int id, bool value, GUIContent content, IntPtr style)
		{
			return GUI.INTERNAL_CALL_DoToggle(ref position, id, value, content, style);
		}

		// Token: 0x06000487 RID: 1159
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_DoToggle(ref Rect position, int id, bool value, GUIContent content, IntPtr style);

		// Token: 0x06000488 RID: 1160 RVA: 0x0000A380 File Offset: 0x00008580
		public static int Toolbar(Rect position, int selected, string[] texts)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), GUI.s_Skin.button);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000A39C File Offset: 0x0000859C
		public static int Toolbar(Rect position, int selected, Texture[] images)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), GUI.s_Skin.button);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000A3B8 File Offset: 0x000085B8
		public static int Toolbar(Rect position, int selected, GUIContent[] content)
		{
			return GUI.Toolbar(position, selected, content, GUI.s_Skin.button);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000A3CC File Offset: 0x000085CC
		public static int Toolbar(Rect position, int selected, string[] texts, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), style);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000A3DC File Offset: 0x000085DC
		public static int Toolbar(Rect position, int selected, Texture[] images, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), style);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000A3EC File Offset: 0x000085EC
		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style)
		{
			GUIStyle guistyle;
			GUIStyle guistyle2;
			GUIStyle guistyle3;
			GUI.FindStyles(ref style, out guistyle, out guistyle2, out guistyle3, "left", "mid", "right");
			return GUI.DoButtonGrid(position, selected, contents, contents.Length, style, guistyle, guistyle2, guistyle3);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000A424 File Offset: 0x00008624
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, null);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000A438 File Offset: 0x00008638
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, null);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000A44C File Offset: 0x0000864C
		public static int SelectionGrid(Rect position, int selected, GUIContent[] content, int xCount)
		{
			return GUI.SelectionGrid(position, selected, content, xCount, null);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000A458 File Offset: 0x00008658
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, style);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000A46C File Offset: 0x0000866C
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, style);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000A480 File Offset: 0x00008680
		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style)
		{
			if (style == null)
			{
				style = GUI.s_Skin.button;
			}
			return GUI.DoButtonGrid(position, selected, contents, xCount, style, style, style, style);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000A4B4 File Offset: 0x000086B4
		internal static void FindStyles(ref GUIStyle style, out GUIStyle firstStyle, out GUIStyle midStyle, out GUIStyle lastStyle, string first, string mid, string last)
		{
			if (style == null)
			{
				style = GUI.skin.button;
			}
			string name = style.name;
			midStyle = GUI.skin.FindStyle(name + mid);
			if (midStyle == null)
			{
				midStyle = style;
			}
			firstStyle = GUI.skin.FindStyle(name + first);
			if (firstStyle == null)
			{
				firstStyle = midStyle;
			}
			lastStyle = GUI.skin.FindStyle(name + last);
			if (lastStyle == null)
			{
				lastStyle = midStyle;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000A53C File Offset: 0x0000873C
		internal static int CalcTotalHorizSpacing(int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			if (xCount < 2)
			{
				return 0;
			}
			if (xCount == 2)
			{
				return Mathf.Max(firstStyle.margin.right, lastStyle.margin.left);
			}
			int num = Mathf.Max(midStyle.margin.left, midStyle.margin.right);
			return Mathf.Max(firstStyle.margin.right, midStyle.margin.left) + Mathf.Max(midStyle.margin.right, lastStyle.margin.left) + num * (xCount - 3);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000A5D0 File Offset: 0x000087D0
		private static int DoButtonGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			GUIUtility.CheckOnGUI();
			int num = contents.Length;
			if (num == 0)
			{
				return selected;
			}
			if (xCount <= 0)
			{
				Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set xCount to a positive value.");
				return selected;
			}
			int controlID = GUIUtility.GetControlID(GUI.buttonGridHash, FocusType.Native, position);
			int num2 = num / xCount;
			if (num % xCount != 0)
			{
				num2++;
			}
			float num3 = (float)GUI.CalcTotalHorizSpacing(xCount, style, firstStyle, midStyle, lastStyle);
			float num4 = (float)(Mathf.Max(style.margin.top, style.margin.bottom) * (num2 - 1));
			float num5 = (position.width - num3) / (float)xCount;
			float num6 = (position.height - num4) / (float)num2;
			if (style.fixedWidth != 0f)
			{
				num5 = style.fixedWidth;
			}
			if (style.fixedHeight != 0f)
			{
				num6 = style.fixedHeight;
			}
			switch (Event.current.GetTypeForControl(controlID))
			{
			case EventType.MouseDown:
				if (position.Contains(Event.current.mousePosition))
				{
					Rect[] array = GUI.CalcMouseRects(position, num, xCount, num5, num6, style, firstStyle, midStyle, lastStyle, false);
					if (GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, true) != -1)
					{
						GUIUtility.hotControl = controlID;
						Event.current.Use();
					}
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					Rect[] array = GUI.CalcMouseRects(position, num, xCount, num5, num6, style, firstStyle, midStyle, lastStyle, false);
					int buttonGridMouseSelection = GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, true);
					GUI.changed = true;
					return buttonGridMouseSelection;
				}
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == controlID)
				{
					Event.current.Use();
				}
				break;
			case EventType.Repaint:
			{
				GUIStyle guistyle = null;
				GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
				position = new Rect(0f, 0f, position.width, position.height);
				Rect[] array = GUI.CalcMouseRects(position, num, xCount, num5, num6, style, firstStyle, midStyle, lastStyle, false);
				int buttonGridMouseSelection2 = GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, controlID == GUIUtility.hotControl);
				bool flag = position.Contains(Event.current.mousePosition);
				GUIUtility.mouseUsed = GUIUtility.mouseUsed || flag;
				for (int i = 0; i < num; i++)
				{
					GUIStyle guistyle2;
					if (i != 0)
					{
						guistyle2 = midStyle;
					}
					else
					{
						guistyle2 = firstStyle;
					}
					if (i == num - 1)
					{
						guistyle2 = lastStyle;
					}
					if (num == 1)
					{
						guistyle2 = style;
					}
					if (i != selected)
					{
						guistyle2.Draw(array[i], contents[i], i == buttonGridMouseSelection2 && (GUI.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl && GUI.enabled, false, false);
					}
					else
					{
						guistyle = guistyle2;
					}
				}
				if (selected < num && selected > -1)
				{
					guistyle.Draw(array[selected], contents[selected], selected == buttonGridMouseSelection2 && (GUI.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl, true, false);
				}
				if (buttonGridMouseSelection2 >= 0)
				{
					GUI.tooltip = contents[buttonGridMouseSelection2].tooltip;
				}
				GUIClip.Pop();
				break;
			}
			}
			return selected;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000A96C File Offset: 0x00008B6C
		private static Rect[] CalcMouseRects(Rect position, int count, int xCount, float elemWidth, float elemHeight, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, bool addBorders)
		{
			int num = 0;
			int num2 = 0;
			float num3 = position.xMin;
			float num4 = position.yMin;
			GUIStyle guistyle = style;
			Rect[] array = new Rect[count];
			if (count > 1)
			{
				guistyle = firstStyle;
			}
			for (int i = 0; i < count; i++)
			{
				if (!addBorders)
				{
					array[i] = new Rect(num3, num4, elemWidth, elemHeight);
				}
				else
				{
					array[i] = guistyle.margin.Add(new Rect(num3, num4, elemWidth, elemHeight));
				}
				array[i].width = Mathf.Round(array[i].xMax) - Mathf.Round(array[i].x);
				array[i].x = Mathf.Round(array[i].x);
				GUIStyle guistyle2 = midStyle;
				if (i == count - 2)
				{
					guistyle2 = lastStyle;
				}
				num3 += elemWidth + (float)Mathf.Max(guistyle.margin.right, guistyle2.margin.left);
				num2++;
				if (num2 >= xCount)
				{
					num++;
					num2 = 0;
					num4 += elemHeight + (float)Mathf.Max(style.margin.top, style.margin.bottom);
					num3 = position.xMin;
				}
			}
			return array;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000AACC File Offset: 0x00008CCC
		private static int GetButtonGridMouseSelection(Rect[] buttonRects, Vector2 mousePos, bool findNearest)
		{
			for (int i = 0; i < buttonRects.Length; i++)
			{
				if (buttonRects[i].Contains(mousePos))
				{
					return i;
				}
			}
			if (!findNearest)
			{
				return -1;
			}
			float num = 10000000f;
			int num2 = -1;
			for (int j = 0; j < buttonRects.Length; j++)
			{
				Rect rect = buttonRects[j];
				Vector2 vector = new Vector2(Mathf.Clamp(mousePos.x, rect.xMin, rect.xMax), Mathf.Clamp(mousePos.y, rect.yMin, rect.yMax));
				float sqrMagnitude = (mousePos - vector).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num2 = j;
					num = sqrMagnitude;
				}
			}
			return num2;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000AB90 File Offset: 0x00008D90
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, true, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, slider, thumb, true, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, false, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000AC34 File Offset: 0x00008E34
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, slider, thumb, false, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000AC60 File Offset: 0x00008E60
		public static float Slider(Rect position, float value, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id)
		{
			GUIUtility.CheckOnGUI();
			SliderHandler sliderHandler = new SliderHandler(position, value, size, start, end, slider, thumb, horiz, id);
			return sliderHandler.Handle();
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600049E RID: 1182
		internal static extern bool usePageScrollbars
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000AC90 File Offset: 0x00008E90
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, GUI.skin.horizontalScrollbarThumb, GUI.skin.horizontalScrollbarLeftButton, GUI.skin.horizontalScrollbarRightButton, true);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "leftbutton"), GUI.skin.GetStyle(style.name + "rightbutton"), true);
		}

		// Token: 0x060004A1 RID: 1185
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void InternalRepaintEditorWindow();

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000AD40 File Offset: 0x00008F40
		internal static bool ScrollerRepeatButton(int scrollerID, Rect rect, GUIStyle style)
		{
			bool flag = false;
			if (GUI.DoRepeatButton(rect, GUIContent.none, style, FocusType.Passive))
			{
				bool flag2 = GUI.scrollControlID != scrollerID;
				GUI.scrollControlID = scrollerID;
				if (flag2)
				{
					flag = true;
					GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(250.0);
				}
				else if (DateTime.Now >= GUI.nextScrollStepTime)
				{
					flag = true;
					GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(30.0);
				}
				if (Event.current.type == EventType.Repaint)
				{
					GUI.InternalRepaintEditorWindow();
				}
			}
			return flag;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000ADE4 File Offset: 0x00008FE4
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, GUI.skin.verticalScrollbarThumb, GUI.skin.verticalScrollbarUpButton, GUI.skin.verticalScrollbarDownButton, false);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000AE28 File Offset: 0x00009028
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "upbutton"), GUI.skin.GetStyle(style.name + "downbutton"), false);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000AE94 File Offset: 0x00009094
		private static float Scroller(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle leftButton, GUIStyle rightButton, bool horiz)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive, position);
			Rect rect;
			Rect rect2;
			Rect rect3;
			if (horiz)
			{
				rect = new Rect(position.x + leftButton.fixedWidth, position.y, position.width - leftButton.fixedWidth - rightButton.fixedWidth, position.height);
				rect2 = new Rect(position.x, position.y, leftButton.fixedWidth, position.height);
				rect3 = new Rect(position.xMax - rightButton.fixedWidth, position.y, rightButton.fixedWidth, position.height);
			}
			else
			{
				rect = new Rect(position.x, position.y + leftButton.fixedHeight, position.width, position.height - leftButton.fixedHeight - rightButton.fixedHeight);
				rect2 = new Rect(position.x, position.y, position.width, leftButton.fixedHeight);
				rect3 = new Rect(position.x, position.yMax - rightButton.fixedHeight, position.width, rightButton.fixedHeight);
			}
			value = GUI.Slider(rect, value, size, leftValue, rightValue, slider, thumb, horiz, controlID);
			bool flag = false;
			if (Event.current.type == EventType.MouseUp)
			{
				flag = true;
			}
			if (GUI.ScrollerRepeatButton(controlID, rect2, leftButton))
			{
				value -= GUI.scrollStepSize * ((leftValue >= rightValue) ? (-1f) : 1f);
			}
			if (GUI.ScrollerRepeatButton(controlID, rect3, rightButton))
			{
				value += GUI.scrollStepSize * ((leftValue >= rightValue) ? (-1f) : 1f);
			}
			if (flag && Event.current.type == EventType.Used)
			{
				GUI.scrollControlID = 0;
			}
			if (leftValue < rightValue)
			{
				value = Mathf.Clamp(value, leftValue, rightValue - size);
			}
			else
			{
				value = Mathf.Clamp(value, rightValue, leftValue - size);
			}
			return value;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000B0A0 File Offset: 0x000092A0
		public static void BeginGroup(Rect position)
		{
			GUI.BeginGroup(position, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public static void BeginGroup(Rect position, string text)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), GUIStyle.none);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000B0C8 File Offset: 0x000092C8
		public static void BeginGroup(Rect position, Texture image)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), GUIStyle.none);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000B0DC File Offset: 0x000092DC
		public static void BeginGroup(Rect position, GUIContent content)
		{
			GUI.BeginGroup(position, content, GUIStyle.none);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000B0EC File Offset: 0x000092EC
		public static void BeginGroup(Rect position, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.none, style);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000B0FC File Offset: 0x000092FC
		public static void BeginGroup(Rect position, string text, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), style);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000B10C File Offset: 0x0000930C
		public static void BeginGroup(Rect position, Texture image, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), style);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000B11C File Offset: 0x0000931C
		public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.beginGroupHash, FocusType.Passive);
			if (content != GUIContent.none || style != GUIStyle.none)
			{
				EventType type = Event.current.type;
				if (type != EventType.Repaint)
				{
					if (position.Contains(Event.current.mousePosition))
					{
						GUIUtility.mouseUsed = true;
					}
				}
				else
				{
					style.Draw(position, content, controlID);
				}
			}
			GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000B1A8 File Offset: 0x000093A8
		public static void EndGroup()
		{
			GUIClip.Pop();
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000B1E8 File Offset: 0x000093E8
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000B220 File Offset: 0x00009420
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000B244 File Offset: 0x00009444
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, null);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000B264 File Offset: 0x00009464
		protected static Vector2 DoBeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000B284 File Offset: 0x00009484
		internal static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.scrollviewHash, FocusType.Passive);
			GUI.ScrollViewState scrollViewState = (GUI.ScrollViewState)GUIUtility.GetStateObject(typeof(GUI.ScrollViewState), controlID);
			if (scrollViewState.apply)
			{
				scrollPosition = scrollViewState.scrollPosition;
				scrollViewState.apply = false;
			}
			scrollViewState.position = position;
			scrollViewState.scrollPosition = scrollPosition;
			scrollViewState.visibleRect = (scrollViewState.viewRect = viewRect);
			scrollViewState.visibleRect.width = position.width;
			scrollViewState.visibleRect.height = position.height;
			GUI.s_ScrollViewStates.Push(scrollViewState);
			Rect rect = new Rect(position);
			EventType type = Event.current.type;
			if (type != EventType.Layout)
			{
				if (type != EventType.Used)
				{
					bool flag = alwaysShowVertical;
					bool flag2 = alwaysShowHorizontal;
					if (flag2 || viewRect.width > rect.width)
					{
						scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						rect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						flag2 = true;
					}
					if (flag || viewRect.height > rect.height)
					{
						scrollViewState.visibleRect.width = position.width - verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						rect.width -= verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						flag = true;
						if (!flag2 && viewRect.width > rect.width)
						{
							scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							rect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							flag2 = true;
						}
					}
					if (Event.current.type == EventType.Repaint && background != GUIStyle.none)
					{
						background.Draw(position, position.Contains(Event.current.mousePosition), false, flag2 && flag, false);
					}
					if (flag2 && horizontalScrollbar != GUIStyle.none)
					{
						scrollPosition.x = GUI.HorizontalScrollbar(new Rect(position.x, position.yMax - horizontalScrollbar.fixedHeight, rect.width, horizontalScrollbar.fixedHeight), scrollPosition.x, rect.width, 0f, viewRect.width, horizontalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						if (horizontalScrollbar != GUIStyle.none)
						{
							scrollPosition.x = 0f;
						}
						else
						{
							scrollPosition.x = Mathf.Clamp(scrollPosition.x, 0f, Mathf.Max(viewRect.width - position.width, 0f));
						}
					}
					if (flag && verticalScrollbar != GUIStyle.none)
					{
						scrollPosition.y = GUI.VerticalScrollbar(new Rect(rect.xMax + (float)verticalScrollbar.margin.left, rect.y, verticalScrollbar.fixedWidth, rect.height), scrollPosition.y, rect.height, 0f, viewRect.height, verticalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						if (verticalScrollbar != GUIStyle.none)
						{
							scrollPosition.y = 0f;
						}
						else
						{
							scrollPosition.y = Mathf.Clamp(scrollPosition.y, 0f, Mathf.Max(viewRect.height - position.height, 0f));
						}
					}
				}
			}
			else
			{
				GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
			}
			GUIClip.Push(rect, new Vector2(Mathf.Round(-scrollPosition.x - viewRect.x), Mathf.Round(-scrollPosition.y - viewRect.y)), Vector2.zero, false);
			return scrollPosition;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000B718 File Offset: 0x00009918
		public static void EndScrollView()
		{
			GUI.EndScrollView(true);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000B720 File Offset: 0x00009920
		public static void EndScrollView(bool handleScrollWheel)
		{
			GUI.ScrollViewState scrollViewState = (GUI.ScrollViewState)GUI.s_ScrollViewStates.Peek();
			GUIUtility.CheckOnGUI();
			GUIClip.Pop();
			GUI.s_ScrollViewStates.Pop();
			if (handleScrollWheel && Event.current.type == EventType.ScrollWheel && scrollViewState.position.Contains(Event.current.mousePosition))
			{
				scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.scrollPosition.x + Event.current.delta.x * 20f, 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
				scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.scrollPosition.y + Event.current.delta.y * 20f, 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
				scrollViewState.apply = true;
				Event.current.Use();
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000B834 File Offset: 0x00009A34
		internal static GUI.ScrollViewState GetTopScrollView()
		{
			if (GUI.s_ScrollViewStates.Count != 0)
			{
				return (GUI.ScrollViewState)GUI.s_ScrollViewStates.Peek();
			}
			return null;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000B858 File Offset: 0x00009A58
		public static void ScrollTo(Rect position)
		{
			GUI.ScrollViewState topScrollView = GUI.GetTopScrollView();
			if (topScrollView != null)
			{
				topScrollView.ScrollTo(position);
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000B878 File Offset: 0x00009A78
		public static bool ScrollTowards(Rect position, float maxDelta)
		{
			GUI.ScrollViewState topScrollView = GUI.GetTopScrollView();
			return topScrollView != null && topScrollView.ScrollTowards(position, maxDelta);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000B89C File Offset: 0x00009A9C
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000B8BC File Offset: 0x00009ABC
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000B8DC File Offset: 0x00009ADC
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			return GUI.DoWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000B8F8 File Offset: 0x00009AF8
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin, true);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000B910 File Offset: 0x00009B10
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin, true);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000B928 File Offset: 0x00009B28
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style)
		{
			return GUI.DoWindow(id, clientRect, func, title, style, GUI.skin, true);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000B93C File Offset: 0x00009B3C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000B95C File Offset: 0x00009B5C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000B97C File Offset: 0x00009B7C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			return GUI.DoModalWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000B998 File Offset: 0x00009B98
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style)
		{
			return GUI.DoModalWindow(id, clientRect, func, content, style, GUI.skin);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000B9DC File Offset: 0x00009BDC
		private static Rect DoModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin)
		{
			return GUI.INTERNAL_CALL_DoModalWindow(id, ref clientRect, func, content, style, skin);
		}

		// Token: 0x060004C7 RID: 1223
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Rect INTERNAL_CALL_DoModalWindow(int id, ref Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin);

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000B9EC File Offset: 0x00009BEC
		internal static void CallWindowDelegate(GUI.WindowFunction func, int id, GUISkin _skin, int forceRect, float width, float height, GUIStyle style)
		{
			GUILayoutUtility.SelectIDList(id, true);
			GUISkin skin = GUI.skin;
			if (Event.current.type == EventType.Layout)
			{
				if (forceRect != 0)
				{
					GUILayoutOption[] array = new GUILayoutOption[]
					{
						GUILayout.Width(width),
						GUILayout.Height(height)
					};
					GUILayoutUtility.BeginWindow(id, style, array);
				}
				else
				{
					GUILayoutUtility.BeginWindow(id, style, null);
				}
			}
			GUI.skin = _skin;
			func(id);
			if (Event.current.type == EventType.Layout)
			{
				GUILayoutUtility.Layout();
			}
			GUI.skin = skin;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000BA78 File Offset: 0x00009C78
		private static Rect DoWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout)
		{
			return GUI.INTERNAL_CALL_DoWindow(id, ref clientRect, func, title, style, skin, forceRectOnLayout);
		}

		// Token: 0x060004CA RID: 1226
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Rect INTERNAL_CALL_DoWindow(int id, ref Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout);

		// Token: 0x060004CB RID: 1227 RVA: 0x0000BA8C File Offset: 0x00009C8C
		public static void DragWindow(Rect position)
		{
			GUI.INTERNAL_CALL_DragWindow(ref position);
		}

		// Token: 0x060004CC RID: 1228
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DragWindow(ref Rect position);

		// Token: 0x060004CD RID: 1229 RVA: 0x0000BA98 File Offset: 0x00009C98
		public static void DragWindow()
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
		}

		// Token: 0x060004CE RID: 1230
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void BringWindowToFront(int windowID);

		// Token: 0x060004CF RID: 1231
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void BringWindowToBack(int windowID);

		// Token: 0x060004D0 RID: 1232
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void FocusWindow(int windowID);

		// Token: 0x060004D1 RID: 1233
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void UnfocusWindow();

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		internal static void BeginWindows(int skinMode, int editorWindowInstanceID)
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			Matrix4x4 matrix = GUI.matrix;
			GUI.Internal_BeginWindows();
			GUI.matrix = matrix;
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x060004D3 RID: 1235
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_BeginWindows();

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000BB18 File Offset: 0x00009D18
		internal static void EndWindows()
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			GUI.Internal_EndWindows();
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x060004D5 RID: 1237
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_EndWindows();

		// Token: 0x040000D4 RID: 212
		private static float scrollStepSize = 10f;

		// Token: 0x040000D5 RID: 213
		private static int scrollControlID;

		// Token: 0x040000D6 RID: 214
		private static int hotTextField = -1;

		// Token: 0x040000D7 RID: 215
		private static GUISkin s_Skin;

		// Token: 0x040000D8 RID: 216
		internal static Rect s_ToolTipRect;

		// Token: 0x040000D9 RID: 217
		private static int boxHash = "Box".GetHashCode();

		// Token: 0x040000DA RID: 218
		private static int repeatButtonHash = "repeatButton".GetHashCode();

		// Token: 0x040000DB RID: 219
		private static int toggleHash = "Toggle".GetHashCode();

		// Token: 0x040000DC RID: 220
		private static int buttonGridHash = "ButtonGrid".GetHashCode();

		// Token: 0x040000DD RID: 221
		private static int sliderHash = "Slider".GetHashCode();

		// Token: 0x040000DE RID: 222
		private static int beginGroupHash = "BeginGroup".GetHashCode();

		// Token: 0x040000DF RID: 223
		private static int scrollviewHash = "scrollView".GetHashCode();

		// Token: 0x040000E0 RID: 224
		private static GenericStack s_ScrollViewStates = new GenericStack();

		// Token: 0x02000061 RID: 97
		internal sealed class ScrollViewState
		{
			// Token: 0x060004D7 RID: 1239 RVA: 0x0000BB74 File Offset: 0x00009D74
			internal void ScrollTo(Rect position)
			{
				this.ScrollTowards(position, float.PositiveInfinity);
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x0000BB84 File Offset: 0x00009D84
			internal bool ScrollTowards(Rect position, float maxDelta)
			{
				Vector2 vector = this.ScrollNeeded(position);
				if (vector.sqrMagnitude < 0.0001f)
				{
					return false;
				}
				if (maxDelta == 0f)
				{
					return true;
				}
				if (vector.magnitude > maxDelta)
				{
					vector = vector.normalized * maxDelta;
				}
				this.scrollPosition += vector;
				this.apply = true;
				return true;
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x0000BBF0 File Offset: 0x00009DF0
			internal Vector2 ScrollNeeded(Rect position)
			{
				Rect rect = this.visibleRect;
				rect.x += this.scrollPosition.x;
				rect.y += this.scrollPosition.y;
				float num = position.width - this.visibleRect.width;
				if (num > 0f)
				{
					position.width -= num;
					position.x += num * 0.5f;
				}
				num = position.height - this.visibleRect.height;
				if (num > 0f)
				{
					position.height -= num;
					position.y += num * 0.5f;
				}
				Vector2 zero = Vector2.zero;
				if (position.xMax > rect.xMax)
				{
					zero.x += position.xMax - rect.xMax;
				}
				else if (position.xMin < rect.xMin)
				{
					zero.x -= rect.xMin - position.xMin;
				}
				if (position.yMax > rect.yMax)
				{
					zero.y += position.yMax - rect.yMax;
				}
				else if (position.yMin < rect.yMin)
				{
					zero.y -= rect.yMin - position.yMin;
				}
				Rect rect2 = this.viewRect;
				rect2.width = Mathf.Max(rect2.width, this.visibleRect.width);
				rect2.height = Mathf.Max(rect2.height, this.visibleRect.height);
				zero.x = Mathf.Clamp(zero.x, rect2.xMin - this.scrollPosition.x, rect2.xMax - this.visibleRect.width - this.scrollPosition.x);
				zero.y = Mathf.Clamp(zero.y, rect2.yMin - this.scrollPosition.y, rect2.yMax - this.visibleRect.height - this.scrollPosition.y);
				return zero;
			}

			// Token: 0x040000E3 RID: 227
			public Rect position;

			// Token: 0x040000E4 RID: 228
			public Rect visibleRect;

			// Token: 0x040000E5 RID: 229
			public Rect viewRect;

			// Token: 0x040000E6 RID: 230
			public Vector2 scrollPosition;

			// Token: 0x040000E7 RID: 231
			public bool apply;

			// Token: 0x040000E8 RID: 232
			public bool hasScrollTo;
		}

		// Token: 0x02000062 RID: 98
		// (Invoke) Token: 0x060004DB RID: 1243
		public delegate void WindowFunction(int id);
	}
}
