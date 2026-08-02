using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x02000028 RID: 40
	public class DecimalConverter : BaseNumberConverter
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004F0C File Offset: 0x0000310C
		internal override bool SupportHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004F10 File Offset: 0x00003110
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004F2C File Offset: 0x0000312C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor) && value is decimal)
			{
				decimal num = (decimal)value;
				ConstructorInfo constructor = typeof(decimal).GetConstructor(new Type[] { typeof(int[]) });
				return new global::System.ComponentModel.Design.Serialization.InstanceDescriptor(constructor, new object[] { decimal.GetBits(num) });
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004FA0 File Offset: 0x000031A0
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return decimal.Parse(value, NumberStyles.Float, format);
		}
	}
}
