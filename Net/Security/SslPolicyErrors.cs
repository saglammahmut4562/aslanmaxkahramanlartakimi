using System;

namespace System.Net.Security
{
	// Token: 0x0200008C RID: 140
	[Flags]
	public enum SslPolicyErrors
	{
		// Token: 0x0400018A RID: 394
		None = 0,
		// Token: 0x0400018B RID: 395
		RemoteCertificateNotAvailable = 1,
		// Token: 0x0400018C RID: 396
		RemoteCertificateNameMismatch = 2,
		// Token: 0x0400018D RID: 397
		RemoteCertificateChainErrors = 4
	}
}
