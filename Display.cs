using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000048 RID: 72
	public sealed class Display
	{
		// Token: 0x06000328 RID: 808 RVA: 0x00007C84 File Offset: 0x00005E84
		internal Display()
		{
			this.nativeDisplay = new IntPtr(0);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00007C98 File Offset: 0x00005E98
		internal Display(IntPtr nativeDisplay)
		{
			this.nativeDisplay = nativeDisplay;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00007CA8 File Offset: 0x00005EA8
		// Note: this type is marked as 'beforefieldinit'.
		static Display()
		{
			Display.onDisplaysUpdated = null;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600032B RID: 811 RVA: 0x00007CD0 File Offset: 0x00005ED0
		// (remove) Token: 0x0600032C RID: 812 RVA: 0x00007CE8 File Offset: 0x00005EE8
		public static event Display.DisplaysUpdatedDelegate onDisplaysUpdated;

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00007D00 File Offset: 0x00005F00
		public int renderingWidth
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out num2);
				return num;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00007D24 File Offset: 0x00005F24
		public int renderingHeight
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out num2);
				return num2;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00007D48 File Offset: 0x00005F48
		public int systemWidth
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out num2);
				return num;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00007D6C File Offset: 0x00005F6C
		public int systemHeight
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out num2);
				return num2;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00007D90 File Offset: 0x00005F90
		public RenderBuffer colorBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer renderBuffer2;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out renderBuffer2);
				return renderBuffer;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00007DB0 File Offset: 0x00005FB0
		public RenderBuffer depthBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer renderBuffer2;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out renderBuffer2);
				return renderBuffer2;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00007DD0 File Offset: 0x00005FD0
		public void Activate()
		{
			Display.ActivateDisplayImpl(this.nativeDisplay);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00007DE0 File Offset: 0x00005FE0
		public void SetRenderingResolution(int w, int h)
		{
			Display.SetRenderingResolutionImpl(this.nativeDisplay, w, h);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public static Display main
		{
			get
			{
				return Display._mainDisplay;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00007DF8 File Offset: 0x00005FF8
		private static void RecreateDisplayList(IntPtr[] nativeDisplay)
		{
			Display.displays = new Display[nativeDisplay.Length];
			for (int i = 0; i < nativeDisplay.Length; i++)
			{
				Display.displays[i] = new Display(nativeDisplay[i]);
			}
			Display._mainDisplay = Display.displays[0];
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00007E44 File Offset: 0x00006044
		private static void FireDisplaysUpdated()
		{
			if (Display.onDisplaysUpdated != null)
			{
				Display.onDisplaysUpdated();
			}
		}

		// Token: 0x06000338 RID: 824
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void GetSystemExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x06000339 RID: 825
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void GetRenderingExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x0600033A RID: 826
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void GetRenderingBuffersImpl(IntPtr nativeDisplay, out RenderBuffer color, out RenderBuffer depth);

		// Token: 0x0600033B RID: 827
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void SetRenderingResolutionImpl(IntPtr nativeDisplay, int w, int h);

		// Token: 0x0600033C RID: 828
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void ActivateDisplayImpl(IntPtr nativeDisplay);

		// Token: 0x0400007C RID: 124
		internal IntPtr nativeDisplay;

		// Token: 0x0400007D RID: 125
		public static Display[] displays = new Display[]
		{
			new Display()
		};

		// Token: 0x0400007E RID: 126
		private static Display _mainDisplay = Display.displays[0];

		// Token: 0x02000049 RID: 73
		// (Invoke) Token: 0x0600033E RID: 830
		public delegate void DisplaysUpdatedDelegate();
	}
}
