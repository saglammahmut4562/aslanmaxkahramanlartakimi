using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020000E4 RID: 228
	public class AchievementDescription : IAchievementDescription
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x00013348 File Offset: 0x00011548
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.id, " - ", this.title, " - ", this.achievedDescription, " - ", this.unachievedDescription, " - ", this.points, " - ",
				this.hidden
			});
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x000133CC File Offset: 0x000115CC
		public string id { get; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x000133D4 File Offset: 0x000115D4
		public string title
		{
			get
			{
				return this.m_Title;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x000133DC File Offset: 0x000115DC
		public string achievedDescription
		{
			get
			{
				return this.m_AchievedDescription;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x000133E4 File Offset: 0x000115E4
		public string unachievedDescription
		{
			get
			{
				return this.m_UnachievedDescription;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x000133EC File Offset: 0x000115EC
		public bool hidden
		{
			get
			{
				return this.m_Hidden;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x000133F4 File Offset: 0x000115F4
		public int points
		{
			get
			{
				return this.m_Points;
			}
		}

		// Token: 0x0400037D RID: 893
		private string m_Title;

		// Token: 0x0400037E RID: 894
		private Texture2D m_Image;

		// Token: 0x0400037F RID: 895
		private string m_AchievedDescription;

		// Token: 0x04000380 RID: 896
		private string m_UnachievedDescription;

		// Token: 0x04000381 RID: 897
		private bool m_Hidden;

		// Token: 0x04000382 RID: 898
		private int m_Points;
	}
}
