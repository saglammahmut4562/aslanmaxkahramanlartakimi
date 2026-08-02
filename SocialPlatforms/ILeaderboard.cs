using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020000E1 RID: 225
	public interface ILeaderboard
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060007FD RID: 2045
		string id { get; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007FE RID: 2046
		UserScope userScope { get; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060007FF RID: 2047
		Range range { get; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000800 RID: 2048
		TimeScope timeScope { get; }
	}
}
