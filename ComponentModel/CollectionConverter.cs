using System;
using System.Collections;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000022 RID: 34
	public class CollectionConverter : TypeConverter
	{
		// Token: 0x060000DE RID: 222 RVA: 0x000048F0 File Offset: 0x00002AF0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value != null && value is ICollection)
			{
				return "(Collection)";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
