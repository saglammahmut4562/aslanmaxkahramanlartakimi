using System;
using System.Net.Sockets;

namespace System.Net
{
	// Token: 0x02000092 RID: 146
	public class SocketAddress
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		public SocketAddress(global::System.Net.Sockets.AddressFamily family, int size)
		{
			if (size < 2)
			{
				throw new ArgumentOutOfRangeException("size is too small");
			}
			this.data = new byte[size];
			this.data[0] = (byte)family;
			this.data[1] = (byte)(family >> 8);
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000F504 File Offset: 0x0000D704
		public global::System.Net.Sockets.AddressFamily Family
		{
			get
			{
				return (global::System.Net.Sockets.AddressFamily)((int)this.data[0] + ((int)this.data[1] << 8));
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000F51C File Offset: 0x0000D71C
		public int Size
		{
			get
			{
				return this.data.Length;
			}
		}

		// Token: 0x170000E6 RID: 230
		public byte this[int offset]
		{
			get
			{
				return this.data[offset];
			}
			set
			{
				this.data[offset] = value;
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000F540 File Offset: 0x0000D740
		public override string ToString()
		{
			string text = ((global::System.Net.Sockets.AddressFamily)this.data[0]).ToString();
			int num = this.data.Length;
			string text2 = string.Concat(new object[] { text, ":", num, ":{" });
			for (int i = 2; i < num; i++)
			{
				int num2 = (int)this.data[i];
				text2 += num2;
				if (i < num - 1)
				{
					text2 += ",";
				}
			}
			return text2 + "}";
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		public override bool Equals(object obj)
		{
			SocketAddress socketAddress = obj as SocketAddress;
			if (socketAddress != null && socketAddress.data.Length == this.data.Length)
			{
				byte[] array = socketAddress.data;
				for (int i = 0; i < this.data.Length; i++)
				{
					if (array[i] != this.data[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000F644 File Offset: 0x0000D844
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.data.Length; i++)
			{
				num += (int)this.data[i] + i;
			}
			return num;
		}

		// Token: 0x040001B4 RID: 436
		private byte[] data;
	}
}
