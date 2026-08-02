using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	[StructLayout(0)]
	public sealed class AssetBundleRequest : AsyncOperation
	{
		// Token: 0x0400002A RID: 42
		internal AssetBundle m_AssetBundle;

		// Token: 0x0400002B RID: 43
		internal string m_Path;

		// Token: 0x0400002C RID: 44
		internal Type m_Type;
	}
}
