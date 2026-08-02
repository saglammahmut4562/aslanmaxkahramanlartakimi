using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020000E5 RID: 229
	public class Leaderboard : ILeaderboard
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x000133FC File Offset: 0x000115FC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"ID: '",
				this.id,
				"' Title: '",
				this.m_Title,
				"' Loading: '",
				this.m_Loading,
				"' Range: [",
				this.range.from,
				",",
				this.range.count,
				"] MaxRange: '",
				this.m_MaxRange,
				"' Scores: '",
				this.m_Scores.Length,
				"' UserScope: '",
				this.userScope,
				"' TimeScope: '",
				this.timeScope,
				"' UserFilter: '",
				this.m_UserIDs.Length
			});
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00013508 File Offset: 0x00011708
		public string id { get; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00013510 File Offset: 0x00011710
		public UserScope userScope { get; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00013518 File Offset: 0x00011718
		public Range range { get; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00013520 File Offset: 0x00011720
		public TimeScope timeScope { get; }

		// Token: 0x04000384 RID: 900
		private bool m_Loading;

		// Token: 0x04000385 RID: 901
		private IScore m_LocalUserScore;

		// Token: 0x04000386 RID: 902
		private uint m_MaxRange;

		// Token: 0x04000387 RID: 903
		private IScore[] m_Scores;

		// Token: 0x04000388 RID: 904
		private string m_Title;

		// Token: 0x04000389 RID: 905
		private string[] m_UserIDs;
	}
}
