using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000D8 RID: 216
	public sealed class Shader : Object
	{
		// Token: 0x060007BB RID: 1979
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Shader Find(string name);

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060007BC RID: 1980
		public extern bool isSupported
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060007BD RID: 1981
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int PropertyToID(string name);
	}
}
