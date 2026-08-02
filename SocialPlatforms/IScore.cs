using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020000E9 RID: 233
	public interface IScore
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600081B RID: 2075
		string leaderboardID { get; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600081C RID: 2076
		long value { get; }
	}
}
