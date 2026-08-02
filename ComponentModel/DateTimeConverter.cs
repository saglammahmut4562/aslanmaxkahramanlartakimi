using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x02000027 RID: 39
	public class DateTimeConverter : TypeConverter
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00004CDC File Offset: 0x00002EDC
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004D14 File Offset: 0x00002F14
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = (string)value;
				try
				{
					if (text != null && text.Trim().Length == 0)
					{
						return DateTime.MinValue;
					}
					if (culture == null)
					{
						return DateTime.Parse(text);
					}
					DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
					return DateTime.Parse(text, dateTimeFormatInfo);
				}
				catch
				{
					throw new FormatException(text + " is not a valid DateTime value.");
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				if (destinationType == typeof(string))
				{
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					if (dateTime == DateTime.MinValue)
					{
						return string.Empty;
					}
					DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
					if (culture == CultureInfo.InvariantCulture)
					{
						if (dateTime.Equals(dateTime.Date))
						{
							return dateTime.ToString("yyyy-MM-dd", culture);
						}
						return dateTime.ToString(culture);
					}
					else
					{
						if (dateTime == dateTime.Date)
						{
							return dateTime.ToString(dateTimeFormatInfo.ShortDatePattern, culture);
						}
						return dateTime.ToString(dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.ShortTimePattern, culture);
					}
				}
				else if (destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor))
				{
					ConstructorInfo constructor = typeof(DateTime).GetConstructor(new Type[] { typeof(long) });
					return new global::System.ComponentModel.Design.Serialization.InstanceDescriptor(constructor, new object[] { dateTime.Ticks });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
