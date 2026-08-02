using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public class LockRecursionException : Exception
	{
		// Token: 0x06000175 RID: 373 RVA: 0x000068F4 File Offset: 0x00004AF4
		public LockRecursionException()
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000068FC File Offset: 0x00004AFC
		public LockRecursionException(string message)
			: base(message)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006908 File Offset: 0x00004B08
		protected LockRecursionException(SerializationInfo info, StreamingContext sc)
			: base(info, sc)
		{
		}
	}
}
