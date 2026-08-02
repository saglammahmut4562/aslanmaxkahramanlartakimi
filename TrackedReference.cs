using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000111 RID: 273
	[StructLayout(0)]
	public class TrackedReference
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0001714C File Offset: 0x0001534C
		public override bool Equals(object o)
		{
			return o as TrackedReference == this;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001715C File Offset: 0x0001535C
		public override int GetHashCode()
		{
			return (int)this.m_Ptr;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001716C File Offset: 0x0001536C
		public static bool operator ==(TrackedReference x, TrackedReference y)
		{
			if (y == null && x == null)
			{
				return true;
			}
			if (y == null)
			{
				return x.m_Ptr == IntPtr.Zero;
			}
			if (x == null)
			{
				return y.m_Ptr == IntPtr.Zero;
			}
			return x.m_Ptr == y.m_Ptr;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000171CC File Offset: 0x000153CC
		public static bool operator !=(TrackedReference x, TrackedReference y)
		{
			return !(x == y);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000171D8 File Offset: 0x000153D8
		public static implicit operator bool(TrackedReference exists)
		{
			return exists != null;
		}

		// Token: 0x040004AD RID: 1197
		[NotRenamed]
		internal IntPtr m_Ptr;
	}
}
