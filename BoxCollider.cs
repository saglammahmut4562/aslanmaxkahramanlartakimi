using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	public sealed class BoxCollider : Collider
	{
		// Token: 0x06000298 RID: 664
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_center(out Vector3 value);

		// Token: 0x06000299 RID: 665
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_center(ref Vector3 value);

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00007520 File Offset: 0x00005720
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00007538 File Offset: 0x00005738
		public Vector3 center
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_center(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_center(ref value);
			}
		}

		// Token: 0x0600029C RID: 668
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_size(out Vector3 value);

		// Token: 0x0600029D RID: 669
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_size(ref Vector3 value);

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00007544 File Offset: 0x00005744
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000755C File Offset: 0x0000575C
		public Vector3 size
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_size(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_size(ref value);
			}
		}
	}
}
