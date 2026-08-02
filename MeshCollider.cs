using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200009A RID: 154
	public sealed class MeshCollider : Collider
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060006B0 RID: 1712
		// (set) Token: 0x060006B1 RID: 1713
		public extern Mesh sharedMesh
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000153 RID: 339
		// (set) Token: 0x060006B2 RID: 1714
		public extern bool convex
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000154 RID: 340
		// (set) Token: 0x060006B3 RID: 1715
		public extern bool smoothSphereCollisions
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}
	}
}
