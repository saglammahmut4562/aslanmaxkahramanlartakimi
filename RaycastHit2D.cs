using System;

namespace UnityEngine
{
	// Token: 0x020000BD RID: 189
	public struct RaycastHit2D
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		public Collider2D collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00011ABC File Offset: 0x0000FCBC
		public Rigidbody2D rigidbody
		{
			get
			{
				return (!(this.collider != null)) ? null : this.collider.attachedRigidbody;
			}
		}

		// Token: 0x04000305 RID: 773
		private Vector2 m_Centroid;

		// Token: 0x04000306 RID: 774
		private Vector2 m_Point;

		// Token: 0x04000307 RID: 775
		private Vector2 m_Normal;

		// Token: 0x04000308 RID: 776
		private float m_Distance;

		// Token: 0x04000309 RID: 777
		private float m_Fraction;

		// Token: 0x0400030A RID: 778
		private Collider2D m_Collider;
	}
}
