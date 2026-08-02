using System;

namespace UnityEngine
{
	// Token: 0x020000BB RID: 187
	public struct Ray
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x00011A20 File Offset: 0x0000FC20
		public Ray(Vector3 origin, Vector3 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00011A38 File Offset: 0x0000FC38
		public Vector3 origin
		{
			get
			{
				return this.m_Origin;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00011A40 File Offset: 0x0000FC40
		public Vector3 direction
		{
			get
			{
				return this.m_Direction;
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00011A48 File Offset: 0x0000FC48
		public override string ToString()
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[] { this.m_Origin, this.m_Direction });
		}

		// Token: 0x040002FD RID: 765
		private Vector3 m_Origin;

		// Token: 0x040002FE RID: 766
		private Vector3 m_Direction;
	}
}
