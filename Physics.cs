using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000AD RID: 173
	public class Physics
	{
		// Token: 0x060006F3 RID: 1779
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_gravity(out Vector3 value);

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00011464 File Offset: 0x0000F664
		public static Vector3 gravity
		{
			get
			{
				Vector3 vector;
				Physics.INTERNAL_get_gravity(out vector);
				return vector;
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001147C File Offset: 0x0000F67C
		private static bool Internal_Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float distance, int layermask)
		{
			return Physics.INTERNAL_CALL_Internal_Raycast(ref origin, ref direction, out hitInfo, distance, layermask);
		}

		// Token: 0x060006F6 RID: 1782
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_Internal_Raycast(ref Vector3 origin, ref Vector3 direction, out RaycastHit hitInfo, float distance, int layermask);

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001148C File Offset: 0x0000F68C
		private static bool Internal_RaycastTest(Vector3 origin, Vector3 direction, float distance, int layermask)
		{
			return Physics.INTERNAL_CALL_Internal_RaycastTest(ref origin, ref direction, distance, layermask);
		}

		// Token: 0x060006F8 RID: 1784
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_Internal_RaycastTest(ref Vector3 origin, ref Vector3 direction, float distance, int layermask);

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001149C File Offset: 0x0000F69C
		public static bool Raycast(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Internal_RaycastTest(origin, direction, distance, layerMask);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000114A8 File Offset: 0x0000F6A8
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Internal_Raycast(origin, direction, out hitInfo, distance, layerMask);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x000114B8 File Offset: 0x0000F6B8
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, float distance)
		{
			int num = -5;
			return Physics.Raycast(ray, distance, num);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000114D0 File Offset: 0x0000F6D0
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.Raycast(ray, positiveInfinity, num);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000114F0 File Offset: 0x0000F6F0
		public static bool Raycast(Ray ray, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Raycast(ray.origin, ray.direction, distance, layerMask);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00011508 File Offset: 0x0000F708
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.Raycast(ray, out hitInfo, positiveInfinity, num);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00011528 File Offset: 0x0000F728
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Raycast(ray.origin, ray.direction, out hitInfo, distance, layerMask);
		}

		// Token: 0x040002DE RID: 734
		public const int kIgnoreRaycastLayer = 4;

		// Token: 0x040002DF RID: 735
		public const int kDefaultRaycastLayers = -5;

		// Token: 0x040002E0 RID: 736
		public const int kAllLayers = -1;

		// Token: 0x040002E1 RID: 737
		public const int IgnoreRaycastLayer = 4;

		// Token: 0x040002E2 RID: 738
		public const int DefaultRaycastLayers = -5;

		// Token: 0x040002E3 RID: 739
		public const int AllLayers = -1;
	}
}
