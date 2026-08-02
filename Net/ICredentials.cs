using System;

namespace System.Net
{
	// Token: 0x02000080 RID: 128
	public interface ICredentials
	{
		// Token: 0x0600030A RID: 778
		NetworkCredential GetCredential(global::System.Uri uri, string authType);
	}
}
