using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000DA RID: 218
	public class SkinnedMeshRenderer : Renderer
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060007BF RID: 1983
		// (set) Token: 0x060007C0 RID: 1984
		public extern Transform[] bones
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007C1 RID: 1985
		// (set) Token: 0x060007C2 RID: 1986
		public extern Transform rootBone
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060007C3 RID: 1987
		// (set) Token: 0x060007C4 RID: 1988
		public extern SkinQuality quality
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007C5 RID: 1989
		// (set) Token: 0x060007C6 RID: 1990
		public extern Mesh sharedMesh
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x000127F4 File Offset: 0x000109F4
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x000127F8 File Offset: 0x000109F8
		[Obsolete("Has no effect.")]
		public bool skinNormals
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007C9 RID: 1993
		// (set) Token: 0x060007CA RID: 1994
		public extern bool updateWhenOffscreen
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060007CB RID: 1995
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localBounds(out Bounds value);

		// Token: 0x060007CC RID: 1996
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localBounds(ref Bounds value);

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x000127FC File Offset: 0x000109FC
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x00012814 File Offset: 0x00010A14
		public Bounds localBounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_localBounds(out bounds);
				return bounds;
			}
			set
			{
				this.INTERNAL_set_localBounds(ref value);
			}
		}

		// Token: 0x060007CF RID: 1999
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void BakeMesh(Mesh mesh);

		// Token: 0x060007D0 RID: 2000
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern float GetBlendShapeWeight(int index);

		// Token: 0x060007D1 RID: 2001
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetBlendShapeWeight(int index, float value);
	}
}
