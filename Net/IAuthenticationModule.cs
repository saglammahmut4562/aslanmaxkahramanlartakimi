using System;

namespace System.Net
{
	// Token: 0x0200007D RID: 125
	public interface IAuthenticationModule
	{
		// Token: 0x06000306 RID: 774
		Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials);

		// Token: 0x06000307 RID: 775
		Authorization PreAuthenticate(WebRequest request, ICredentials credentials);

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000308 RID: 776
		string AuthenticationType { get; }
	}
}
