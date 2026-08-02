using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000044 RID: 68
	[StructLayout(0)]
	public sealed class Coroutine : YieldInstruction
	{
		// Token: 0x0600031B RID: 795
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void ReleaseCoroutine();

		// Token: 0x0600031C RID: 796 RVA: 0x00007BA4 File Offset: 0x00005DA4
		~Coroutine()
		{
			this.ReleaseCoroutine();
		}

		// Token: 0x04000077 RID: 119
		internal IntPtr m_Ptr;
	}
}
