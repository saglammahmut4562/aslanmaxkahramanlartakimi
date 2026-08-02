using System;

namespace System.ComponentModel
{
	// Token: 0x0200002A RID: 42
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00005020 File Offset: 0x00003220
		public DescriptionAttribute()
		{
			this.desc = string.Empty;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005040 File Offset: 0x00003240
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005048 File Offset: 0x00003248
		protected string DescriptionValue
		{
			get
			{
				return this.desc;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005050 File Offset: 0x00003250
		public override bool Equals(object obj)
		{
			return obj is DescriptionAttribute && (obj == this || ((DescriptionAttribute)obj).Description == this.desc);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005080 File Offset: 0x00003280
		public override int GetHashCode()
		{
			return this.desc.GetHashCode();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005090 File Offset: 0x00003290
		public override bool IsDefaultAttribute()
		{
			return this == DescriptionAttribute.Default;
		}

		// Token: 0x04000061 RID: 97
		private string desc;

		// Token: 0x04000062 RID: 98
		public static readonly DescriptionAttribute Default = new DescriptionAttribute();
	}
}
