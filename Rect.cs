using System;

namespace UnityEngine
{
	// Token: 0x020000BE RID: 190
	public struct Rect
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x00011AE0 File Offset: 0x0000FCE0
		public Rect(float left, float top, float width, float height)
		{
			this.m_XMin = left;
			this.m_YMin = top;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00011B00 File Offset: 0x0000FD00
		public Rect(Rect source)
		{
			this.m_XMin = source.m_XMin;
			this.m_YMin = source.m_YMin;
			this.m_Width = source.m_Width;
			this.m_Height = source.m_Height;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00011B38 File Offset: 0x0000FD38
		public static Rect MinMaxRect(float left, float top, float right, float bottom)
		{
			return new Rect(left, top, right - left, bottom - top);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00011B48 File Offset: 0x0000FD48
		public void Set(float left, float top, float width, float height)
		{
			this.m_XMin = left;
			this.m_YMin = top;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00011B68 File Offset: 0x0000FD68
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00011B70 File Offset: 0x0000FD70
		public float x
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				this.m_XMin = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00011B7C File Offset: 0x0000FD7C
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00011B84 File Offset: 0x0000FD84
		public float y
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				this.m_YMin = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00011B90 File Offset: 0x0000FD90
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00011B98 File Offset: 0x0000FD98
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00011BA4 File Offset: 0x0000FDA4
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x00011BAC File Offset: 0x0000FDAC
		public float height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00011BB8 File Offset: 0x0000FDB8
		public float xMin
		{
			get
			{
				return this.m_XMin;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00011BC0 File Offset: 0x0000FDC0
		public float yMin
		{
			get
			{
				return this.m_YMin;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00011BC8 File Offset: 0x0000FDC8
		public float xMax
		{
			get
			{
				return this.m_Width + this.m_XMin;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00011BD8 File Offset: 0x0000FDD8
		public float yMax
		{
			get
			{
				return this.m_Height + this.m_YMin;
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		public override string ToString()
		{
			return UnityString.Format("(x:{0:F2}, y:{1:F2}, width:{2:F2}, height:{3:F2})", new object[] { this.x, this.y, this.width, this.height });
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00011C40 File Offset: 0x0000FE40
		public bool Contains(Vector2 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00011C98 File Offset: 0x0000FE98
		public bool Contains(Vector3 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.width.GetHashCode() << 2) ^ (this.y.GetHashCode() >> 2) ^ (this.height.GetHashCode() >> 1);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00011D40 File Offset: 0x0000FF40
		public override bool Equals(object other)
		{
			if (!(other is Rect))
			{
				return false;
			}
			Rect rect = (Rect)other;
			return this.x.Equals(rect.x) && this.y.Equals(rect.y) && this.width.Equals(rect.width) && this.height.Equals(rect.height);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00011DC8 File Offset: 0x0000FFC8
		public static bool operator !=(Rect lhs, Rect rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.width != rhs.width || lhs.height != rhs.height;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00011E24 File Offset: 0x00010024
		public static bool operator ==(Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x0400030B RID: 779
		private float m_XMin;

		// Token: 0x0400030C RID: 780
		private float m_YMin;

		// Token: 0x0400030D RID: 781
		private float m_Width;

		// Token: 0x0400030E RID: 782
		private float m_Height;
	}
}
