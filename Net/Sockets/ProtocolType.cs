using System;

namespace System.Net.Sockets
{
	// Token: 0x02000097 RID: 151
	public enum ProtocolType
	{
		// Token: 0x040001E1 RID: 481
		IP,
		// Token: 0x040001E2 RID: 482
		Icmp,
		// Token: 0x040001E3 RID: 483
		Igmp,
		// Token: 0x040001E4 RID: 484
		Ggp,
		// Token: 0x040001E5 RID: 485
		Tcp = 6,
		// Token: 0x040001E6 RID: 486
		Pup = 12,
		// Token: 0x040001E7 RID: 487
		Udp = 17,
		// Token: 0x040001E8 RID: 488
		Idp = 22,
		// Token: 0x040001E9 RID: 489
		IPv6 = 41,
		// Token: 0x040001EA RID: 490
		ND = 77,
		// Token: 0x040001EB RID: 491
		Raw = 255,
		// Token: 0x040001EC RID: 492
		Unspecified = 0,
		// Token: 0x040001ED RID: 493
		Ipx = 1000,
		// Token: 0x040001EE RID: 494
		Spx = 1256,
		// Token: 0x040001EF RID: 495
		SpxII,
		// Token: 0x040001F0 RID: 496
		Unknown = -1,
		// Token: 0x040001F1 RID: 497
		IPv4 = 4,
		// Token: 0x040001F2 RID: 498
		IPv6RoutingHeader = 43,
		// Token: 0x040001F3 RID: 499
		IPv6FragmentHeader,
		// Token: 0x040001F4 RID: 500
		IPSecEncapsulatingSecurityPayload = 50,
		// Token: 0x040001F5 RID: 501
		IPSecAuthenticationHeader,
		// Token: 0x040001F6 RID: 502
		IcmpV6 = 58,
		// Token: 0x040001F7 RID: 503
		IPv6NoNextHeader,
		// Token: 0x040001F8 RID: 504
		IPv6DestinationOptions,
		// Token: 0x040001F9 RID: 505
		IPv6HopByHopOptions = 0
	}
}
