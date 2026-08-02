using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020000E0 RID: 224
	public interface IAchievementDescription
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060007F7 RID: 2039
		string id { get; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060007F8 RID: 2040
		string title { get; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060007F9 RID: 2041
		string achievedDescription { get; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060007FA RID: 2042
		string unachievedDescription { get; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060007FB RID: 2043
		bool hidden { get; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060007FC RID: 2044
		int points { get; }
	}
}
