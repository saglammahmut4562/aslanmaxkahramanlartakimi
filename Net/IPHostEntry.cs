using System;

namespace System.Net
{
	// Token: 0x02000083 RID: 131
	public class IPHostEntry
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000D9A8 File Offset: 0x0000BBA8
		public IPAddress[] AddressList
		{
			get
			{
				return this.addressList;
			}
			set
			{
				this.addressList = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
		public string[] Aliases
		{
			set
			{
				this.aliases = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		public string HostName
		{
			set
			{
				this.hostName = value;
			}
		}

		// Token: 0x04000175 RID: 373
		private IPAddress[] addressList;

		// Token: 0x04000176 RID: 374
		private string[] aliases;

		// Token: 0x04000177 RID: 375
		private string hostName;
	}
}
