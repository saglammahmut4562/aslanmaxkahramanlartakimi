using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000105 RID: 261
	public sealed class Texture2D : Texture
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x00016E8C File Offset: 0x0001508C
		public Texture2D(int width, int height, TextureFormat format, bool mipmap)
		{
			Texture2D.Internal_Create(this, width, height, format, mipmap, false, IntPtr.Zero);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00016EA8 File Offset: 0x000150A8
		public Texture2D(int width, int height, TextureFormat format, bool mipmap, bool linear)
		{
			Texture2D.Internal_Create(this, width, height, format, mipmap, linear, IntPtr.Zero);
		}

		// Token: 0x060008C9 RID: 2249
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] Texture2D mono, int width, int height, TextureFormat format, bool mipmap, bool linear, IntPtr nativeTex);

		// Token: 0x060008CA RID: 2250 RVA: 0x00016EC4 File Offset: 0x000150C4
		public void SetPixel(int x, int y, Color color)
		{
			Texture2D.INTERNAL_CALL_SetPixel(this, x, y, ref color);
		}

		// Token: 0x060008CB RID: 2251
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetPixel(Texture2D self, int x, int y, ref Color color);

		// Token: 0x060008CC RID: 2252 RVA: 0x00016ED0 File Offset: 0x000150D0
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors)
		{
			int num = 0;
			this.SetPixels(colors, num);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00016EE8 File Offset: 0x000150E8
		public void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			if (num2 < 1)
			{
				num2 = 1;
			}
			this.SetPixels(0, 0, num, num2, colors, miplevel);
		}

		// Token: 0x060008CE RID: 2254
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue("0")] int miplevel);

		// Token: 0x060008CF RID: 2255
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetPixels32(Color32[] colors, [DefaultValue("0")] int miplevel);

		// Token: 0x060008D0 RID: 2256 RVA: 0x00016F2C File Offset: 0x0001512C
		[ExcludeFromDocs]
		public void SetPixels32(Color32[] colors)
		{
			int num = 0;
			this.SetPixels32(colors, num);
		}

		// Token: 0x060008D1 RID: 2257
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool LoadImage(byte[] data);

		// Token: 0x060008D2 RID: 2258 RVA: 0x00016F44 File Offset: 0x00015144
		[ExcludeFromDocs]
		public Color[] GetPixels()
		{
			int num = 0;
			return this.GetPixels(num);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00016F5C File Offset: 0x0001515C
		public Color[] GetPixels([DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			if (num2 < 1)
			{
				num2 = 1;
			}
			return this.GetPixels(0, 0, num, num2, miplevel);
		}

		// Token: 0x060008D4 RID: 2260
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight, [DefaultValue("0")] int miplevel);

		// Token: 0x060008D5 RID: 2261
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Color32[] GetPixels32([DefaultValue("0")] int miplevel);

		// Token: 0x060008D6 RID: 2262 RVA: 0x00016FA0 File Offset: 0x000151A0
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			int num = 0;
			return this.GetPixels32(num);
		}

		// Token: 0x060008D7 RID: 2263
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable);

		// Token: 0x060008D8 RID: 2264 RVA: 0x00016FB8 File Offset: 0x000151B8
		[ExcludeFromDocs]
		public void Apply()
		{
			bool flag = false;
			bool flag2 = true;
			this.Apply(flag2, flag);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00016FD4 File Offset: 0x000151D4
		[ExcludeFromDocs]
		public void ReadPixels(Rect source, int destX, int destY)
		{
			bool flag = true;
			Texture2D.INTERNAL_CALL_ReadPixels(this, ref source, destX, destY, flag);
		}

		// Token: 0x060008DA RID: 2266
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_ReadPixels(Texture2D self, ref Rect source, int destX, int destY, bool recalculateMipMaps);
	}
}
