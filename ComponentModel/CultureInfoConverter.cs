using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x02000025 RID: 37
	public class CultureInfoConverter : TypeConverter
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x000049B0 File Offset: 0x00002BB0
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000049CC File Offset: 0x00002BCC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000049FC File Offset: 0x00002BFC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (string.Compare(text, "(Default)", false) == 0)
			{
				return CultureInfo.InvariantCulture;
			}
			try
			{
				return new CultureInfo(text);
			}
			catch
			{
				foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.AllCultures))
				{
					if (string.Compare(cultureInfo.DisplayName, 0, text, 0, text.Length, true) == 0)
					{
						return cultureInfo;
					}
				}
			}
			throw new ArgumentException(string.Format("Culture {0} cannot be converted to a CultureInfo or is not available in this environment.", value));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				if (value == null || !(value is CultureInfo))
				{
					return "(Default)";
				}
				if (value == CultureInfo.InvariantCulture)
				{
					return "(Default)";
				}
				return ((CultureInfo)value).DisplayName;
			}
			else
			{
				if (destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor) && value is CultureInfo)
				{
					CultureInfo cultureInfo = (CultureInfo)value;
					ConstructorInfo constructor = typeof(CultureInfo).GetConstructor(new Type[] { typeof(int) });
					return new global::System.ComponentModel.Design.Serialization.InstanceDescriptor(constructor, new object[] { cultureInfo.LCID });
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		// Token: 0x0400005E RID: 94
		private TypeConverter.StandardValuesCollection _standardValues;
	}
}
