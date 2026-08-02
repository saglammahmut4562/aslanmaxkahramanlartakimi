using System;

namespace UnityEngine
{
	// Token: 0x0200006A RID: 106
	public sealed class GUILayoutOption
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		internal GUILayoutOption(GUILayoutOption.Type type, object value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x0400010B RID: 267
		internal GUILayoutOption.Type type;

		// Token: 0x0400010C RID: 268
		internal object value;

		// Token: 0x0200006B RID: 107
		internal enum Type
		{
			// Token: 0x0400010E RID: 270
			fixedWidth,
			// Token: 0x0400010F RID: 271
			fixedHeight,
			// Token: 0x04000110 RID: 272
			minWidth,
			// Token: 0x04000111 RID: 273
			maxWidth,
			// Token: 0x04000112 RID: 274
			minHeight,
			// Token: 0x04000113 RID: 275
			maxHeight,
			// Token: 0x04000114 RID: 276
			stretchWidth,
			// Token: 0x04000115 RID: 277
			stretchHeight,
			// Token: 0x04000116 RID: 278
			alignStart,
			// Token: 0x04000117 RID: 279
			alignMiddle,
			// Token: 0x04000118 RID: 280
			alignEnd,
			// Token: 0x04000119 RID: 281
			alignJustify,
			// Token: 0x0400011A RID: 282
			equalSize,
			// Token: 0x0400011B RID: 283
			spacing
		}
	}
}
