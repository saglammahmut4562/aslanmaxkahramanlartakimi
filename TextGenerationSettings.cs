using System;

namespace UnityEngine
{
	// Token: 0x020000FF RID: 255
	public struct TextGenerationSettings
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x000166F0 File Offset: 0x000148F0
		private bool CompareColors(Color left, Color right)
		{
			Color32 color = left;
			Color32 color2 = right;
			return color.Equals(color2);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001671C File Offset: 0x0001491C
		private bool CompareVector2(Vector2 left, Vector2 right)
		{
			return Mathf.Approximately(left.x, right.x) && Mathf.Approximately(left.y, right.y);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001674C File Offset: 0x0001494C
		public bool Equals(TextGenerationSettings other)
		{
			return this.CompareColors(this.color, other.color) && this.size == other.size && this.style == other.style && this.richText == other.richText && this.anchor == other.anchor && this.wrapMode == other.wrapMode && this.CompareVector2(this.extents, other.extents) && this.CompareVector2(this.pivot, other.pivot) && this.font == other.font;
		}

		// Token: 0x0400043E RID: 1086
		public Color color;

		// Token: 0x0400043F RID: 1087
		public int size;

		// Token: 0x04000440 RID: 1088
		public FontStyle style;

		// Token: 0x04000441 RID: 1089
		public bool richText;

		// Token: 0x04000442 RID: 1090
		public TextAnchor anchor;

		// Token: 0x04000443 RID: 1091
		public TextWrapMode wrapMode;

		// Token: 0x04000444 RID: 1092
		public Vector2 extents;

		// Token: 0x04000445 RID: 1093
		public Vector2 pivot;

		// Token: 0x04000446 RID: 1094
		public Font font;
	}
}
