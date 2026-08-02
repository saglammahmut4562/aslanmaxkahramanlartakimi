using System;

namespace UnityEngine
{
	// Token: 0x0200007B RID: 123
	[Flags]
	public enum HideFlags
	{
		// Token: 0x0400016C RID: 364
		None = 0,
		// Token: 0x0400016D RID: 365
		HideInHierarchy = 1,
		// Token: 0x0400016E RID: 366
		HideInInspector = 2,
		// Token: 0x0400016F RID: 367
		DontSave = 4,
		// Token: 0x04000170 RID: 368
		NotEditable = 8,
		// Token: 0x04000171 RID: 369
		HideAndDontSave = 13
	}
}
