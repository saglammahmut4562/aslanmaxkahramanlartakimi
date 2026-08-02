using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000034 RID: 52
	public sealed class MemberListBinding : MemberBinding
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006258 File Offset: 0x00004458
		public ReadOnlyCollection<ElementInit> Initializers
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x04000124 RID: 292
		private ReadOnlyCollection<ElementInit> initializers;
	}
}
