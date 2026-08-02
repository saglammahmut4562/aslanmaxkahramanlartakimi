using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000C4 RID: 196
	public sealed class RenderTexture : Texture
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x00011F5C File Offset: 0x0001015C
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format)
		{
			RenderTexture.Internal_CreateRenderTexture(this);
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.format = format;
			RenderTexture.Internal_SetSRGBReadWrite(this, QualitySettings.activeColorSpace == ColorSpace.Linear);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00011F98 File Offset: 0x00010198
		public RenderTexture(int width, int height, int depth)
		{
			RenderTexture.Internal_CreateRenderTexture(this);
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.format = RenderTextureFormat.Default;
			RenderTexture.Internal_SetSRGBReadWrite(this, QualitySettings.activeColorSpace == ColorSpace.Linear);
		}

		// Token: 0x06000778 RID: 1912
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateRenderTexture([Writable] RenderTexture rt);

		// Token: 0x06000779 RID: 1913
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern RenderTexture GetTemporary(int width, int height, [DefaultValue("0")] int depthBuffer, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite, [DefaultValue("1")] int antiAliasing);

		// Token: 0x0600077A RID: 1914 RVA: 0x00011FD0 File Offset: 0x000101D0
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format)
		{
			int num = 1;
			RenderTextureReadWrite renderTextureReadWrite = RenderTextureReadWrite.Default;
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, renderTextureReadWrite, num);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00011FEC File Offset: 0x000101EC
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
		{
			int num = 1;
			RenderTextureReadWrite renderTextureReadWrite = RenderTextureReadWrite.Default;
			RenderTextureFormat renderTextureFormat = RenderTextureFormat.Default;
			return RenderTexture.GetTemporary(width, height, depthBuffer, renderTextureFormat, renderTextureReadWrite, num);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001200C File Offset: 0x0001020C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height)
		{
			int num = 1;
			RenderTextureReadWrite renderTextureReadWrite = RenderTextureReadWrite.Default;
			RenderTextureFormat renderTextureFormat = RenderTextureFormat.Default;
			int num2 = 0;
			return RenderTexture.GetTemporary(width, height, num2, renderTextureFormat, renderTextureReadWrite, num);
		}

		// Token: 0x0600077D RID: 1917
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void ReleaseTemporary(RenderTexture temp);

		// Token: 0x0600077E RID: 1918
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int Internal_GetWidth(RenderTexture mono);

		// Token: 0x0600077F RID: 1919
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetWidth(RenderTexture mono, int width);

		// Token: 0x06000780 RID: 1920
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int Internal_GetHeight(RenderTexture mono);

		// Token: 0x06000781 RID: 1921
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetHeight(RenderTexture mono, int width);

		// Token: 0x06000782 RID: 1922
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetSRGBReadWrite(RenderTexture mono, bool sRGB);

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0001202C File Offset: 0x0001022C
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x00012034 File Offset: 0x00010234
		public override int width
		{
			get
			{
				return RenderTexture.Internal_GetWidth(this);
			}
			set
			{
				RenderTexture.Internal_SetWidth(this, value);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00012040 File Offset: 0x00010240
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x00012048 File Offset: 0x00010248
		public override int height
		{
			get
			{
				return RenderTexture.Internal_GetHeight(this);
			}
			set
			{
				RenderTexture.Internal_SetHeight(this, value);
			}
		}

		// Token: 0x1700018C RID: 396
		// (set) Token: 0x06000787 RID: 1927
		public extern int depth
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000788 RID: 1928
		// (set) Token: 0x06000789 RID: 1929
		public extern RenderTextureFormat format
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00012054 File Offset: 0x00010254
		public void DiscardContents()
		{
			RenderTexture.INTERNAL_CALL_DiscardContents(this);
		}

		// Token: 0x0600078B RID: 1931
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DiscardContents(RenderTexture self);

		// Token: 0x1700018E RID: 398
		// (set) Token: 0x0600078C RID: 1932
		public static extern RenderTexture active
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}
	}
}
