using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x02000072 RID: 114
	internal class DefaultCertificatePolicy : ICertificatePolicy
	{
		// Token: 0x06000295 RID: 661 RVA: 0x00009B6C File Offset: 0x00007D6C
		public bool CheckValidationResult(ServicePoint point, X509Certificate certificate, WebRequest request, int certificateProblem)
		{
			return ServicePointManager.ServerCertificateValidationCallback != null || certificateProblem == -2146762495 || certificateProblem == 0;
		}
	}
}
