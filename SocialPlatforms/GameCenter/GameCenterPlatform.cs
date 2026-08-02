using System;

namespace UnityEngine.SocialPlatforms.GameCenter
{
	// Token: 0x020000DE RID: 222
	public class GameCenterPlatform : Local
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x00013278 File Offset: 0x00011478
		public static void ResetAllAchievements(Action<bool> callback)
		{
			Debug.Log("ResetAllAchievements - no effect in editor");
			callback(true);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001328C File Offset: 0x0001148C
		public static void ShowDefaultAchievementCompletionBanner(bool value)
		{
			Debug.Log("ShowDefaultAchievementCompletionBanner - no effect in editor");
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00013298 File Offset: 0x00011498
		public static void ShowLeaderboardUI(string leaderboardID, TimeScope timeScope)
		{
			Debug.Log("ShowLeaderboardUI - no effect in editor");
		}
	}
}
