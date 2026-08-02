using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000073 RID: 115
	[Serializable]
	[StructLayout(0)]
	public sealed class GUIStyle
	{
		// Token: 0x0600058A RID: 1418 RVA: 0x0000F474 File Offset: 0x0000D674
		public GUIStyle()
		{
			this.Init();
			this.RegisterObjectForAssetGarbageCollection();
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000F488 File Offset: 0x0000D688
		public GUIStyle(GUIStyle other)
		{
			this.InitCopy(other);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		~GUIStyle()
		{
			this.Cleanup();
		}

		// Token: 0x0600058E RID: 1422
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void RegisterObjectForAssetGarbageCollection();

		// Token: 0x0600058F RID: 1423
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x06000590 RID: 1424
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void InitCopy(GUIStyle other);

		// Token: 0x06000591 RID: 1425
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000592 RID: 1426
		// (set) Token: 0x06000593 RID: 1427
		public extern string name
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		public GUIStyleState normal
		{
			get
			{
				if (this.m_Normal == null)
				{
					this.m_Normal = new GUIStyleState(this, this.GetStyleStatePtr(0));
				}
				return this.m_Normal;
			}
		}

		// Token: 0x06000595 RID: 1429
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern IntPtr GetStyleStatePtr(int idx);

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000F4F8 File Offset: 0x0000D6F8
		public RectOffset margin
		{
			get
			{
				if (this.m_Margin == null)
				{
					this.m_Margin = new RectOffset(this, this.GetRectOffsetPtr(1));
				}
				return this.m_Margin;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000F520 File Offset: 0x0000D720
		public RectOffset padding
		{
			get
			{
				if (this.m_Padding == null)
				{
					this.m_Padding = new RectOffset(this, this.GetRectOffsetPtr(2));
				}
				return this.m_Padding;
			}
		}

		// Token: 0x06000598 RID: 1432
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern IntPtr GetRectOffsetPtr(int idx);

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000599 RID: 1433
		public extern ImagePosition imagePosition
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000116 RID: 278
		// (set) Token: 0x0600059A RID: 1434
		public extern TextAnchor alignment
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600059B RID: 1435
		public extern bool wordWrap
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600059C RID: 1436
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_contentOffset(out Vector2 value);

		// Token: 0x0600059D RID: 1437
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_contentOffset(ref Vector2 value);

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000F548 File Offset: 0x0000D748
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0000F560 File Offset: 0x0000D760
		public Vector2 contentOffset
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_contentOffset(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_contentOffset(ref value);
			}
		}

		// Token: 0x060005A0 RID: 1440
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_Internal_clipOffset(ref Vector2 value);

		// Token: 0x17000119 RID: 281
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0000F56C File Offset: 0x0000D76C
		internal Vector2 Internal_clipOffset
		{
			set
			{
				this.INTERNAL_set_Internal_clipOffset(ref value);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005A2 RID: 1442
		public extern float fixedWidth
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005A3 RID: 1443
		public extern float fixedHeight
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005A4 RID: 1444
		// (set) Token: 0x060005A5 RID: 1445
		public extern bool stretchWidth
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005A6 RID: 1446
		// (set) Token: 0x060005A7 RID: 1447
		public extern bool stretchHeight
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060005A8 RID: 1448
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern float Internal_GetLineHeight(IntPtr target);

		// Token: 0x1700011E RID: 286
		// (set) Token: 0x060005A9 RID: 1449
		public extern int fontSize
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000F578 File Offset: 0x0000D778
		public float lineHeight
		{
			get
			{
				return Mathf.Round(GUIStyle.Internal_GetLineHeight(this.m_Ptr));
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000F58C File Offset: 0x0000D78C
		private static void Internal_Draw(IntPtr target, Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			Internal_DrawArguments internal_DrawArguments = default(Internal_DrawArguments);
			internal_DrawArguments.target = target;
			internal_DrawArguments.position = position;
			internal_DrawArguments.isHover = ((!isHover) ? 0 : 1);
			internal_DrawArguments.isActive = ((!isActive) ? 0 : 1);
			internal_DrawArguments.on = ((!on) ? 0 : 1);
			internal_DrawArguments.hasKeyboardFocus = ((!hasKeyboardFocus) ? 0 : 1);
			GUIStyle.Internal_Draw(content, ref internal_DrawArguments);
		}

		// Token: 0x060005AC RID: 1452
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_Draw(GUIContent content, ref Internal_DrawArguments arguments);

		// Token: 0x060005AD RID: 1453 RVA: 0x0000F60C File Offset: 0x0000D80C
		public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, GUIContent.none, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000F628 File Offset: 0x0000D828
		public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, content, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000F640 File Offset: 0x0000D840
		[ExcludeFromDocs]
		public void Draw(Rect position, GUIContent content, int controlID)
		{
			bool flag = false;
			this.Draw(position, content, controlID, flag);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000F65C File Offset: 0x0000D85C
		public void Draw(Rect position, GUIContent content, int controlID, [DefaultValue("false")] bool on)
		{
			if (content != null)
			{
				GUIStyle.Internal_Draw2(this.m_Ptr, position, content, controlID, on);
			}
			else
			{
				Debug.LogError("Style.Draw may not be called with GUIContent that is null.");
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000F684 File Offset: 0x0000D884
		private static void Internal_Draw2(IntPtr style, Rect position, GUIContent content, int controlID, bool on)
		{
			GUIStyle.INTERNAL_CALL_Internal_Draw2(style, ref position, content, controlID, on);
		}

		// Token: 0x060005B2 RID: 1458
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_Draw2(IntPtr style, ref Rect position, GUIContent content, int controlID, bool on);

		// Token: 0x060005B3 RID: 1459
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern float Internal_GetCursorFlashOffset();

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000F694 File Offset: 0x0000D894
		private static void Internal_DrawCursor(IntPtr target, Rect position, GUIContent content, int pos, Color cursorColor)
		{
			GUIStyle.INTERNAL_CALL_Internal_DrawCursor(target, ref position, content, pos, ref cursorColor);
		}

		// Token: 0x060005B5 RID: 1461
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_DrawCursor(IntPtr target, ref Rect position, GUIContent content, int pos, ref Color cursorColor);

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
		public void DrawCursor(Rect position, GUIContent content, int controlID, int Character)
		{
			Event current = Event.current;
			if (current.type == EventType.Repaint)
			{
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				if (cursorFlashSpeed == 0f || num < 0.5f)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				GUIStyle.Internal_DrawCursor(this.m_Ptr, position, content, Character, cursorColor);
			}
		}

		// Token: 0x060005B7 RID: 1463
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_DrawWithTextSelection(GUIContent content, ref Internal_DrawWithTextSelectionArguments arguments);

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000F734 File Offset: 0x0000D934
		internal void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition)
		{
			Event current = Event.current;
			Color cursorColor = new Color(0f, 0f, 0f, 0f);
			float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
			float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
			if (cursorFlashSpeed == 0f || num < 0.5f)
			{
				cursorColor = GUI.skin.settings.cursorColor;
			}
			Internal_DrawWithTextSelectionArguments internal_DrawWithTextSelectionArguments = default(Internal_DrawWithTextSelectionArguments);
			internal_DrawWithTextSelectionArguments.target = this.m_Ptr;
			internal_DrawWithTextSelectionArguments.position = position;
			internal_DrawWithTextSelectionArguments.firstPos = firstSelectedCharacter;
			internal_DrawWithTextSelectionArguments.lastPos = lastSelectedCharacter;
			internal_DrawWithTextSelectionArguments.cursorColor = cursorColor;
			internal_DrawWithTextSelectionArguments.selectionColor = GUI.skin.settings.selectionColor;
			internal_DrawWithTextSelectionArguments.isHover = ((!position.Contains(current.mousePosition)) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.isActive = ((controlID != GUIUtility.hotControl) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.on = 0;
			internal_DrawWithTextSelectionArguments.hasKeyboardFocus = ((controlID != GUIUtility.keyboardControl || !GUIStyle.showKeyboardFocus) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.drawSelectionAsComposition = ((!drawSelectionAsComposition) ? 0 : 1);
			GUIStyle.Internal_DrawWithTextSelection(content, ref internal_DrawWithTextSelectionArguments);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000F878 File Offset: 0x0000DA78
		public void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
		{
			this.DrawWithTextSelection(position, content, controlID, firstSelectedCharacter, lastSelectedCharacter, false);
		}

		// Token: 0x060005BA RID: 1466
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void SetDefaultFont(Font font);

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000F888 File Offset: 0x0000DA88
		public static GUIStyle none
		{
			get
			{
				if (GUIStyle.s_None == null)
				{
					GUIStyle.s_None = new GUIStyle();
				}
				return GUIStyle.s_None;
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			Vector2 vector;
			GUIStyle.Internal_GetCursorPixelPosition(this.m_Ptr, position, content, cursorStringIndex, out vector);
			return vector;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
		internal static void Internal_GetCursorPixelPosition(IntPtr target, Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret)
		{
			GUIStyle.INTERNAL_CALL_Internal_GetCursorPixelPosition(target, ref position, content, cursorStringIndex, out ret);
		}

		// Token: 0x060005BE RID: 1470
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_GetCursorPixelPosition(IntPtr target, ref Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret);

		// Token: 0x060005BF RID: 1471 RVA: 0x0000F8D4 File Offset: 0x0000DAD4
		public int GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return GUIStyle.Internal_GetCursorStringIndex(this.m_Ptr, position, content, cursorPixelPosition);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
		internal static int Internal_GetCursorStringIndex(IntPtr target, Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return GUIStyle.INTERNAL_CALL_Internal_GetCursorStringIndex(target, ref position, content, ref cursorPixelPosition);
		}

		// Token: 0x060005C1 RID: 1473
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int INTERNAL_CALL_Internal_GetCursorStringIndex(IntPtr target, ref Rect position, GUIContent content, ref Vector2 cursorPixelPosition);

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000F8F4 File Offset: 0x0000DAF4
		public Vector2 CalcSize(GUIContent content)
		{
			Vector2 vector;
			GUIStyle.Internal_CalcSize(this.m_Ptr, content, out vector);
			return vector;
		}

		// Token: 0x060005C3 RID: 1475
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void Internal_CalcSize(IntPtr target, GUIContent content, out Vector2 ret);

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000F910 File Offset: 0x0000DB10
		public float CalcHeight(GUIContent content, float width)
		{
			return GUIStyle.Internal_CalcHeight(this.m_Ptr, content, width);
		}

		// Token: 0x060005C5 RID: 1477
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern float Internal_CalcHeight(IntPtr target, GUIContent content, float width);

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000F920 File Offset: 0x0000DB20
		public bool isHeightDependantOnWidth
		{
			get
			{
				return this.fixedHeight == 0f && this.wordWrap && this.imagePosition != ImagePosition.ImageOnly;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000F950 File Offset: 0x0000DB50
		public void CalcMinMaxWidth(GUIContent content, out float minWidth, out float maxWidth)
		{
			GUIStyle.Internal_CalcMinMaxWidth(this.m_Ptr, content, out minWidth, out maxWidth);
		}

		// Token: 0x060005C8 RID: 1480
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CalcMinMaxWidth(IntPtr target, GUIContent content, out float minWidth, out float maxWidth);

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000F960 File Offset: 0x0000DB60
		public override string ToString()
		{
			return UnityString.Format("GUIStyle '{0}'", new object[] { this.name });
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000F97C File Offset: 0x0000DB7C
		public static implicit operator GUIStyle(string str)
		{
			if (GUISkin.current == null)
			{
				Debug.LogError("Unable to use a named GUIStyle without a current skin. Most likely you need to move your GUIStyle initialization code to OnGUI");
				return GUISkin.error;
			}
			return GUISkin.current.GetStyle(str);
		}

		// Token: 0x04000151 RID: 337
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000152 RID: 338
		[NonSerialized]
		private GUIStyleState m_Normal;

		// Token: 0x04000153 RID: 339
		[NonSerialized]
		private GUIStyleState m_Hover;

		// Token: 0x04000154 RID: 340
		[NonSerialized]
		private GUIStyleState m_Active;

		// Token: 0x04000155 RID: 341
		[NonSerialized]
		private GUIStyleState m_Focused;

		// Token: 0x04000156 RID: 342
		[NonSerialized]
		private GUIStyleState m_OnNormal;

		// Token: 0x04000157 RID: 343
		[NonSerialized]
		private GUIStyleState m_OnHover;

		// Token: 0x04000158 RID: 344
		[NonSerialized]
		private GUIStyleState m_OnActive;

		// Token: 0x04000159 RID: 345
		[NonSerialized]
		private GUIStyleState m_OnFocused;

		// Token: 0x0400015A RID: 346
		[NonSerialized]
		private RectOffset m_Border;

		// Token: 0x0400015B RID: 347
		[NonSerialized]
		private RectOffset m_Padding;

		// Token: 0x0400015C RID: 348
		[NonSerialized]
		private RectOffset m_Margin;

		// Token: 0x0400015D RID: 349
		[NonSerialized]
		private RectOffset m_Overflow;

		// Token: 0x0400015E RID: 350
		[NonSerialized]
		private Font m_FontInternal;

		// Token: 0x0400015F RID: 351
		internal static bool showKeyboardFocus = true;

		// Token: 0x04000160 RID: 352
		private static GUIStyle s_None;
	}
}
