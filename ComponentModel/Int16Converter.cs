using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200003C RID: 60
	public class Int16Converter : BaseNumberConverter
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005DFC File Offset: 0x00003FFC
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005E00 File Offset: 0x00004000
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return short.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005E10 File Offset: 0x00004010
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToInt16(value, fromBase);
		}
	}
}
