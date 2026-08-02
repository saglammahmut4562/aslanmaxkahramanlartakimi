using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x0200008B RID: 139
	// (Invoke) Token: 0x06000351 RID: 849
	public delegate bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, global::System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors);
}
