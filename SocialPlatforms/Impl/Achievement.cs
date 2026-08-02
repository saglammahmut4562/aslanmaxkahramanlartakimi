using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020000E3 RID: 227
	public class Achievement : IAchievement
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x000132A4 File Offset: 0x000114A4
		public override string ToString()
		{
			return string.Concat(new object[] { this.id, " - ", this.percentCompleted, " - ", this.completed, " - ", this.hidden, " - ", this.lastReportedDate });
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00013320 File Offset: 0x00011520
		public string id { get; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00013328 File Offset: 0x00011528
		public double percentCompleted { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00013330 File Offset: 0x00011530
		public bool completed
		{
			get
			{
				return this.m_Completed;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00013338 File Offset: 0x00011538
		public bool hidden
		{
			get
			{
				return this.m_Hidden;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00013340 File Offset: 0x00011540
		public DateTime lastReportedDate
		{
			get
			{
				return this.m_LastReportedDate;
			}
		}

		// Token: 0x04000378 RID: 888
		private bool m_Completed;

		// Token: 0x04000379 RID: 889
		private bool m_Hidden;

		// Token: 0x0400037A RID: 890
		private DateTime m_LastReportedDate;
	}
}
