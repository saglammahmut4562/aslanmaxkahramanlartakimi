using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000091 RID: 145
	public struct LayerMask
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00010060 File Offset: 0x0000E260
		public int value
		{
			get
			{
				return this.m_Mask;
			}
		}

		// Token: 0x0600062A RID: 1578
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int NameToLayer(string layerName);

		// Token: 0x0600062B RID: 1579 RVA: 0x00010068 File Offset: 0x0000E268
		public static implicit operator int(LayerMask mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00010074 File Offset: 0x0000E274
		public static implicit operator LayerMask(int intVal)
		{
			LayerMask layerMask;
			layerMask.m_Mask = intVal;
			return layerMask;
		}

		// Token: 0x040002A1 RID: 673
		private int m_Mask;
	}
}
