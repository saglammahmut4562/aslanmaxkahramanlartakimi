using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020000DF RID: 223
	public interface IAchievement
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060007F2 RID: 2034
		string id { get; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060007F3 RID: 2035
		double percentCompleted { get; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060007F4 RID: 2036
		bool completed { get; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007F5 RID: 2037
		bool hidden { get; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007F6 RID: 2038
		DateTime lastReportedDate { get; }
	}
}
