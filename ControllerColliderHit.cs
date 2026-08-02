using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000043 RID: 67
	[StructLayout(0)]
	public sealed class ControllerColliderHit
	{
		// Token: 0x04000070 RID: 112
		internal CharacterController m_Controller;

		// Token: 0x04000071 RID: 113
		internal Collider m_Collider;

		// Token: 0x04000072 RID: 114
		internal Vector3 m_Point;

		// Token: 0x04000073 RID: 115
		internal Vector3 m_Normal;

		// Token: 0x04000074 RID: 116
		internal Vector3 m_MoveDirection;

		// Token: 0x04000075 RID: 117
		internal float m_MoveLength;

		// Token: 0x04000076 RID: 118
		internal int m_Push;
	}
}
