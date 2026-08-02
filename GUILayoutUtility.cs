using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x0200006C RID: 108
	public class GUILayoutUtility
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x0000DE38 File Offset: 0x0000C038
		internal static GUILayoutUtility.LayoutCache SelectIDList(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> dictionary = ((!isWindow) ? GUILayoutUtility.storedLayouts : GUILayoutUtility.storedWindows);
			GUILayoutUtility.LayoutCache layoutCache;
			if (!dictionary.TryGetValue(instanceID, out layoutCache))
			{
				layoutCache = new GUILayoutUtility.LayoutCache();
				dictionary[instanceID] = layoutCache;
			}
			GUILayoutUtility.current.topLevel = layoutCache.topLevel;
			GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
			GUILayoutUtility.current.windows = layoutCache.windows;
			return layoutCache;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		internal static void Begin(int instanceID)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(instanceID, false);
			if (Event.current.type == EventType.Layout)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000DF64 File Offset: 0x0000C164
		internal static void BeginWindow(int windowID, GUIStyle style, GUILayoutOption[] options)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(windowID, true);
			if (Event.current.type == EventType.Layout)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.topLevel.style = style;
				GUILayoutUtility.current.topLevel.windowID = windowID;
				if (options != null)
				{
					GUILayoutUtility.current.topLevel.ApplyOptions(options);
				}
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000E04C File Offset: 0x0000C24C
		public static void EndGroup(string groupName)
		{
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000E050 File Offset: 0x0000C250
		internal static void Layout()
		{
			if (GUILayoutUtility.current.topLevel.windowID == -1)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, Mathf.Min((float)Screen.width, GUILayoutUtility.current.topLevel.maxWidth));
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height, GUILayoutUtility.current.topLevel.maxHeight));
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000E120 File Offset: 0x0000C320
		internal static void LayoutFromEditorWindow()
		{
			GUILayoutUtility.current.topLevel.CalcWidth();
			GUILayoutUtility.current.topLevel.SetHorizontal(0f, (float)Screen.width);
			GUILayoutUtility.current.topLevel.CalcHeight();
			GUILayoutUtility.current.topLevel.SetVertical(0f, (float)Screen.height);
			GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000E190 File Offset: 0x0000C390
		internal static void LayoutFreeGroup(GUILayoutGroup toplevel)
		{
			foreach (GUILayoutEntry guilayoutEntry in toplevel.entries)
			{
				GUILayoutGroup guilayoutGroup = (GUILayoutGroup)guilayoutEntry;
				GUILayoutUtility.LayoutSingleGroup(guilayoutGroup);
			}
			toplevel.ResetCursor();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		private static void LayoutSingleGroup(GUILayoutGroup i)
		{
			if (!i.isWindow)
			{
				float minWidth = i.minWidth;
				float maxWidth = i.maxWidth;
				i.CalcWidth();
				i.SetHorizontal(i.rect.x, Mathf.Clamp(i.maxWidth, minWidth, maxWidth));
				float minHeight = i.minHeight;
				float maxHeight = i.maxHeight;
				i.CalcHeight();
				i.SetVertical(i.rect.y, Mathf.Clamp(i.maxHeight, minHeight, maxHeight));
			}
			else
			{
				i.CalcWidth();
				Rect rect = GUILayoutUtility.Internal_GetWindowRect(i.windowID);
				i.SetHorizontal(rect.x, Mathf.Clamp(rect.width, i.minWidth, i.maxWidth));
				i.CalcHeight();
				i.SetVertical(rect.y, Mathf.Clamp(rect.height, i.minHeight, i.maxHeight));
				GUILayoutUtility.Internal_MoveWindow(i.windowID, i.rect);
			}
		}

		// Token: 0x06000536 RID: 1334
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Rect Internal_GetWindowRect(int windowID);

		// Token: 0x06000537 RID: 1335 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		private static void Internal_MoveWindow(int windowID, Rect r)
		{
			GUILayoutUtility.INTERNAL_CALL_Internal_MoveWindow(windowID, ref r);
		}

		// Token: 0x06000538 RID: 1336
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_MoveWindow(int windowID, ref Rect r);

		// Token: 0x06000539 RID: 1337 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		[SecuritySafeCritical]
		private static GUILayoutGroup CreateGUILayoutGroupInstanceOfType(Type LayoutType)
		{
			if (!typeof(GUILayoutGroup).IsAssignableFrom(LayoutType))
			{
				throw new ArgumentException("LayoutType needs to be of type GUILayoutGroup");
			}
			return (GUILayoutGroup)Activator.CreateInstance(LayoutType);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000E328 File Offset: 0x0000C528
		internal static GUILayoutGroup BeginLayoutGroup(GUIStyle style, GUILayoutOption[] options, Type LayoutType)
		{
			EventType type = Event.current.type;
			GUILayoutGroup guilayoutGroup;
			if (type != EventType.Layout && type != EventType.Used)
			{
				guilayoutGroup = GUILayoutUtility.current.topLevel.GetNext() as GUILayoutGroup;
				if (guilayoutGroup == null)
				{
					throw new ArgumentException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(LayoutType);
				guilayoutGroup.style = style;
				if (options != null)
				{
					guilayoutGroup.ApplyOptions(options);
				}
				GUILayoutUtility.current.topLevel.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
		internal static void EndLayoutGroup()
		{
			EventType type = Event.current.type;
			GUILayoutUtility.current.layoutGroups.Pop();
			GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000E430 File Offset: 0x0000C630
		internal static GUILayoutGroup BeginLayoutArea(GUIStyle style, Type LayoutType)
		{
			EventType type = Event.current.type;
			GUILayoutGroup guilayoutGroup;
			if (type != EventType.Layout && type != EventType.Used)
			{
				guilayoutGroup = GUILayoutUtility.current.windows.GetNext() as GUILayoutGroup;
				if (guilayoutGroup == null)
				{
					throw new ArgumentException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(LayoutType);
				guilayoutGroup.style = style;
				GUILayoutUtility.current.windows.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		public static Rect GetRect(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(content, style, options);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		private static Rect DoGetRect(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				if (style.isHeightDependantOnWidth)
				{
					GUILayoutUtility.current.topLevel.Add(new GUIWordWrapSizer(style, content, options));
				}
				else
				{
					Vector2 vector = style.CalcSize(content);
					GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(vector.x, vector.x, vector.y, vector.y, style, options));
				}
				return GUILayoutUtility.kDummyRect;
			}
			if (type != EventType.Used)
			{
				return GUILayoutUtility.current.topLevel.GetNext().rect;
			}
			return GUILayoutUtility.kDummyRect;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000E5A0 File Offset: 0x0000C7A0
		public static Rect GetRect(float width, float height, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, options);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		private static Rect DoGetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(minWidth, maxWidth, minHeight, maxHeight, style, options));
				return GUILayoutUtility.kDummyRect;
			}
			if (type != EventType.Used)
			{
				return GUILayoutUtility.current.topLevel.GetNext().rect;
			}
			return GUILayoutUtility.kDummyRect;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000E618 File Offset: 0x0000C818
		internal static GUIStyle spaceStyle
		{
			get
			{
				if (GUILayoutUtility.s_SpaceStyle == null)
				{
					GUILayoutUtility.s_SpaceStyle = new GUIStyle();
				}
				GUILayoutUtility.s_SpaceStyle.stretchWidth = false;
				return GUILayoutUtility.s_SpaceStyle;
			}
		}

		// Token: 0x0400011C RID: 284
		private static Dictionary<int, GUILayoutUtility.LayoutCache> storedLayouts = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x0400011D RID: 285
		private static Dictionary<int, GUILayoutUtility.LayoutCache> storedWindows = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x0400011E RID: 286
		internal static GUILayoutUtility.LayoutCache current = new GUILayoutUtility.LayoutCache();

		// Token: 0x0400011F RID: 287
		private static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000120 RID: 288
		private static GUIStyle s_SpaceStyle;

		// Token: 0x0200006D RID: 109
		internal sealed class LayoutCache
		{
			// Token: 0x06000542 RID: 1346 RVA: 0x0000E640 File Offset: 0x0000C840
			internal LayoutCache()
			{
				this.layoutGroups.Push(this.topLevel);
			}

			// Token: 0x04000121 RID: 289
			internal GUILayoutGroup topLevel = new GUILayoutGroup();

			// Token: 0x04000122 RID: 290
			internal GenericStack layoutGroups = new GenericStack();

			// Token: 0x04000123 RID: 291
			internal GUILayoutGroup windows = new GUILayoutGroup();
		}
	}
}
