using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020000EB RID: 235
	public interface IUserProfile
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600081D RID: 2077
		string userName { get; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600081E RID: 2078
		string id { get; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600081F RID: 2079
		bool isFriend { get; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000820 RID: 2080
		UserState state { get; }
	}
}
