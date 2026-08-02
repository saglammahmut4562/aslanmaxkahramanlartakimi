using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200002F RID: 47
	public class DoubleConverter : BaseNumberConverter
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000532C File Offset: 0x0000352C
		internal override bool SupportHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005330 File Offset: 0x00003530
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return double.Parse(value, NumberStyles.Float, format);
		}
	}
}
