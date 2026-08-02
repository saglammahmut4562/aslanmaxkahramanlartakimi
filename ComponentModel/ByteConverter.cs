using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000020 RID: 32
	public class ByteConverter : BaseNumberConverter
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000047E4 File Offset: 0x000029E4
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000047E8 File Offset: 0x000029E8
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return byte.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000047F8 File Offset: 0x000029F8
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToByte(value, fromBase);
		}
	}
}
