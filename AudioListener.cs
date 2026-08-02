using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000024 RID: 36
	public sealed class AudioListener : Behaviour
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600024F RID: 591
		// (set) Token: 0x06000250 RID: 592
		public static extern float volume
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}
	}
}
