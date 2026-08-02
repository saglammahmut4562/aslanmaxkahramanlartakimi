using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000036 RID: 54
	public class Collider : Component
	{
		// Token: 0x17000082 RID: 130
		// (set) Token: 0x060002D7 RID: 727
		public extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002D8 RID: 728
		public extern Rigidbody attachedRigidbody
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000084 RID: 132
		// (set) Token: 0x060002D9 RID: 729
		public extern PhysicMaterial sharedMaterial
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060002DA RID: 730
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000763C File Offset: 0x0000583C
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_bounds(out bounds);
				return bounds;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00007654 File Offset: 0x00005854
		private static bool Internal_Raycast(Collider col, Ray ray, out RaycastHit hitInfo, float distance)
		{
			return Collider.INTERNAL_CALL_Internal_Raycast(col, ref ray, out hitInfo, distance);
		}

		// Token: 0x060002DD RID: 733
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_Internal_Raycast(Collider col, ref Ray ray, out RaycastHit hitInfo, float distance);

		// Token: 0x060002DE RID: 734 RVA: 0x00007660 File Offset: 0x00005860
		public bool Raycast(Ray ray, out RaycastHit hitInfo, float distance)
		{
			return Collider.Internal_Raycast(this, ray, out hitInfo, distance);
		}
	}
}
