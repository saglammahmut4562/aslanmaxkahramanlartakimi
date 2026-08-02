using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public class UnityException : SystemException
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x00017748 File Offset: 0x00015948
		public UnityException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00017760 File Offset: 0x00015960
		public UnityException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00017774 File Offset: 0x00015974
		public UnityException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001778C File Offset: 0x0001598C
		protected UnityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040004BB RID: 1211
		private const int Result = -2147467261;

		// Token: 0x040004BC RID: 1212
		private string unityStackTrace;
	}
}
