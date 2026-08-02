using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000089 RID: 137
	[Obsolete("iPhoneKeyboard class is deprecated. Please use TouchScreenKeyboard instead.")]
	public sealed class iPhoneKeyboard
	{
		// Token: 0x06000621 RID: 1569
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Destroy();

		// Token: 0x06000622 RID: 1570 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		~iPhoneKeyboard()
		{
			this.Destroy();
		}

		// Token: 0x0400019D RID: 413
		private IntPtr keyboardWrapper;
	}
}
