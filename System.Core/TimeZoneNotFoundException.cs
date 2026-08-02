using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public class TimeZoneNotFoundException : Exception
	{
		// Token: 0x060001CF RID: 463 RVA: 0x000083AC File Offset: 0x000065AC
		public TimeZoneNotFoundException()
		{
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000083B4 File Offset: 0x000065B4
		protected TimeZoneNotFoundException(SerializationInfo info, StreamingContext sc)
			: base(info, sc)
		{
		}
	}
}
