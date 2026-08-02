using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x0200002C RID: 44
	public interface ITypeDescriptorFilterService
	{
		// Token: 0x06000104 RID: 260
		bool FilterAttributes(IComponent component, IDictionary attributes);
	}
}
