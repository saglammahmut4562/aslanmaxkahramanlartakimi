using System;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020000EC RID: 236
	public class Local : ISocialPlatform
	{
		// Token: 0x0400039C RID: 924
		private static LocalUser m_LocalUser;

		// Token: 0x0400039D RID: 925
		private List<UserProfile> m_Friends = new List<UserProfile>();

		// Token: 0x0400039E RID: 926
		private List<UserProfile> m_Users = new List<UserProfile>();

		// Token: 0x0400039F RID: 927
		private List<AchievementDescription> m_AchievementDescriptions = new List<AchievementDescription>();

		// Token: 0x040003A0 RID: 928
		private List<Achievement> m_Achievements = new List<Achievement>();

		// Token: 0x040003A1 RID: 929
		private List<Leaderboard> m_Leaderboards = new List<Leaderboard>();

		// Token: 0x040003A2 RID: 930
		private Texture2D m_DefaultTexture;
	}
}
