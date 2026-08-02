using System;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	public struct Bounds
	{
		// Token: 0x0600028C RID: 652 RVA: 0x000073B8 File Offset: 0x000055B8
		public Bounds(Vector3 center, Vector3 size)
		{
			this.m_Center = center;
			this.m_Extents = size * 0.5f;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000073D4 File Offset: 0x000055D4
		public override int GetHashCode()
		{
			return this.center.GetHashCode() ^ (this.extents.GetHashCode() << 2);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007400 File Offset: 0x00005600
		public override bool Equals(object other)
		{
			if (!(other is Bounds))
			{
				return false;
			}
			Bounds bounds = (Bounds)other;
			return this.center.Equals(bounds.center) && this.extents.Equals(bounds.extents);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00007460 File Offset: 0x00005660
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00007468 File Offset: 0x00005668
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00007474 File Offset: 0x00005674
		public Vector3 size
		{
			get
			{
				return this.m_Extents * 2f;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00007488 File Offset: 0x00005688
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00007490 File Offset: 0x00005690
		public Vector3 extents
		{
			get
			{
				return this.m_Extents;
			}
			set
			{
				this.m_Extents = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000749C File Offset: 0x0000569C
		public Vector3 min
		{
			get
			{
				return this.center - this.extents;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000074B0 File Offset: 0x000056B0
		public Vector3 max
		{
			get
			{
				return this.center + this.extents;
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000074C4 File Offset: 0x000056C4
		public void SetMinMax(Vector3 min, Vector3 max)
		{
			this.extents = (max - min) * 0.5f;
			this.center = min + this.extents;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000074F0 File Offset: 0x000056F0
		public override string ToString()
		{
			return UnityString.Format("Center: {0}, Extents: {1}", new object[] { this.m_Center, this.m_Extents });
		}

		// Token: 0x04000040 RID: 64
		private Vector3 m_Center;

		// Token: 0x04000041 RID: 65
		private Vector3 m_Extents;
	}
}
