using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020000E7 RID: 231
	public class Score : IScore
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x00013528 File Offset: 0x00011728
		public override string ToString()
		{
			return string.Concat(new object[] { "Rank: '", this.m_Rank, "' Value: '", this.value, "' Category: '", this.leaderboardID, "' PlayerID: '", this.m_UserID, "' Date: '", this.m_Date });
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x000135A8 File Offset: 0x000117A8
		public string leaderboardID { get; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x000135B0 File Offset: 0x000117B0
		public long value { get; }

		// Token: 0x04000391 RID: 913
		private DateTime m_Date;

		// Token: 0x04000392 RID: 914
		private string m_FormattedValue;

		// Token: 0x04000393 RID: 915
		private string m_UserID;

		// Token: 0x04000394 RID: 916
		private int m_Rank;
	}
}
