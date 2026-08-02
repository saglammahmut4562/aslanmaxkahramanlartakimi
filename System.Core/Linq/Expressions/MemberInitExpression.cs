using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000033 RID: 51
	public sealed class MemberInitExpression : Expression
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006248 File Offset: 0x00004448
		public NewExpression NewExpression
		{
			get
			{
				return this.new_expression;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006250 File Offset: 0x00004450
		public ReadOnlyCollection<MemberBinding> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x04000122 RID: 290
		private NewExpression new_expression;

		// Token: 0x04000123 RID: 291
		private ReadOnlyCollection<MemberBinding> bindings;
	}
}
