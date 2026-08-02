using System;

namespace System.ComponentModel
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.All)]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004FB4 File Offset: 0x000031B4
		public virtual object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004FBC File Offset: 0x000031BC
		public override bool Equals(object obj)
		{
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.DefaultValue == null)
			{
				return defaultValueAttribute.Value == null;
			}
			return this.DefaultValue.Equals(defaultValueAttribute.Value);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005000 File Offset: 0x00003200
		public override int GetHashCode()
		{
			if (this.DefaultValue == null)
			{
				return base.GetHashCode();
			}
			return this.DefaultValue.GetHashCode();
		}

		// Token: 0x04000060 RID: 96
		private object DefaultValue;
	}
}
