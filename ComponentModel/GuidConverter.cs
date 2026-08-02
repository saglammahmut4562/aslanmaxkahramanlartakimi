using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x02000035 RID: 53
	public class GuidConverter : TypeConverter
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00005A78 File Offset: 0x00003C78
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005A94 File Offset: 0x00003C94
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value.GetType() == typeof(string))
			{
				string text = (string)value;
				try
				{
					return new Guid(text);
				}
				catch
				{
					throw new FormatException(text + "is not a valid GUID.");
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005B38 File Offset: 0x00003D38
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is Guid)
			{
				Guid guid = (Guid)value;
				if (destinationType == typeof(string) && value != null)
				{
					return guid.ToString("D");
				}
				if (destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor))
				{
					ConstructorInfo constructor = typeof(Guid).GetConstructor(new Type[] { typeof(string) });
					return new global::System.ComponentModel.Design.Serialization.InstanceDescriptor(constructor, new object[] { guid.ToString("D") });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
