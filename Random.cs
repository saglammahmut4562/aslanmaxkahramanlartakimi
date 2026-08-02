using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B9 RID: 185
	public sealed class Random
	{
		// Token: 0x06000726 RID: 1830
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float Range(float min, float max);

		// Token: 0x06000727 RID: 1831 RVA: 0x000119FC File Offset: 0x0000FBFC
		public static int Range(int min, int max)
		{
			return Random.RandomRangeInt(min, max);
		}

		// Token: 0x06000728 RID: 1832
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int RandomRangeInt(int min, int max);

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000729 RID: 1833
		public static extern float value
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600072A RID: 1834
		public static extern Vector3 insideUnitSphere
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600072B RID: 1835
		public static extern Vector3 onUnitSphere
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}
	}
}
