using System;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	public struct AnimatorTransitionInfo
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00006A04 File Offset: 0x00004C04
		public bool IsName(string name)
		{
			return Animator.StringToHash(name) == this.m_Name;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006A14 File Offset: 0x00004C14
		public bool IsUserName(string name)
		{
			return Animator.StringToHash(name) == this.m_UserName;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00006A24 File Offset: 0x00004C24
		public int nameHash
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00006A2C File Offset: 0x00004C2C
		public int userNameHash
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00006A34 File Offset: 0x00004C34
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x04000026 RID: 38
		private int m_Name;

		// Token: 0x04000027 RID: 39
		private int m_UserName;

		// Token: 0x04000028 RID: 40
		private float m_NormalizedTime;
	}
}
