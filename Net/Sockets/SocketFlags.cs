using System;

namespace System.Net.Sockets
{
	// Token: 0x020000A0 RID: 160
	[Flags]
	public enum SocketFlags
	{
		// Token: 0x04000271 RID: 625
		None = 0,
		// Token: 0x04000272 RID: 626
		OutOfBand = 1,
		// Token: 0x04000273 RID: 627
		Peek = 2,
		// Token: 0x04000274 RID: 628
		DontRoute = 4,
		// Token: 0x04000275 RID: 629
		MaxIOVectorLength = 16,
		// Token: 0x04000276 RID: 630
		Truncated = 256,
		// Token: 0x04000277 RID: 631
		ControlDataTruncated = 512,
		// Token: 0x04000278 RID: 632
		Broadcast = 1024,
		// Token: 0x04000279 RID: 633
		Multicast = 2048,
		// Token: 0x0400027A RID: 634
		Partial = 32768
	}
}
