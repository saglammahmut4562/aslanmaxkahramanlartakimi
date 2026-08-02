using System;

namespace UnityEngine
{
	// Token: 0x0200006E RID: 110
	internal sealed class GUIScrollGroup : GUILayoutGroup
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x0000E694 File Offset: 0x0000C894
		public override void CalcWidth()
		{
			float minWidth = this.minWidth;
			float maxWidth = this.maxWidth;
			if (this.allowHorizontalScroll)
			{
				this.minWidth = 0f;
				this.maxWidth = 0f;
			}
			base.CalcWidth();
			this.calcMinWidth = this.minWidth;
			this.calcMaxWidth = this.maxWidth;
			if (this.allowHorizontalScroll)
			{
				if (this.minWidth > 32f)
				{
					this.minWidth = 32f;
				}
				if (minWidth != 0f)
				{
					this.minWidth = minWidth;
				}
				if (maxWidth != 0f)
				{
					this.maxWidth = maxWidth;
					this.stretchWidth = 0;
				}
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000E740 File Offset: 0x0000C940
		public override void SetHorizontal(float x, float width)
		{
			float num = ((!this.needsVerticalScrollbar) ? width : (width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left));
			if (this.allowHorizontalScroll && num < this.calcMinWidth)
			{
				this.needsHorizontalScrollbar = true;
				this.minWidth = this.calcMinWidth;
				this.maxWidth = this.calcMaxWidth;
				base.SetHorizontal(x, this.calcMinWidth);
				this.rect.width = width;
				this.clientWidth = this.calcMinWidth;
			}
			else
			{
				this.needsHorizontalScrollbar = false;
				if (this.allowHorizontalScroll)
				{
					this.minWidth = this.calcMinWidth;
					this.maxWidth = this.calcMaxWidth;
				}
				base.SetHorizontal(x, num);
				this.rect.width = width;
				this.clientWidth = num;
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000E824 File Offset: 0x0000CA24
		public override void CalcHeight()
		{
			float minHeight = this.minHeight;
			float maxHeight = this.maxHeight;
			if (this.allowVerticalScroll)
			{
				this.minHeight = 0f;
				this.maxHeight = 0f;
			}
			base.CalcHeight();
			this.calcMinHeight = this.minHeight;
			this.calcMaxHeight = this.maxHeight;
			if (this.needsHorizontalScrollbar)
			{
				float num = this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
				this.minHeight += num;
				this.maxHeight += num;
			}
			if (this.allowVerticalScroll)
			{
				if (this.minHeight > 32f)
				{
					this.minHeight = 32f;
				}
				if (minHeight != 0f)
				{
					this.minHeight = minHeight;
				}
				if (maxHeight != 0f)
				{
					this.maxHeight = maxHeight;
					this.stretchHeight = 0;
				}
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000E914 File Offset: 0x0000CB14
		public override void SetVertical(float y, float height)
		{
			float num = height;
			if (this.needsHorizontalScrollbar)
			{
				num -= this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
			}
			if (this.allowVerticalScroll && num < this.calcMinHeight)
			{
				if (!this.needsHorizontalScrollbar && !this.needsVerticalScrollbar)
				{
					this.clientWidth = this.rect.width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left;
					if (this.clientWidth < this.calcMinWidth)
					{
						this.clientWidth = this.calcMinWidth;
					}
					float width = this.rect.width;
					this.SetHorizontal(this.rect.x, this.clientWidth);
					this.CalcHeight();
					this.rect.width = width;
				}
				float minHeight = this.minHeight;
				float maxHeight = this.maxHeight;
				this.minHeight = this.calcMinHeight;
				this.maxHeight = this.calcMaxHeight;
				base.SetVertical(y, this.calcMinHeight);
				this.minHeight = minHeight;
				this.maxHeight = maxHeight;
				this.rect.height = height;
				this.clientHeight = this.calcMinHeight;
			}
			else
			{
				if (this.allowVerticalScroll)
				{
					this.minHeight = this.calcMinHeight;
					this.maxHeight = this.calcMaxHeight;
				}
				base.SetVertical(y, num);
				this.rect.height = height;
				this.clientHeight = num;
			}
		}

		// Token: 0x04000124 RID: 292
		public float calcMinWidth;

		// Token: 0x04000125 RID: 293
		public float calcMaxWidth;

		// Token: 0x04000126 RID: 294
		public float calcMinHeight;

		// Token: 0x04000127 RID: 295
		public float calcMaxHeight;

		// Token: 0x04000128 RID: 296
		public float clientWidth;

		// Token: 0x04000129 RID: 297
		public float clientHeight;

		// Token: 0x0400012A RID: 298
		public bool allowHorizontalScroll = true;

		// Token: 0x0400012B RID: 299
		public bool allowVerticalScroll = true;

		// Token: 0x0400012C RID: 300
		public bool needsHorizontalScrollbar;

		// Token: 0x0400012D RID: 301
		public bool needsVerticalScrollbar;

		// Token: 0x0400012E RID: 302
		public GUIStyle horizontalScrollbar;

		// Token: 0x0400012F RID: 303
		public GUIStyle verticalScrollbar;
	}
}
