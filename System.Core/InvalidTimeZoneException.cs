using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class InvalidTimeZoneException : Exception
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002A30 File Offset: 0x00000C30
		public InvalidTimeZoneException()
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A38 File Offset: 0x00000C38
		public InvalidTimeZoneException(string message)
			: base(message)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A44 File Offset: 0x00000C44
		protected InvalidTimeZoneException(SerializationInfo info, StreamingContext sc)
			: base(info, sc)
		{
		}
	}
}
