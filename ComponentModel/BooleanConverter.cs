using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200001F RID: 31
	public class BooleanConverter : TypeConverter
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x000047A0 File Offset: 0x000029A0
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000047BC File Offset: 0x000029BC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return bool.Parse((string)value);
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
