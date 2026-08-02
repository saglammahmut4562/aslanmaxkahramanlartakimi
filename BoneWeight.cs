using System;

namespace UnityEngine
{
	// Token: 0x0200002B RID: 43
	public struct BoneWeight
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00007204 File Offset: 0x00005404
		public float weight0
		{
			get
			{
				return this.m_Weight0;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000720C File Offset: 0x0000540C
		public float weight1
		{
			get
			{
				return this.m_Weight1;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00007214 File Offset: 0x00005414
		public float weight2
		{
			get
			{
				return this.m_Weight2;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000721C File Offset: 0x0000541C
		public float weight3
		{
			get
			{
				return this.m_Weight3;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00007224 File Offset: 0x00005424
		public int boneIndex0
		{
			get
			{
				return this.m_BoneIndex0;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000722C File Offset: 0x0000542C
		public int boneIndex1
		{
			get
			{
				return this.m_BoneIndex1;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00007234 File Offset: 0x00005434
		public int boneIndex2
		{
			get
			{
				return this.m_BoneIndex2;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000723C File Offset: 0x0000543C
		public int boneIndex3
		{
			get
			{
				return this.m_BoneIndex3;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00007244 File Offset: 0x00005444
		public override int GetHashCode()
		{
			return this.boneIndex0.GetHashCode() ^ (this.boneIndex1.GetHashCode() << 2) ^ (this.boneIndex2.GetHashCode() >> 2) ^ (this.boneIndex3.GetHashCode() >> 1) ^ (this.weight0.GetHashCode() << 5) ^ (this.weight1.GetHashCode() << 4) ^ (this.weight2.GetHashCode() >> 4) ^ (this.weight3.GetHashCode() >> 3);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000072DC File Offset: 0x000054DC
		public override bool Equals(object other)
		{
			if (!(other is BoneWeight))
			{
				return false;
			}
			BoneWeight boneWeight = (BoneWeight)other;
			bool flag;
			if (this.boneIndex0.Equals(boneWeight.boneIndex0) && this.boneIndex1.Equals(boneWeight.boneIndex1) && this.boneIndex2.Equals(boneWeight.boneIndex2) && this.boneIndex3.Equals(boneWeight.boneIndex3))
			{
				Vector4 vector = new Vector4(this.weight0, this.weight1, this.weight2, this.weight3);
				flag = vector.Equals(new Vector4(boneWeight.weight0, boneWeight.weight1, boneWeight.weight2, boneWeight.weight3));
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000038 RID: 56
		private float m_Weight0;

		// Token: 0x04000039 RID: 57
		private float m_Weight1;

		// Token: 0x0400003A RID: 58
		private float m_Weight2;

		// Token: 0x0400003B RID: 59
		private float m_Weight3;

		// Token: 0x0400003C RID: 60
		private int m_BoneIndex0;

		// Token: 0x0400003D RID: 61
		private int m_BoneIndex1;

		// Token: 0x0400003E RID: 62
		private int m_BoneIndex2;

		// Token: 0x0400003F RID: 63
		private int m_BoneIndex3;
	}
}
