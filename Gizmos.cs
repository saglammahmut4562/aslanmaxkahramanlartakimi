using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200005A RID: 90
	public sealed class Gizmos
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x00009348 File Offset: 0x00007548
		public static void DrawWireCube(Vector3 center, Vector3 size)
		{
			Gizmos.INTERNAL_CALL_DrawWireCube(ref center, ref size);
		}

		// Token: 0x060003EF RID: 1007
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DrawWireCube(ref Vector3 center, ref Vector3 size);

		// Token: 0x060003F0 RID: 1008 RVA: 0x00009354 File Offset: 0x00007554
		public static void DrawCube(Vector3 center, Vector3 size)
		{
			Gizmos.INTERNAL_CALL_DrawCube(ref center, ref size);
		}

		// Token: 0x060003F1 RID: 1009
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DrawCube(ref Vector3 center, ref Vector3 size);

		// Token: 0x060003F2 RID: 1010
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_color(ref Color value);

		// Token: 0x170000DA RID: 218
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00009360 File Offset: 0x00007560
		public static Color color
		{
			set
			{
				Gizmos.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x060003F4 RID: 1012
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_matrix(ref Matrix4x4 value);

		// Token: 0x170000DB RID: 219
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000936C File Offset: 0x0000756C
		public static Matrix4x4 matrix
		{
			set
			{
				Gizmos.INTERNAL_set_matrix(ref value);
			}
		}
	}
}
