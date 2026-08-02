using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	[StructLayout(0)]
	public class Collision
	{
		// Token: 0x04000052 RID: 82
		internal Vector3 m_RelativeVelocity;

		// Token: 0x04000053 RID: 83
		internal Rigidbody m_Rigidbody;

		// Token: 0x04000054 RID: 84
		internal Collider m_Collider;

		// Token: 0x04000055 RID: 85
		internal ContactPoint[] m_Contacts;
	}
}
