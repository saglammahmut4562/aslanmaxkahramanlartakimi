using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200008E RID: 142
	[StructLayout(2)]
	public struct jvalue
	{
		// Token: 0x040001A2 RID: 418
		[FieldOffset(0)]
		public bool z;

		// Token: 0x040001A3 RID: 419
		[FieldOffset(0)]
		public byte b;

		// Token: 0x040001A4 RID: 420
		[FieldOffset(0)]
		public char c;

		// Token: 0x040001A5 RID: 421
		[FieldOffset(0)]
		public short s;

		// Token: 0x040001A6 RID: 422
		[FieldOffset(0)]
		public int i;

		// Token: 0x040001A7 RID: 423
		[FieldOffset(0)]
		public long j;

		// Token: 0x040001A8 RID: 424
		[FieldOffset(0)]
		public float f;

		// Token: 0x040001A9 RID: 425
		[FieldOffset(0)]
		public double d;

		// Token: 0x040001AA RID: 426
		[FieldOffset(0)]
		public IntPtr l;
	}
}
