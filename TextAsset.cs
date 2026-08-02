using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000FA RID: 250
	public class TextAsset : Object
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000833 RID: 2099
		public extern string text
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000834 RID: 2100
		public extern byte[] bytes
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00013D18 File Offset: 0x00011F18
		public override string ToString()
		{
			return this.text;
		}
	}
}
