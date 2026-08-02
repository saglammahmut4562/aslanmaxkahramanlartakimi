using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200001C RID: 28
	public class ArrayConverter : CollectionConverter
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00004394 File Offset: 0x00002594
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is Array)
			{
				return value.GetType().Name + " Array";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
