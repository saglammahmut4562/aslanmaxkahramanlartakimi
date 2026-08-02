using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200010B RID: 267
	public sealed class Time
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008E1 RID: 2273
		public static extern float time
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060008E2 RID: 2274
		public static extern float deltaTime
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060008E3 RID: 2275
		public static extern float maximumDeltaTime
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060008E4 RID: 2276
		// (set) Token: 0x060008E5 RID: 2277
		public static extern float timeScale
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060008E6 RID: 2278
		public static extern int frameCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060008E7 RID: 2279
		public static extern float realtimeSinceStartup
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}
	}
}
