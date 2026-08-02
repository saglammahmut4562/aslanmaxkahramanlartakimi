using System;

namespace UnityEngine
{
	// Token: 0x0200010C RID: 268
	public struct Touch
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00017038 File Offset: 0x00015238
		public int fingerId
		{
			get
			{
				return this.m_FingerId;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00017040 File Offset: 0x00015240
		public Vector2 position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00017048 File Offset: 0x00015248
		public TouchPhase phase
		{
			get
			{
				return this.m_Phase;
			}
		}

		// Token: 0x04000491 RID: 1169
		private int m_FingerId;

		// Token: 0x04000492 RID: 1170
		private Vector2 m_Position;

		// Token: 0x04000493 RID: 1171
		private Vector2 m_RawPosition;

		// Token: 0x04000494 RID: 1172
		private Vector2 m_PositionDelta;

		// Token: 0x04000495 RID: 1173
		private float m_TimeDelta;

		// Token: 0x04000496 RID: 1174
		private int m_TapCount;

		// Token: 0x04000497 RID: 1175
		private TouchPhase m_Phase;
	}
}
