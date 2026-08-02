using System;

namespace System.Net
{
	// Token: 0x02000085 RID: 133
	public interface IWebProxy
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000343 RID: 835
		ICredentials Credentials { get; }

		// Token: 0x06000344 RID: 836
		global::System.Uri GetProxy(global::System.Uri destination);

		// Token: 0x06000345 RID: 837
		bool IsBypassed(global::System.Uri host);
	}
}
