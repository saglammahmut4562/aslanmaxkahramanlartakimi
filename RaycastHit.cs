using System;

namespace UnityEngine
{
	// Token: 0x020000BC RID: 188
	public struct RaycastHit
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00011A78 File Offset: 0x0000FC78
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00011A80 File Offset: 0x0000FC80
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00011A88 File Offset: 0x0000FC88
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00011A90 File Offset: 0x0000FC90
		public Rigidbody rigidbody
		{
			get
			{
				return (!(this.collider != null)) ? null : this.collider.attachedRigidbody;
			}
		}

		// Token: 0x040002FF RID: 767
		private Vector3 m_Point;

		// Token: 0x04000300 RID: 768
		private Vector3 m_Normal;

		// Token: 0x04000301 RID: 769
		private int m_FaceID;

		// Token: 0x04000302 RID: 770
		private float m_Distance;

		// Token: 0x04000303 RID: 771
		private Vector2 m_UV;

		// Token: 0x04000304 RID: 772
		private Collider m_Collider;
	}
}
