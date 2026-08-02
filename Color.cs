using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	[DefaultMember("Item")]
	public struct Color
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x0000766C File Offset: 0x0000586C
		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000768C File Offset: 0x0000588C
		public Color(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = 1f;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000076B0 File Offset: 0x000058B0
		public override string ToString()
		{
			return UnityString.Format("RGBA({0:F3}, {1:F3}, {2:F3}, {3:F3})", new object[] { this.r, this.g, this.b, this.a });
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00007708 File Offset: 0x00005908
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00007728 File Offset: 0x00005928
		public override bool Equals(object other)
		{
			if (!(other is Color))
			{
				return false;
			}
			Color color = (Color)other;
			return this.r.Equals(color.r) && this.g.Equals(color.g) && this.b.Equals(color.b) && this.a.Equals(color.a);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000077A4 File Offset: 0x000059A4
		public static Color Lerp(Color a, Color b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00007824 File Offset: 0x00005A24
		public static Color red
		{
			get
			{
				return new Color(1f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00007840 File Offset: 0x00005A40
		public static Color green
		{
			get
			{
				return new Color(0f, 1f, 0f, 1f);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000785C File Offset: 0x00005A5C
		public static Color white
		{
			get
			{
				return new Color(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00007878 File Offset: 0x00005A78
		public static Color black
		{
			get
			{
				return new Color(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00007894 File Offset: 0x00005A94
		public static Color yellow
		{
			get
			{
				return new Color(1f, 0.92156863f, 0.015686275f, 1f);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000078B0 File Offset: 0x00005AB0
		public static Color cyan
		{
			get
			{
				return new Color(0f, 1f, 1f, 1f);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000078CC File Offset: 0x00005ACC
		public static Color grey
		{
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000078E8 File Offset: 0x00005AE8
		public static Color clear
		{
			get
			{
				return new Color(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00007904 File Offset: 0x00005B04
		public static Color operator *(float b, Color a)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00007930 File Offset: 0x00005B30
		public static bool operator !=(Color lhs, Color rhs)
		{
			return lhs != rhs;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00007944 File Offset: 0x00005B44
		public static implicit operator Vector4(Color c)
		{
			return new Vector4(c.r, c.g, c.b, c.a);
		}

		// Token: 0x04000056 RID: 86
		public float r;

		// Token: 0x04000057 RID: 87
		public float g;

		// Token: 0x04000058 RID: 88
		public float b;

		// Token: 0x04000059 RID: 89
		public float a;
	}
}
