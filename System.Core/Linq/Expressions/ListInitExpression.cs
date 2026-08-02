using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200002E RID: 46
	public sealed class ListInitExpression : Expression
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000061EC File Offset: 0x000043EC
		public NewExpression NewExpression
		{
			get
			{
				return this.new_expression;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000061F4 File Offset: 0x000043F4
		public ReadOnlyCollection<ElementInit> Initializers
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x04000117 RID: 279
		private NewExpression new_expression;

		// Token: 0x04000118 RID: 280
		private ReadOnlyCollection<ElementInit> initializers;
	}
}
