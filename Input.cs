using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000083 RID: 131
	public sealed class Input
	{
		// Token: 0x0600060D RID: 1549
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool GetKeyDownInt(int key);

		// Token: 0x0600060E RID: 1550
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float GetAxis(string axisName);

		// Token: 0x0600060F RID: 1551 RVA: 0x0000FF08 File Offset: 0x0000E108
		public static bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDownInt((int)key);
		}

		// Token: 0x06000610 RID: 1552
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool GetMouseButton(int button);

		// Token: 0x06000611 RID: 1553
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool GetMouseButtonDown(int button);

		// Token: 0x06000612 RID: 1554
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool GetMouseButtonUp(int button);

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000613 RID: 1555
		public static extern Vector3 mousePosition
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000614 RID: 1556
		public static extern string inputString
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000FF10 File Offset: 0x0000E110
		public static Touch[] touches
		{
			get
			{
				int touchCount = Input.touchCount;
				Touch[] array = new Touch[touchCount];
				for (int i = 0; i < touchCount; i++)
				{
					array[i] = Input.GetTouch(i);
				}
				return array;
			}
		}

		// Token: 0x06000616 RID: 1558
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Touch GetTouch(int index);

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000617 RID: 1559
		public static extern int touchCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000618 RID: 1560
		public static extern bool multiTouchEnabled
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000619 RID: 1561
		public static extern string compositionString
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600061A RID: 1562
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_compositionCursorPos(ref Vector2 value);

		// Token: 0x17000132 RID: 306
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x0000FF50 File Offset: 0x0000E150
		public static Vector2 compositionCursorPos
		{
			set
			{
				Input.INTERNAL_set_compositionCursorPos(ref value);
			}
		}

		// Token: 0x0400017F RID: 383
		private static Gyroscope m_MainGyro;

		// Token: 0x04000180 RID: 384
		private static LocationService locationServiceInstance;

		// Token: 0x04000181 RID: 385
		private static Compass compassInstance;
	}
}
