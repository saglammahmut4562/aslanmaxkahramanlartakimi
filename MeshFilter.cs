using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200009B RID: 155
	public sealed class MeshFilter : Component
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006B4 RID: 1716
		// (set) Token: 0x060006B5 RID: 1717
		public extern Mesh mesh
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006B6 RID: 1718
		// (set) Token: 0x060006B7 RID: 1719
		public extern Mesh sharedMesh
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}
	}
}
