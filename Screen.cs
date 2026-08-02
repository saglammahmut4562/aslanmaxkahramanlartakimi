using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000CF RID: 207
	public sealed class Screen
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060007A8 RID: 1960
		public static extern int width
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060007A9 RID: 1961
		public static extern int height
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007AA RID: 1962
		public static extern ScreenOrientation orientation
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}
	}
}
