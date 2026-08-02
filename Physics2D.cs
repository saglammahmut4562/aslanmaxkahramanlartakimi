using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000AE RID: 174
	public class Physics2D
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x0001154C File Offset: 0x0000F74C
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.INTERNAL_CALL_GetRayIntersectionNonAlloc(ref ray, results, distance, layerMask);
		}

		// Token: 0x06000702 RID: 1794
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int INTERNAL_CALL_GetRayIntersectionNonAlloc(ref Ray ray, RaycastHit2D[] results, float distance, int layerMask);

		// Token: 0x06000703 RID: 1795 RVA: 0x00011558 File Offset: 0x0000F758
		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPoint(ref point, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06000704 RID: 1796
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Collider2D INTERNAL_CALL_OverlapPoint(ref Vector2 point, int layerMask, float minDepth, float maxDepth);

		// Token: 0x040002E4 RID: 740
		public const int IgnoreRaycastLayer = 4;

		// Token: 0x040002E5 RID: 741
		public const int DefaultRaycastLayers = -5;

		// Token: 0x040002E6 RID: 742
		public const int AllLayers = -1;

		// Token: 0x040002E7 RID: 743
		private static List<Rigidbody2D> m_LastDisabledRigidbody2D = new List<Rigidbody2D>();
	}
}
