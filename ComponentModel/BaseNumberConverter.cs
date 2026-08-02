using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200001E RID: 30
	public abstract class BaseNumberConverter : TypeConverter
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000CF RID: 207
		internal abstract bool SupportHex { get; }

		// Token: 0x060000D0 RID: 208 RVA: 0x000045D4 File Offset: 0x000027D4
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000045F4 File Offset: 0x000027F4
		public override bool CanConvertTo(ITypeDescriptorContext context, Type t)
		{
			return t.IsPrimitive || base.CanConvertTo(context, t);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000460C File Offset: 0x0000280C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string text = value as string;
			if (text != null)
			{
				try
				{
					if (this.SupportHex)
					{
						if (text.Length >= 1 && text[0] == '#')
						{
							return this.ConvertFromString(text.Substring(1), 16);
						}
						if (text.StartsWith("0x") || text.StartsWith("0X"))
						{
							return this.ConvertFromString(text, 16);
						}
					}
					NumberFormatInfo numberFormatInfo = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
					return this.ConvertFromString(text, numberFormatInfo);
				}
				catch (Exception ex)
				{
					throw new Exception(value.ToString() + " is not a valid value for " + this.InnerType.Name + ".", ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004710 File Offset: 0x00002910
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			if (destinationType == typeof(string) && value is IConvertible)
			{
				return ((IConvertible)value).ToType(destinationType, culture);
			}
			if (destinationType.IsPrimitive)
			{
				return Convert.ChangeType(value, destinationType, culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060000D4 RID: 212
		internal abstract object ConvertFromString(string value, NumberFormatInfo format);

		// Token: 0x060000D5 RID: 213 RVA: 0x00004788 File Offset: 0x00002988
		internal virtual object ConvertFromString(string value, int fromBase)
		{
			if (this.SupportHex)
			{
				throw new NotImplementedException();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0400005D RID: 93
		internal Type InnerType;
	}
}
