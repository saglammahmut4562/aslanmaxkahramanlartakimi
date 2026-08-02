using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200005C RID: 92
	[StructLayout(0)]
	public sealed class Gradient
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x000093C4 File Offset: 0x000075C4
		public Gradient()
		{
			this.Init();
		}

		// Token: 0x06000409 RID: 1033
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x0600040A RID: 1034
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x0600040B RID: 1035 RVA: 0x000093D4 File Offset: 0x000075D4
		~Gradient()
		{
			this.Cleanup();
		}

		// Token: 0x0600040C RID: 1036
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Color Evaluate(float time);

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600040D RID: 1037
		// (set) Token: 0x0600040E RID: 1038
		public extern GradientColorKey[] colorKeys
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600040F RID: 1039
		// (set) Token: 0x06000410 RID: 1040
		public extern GradientAlphaKey[] alphaKeys
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000411 RID: 1041
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys);

		// Token: 0x040000CF RID: 207
		internal IntPtr m_Ptr;
	}
}
