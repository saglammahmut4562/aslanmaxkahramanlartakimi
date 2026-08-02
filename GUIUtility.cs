using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000077 RID: 119
	public class GUIUtility
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x0000FA38 File Offset: 0x0000DC38
		public static int GetControlID(FocusType focus)
		{
			return GUIUtility.GetControlID(0, focus);
		}

		// Token: 0x060005D6 RID: 1494
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetControlID(int hint, FocusType focus);

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000FA44 File Offset: 0x0000DC44
		public static int GetControlID(GUIContent contents, FocusType focus)
		{
			return GUIUtility.GetControlID(contents.hash, focus);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000FA54 File Offset: 0x0000DC54
		public static int GetControlID(FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(0, focus, position);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000FA60 File Offset: 0x0000DC60
		public static int GetControlID(int hint, FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(hint, focus, position);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		public static int GetControlID(GUIContent contents, FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(contents.hash, focus, position);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000FA7C File Offset: 0x0000DC7C
		private static int Internal_GetNextControlID2(int hint, FocusType focusType, Rect rect)
		{
			return GUIUtility.INTERNAL_CALL_Internal_GetNextControlID2(hint, focusType, ref rect);
		}

		// Token: 0x060005DC RID: 1500
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int INTERNAL_CALL_Internal_GetNextControlID2(int hint, FocusType focusType, ref Rect rect);

		// Token: 0x060005DD RID: 1501 RVA: 0x0000FA88 File Offset: 0x0000DC88
		public static object GetStateObject(Type t, int controlID)
		{
			return GUIStateObjects.GetStateObject(t, controlID);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000FA94 File Offset: 0x0000DC94
		public static object QueryStateObject(Type t, int controlID)
		{
			return GUIStateObjects.QueryStateObject(t, controlID);
		}

		// Token: 0x060005DF RID: 1503
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern int GetPermanentControlID();

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0000FAA8 File Offset: 0x0000DCA8
		public static int hotControl
		{
			get
			{
				return GUIUtility.Internal_GetHotControl();
			}
			set
			{
				GUIUtility.Internal_SetHotControl(value);
			}
		}

		// Token: 0x060005E2 RID: 1506
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int Internal_GetHotControl();

		// Token: 0x060005E3 RID: 1507
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetHotControl(int value);

		// Token: 0x060005E4 RID: 1508
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void UpdateUndoName();

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005E5 RID: 1509
		// (set) Token: 0x060005E6 RID: 1510
		public static extern int keyboardControl
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		public static void ExitGUI()
		{
			throw new ExitGUIException();
		}

		// Token: 0x060005E8 RID: 1512
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void SetDidGUIWindowsEatLastEvent(bool value);

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005E9 RID: 1513
		// (set) Token: 0x060005EA RID: 1514
		internal static extern string systemCopyBuffer
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		internal static GUISkin GetDefaultSkin()
		{
			return GUIUtility.Internal_GetDefaultSkin(GUIUtility.s_SkinMode);
		}

		// Token: 0x060005EC RID: 1516
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern GUISkin Internal_GetDefaultSkin(int skinMode);

		// Token: 0x060005ED RID: 1517
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Object Internal_GetBuiltinSkin(int skin);

		// Token: 0x060005EE RID: 1518 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
		internal static GUISkin GetBuiltinSkin(int skin)
		{
			return GUIUtility.Internal_GetBuiltinSkin(skin) as GUISkin;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		internal static void BeginGUI(int skinMode, int instanceID, int useGUILayout)
		{
			GUIUtility.s_SkinMode = skinMode;
			GUIUtility.s_OriginalID = instanceID;
			GUI.skin = null;
			if (useGUILayout != 0)
			{
				GUILayoutUtility.SelectIDList(instanceID, false);
				GUILayoutUtility.Begin(instanceID);
			}
			GUI.changed = false;
		}

		// Token: 0x060005F0 RID: 1520
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_ExitGUI();

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000FB04 File Offset: 0x0000DD04
		internal static void EndGUI(int layoutType)
		{
			try
			{
				if (Event.current.type == EventType.Layout)
				{
					switch (layoutType)
					{
					case 1:
						GUILayoutUtility.Layout();
						break;
					case 2:
						GUILayoutUtility.LayoutFromEditorWindow();
						break;
					}
				}
				GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
				GUIContent.ClearStaticCache();
			}
			finally
			{
				GUIUtility.Internal_ExitGUI();
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000FB80 File Offset: 0x0000DD80
		internal static bool EndGUIFromException(Exception exception)
		{
			if (exception == null)
			{
				return false;
			}
			if (!(exception is ExitGUIException) && !(exception.InnerException is ExitGUIException))
			{
				return false;
			}
			GUIUtility.Internal_ExitGUI();
			return true;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		internal static void CheckOnGUI()
		{
			if (GUIUtility.Internal_GetGUIDepth() <= 0)
			{
				throw new ArgumentException("You can only call GUI functions from inside OnGUI.");
			}
		}

		// Token: 0x060005F4 RID: 1524
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern int Internal_GetGUIDepth();

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005F5 RID: 1525
		// (set) Token: 0x060005F6 RID: 1526
		internal static extern bool mouseUsed
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		public static Vector2 GUIToScreenPoint(Vector2 guiPoint)
		{
			return GUIClip.Unclip(guiPoint) + GUIUtility.s_EditorScreenPointOffset;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000FBDC File Offset: 0x0000DDDC
		internal static Rect GUIToScreenRect(Rect guiRect)
		{
			Vector2 vector = GUIUtility.GUIToScreenPoint(new Vector2(guiRect.x, guiRect.y));
			guiRect.x = vector.x;
			guiRect.y = vector.y;
			return guiRect;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000FC20 File Offset: 0x0000DE20
		public static Vector2 ScreenToGUIPoint(Vector2 screenPoint)
		{
			return GUIClip.Clip(screenPoint) - GUIUtility.s_EditorScreenPointOffset;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000FC34 File Offset: 0x0000DE34
		public static Rect ScreenToGUIRect(Rect screenRect)
		{
			Vector2 vector = GUIUtility.ScreenToGUIPoint(new Vector2(screenRect.x, screenRect.y));
			screenRect.x = vector.x;
			screenRect.y = vector.y;
			return screenRect;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000FC78 File Offset: 0x0000DE78
		public static void RotateAroundPivot(float angle, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			GUI.matrix = Matrix4x4.identity;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.Euler(0f, 0f, angle), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = matrix4x * matrix;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		public static void ScaleAroundPivot(Vector2 scale, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.identity, new Vector3(scale.x, scale.y, 1f)) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = matrix4x * matrix;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005FD RID: 1533
		public static extern bool hasModalWindow
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005FE RID: 1534
		// (set) Token: 0x060005FF RID: 1535
		internal static extern bool textFieldInput
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x04000163 RID: 355
		[NotRenamed]
		internal static int s_SkinMode;

		// Token: 0x04000164 RID: 356
		[NotRenamed]
		internal static int s_OriginalID;

		// Token: 0x04000165 RID: 357
		internal static Vector2 s_EditorScreenPointOffset = Vector2.zero;

		// Token: 0x04000166 RID: 358
		internal static bool s_HasKeyboardFocus = false;
	}
}
