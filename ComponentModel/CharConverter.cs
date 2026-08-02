using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000021 RID: 33
	public class CharConverter : TypeConverter
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00004808 File Offset: 0x00002A08
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004824 File Offset: 0x00002A24
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (text.Length > 1)
			{
				text = text.Trim();
			}
			if (text.Length > 1)
			{
				throw new FormatException(string.Format("String {0} is not a valid Char: it has to be less than or equal to one char long.", text));
			}
			if (text.Length == 0)
			{
				return '\0';
			}
			return text[0];
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004898 File Offset: 0x00002A98
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string) || value == null || !(value is char))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			char c = (char)value;
			if (c == '\0')
			{
				return string.Empty;
			}
			return new string(c, 1);
		}
	}
}
