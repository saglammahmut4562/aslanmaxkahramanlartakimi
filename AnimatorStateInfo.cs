using System;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	public struct AnimatorStateInfo
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00006998 File Offset: 0x00004B98
		public bool IsName(string name)
		{
			int num = Animator.StringToHash(name);
			return num == this.m_Name || num == this.m_Path;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000069C4 File Offset: 0x00004BC4
		public int nameHash
		{
			get
			{
				return this.m_Path;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000069CC File Offset: 0x00004BCC
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000069D4 File Offset: 0x00004BD4
		public float length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000069DC File Offset: 0x00004BDC
		public int tagHash
		{
			get
			{
				return this.m_Tag;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000069E4 File Offset: 0x00004BE4
		public bool IsTag(string tag)
		{
			return Animator.StringToHash(tag) == this.m_Tag;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000069F4 File Offset: 0x00004BF4
		public bool loop
		{
			get
			{
				return this.m_Loop != 0;
			}
		}

		// Token: 0x04000020 RID: 32
		private int m_Name;

		// Token: 0x04000021 RID: 33
		private int m_Path;

		// Token: 0x04000022 RID: 34
		private float m_NormalizedTime;

		// Token: 0x04000023 RID: 35
		private float m_Length;

		// Token: 0x04000024 RID: 36
		private int m_Tag;

		// Token: 0x04000025 RID: 37
		private int m_Loop;
	}
}
