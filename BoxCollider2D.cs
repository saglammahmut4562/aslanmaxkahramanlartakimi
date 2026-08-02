using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200002E RID: 46
	public sealed class BoxCollider2D : Collider2D
	{
		// Token: 0x060002A0 RID: 672
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_center(ref Vector2 value);

		// Token: 0x17000069 RID: 105
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00007568 File Offset: 0x00005768
		public Vector2 center
		{
			set
			{
				this.INTERNAL_set_center(ref value);
			}
		}

		// Token: 0x060002A2 RID: 674
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_size(ref Vector2 value);

		// Token: 0x1700006A RID: 106
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x00007574 File Offset: 0x00005774
		public Vector2 size
		{
			set
			{
				this.INTERNAL_set_size(ref value);
			}
		}
	}
}
