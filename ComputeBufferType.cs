using System;

namespace UnityEngine
{
	// Token: 0x02000040 RID: 64
	[Flags]
	public enum ComputeBufferType
	{
		// Token: 0x04000067 RID: 103
		Default = 0,
		// Token: 0x04000068 RID: 104
		Raw = 1,
		// Token: 0x04000069 RID: 105
		Append = 2,
		// Token: 0x0400006A RID: 106
		Counter = 4,
		// Token: 0x0400006B RID: 107
		DrawIndirect = 256
	}
}
