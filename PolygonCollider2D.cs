using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B3 RID: 179
	public sealed class PolygonCollider2D : Collider2D
	{
		// Token: 0x17000162 RID: 354
		// (set) Token: 0x0600070D RID: 1805
		public extern Vector2[] points
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}
	}
}
