using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000C2 RID: 194
	public class Renderer : Component
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000763 RID: 1891
		// (set) Token: 0x06000764 RID: 1892
		public extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000765 RID: 1893
		// (set) Token: 0x06000766 RID: 1894
		public extern bool castShadows
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000767 RID: 1895
		// (set) Token: 0x06000768 RID: 1896
		public extern bool receiveShadows
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000769 RID: 1897
		// (set) Token: 0x0600076A RID: 1898
		public extern Material material
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600076B RID: 1899
		public extern Material sharedMaterial
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600076C RID: 1900
		// (set) Token: 0x0600076D RID: 1901
		public extern Material[] sharedMaterials
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600076E RID: 1902
		// (set) Token: 0x0600076F RID: 1903
		public extern Material[] materials
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000770 RID: 1904
		public extern Bounds bounds
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000771 RID: 1905
		// (set) Token: 0x06000772 RID: 1906
		public extern bool useLightProbes
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000188 RID: 392
		// (set) Token: 0x06000773 RID: 1907
		public extern string sortingLayerName
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000774 RID: 1908
		// (set) Token: 0x06000775 RID: 1909
		public extern int sortingOrder
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}
	}
}
