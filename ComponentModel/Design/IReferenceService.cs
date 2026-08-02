using System;

namespace System.ComponentModel.Design
{
	// Token: 0x0200002B RID: 43
	public interface IReferenceService
	{
		// Token: 0x06000102 RID: 258
		string GetName(object reference);

		// Token: 0x06000103 RID: 259
		object GetReference(string name);
	}
}
