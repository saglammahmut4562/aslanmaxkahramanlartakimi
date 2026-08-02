using System;

namespace UnityEngine
{
	// Token: 0x0200004C RID: 76
	[Flags]
	public enum EventModifiers
	{
		// Token: 0x04000085 RID: 133
		Shift = 1,
		// Token: 0x04000086 RID: 134
		Control = 2,
		// Token: 0x04000087 RID: 135
		Alt = 4,
		// Token: 0x04000088 RID: 136
		Command = 8,
		// Token: 0x04000089 RID: 137
		Numeric = 16,
		// Token: 0x0400008A RID: 138
		CapsLock = 32,
		// Token: 0x0400008B RID: 139
		FunctionKey = 64
	}
}
