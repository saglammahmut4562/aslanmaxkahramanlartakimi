using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000106 RID: 262
	public sealed class Texture3D : Texture
	{
		// Token: 0x060008DB RID: 2267 RVA: 0x00016FF0 File Offset: 0x000151F0
		public Texture3D(int width, int height, int depth, TextureFormat format, bool mipmap)
		{
			Texture3D.Internal_Create(this, width, height, depth, format, mipmap);
		}

		// Token: 0x060008DC RID: 2268
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel);

		// Token: 0x060008DD RID: 2269 RVA: 0x00017008 File Offset: 0x00015208
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors)
		{
			int num = 0;
			this.SetPixels(colors, num);
		}

		// Token: 0x060008DE RID: 2270
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Apply([DefaultValue("true")] bool updateMipmaps);

		// Token: 0x060008DF RID: 2271 RVA: 0x00017020 File Offset: 0x00015220
		[ExcludeFromDocs]
		public void Apply()
		{
			bool flag = true;
			this.Apply(flag);
		}

		// Token: 0x060008E0 RID: 2272
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] Texture3D mono, int width, int height, int depth, TextureFormat format, bool mipmap);
	}
}
