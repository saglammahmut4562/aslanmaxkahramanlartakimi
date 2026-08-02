using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200004A RID: 74
	public sealed class EdgeCollider2D : Collider2D
	{
		// Token: 0x170000A4 RID: 164
		// (set) Token: 0x06000341 RID: 833
		public extern Vector2[] points
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}
	}
}
