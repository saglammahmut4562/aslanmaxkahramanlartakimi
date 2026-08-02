using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	[StructLayout(0)]
	public sealed class AnimationEvent
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00006900 File Offset: 0x00004B00
		~AnimationEvent()
		{
			if (this.m_OwnsData != 0)
			{
				this.Destroy();
			}
		}

		// Token: 0x060001C9 RID: 457
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Destroy();

		// Token: 0x04000018 RID: 24
		[NotRenamed]
		internal IntPtr m_Ptr;

		// Token: 0x04000019 RID: 25
		private int m_OwnsData;
	}
}
