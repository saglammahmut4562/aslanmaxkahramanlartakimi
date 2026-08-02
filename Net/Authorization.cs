using System;

namespace System.Net
{
	// Token: 0x02000065 RID: 101
	public class Authorization
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00007FC0 File Offset: 0x000061C0
		public Authorization(string token)
			: this(token, true)
		{
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007FCC File Offset: 0x000061CC
		public Authorization(string token, bool complete)
			: this(token, complete, null)
		{
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007FD8 File Offset: 0x000061D8
		public Authorization(string token, bool complete, string connectionGroupId)
		{
			this.token = token;
			this.complete = complete;
			this.connectionGroupId = connectionGroupId;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00007FF8 File Offset: 0x000061F8
		public string Message
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00008000 File Offset: 0x00006200
		public bool Complete
		{
			get
			{
				return this.complete;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00008008 File Offset: 0x00006208
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00008010 File Offset: 0x00006210
		internal IAuthenticationModule Module
		{
			get
			{
				return this.module;
			}
			set
			{
				this.module = value;
			}
		}

		// Token: 0x040000AE RID: 174
		private string token;

		// Token: 0x040000AF RID: 175
		private bool complete;

		// Token: 0x040000B0 RID: 176
		private string connectionGroupId;

		// Token: 0x040000B1 RID: 177
		private string[] protectionRealm;

		// Token: 0x040000B2 RID: 178
		private IAuthenticationModule module;
	}
}
