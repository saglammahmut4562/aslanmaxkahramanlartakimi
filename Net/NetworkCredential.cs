using System;

namespace System.Net
{
	// Token: 0x02000087 RID: 135
	public class NetworkCredential : ICredentials
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000E2F0 File Offset: 0x0000C4F0
		public string Domain
		{
			get
			{
				return (this.domain != null) ? this.domain : string.Empty;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000E310 File Offset: 0x0000C510
		public string UserName
		{
			get
			{
				return (this.userName != null) ? this.userName : string.Empty;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000E330 File Offset: 0x0000C530
		public string Password
		{
			get
			{
				return (this.password != null) ? this.password : string.Empty;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000E350 File Offset: 0x0000C550
		public NetworkCredential GetCredential(global::System.Uri uri, string authType)
		{
			return this;
		}

		// Token: 0x0400017D RID: 381
		private string userName;

		// Token: 0x0400017E RID: 382
		private string password;

		// Token: 0x0400017F RID: 383
		private string domain;
	}
}
