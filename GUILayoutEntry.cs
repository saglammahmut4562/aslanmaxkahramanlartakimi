using System;

namespace UnityEngine
{
	// Token: 0x02000068 RID: 104
	internal class GUILayoutEntry
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			if (_style == null)
			{
				_style = GUIStyle.none;
			}
			this.style = _style;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000C52C File Offset: 0x0000A72C
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style, GUILayoutOption[] options)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			this.style = _style;
			this.ApplyOptions(options);
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
		public GUIStyle style
		{
			get
			{
				return this.m_Style;
			}
			set
			{
				this.m_Style = value;
				this.ApplyStyleSettings(value);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		public virtual RectOffset margin
		{
			get
			{
				return this.style.margin;
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
		public virtual void CalcWidth()
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		public virtual void CalcHeight()
		{
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		public virtual void SetHorizontal(float x, float width)
		{
			this.rect.x = x;
			this.rect.width = width;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000C60C File Offset: 0x0000A80C
		public virtual void SetVertical(float y, float height)
		{
			this.rect.y = y;
			this.rect.height = height;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000C628 File Offset: 0x0000A828
		protected virtual void ApplyStyleSettings(GUIStyle style)
		{
			this.stretchWidth = ((style.fixedWidth != 0f || !style.stretchWidth) ? 0 : 1);
			this.stretchHeight = ((style.fixedHeight != 0f || !style.stretchHeight) ? 0 : 1);
			this.m_Style = style;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000C68C File Offset: 0x0000A88C
		public virtual void ApplyOptions(GUILayoutOption[] options)
		{
			if (options == null)
			{
				return;
			}
			foreach (GUILayoutOption guilayoutOption in options)
			{
				switch (guilayoutOption.type)
				{
				case GUILayoutOption.Type.fixedWidth:
					this.minWidth = (this.maxWidth = (float)guilayoutOption.value);
					this.stretchWidth = 0;
					break;
				case GUILayoutOption.Type.fixedHeight:
					this.minHeight = (this.maxHeight = (float)guilayoutOption.value);
					this.stretchHeight = 0;
					break;
				case GUILayoutOption.Type.minWidth:
					this.minWidth = (float)guilayoutOption.value;
					if (this.maxWidth < this.minWidth)
					{
						this.maxWidth = this.minWidth;
					}
					break;
				case GUILayoutOption.Type.maxWidth:
					this.maxWidth = (float)guilayoutOption.value;
					if (this.minWidth > this.maxWidth)
					{
						this.minWidth = this.maxWidth;
					}
					this.stretchWidth = 0;
					break;
				case GUILayoutOption.Type.minHeight:
					this.minHeight = (float)guilayoutOption.value;
					if (this.maxHeight < this.minHeight)
					{
						this.maxHeight = this.minHeight;
					}
					break;
				case GUILayoutOption.Type.maxHeight:
					this.maxHeight = (float)guilayoutOption.value;
					if (this.minHeight > this.maxHeight)
					{
						this.minHeight = this.maxHeight;
					}
					this.stretchHeight = 0;
					break;
				case GUILayoutOption.Type.stretchWidth:
					this.stretchWidth = (int)guilayoutOption.value;
					break;
				case GUILayoutOption.Type.stretchHeight:
					this.stretchHeight = (int)guilayoutOption.value;
					break;
				}
			}
			if (this.maxWidth != 0f && this.maxWidth < this.minWidth)
			{
				this.maxWidth = this.minWidth;
			}
			if (this.maxHeight != 0f && this.maxHeight < this.minHeight)
			{
				this.maxHeight = this.minHeight;
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000C89C File Offset: 0x0000AA9C
		public override string ToString()
		{
			string text = string.Empty;
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text += " ";
			}
			return string.Concat(new object[]
			{
				text,
				UnityString.Format("{1}-{0} (x:{2}-{3}, y:{4}-{5})", new object[]
				{
					(this.style == null) ? "NULL" : this.style.name,
					base.GetType(),
					this.rect.x,
					this.rect.xMax,
					this.rect.y,
					this.rect.yMax
				}),
				"   -   W: ",
				this.minWidth,
				"-",
				this.maxWidth,
				(this.stretchWidth == 0) ? string.Empty : "+",
				", H: ",
				this.minHeight,
				"-",
				this.maxHeight,
				(this.stretchHeight == 0) ? string.Empty : "+"
			});
		}

		// Token: 0x040000F0 RID: 240
		public float minWidth;

		// Token: 0x040000F1 RID: 241
		public float maxWidth;

		// Token: 0x040000F2 RID: 242
		public float minHeight;

		// Token: 0x040000F3 RID: 243
		public float maxHeight;

		// Token: 0x040000F4 RID: 244
		public Rect rect = new Rect(0f, 0f, 0f, 0f);

		// Token: 0x040000F5 RID: 245
		public int stretchWidth;

		// Token: 0x040000F6 RID: 246
		public int stretchHeight;

		// Token: 0x040000F7 RID: 247
		private GUIStyle m_Style = GUIStyle.none;

		// Token: 0x040000F8 RID: 248
		internal static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040000F9 RID: 249
		protected static int indent = 0;
	}
}
