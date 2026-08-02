using System;
using System.Globalization;
using System.Net.Sockets;

namespace System.Net
{
	// Token: 0x02000081 RID: 129
	[Serializable]
	public class IPAddress
	{
		// Token: 0x0600030B RID: 779 RVA: 0x0000CE0C File Offset: 0x0000B00C
		public IPAddress(long addr)
		{
			this.m_Address = addr;
			this.m_Family = global::System.Net.Sockets.AddressFamily.InterNetwork;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000CE24 File Offset: 0x0000B024
		internal IPAddress(ushort[] address, long scopeId)
		{
			this.m_Numbers = address;
			for (int i = 0; i < 8; i++)
			{
				this.m_Numbers[i] = (ushort)IPAddress.HostToNetworkOrder((short)this.m_Numbers[i]);
			}
			this.m_Family = global::System.Net.Sockets.AddressFamily.InterNetworkV6;
			this.m_ScopeId = scopeId;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		private static short SwapShort(short number)
		{
			return (short)(((number >> 8) & 255) | (((int)number << 8) & 65280));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000CF04 File Offset: 0x0000B104
		public static short HostToNetworkOrder(short host)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return host;
			}
			return IPAddress.SwapShort(host);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000CF18 File Offset: 0x0000B118
		public static short NetworkToHostOrder(short network)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return network;
			}
			return IPAddress.SwapShort(network);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000CF2C File Offset: 0x0000B12C
		public static IPAddress Parse(string ipString)
		{
			IPAddress ipaddress;
			if (IPAddress.TryParse(ipString, out ipaddress))
			{
				return ipaddress;
			}
			throw new FormatException("An invalid IP address was specified.");
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000CF54 File Offset: 0x0000B154
		public static bool TryParse(string ipString, out IPAddress address)
		{
			if (ipString == null)
			{
				throw new ArgumentNullException("ipString");
			}
			IPAddress ipaddress;
			address = (ipaddress = IPAddress.ParseIPV4(ipString));
			if (ipaddress == null)
			{
				address = (ipaddress = IPAddress.ParseIPV6(ipString));
				if (ipaddress == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000CF98 File Offset: 0x0000B198
		private static IPAddress ParseIPV4(string ip)
		{
			int num = ip.IndexOf(' ');
			if (num != -1)
			{
				string[] array = ip.Substring(num + 1).Split(new char[] { '.' });
				if (array.Length > 0)
				{
					string text = array[array.Length - 1];
					if (text.Length == 0)
					{
						return null;
					}
					foreach (char c in text.ToCharArray())
					{
						if (!global::System.Uri.IsHexDigit(c))
						{
							return null;
						}
					}
				}
				ip = ip.Substring(0, num);
			}
			if (ip.Length == 0 || ip[ip.Length - 1] == '.')
			{
				return null;
			}
			string[] array3 = ip.Split(new char[] { '.' });
			if (array3.Length > 4)
			{
				return null;
			}
			IPAddress ipaddress;
			try
			{
				long num2 = 0L;
				long num3 = 0L;
				for (int j = 0; j < array3.Length; j++)
				{
					string text2 = array3[j];
					if (3 <= text2.Length && text2.Length <= 4 && text2[0] == '0' && (text2[1] == 'x' || text2[1] == 'X'))
					{
						if (text2.Length == 3)
						{
							num3 = (long)((byte)global::System.Uri.FromHex(text2[2]));
						}
						else
						{
							num3 = (long)((byte)((global::System.Uri.FromHex(text2[2]) << 4) | global::System.Uri.FromHex(text2[3])));
						}
					}
					else
					{
						if (text2.Length == 0)
						{
							return null;
						}
						if (text2[0] == '0')
						{
							num3 = 0L;
							for (int k = 1; k < text2.Length; k++)
							{
								if ('0' > text2[k] || text2[k] > '7')
								{
									return null;
								}
								num3 = (num3 << 3) + (long)text2[k] - 48L;
							}
						}
						else if (!long.TryParse(text2, NumberStyles.None, null, out num3))
						{
							return null;
						}
					}
					if (j == array3.Length - 1)
					{
						j = 3;
					}
					else if (num3 > 255L)
					{
						return null;
					}
					int num4 = 0;
					while (num3 > 0L)
					{
						num2 |= (num3 & 255L) << (j - num4 << 3);
						num4++;
						num3 /= 256L;
					}
				}
				ipaddress = new IPAddress(num2);
			}
			catch (Exception)
			{
				ipaddress = null;
			}
			return ipaddress;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D270 File Offset: 0x0000B470
		private static IPAddress ParseIPV6(string ip)
		{
			IPv6Address pv6Address;
			if (IPv6Address.TryParse(ip, out pv6Address))
			{
				return new IPAddress(pv6Address.Address, pv6Address.ScopeId);
			}
			return null;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000D2A0 File Offset: 0x0000B4A0
		internal long InternalIPv4Address
		{
			get
			{
				return this.m_Address;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		public long ScopeId
		{
			get
			{
				if (this.m_Family != global::System.Net.Sockets.AddressFamily.InterNetworkV6)
				{
					throw new Exception("The attempted operation is not supported for the type of object referenced");
				}
				return this.m_ScopeId;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
		public byte[] GetAddressBytes()
		{
			if (this.m_Family == global::System.Net.Sockets.AddressFamily.InterNetworkV6)
			{
				byte[] array = new byte[16];
				Buffer.BlockCopy(this.m_Numbers, 0, array, 0, 16);
				return array;
			}
			return new byte[]
			{
				(byte)(this.m_Address & 255L),
				(byte)((this.m_Address >> 8) & 255L),
				(byte)((this.m_Address >> 16) & 255L),
				(byte)(this.m_Address >> 24)
			};
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000D348 File Offset: 0x0000B548
		public global::System.Net.Sockets.AddressFamily AddressFamily
		{
			get
			{
				return this.m_Family;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D350 File Offset: 0x0000B550
		public override string ToString()
		{
			if (this.m_Family == global::System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return IPAddress.ToString(this.m_Address);
			}
			ushort[] array = this.m_Numbers.Clone() as ushort[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (ushort)IPAddress.NetworkToHostOrder((short)array[i]);
			}
			return new IPv6Address(array)
			{
				ScopeId = this.ScopeId
			}.ToString();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		private static string ToString(long addr)
		{
			return string.Concat(new string[]
			{
				(addr & 255L).ToString(),
				".",
				((addr >> 8) & 255L).ToString(),
				".",
				((addr >> 16) & 255L).ToString(),
				".",
				((addr >> 24) & 255L).ToString()
			});
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000D444 File Offset: 0x0000B644
		public override bool Equals(object other)
		{
			IPAddress ipaddress = other as IPAddress;
			if (ipaddress == null)
			{
				return false;
			}
			if (this.AddressFamily != ipaddress.AddressFamily)
			{
				return false;
			}
			if (this.AddressFamily == global::System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return this.m_Address == ipaddress.m_Address;
			}
			ushort[] numbers = ipaddress.m_Numbers;
			for (int i = 0; i < 8; i++)
			{
				if (this.m_Numbers[i] != numbers[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		public override int GetHashCode()
		{
			if (this.m_Family == global::System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return (int)this.m_Address;
			}
			return IPAddress.Hash(((int)this.m_Numbers[0] << 16) + (int)this.m_Numbers[1], ((int)this.m_Numbers[2] << 16) + (int)this.m_Numbers[3], ((int)this.m_Numbers[4] << 16) + (int)this.m_Numbers[5], ((int)this.m_Numbers[6] << 16) + (int)this.m_Numbers[7]);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000D534 File Offset: 0x0000B734
		private static int Hash(int i, int j, int k, int l)
		{
			return i ^ ((j << 13) | (j >> 19)) ^ ((k << 26) | (k >> 6)) ^ ((l << 7) | (l >> 25));
		}

		// Token: 0x04000165 RID: 357
		private long m_Address;

		// Token: 0x04000166 RID: 358
		private global::System.Net.Sockets.AddressFamily m_Family;

		// Token: 0x04000167 RID: 359
		private ushort[] m_Numbers;

		// Token: 0x04000168 RID: 360
		private long m_ScopeId;

		// Token: 0x04000169 RID: 361
		public static readonly IPAddress Any = new IPAddress(0L);

		// Token: 0x0400016A RID: 362
		public static readonly IPAddress Broadcast = IPAddress.Parse("255.255.255.255");

		// Token: 0x0400016B RID: 363
		public static readonly IPAddress Loopback = IPAddress.Parse("127.0.0.1");

		// Token: 0x0400016C RID: 364
		public static readonly IPAddress None = IPAddress.Parse("255.255.255.255");

		// Token: 0x0400016D RID: 365
		public static readonly IPAddress IPv6Any = IPAddress.ParseIPV6("::");

		// Token: 0x0400016E RID: 366
		public static readonly IPAddress IPv6Loopback = IPAddress.ParseIPV6("::1");

		// Token: 0x0400016F RID: 367
		public static readonly IPAddress IPv6None = IPAddress.ParseIPV6("::");

		// Token: 0x04000170 RID: 368
		private int m_HashCode;
	}
}
