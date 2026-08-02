using System;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace System.Net
{
	// Token: 0x02000084 RID: 132
	[DefaultMember("Item")]
	[Serializable]
	internal class IPv6Address
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000D9CC File Offset: 0x0000BBCC
		public IPv6Address(ushort[] addr)
		{
			if (addr == null)
			{
				throw new ArgumentNullException("addr");
			}
			if (addr.Length != 8)
			{
				throw new ArgumentException("addr");
			}
			this.address = addr;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000DA00 File Offset: 0x0000BC00
		public IPv6Address(ushort[] addr, int prefixLength)
			: this(addr)
		{
			if (prefixLength < 0 || prefixLength > 128)
			{
				throw new ArgumentException("prefixLength");
			}
			this.prefixLength = prefixLength;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000DA30 File Offset: 0x0000BC30
		public IPv6Address(ushort[] addr, int prefixLength, int scopeId)
			: this(addr, prefixLength)
		{
			this.scopeId = (long)scopeId;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000DA64 File Offset: 0x0000BC64
		public static IPv6Address Parse(string ipString)
		{
			if (ipString == null)
			{
				throw new ArgumentNullException("ipString");
			}
			IPv6Address pv6Address;
			if (IPv6Address.TryParse(ipString, out pv6Address))
			{
				return pv6Address;
			}
			throw new FormatException("Not a valid IPv6 address");
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000DA9C File Offset: 0x0000BC9C
		private static int Fill(ushort[] addr, string ipString)
		{
			int num = 0;
			int num2 = 0;
			if (ipString.Length == 0)
			{
				return 0;
			}
			if (ipString.IndexOf("::") != -1)
			{
				return -1;
			}
			for (int i = 0; i < ipString.Length; i++)
			{
				char c = ipString[i];
				if (c == ':')
				{
					if (i == ipString.Length - 1)
					{
						return -1;
					}
					if (num2 == 8)
					{
						return -1;
					}
					addr[num2++] = (ushort)num;
					num = 0;
				}
				else
				{
					int num3;
					if ('0' <= c && c <= '9')
					{
						num3 = (int)(c - '0');
					}
					else if ('a' <= c && c <= 'f')
					{
						num3 = (int)(c - 'a' + '\n');
					}
					else
					{
						if ('A' > c || c > 'F')
						{
							return -1;
						}
						num3 = (int)(c - 'A' + '\n');
					}
					num = (num << 4) + num3;
					if (num > 65535)
					{
						return -1;
					}
				}
			}
			if (num2 == 8)
			{
				return -1;
			}
			addr[num2++] = (ushort)num;
			return num2;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
		private static bool TryParse(string prefix, out int res)
		{
			return int.TryParse(prefix, NumberStyles.Integer, CultureInfo.InvariantCulture, out res);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		public static bool TryParse(string ipString, out IPv6Address result)
		{
			result = null;
			if (ipString == null)
			{
				return false;
			}
			if (ipString.Length > 2 && ipString[0] == '[' && ipString[ipString.Length - 1] == ']')
			{
				ipString = ipString.Substring(1, ipString.Length - 2);
			}
			if (ipString.Length < 2)
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			int num3 = ipString.LastIndexOf('/');
			if (num3 != -1)
			{
				string text = ipString.Substring(num3 + 1);
				if (!IPv6Address.TryParse(text, out num))
				{
					num = -1;
				}
				if (num < 0 || num > 128)
				{
					return false;
				}
				ipString = ipString.Substring(0, num3);
			}
			else
			{
				num3 = ipString.LastIndexOf('%');
				if (num3 != -1)
				{
					string text2 = ipString.Substring(num3 + 1);
					if (!IPv6Address.TryParse(text2, out num2))
					{
						num2 = 0;
					}
					ipString = ipString.Substring(0, num3);
				}
			}
			ushort[] array = new ushort[8];
			bool flag = false;
			int num4 = ipString.LastIndexOf(':');
			if (num4 == -1)
			{
				return false;
			}
			int num5 = 0;
			if (num4 < ipString.Length - 1)
			{
				string text3 = ipString.Substring(num4 + 1);
				if (text3.IndexOf('.') != -1)
				{
					IPAddress ipaddress;
					if (!IPAddress.TryParse(text3, out ipaddress))
					{
						return false;
					}
					long internalIPv4Address = ipaddress.InternalIPv4Address;
					array[6] = (ushort)(((int)(internalIPv4Address & 255L) << 8) + (int)((internalIPv4Address >> 8) & 255L));
					array[7] = (ushort)(((int)((internalIPv4Address >> 16) & 255L) << 8) + (int)((internalIPv4Address >> 24) & 255L));
					if (num4 > 0 && ipString[num4 - 1] == ':')
					{
						ipString = ipString.Substring(0, num4 + 1);
					}
					else
					{
						ipString = ipString.Substring(0, num4);
					}
					flag = true;
					num5 = 2;
				}
			}
			int num6 = ipString.IndexOf("::");
			if (num6 != -1)
			{
				int num7 = IPv6Address.Fill(array, ipString.Substring(num6 + 2));
				if (num7 == -1)
				{
					return false;
				}
				if (num7 + num5 > 8)
				{
					return false;
				}
				int num8 = 8 - num5 - num7;
				for (int i = num7; i > 0; i--)
				{
					array[i + num8 - 1] = array[i - 1];
					array[i - 1] = 0;
				}
				int num9 = IPv6Address.Fill(array, ipString.Substring(0, num6));
				if (num9 == -1)
				{
					return false;
				}
				if (num9 + num7 + num5 > 7)
				{
					return false;
				}
			}
			else if (IPv6Address.Fill(array, ipString) != 8 - num5)
			{
				return false;
			}
			bool flag2 = false;
			for (int j = 0; j < num5; j++)
			{
				if (array[j] != 0 || (j == 5 && array[j] != 65535))
				{
					flag2 = true;
				}
			}
			if (flag && !flag2)
			{
				for (int k = 0; k < 5; k++)
				{
					if (array[k] != 0)
					{
						return false;
					}
				}
				if (array[5] != 0 && array[5] != 65535)
				{
					return false;
				}
			}
			result = new IPv6Address(array, num, num2);
			return true;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public ushort[] Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000DEDC File Offset: 0x0000C0DC
		public long ScopeId
		{
			get
			{
				return this.scopeId;
			}
			set
			{
				this.scopeId = value;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		private static ushort SwapUShort(ushort number)
		{
			return (ushort)(((number >> 8) & 255) + (((int)number << 8) & 65280));
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000DF00 File Offset: 0x0000C100
		private int AsIPv4Int()
		{
			return ((int)IPv6Address.SwapUShort(this.address[7]) << 16) + (int)IPv6Address.SwapUShort(this.address[6]);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000DF20 File Offset: 0x0000C120
		public bool IsIPv4Compatible()
		{
			for (int i = 0; i < 6; i++)
			{
				if (this.address[i] != 0)
				{
					return false;
				}
			}
			return this.AsIPv4Int() > 1;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000DF58 File Offset: 0x0000C158
		public bool IsIPv4Mapped()
		{
			for (int i = 0; i < 5; i++)
			{
				if (this.address[i] != 0)
				{
					return false;
				}
			}
			return this.address[5] == ushort.MaxValue;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000DF98 File Offset: 0x0000C198
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsIPv4Compatible() || this.IsIPv4Mapped())
			{
				stringBuilder.Append("::");
				if (this.IsIPv4Mapped())
				{
					stringBuilder.Append("ffff:");
				}
				stringBuilder.Append(new IPAddress((long)this.AsIPv4Int()).ToString());
				return stringBuilder.ToString();
			}
			int num = -1;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 8; i++)
			{
				if (this.address[i] != 0)
				{
					if (num3 > num2 && num3 > 1)
					{
						num2 = num3;
						num = i - num3;
					}
					num3 = 0;
				}
				else
				{
					num3++;
				}
			}
			if (num3 > num2 && num3 > 1)
			{
				num2 = num3;
				num = 8 - num3;
			}
			if (num == 0)
			{
				stringBuilder.Append(":");
			}
			for (int j = 0; j < 8; j++)
			{
				if (j == num)
				{
					stringBuilder.Append(":");
					j += num2 - 1;
				}
				else
				{
					stringBuilder.AppendFormat("{0:x}", this.address[j]);
					if (j < 7)
					{
						stringBuilder.Append(':');
					}
				}
			}
			if (this.scopeId != 0L)
			{
				stringBuilder.Append('%').Append(this.scopeId);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000E0FC File Offset: 0x0000C2FC
		public string ToString(bool fullLength)
		{
			if (!fullLength)
			{
				return this.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.address.Length - 1; i++)
			{
				stringBuilder.AppendFormat("{0:X4}:", this.address[i]);
			}
			stringBuilder.AppendFormat("{0:X4}", this.address[this.address.Length - 1]);
			return stringBuilder.ToString();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000E178 File Offset: 0x0000C378
		public override bool Equals(object other)
		{
			IPv6Address pv6Address = other as IPv6Address;
			if (pv6Address != null)
			{
				for (int i = 0; i < 8; i++)
				{
					if (this.address[i] != pv6Address.address[i])
					{
						return false;
					}
				}
				return true;
			}
			IPAddress ipaddress = other as IPAddress;
			if (ipaddress == null)
			{
				return false;
			}
			for (int j = 0; j < 5; j++)
			{
				if (this.address[j] != 0)
				{
					return false;
				}
			}
			if (this.address[5] != 0 && this.address[5] != 65535)
			{
				return false;
			}
			long internalIPv4Address = ipaddress.InternalIPv4Address;
			return this.address[6] == (ushort)(((int)(internalIPv4Address & 255L) << 8) + (int)((internalIPv4Address >> 8) & 255L)) && this.address[7] == (ushort)(((int)((internalIPv4Address >> 16) & 255L) << 8) + (int)((internalIPv4Address >> 24) & 255L));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000E26C File Offset: 0x0000C46C
		public override int GetHashCode()
		{
			return IPv6Address.Hash(((int)this.address[0] << 16) + (int)this.address[1], ((int)this.address[2] << 16) + (int)this.address[3], ((int)this.address[4] << 16) + (int)this.address[5], ((int)this.address[6] << 16) + (int)this.address[7]);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		private static int Hash(int i, int j, int k, int l)
		{
			return i ^ ((j << 13) | (j >> 19)) ^ ((k << 26) | (k >> 6)) ^ ((l << 7) | (l >> 25));
		}

		// Token: 0x04000178 RID: 376
		private ushort[] address;

		// Token: 0x04000179 RID: 377
		private int prefixLength;

		// Token: 0x0400017A RID: 378
		private long scopeId;

		// Token: 0x0400017B RID: 379
		public static readonly IPv6Address Loopback = IPv6Address.Parse("::1");

		// Token: 0x0400017C RID: 380
		public static readonly IPv6Address Unspecified = IPv6Address.Parse("::");
	}
}
