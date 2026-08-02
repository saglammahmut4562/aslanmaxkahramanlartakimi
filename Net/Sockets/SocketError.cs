using System;

namespace System.Net.Sockets
{
	// Token: 0x0200009E RID: 158
	public enum SocketError
	{
		// Token: 0x04000241 RID: 577
		AccessDenied = 10013,
		// Token: 0x04000242 RID: 578
		AddressAlreadyInUse = 10048,
		// Token: 0x04000243 RID: 579
		AddressFamilyNotSupported = 10047,
		// Token: 0x04000244 RID: 580
		AddressNotAvailable = 10049,
		// Token: 0x04000245 RID: 581
		AlreadyInProgress = 10037,
		// Token: 0x04000246 RID: 582
		ConnectionAborted = 10053,
		// Token: 0x04000247 RID: 583
		ConnectionRefused = 10061,
		// Token: 0x04000248 RID: 584
		ConnectionReset = 10054,
		// Token: 0x04000249 RID: 585
		DestinationAddressRequired = 10039,
		// Token: 0x0400024A RID: 586
		Disconnecting = 10101,
		// Token: 0x0400024B RID: 587
		Fault = 10014,
		// Token: 0x0400024C RID: 588
		HostDown = 10064,
		// Token: 0x0400024D RID: 589
		HostNotFound = 11001,
		// Token: 0x0400024E RID: 590
		HostUnreachable = 10065,
		// Token: 0x0400024F RID: 591
		InProgress = 10036,
		// Token: 0x04000250 RID: 592
		Interrupted = 10004,
		// Token: 0x04000251 RID: 593
		InvalidArgument = 10022,
		// Token: 0x04000252 RID: 594
		IOPending = 997,
		// Token: 0x04000253 RID: 595
		IsConnected = 10056,
		// Token: 0x04000254 RID: 596
		MessageSize = 10040,
		// Token: 0x04000255 RID: 597
		NetworkDown = 10050,
		// Token: 0x04000256 RID: 598
		NetworkReset = 10052,
		// Token: 0x04000257 RID: 599
		NetworkUnreachable = 10051,
		// Token: 0x04000258 RID: 600
		NoBufferSpaceAvailable = 10055,
		// Token: 0x04000259 RID: 601
		NoData = 11004,
		// Token: 0x0400025A RID: 602
		NoRecovery = 11003,
		// Token: 0x0400025B RID: 603
		NotConnected = 10057,
		// Token: 0x0400025C RID: 604
		NotInitialized = 10093,
		// Token: 0x0400025D RID: 605
		NotSocket = 10038,
		// Token: 0x0400025E RID: 606
		OperationAborted = 995,
		// Token: 0x0400025F RID: 607
		OperationNotSupported = 10045,
		// Token: 0x04000260 RID: 608
		ProcessLimit = 10067,
		// Token: 0x04000261 RID: 609
		ProtocolFamilyNotSupported = 10046,
		// Token: 0x04000262 RID: 610
		ProtocolNotSupported = 10043,
		// Token: 0x04000263 RID: 611
		ProtocolOption = 10042,
		// Token: 0x04000264 RID: 612
		ProtocolType = 10041,
		// Token: 0x04000265 RID: 613
		Shutdown = 10058,
		// Token: 0x04000266 RID: 614
		SocketError = -1,
		// Token: 0x04000267 RID: 615
		SocketNotSupported = 10044,
		// Token: 0x04000268 RID: 616
		Success = 0,
		// Token: 0x04000269 RID: 617
		SystemNotReady = 10091,
		// Token: 0x0400026A RID: 618
		TimedOut = 10060,
		// Token: 0x0400026B RID: 619
		TooManyOpenSockets = 10024,
		// Token: 0x0400026C RID: 620
		TryAgain = 11002,
		// Token: 0x0400026D RID: 621
		TypeNotFound = 10109,
		// Token: 0x0400026E RID: 622
		VersionNotSupported = 10092,
		// Token: 0x0400026F RID: 623
		WouldBlock = 10035
	}
}
