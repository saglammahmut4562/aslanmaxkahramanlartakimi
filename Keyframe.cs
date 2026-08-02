using System;

namespace UnityEngine
{
	// Token: 0x02000090 RID: 144
	public struct Keyframe
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x00010010 File Offset: 0x0000E210
		public Keyframe(float time, float value)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = 0f;
			this.m_OutTangent = 0f;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00010038 File Offset: 0x0000E238
		public Keyframe(float time, float value, float inTangent, float outTangent)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00010058 File Offset: 0x0000E258
		public float time
		{
			get
			{
				return this.m_Time;
			}
		}

		// Token: 0x0400029D RID: 669
		private float m_Time;

		// Token: 0x0400029E RID: 670
		private float m_Value;

		// Token: 0x0400029F RID: 671
		private float m_InTangent;

		// Token: 0x040002A0 RID: 672
		private float m_OutTangent;
	}
}
