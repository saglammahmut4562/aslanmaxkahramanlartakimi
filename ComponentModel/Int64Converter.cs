using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200003E RID: 62
	public class Int64Converter : BaseNumberConverter
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005E44 File Offset: 0x00004044
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005E48 File Offset: 0x00004048
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return long.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005E58 File Offset: 0x00004058
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToInt64(value, fromBase);
		}
	}
}
