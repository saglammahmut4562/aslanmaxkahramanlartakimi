using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000F7 RID: 247
	public sealed class SystemInfo
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600082D RID: 2093
		public static extern int processorCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600082E RID: 2094
		public static extern int graphicsShaderLevel
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600082F RID: 2095
		public static extern bool supportsRenderTextures
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000830 RID: 2096
		public static extern bool supportsImageEffects
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000831 RID: 2097
		public static extern bool supportsComputeShaders
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000832 RID: 2098
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool SupportsRenderTextureFormat(RenderTextureFormat format);
	}
}
