using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200003D RID: 61
	public class Int32Converter : BaseNumberConverter
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005E20 File Offset: 0x00004020
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005E24 File Offset: 0x00004024
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return int.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005E34 File Offset: 0x00004034
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToInt32(value, fromBase);
		}
	}
}
