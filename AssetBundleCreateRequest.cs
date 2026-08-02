using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	public sealed class AssetBundleCreateRequest : AsyncOperation
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600022B RID: 555
		public extern AssetBundle assetBundle
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600022C RID: 556
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern void DisableCompatibilityChecks();
	}
}
