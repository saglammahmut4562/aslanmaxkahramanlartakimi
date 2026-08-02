using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000037 RID: 55
	public class Collider2D : Behaviour
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002DF RID: 735
		public extern Rigidbody2D attachedRigidbody
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000087 RID: 135
		// (set) Token: 0x060002E0 RID: 736
		public extern PhysicsMaterial2D sharedMaterial
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}
	}
}
