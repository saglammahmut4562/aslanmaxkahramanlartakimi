using System;

namespace UnityEngine
{
	// Token: 0x02000078 RID: 120
	internal sealed class GUIWordWrapSizer : GUILayoutEntry
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x0000FD58 File Offset: 0x0000DF58
		public GUIWordWrapSizer(GUIStyle _style, GUIContent _content, GUILayoutOption[] options)
			: base(0f, 0f, 0f, 0f, _style)
		{
			this.content = new GUIContent(_content);
			base.ApplyOptions(options);
			this.forcedMinHeight = this.minHeight;
			this.forcedMaxHeight = this.maxHeight;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		public override void CalcWidth()
		{
			if (this.minWidth == 0f || this.maxWidth == 0f)
			{
				float num;
				float num2;
				base.style.CalcMinMaxWidth(this.content, out num, out num2);
				if (this.minWidth == 0f)
				{
					this.minWidth = num;
				}
				if (this.maxWidth == 0f)
				{
					this.maxWidth = num2;
				}
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000FE1C File Offset: 0x0000E01C
		public override void CalcHeight()
		{
			if (this.forcedMinHeight == 0f || this.forcedMaxHeight == 0f)
			{
				float num = base.style.CalcHeight(this.content, this.rect.width);
				if (this.forcedMinHeight == 0f)
				{
					this.minHeight = num;
				}
				else
				{
					this.minHeight = this.forcedMinHeight;
				}
				if (this.forcedMaxHeight == 0f)
				{
					this.maxHeight = num;
				}
				else
				{
					this.maxHeight = this.forcedMaxHeight;
				}
			}
		}

		// Token: 0x04000167 RID: 359
		private GUIContent content;

		// Token: 0x04000168 RID: 360
		private float forcedMinHeight;

		// Token: 0x04000169 RID: 361
		private float forcedMaxHeight;
	}
}
