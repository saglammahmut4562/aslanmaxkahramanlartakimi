using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000104 RID: 260
	public class Texture : Object
	{
		// Token: 0x060008BC RID: 2236
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int Internal_GetWidth(Texture mono);

		// Token: 0x060008BD RID: 2237
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int Internal_GetHeight(Texture mono);

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00016E4C File Offset: 0x0001504C
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00016E54 File Offset: 0x00015054
		public virtual int width
		{
			get
			{
				return Texture.Internal_GetWidth(this);
			}
			set
			{
				throw new Exception("not implemented");
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00016E60 File Offset: 0x00015060
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00016E68 File Offset: 0x00015068
		public virtual int height
		{
			get
			{
				return Texture.Internal_GetHeight(this);
			}
			set
			{
				throw new Exception("not implemented");
			}
		}

		// Token: 0x170001E3 RID: 483
		// (set) Token: 0x060008C2 RID: 2242
		public extern FilterMode filterMode
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001E4 RID: 484
		// (set) Token: 0x060008C3 RID: 2243
		public extern int anisoLevel
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001E5 RID: 485
		// (set) Token: 0x060008C4 RID: 2244
		public extern TextureWrapMode wrapMode
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060008C5 RID: 2245
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_GetTexelSize(Texture tex, out Vector2 output);

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00016E74 File Offset: 0x00015074
		public Vector2 texelSize
		{
			get
			{
				Vector2 vector;
				Texture.Internal_GetTexelSize(this, out vector);
				return vector;
			}
		}
	}
}
