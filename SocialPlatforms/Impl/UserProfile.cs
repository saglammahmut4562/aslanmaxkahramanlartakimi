using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020000E8 RID: 232
	public class UserProfile : IUserProfile
	{
		// Token: 0x06000816 RID: 2070 RVA: 0x000135B8 File Offset: 0x000117B8
		public override string ToString()
		{
			return string.Concat(new object[] { this.id, " - ", this.userName, " - ", this.isFriend, " - ", this.state });
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00013618 File Offset: 0x00011818
		public string userName
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00013620 File Offset: 0x00011820
		public string id
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00013628 File Offset: 0x00011828
		public bool isFriend
		{
			get
			{
				return this.m_IsFriend;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00013630 File Offset: 0x00011830
		public UserState state
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x04000397 RID: 919
		protected string m_UserName;

		// Token: 0x04000398 RID: 920
		protected string m_ID;

		// Token: 0x04000399 RID: 921
		protected bool m_IsFriend;

		// Token: 0x0400039A RID: 922
		protected UserState m_State;

		// Token: 0x0400039B RID: 923
		protected Texture2D m_Image;
	}
}
