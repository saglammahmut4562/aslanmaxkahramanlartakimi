using System;

namespace UnityEngine
{
	// Token: 0x02000042 RID: 66
	public struct ContactPoint
	{
		// Token: 0x0400006C RID: 108
		internal Vector3 m_Point;

		// Token: 0x0400006D RID: 109
		internal Vector3 m_Normal;

		// Token: 0x0400006E RID: 110
		internal Collider m_ThisCollider;

		// Token: 0x0400006F RID: 111
		internal Collider m_OtherCollider;
	}
}
