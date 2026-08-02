using System;

namespace UnityEngine
{
	// Token: 0x0200007E RID: 126
	public struct HumanBone
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000FECC File Offset: 0x0000E0CC
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0000FED4 File Offset: 0x0000E0D4
		public string boneName
		{
			get
			{
				return this.m_BoneName;
			}
			set
			{
				this.m_BoneName = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		public string humanName
		{
			get
			{
				return this.m_HumanName;
			}
			set
			{
				this.m_HumanName = value;
			}
		}

		// Token: 0x04000172 RID: 370
		private string m_BoneName;

		// Token: 0x04000173 RID: 371
		private string m_HumanName;

		// Token: 0x04000174 RID: 372
		public HumanLimit limit;
	}
}
