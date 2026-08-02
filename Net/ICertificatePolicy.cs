using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x0200007E RID: 126
	public interface ICertificatePolicy
	{
		// Token: 0x06000309 RID: 777
		bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem);
	}
}
