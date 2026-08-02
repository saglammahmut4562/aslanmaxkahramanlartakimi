using System;

namespace UnityEngine
{
	// Token: 0x0200003A RID: 58
	public struct Color32
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x00007968 File Offset: 0x00005B68
		public Color32(byte r, byte g, byte b, byte a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00007988 File Offset: 0x00005B88
		public override string ToString()
		{
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[] { this.r, this.g, this.b, this.a });
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000079E0 File Offset: 0x00005BE0
		public static implicit operator Color32(Color c)
		{
			return new Color32((byte)(Mathf.Clamp01(c.r) * 255f), (byte)(Mathf.Clamp01(c.g) * 255f), (byte)(Mathf.Clamp01(c.b) * 255f), (byte)(Mathf.Clamp01(c.a) * 255f));
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00007A40 File Offset: 0x00005C40
		public static implicit operator Color(Color32 c)
		{
			return new Color((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
		}

		// Token: 0x0400005A RID: 90
		public byte r;

		// Token: 0x0400005B RID: 91
		public byte g;

		// Token: 0x0400005C RID: 92
		public byte b;

		// Token: 0x0400005D RID: 93
		public byte a;
	}
}
