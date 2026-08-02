using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000035 RID: 53
	public sealed class MemberMemberBinding : MemberBinding
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006260 File Offset: 0x00004460
		public ReadOnlyCollection<MemberBinding> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x04000125 RID: 293
		private ReadOnlyCollection<MemberBinding> bindings;
	}
}
