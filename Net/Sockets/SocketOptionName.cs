using System;

namespace System.Net.Sockets
{
	// Token: 0x020000A2 RID: 162
	public enum SocketOptionName
	{
		// Token: 0x04000282 RID: 642
		Debug = 1,
		// Token: 0x04000283 RID: 643
		AcceptConnection,
		// Token: 0x04000284 RID: 644
		ReuseAddress = 4,
		// Token: 0x04000285 RID: 645
		KeepAlive = 8,
		// Token: 0x04000286 RID: 646
		DontRoute = 16,
		// Token: 0x04000287 RID: 647
		Broadcast = 32,
		// Token: 0x04000288 RID: 648
		UseLoopback = 64,
		// Token: 0x04000289 RID: 649
		Linger = 128,
		// Token: 0x0400028A RID: 650
		OutOfBandInline = 256,
		// Token: 0x0400028B RID: 651
		DontLinger = -129,
		// Token: 0x0400028C RID: 652
		ExclusiveAddressUse = -5,
		// Token: 0x0400028D RID: 653
		SendBuffer = 4097,
		// Token: 0x0400028E RID: 654
		ReceiveBuffer,
		// Token: 0x0400028F RID: 655
		SendLowWater,
		// Token: 0x04000290 RID: 656
		ReceiveLowWater,
		// Token: 0x04000291 RID: 657
		SendTimeout,
		// Token: 0x04000292 RID: 658
		ReceiveTimeout,
		// Token: 0x04000293 RID: 659
		Error,
		// Token: 0x04000294 RID: 660
		Type,
		// Token: 0x04000295 RID: 661
		MaxConnections = 2147483647,
		// Token: 0x04000296 RID: 662
		IPOptions = 1,
		// Token: 0x04000297 RID: 663
		HeaderIncluded,
		// Token: 0x04000298 RID: 664
		TypeOfService,
		// Token: 0x04000299 RID: 665
		IpTimeToLive,
		// Token: 0x0400029A RID: 666
		MulticastInterface = 9,
		// Token: 0x0400029B RID: 667
		MulticastTimeToLive,
		// Token: 0x0400029C RID: 668
		MulticastLoopback,
		// Token: 0x0400029D RID: 669
		AddMembership,
		// Token: 0x0400029E RID: 670
		DropMembership,
		// Token: 0x0400029F RID: 671
		DontFragment,
		// Token: 0x040002A0 RID: 672
		AddSourceMembership,
		// Token: 0x040002A1 RID: 673
		DropSourceMembership,
		// Token: 0x040002A2 RID: 674
		BlockSource,
		// Token: 0x040002A3 RID: 675
		UnblockSource,
		// Token: 0x040002A4 RID: 676
		PacketInformation,
		// Token: 0x040002A5 RID: 677
		NoDelay = 1,
		// Token: 0x040002A6 RID: 678
		BsdUrgent,
		// Token: 0x040002A7 RID: 679
		Expedited = 2,
		// Token: 0x040002A8 RID: 680
		NoChecksum = 1,
		// Token: 0x040002A9 RID: 681
		ChecksumCoverage = 20,
		// Token: 0x040002AA RID: 682
		HopLimit,
		// Token: 0x040002AB RID: 683
		UpdateAcceptContext = 28683,
		// Token: 0x040002AC RID: 684
		UpdateConnectContext = 28688
	}
}
