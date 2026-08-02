using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	[StructLayout(0)]
	public sealed class RectOffset
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x00011E80 File Offset: 0x00010080
		public RectOffset()
		{
			this.Init();
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00011E90 File Offset: 0x00010090
		internal RectOffset(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00011EA8 File Offset: 0x000100A8
		~RectOffset()
		{
			if (this.m_SourceStyle == null)
			{
				this.Cleanup();
			}
		}

		// Token: 0x06000751 RID: 1873
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x06000752 RID: 1874
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000753 RID: 1875
		// (set) Token: 0x06000754 RID: 1876
		public extern int left
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000755 RID: 1877
		// (set) Token: 0x06000756 RID: 1878
		public extern int right
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000757 RID: 1879
		// (set) Token: 0x06000758 RID: 1880
		public extern int top
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000759 RID: 1881
		// (set) Token: 0x0600075A RID: 1882
		public extern int bottom
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600075B RID: 1883
		public extern int horizontal
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600075C RID: 1884
		public extern int vertical
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00011EE4 File Offset: 0x000100E4
		public Rect Add(Rect rect)
		{
			return RectOffset.INTERNAL_CALL_Add(this, ref rect);
		}

		// Token: 0x0600075E RID: 1886
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Rect INTERNAL_CALL_Add(RectOffset self, ref Rect rect);

		// Token: 0x0600075F RID: 1887 RVA: 0x00011EF0 File Offset: 0x000100F0
		public Rect Remove(Rect rect)
		{
			return RectOffset.INTERNAL_CALL_Remove(this, ref rect);
		}

		// Token: 0x06000760 RID: 1888
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Rect INTERNAL_CALL_Remove(RectOffset self, ref Rect rect);

		// Token: 0x06000761 RID: 1889 RVA: 0x00011EFC File Offset: 0x000100FC
		public override string ToString()
		{
			return UnityString.Format("RectOffset (l:{0} r:{1} t:{2} b:{3})", new object[] { this.left, this.right, this.top, this.bottom });
		}

		// Token: 0x0400030F RID: 783
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000310 RID: 784
		private GUIStyle m_SourceStyle;
	}
}
