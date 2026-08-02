using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000121 RID: 289
	[StructLayout(0)]
	public sealed class WaitForSeconds : YieldInstruction
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x000184FC File Offset: 0x000166FC
		public WaitForSeconds(float seconds)
		{
			this.m_Seconds = seconds;
		}

		// Token: 0x040004CC RID: 1228
		internal float m_Seconds;
	}
}
