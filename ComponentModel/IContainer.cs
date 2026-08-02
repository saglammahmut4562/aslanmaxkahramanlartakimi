using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x02000038 RID: 56
	[ComVisible(true)]
	public interface IContainer : IDisposable
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000137 RID: 311
		ComponentCollection Components { get; }
	}
}
