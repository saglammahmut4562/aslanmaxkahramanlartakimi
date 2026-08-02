using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200001F RID: 31
	[StructLayout(0)]
	public class AsyncOperation : YieldInstruction
	{
		// Token: 0x0600022E RID: 558
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void InternalDestroy();

		// Token: 0x0600022F RID: 559 RVA: 0x00006E24 File Offset: 0x00005024
		~AsyncOperation()
		{
			this.InternalDestroy();
		}

		// Token: 0x0400002D RID: 45
		[NotRenamed]
		internal IntPtr m_Ptr;
	}
}
