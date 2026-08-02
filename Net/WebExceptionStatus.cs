using System;

namespace System.Net
{
	// Token: 0x020000AC RID: 172
	public enum WebExceptionStatus
	{
		// Token: 0x04000314 RID: 788
		Success,
		// Token: 0x04000315 RID: 789
		NameResolutionFailure,
		// Token: 0x04000316 RID: 790
		ConnectFailure,
		// Token: 0x04000317 RID: 791
		ReceiveFailure,
		// Token: 0x04000318 RID: 792
		SendFailure,
		// Token: 0x04000319 RID: 793
		PipelineFailure,
		// Token: 0x0400031A RID: 794
		RequestCanceled,
		// Token: 0x0400031B RID: 795
		ProtocolError,
		// Token: 0x0400031C RID: 796
		ConnectionClosed,
		// Token: 0x0400031D RID: 797
		TrustFailure,
		// Token: 0x0400031E RID: 798
		SecureChannelFailure,
		// Token: 0x0400031F RID: 799
		ServerProtocolViolation,
		// Token: 0x04000320 RID: 800
		KeepAliveFailure,
		// Token: 0x04000321 RID: 801
		Pending,
		// Token: 0x04000322 RID: 802
		Timeout,
		// Token: 0x04000323 RID: 803
		ProxyNameResolutionFailure,
		// Token: 0x04000324 RID: 804
		UnknownError,
		// Token: 0x04000325 RID: 805
		MessageLengthLimitExceeded,
		// Token: 0x04000326 RID: 806
		CacheEntryNotFound,
		// Token: 0x04000327 RID: 807
		RequestProhibitedByCachePolicy,
		// Token: 0x04000328 RID: 808
		RequestProhibitedByProxy
	}
}
