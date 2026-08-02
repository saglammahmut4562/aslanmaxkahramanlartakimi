using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000014 RID: 20
	public struct AnimationInfo
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000693C File Offset: 0x00004B3C
		public AnimationClip clip
		{
			get
			{
				return (this.m_ClipInstanceID == 0) ? null : AnimationInfo.ClipInstanceToScriptingObject(this.m_ClipInstanceID);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000695C File Offset: 0x00004B5C
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
		}

		// Token: 0x060001CC RID: 460
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern AnimationClip ClipInstanceToScriptingObject(int instanceID);

		// Token: 0x0400001A RID: 26
		private int m_ClipInstanceID;

		// Token: 0x0400001B RID: 27
		private float m_Weight;
	}
}
