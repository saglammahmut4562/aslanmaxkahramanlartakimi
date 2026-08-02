using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020000E6 RID: 230
	public class LocalUser : UserProfile, ILocalUser, IUserProfile
	{
		// Token: 0x0400038E RID: 910
		private IUserProfile[] m_Friends;

		// Token: 0x0400038F RID: 911
		private bool m_Authenticated;

		// Token: 0x04000390 RID: 912
		private bool m_Underage;
	}
}
