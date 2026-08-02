using System;

namespace UnityEngine.Internal
{
	// Token: 0x02000084 RID: 132
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
	[Serializable]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x0000FF5C File Offset: 0x0000E15C
		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000FF6C File Offset: 0x0000E16C
		public object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000FF74 File Offset: 0x0000E174
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

		// Token: 0x0600061F RID: 1567 RVA: 0x0000FFB8 File Offset: 0x0000E1B8
		public override int GetHashCode()
		{
			if (this.DefaultValue == null)
			{
				return base.GetHashCode();
			}
			return this.DefaultValue.GetHashCode();
		}

		// Token: 0x04000182 RID: 386
		private object DefaultValue;
	}
}
