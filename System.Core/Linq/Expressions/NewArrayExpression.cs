using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000037 RID: 55
	public sealed class NewArrayExpression : Expression
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000062A4 File Offset: 0x000044A4
		public ReadOnlyCollection<Expression> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x04000129 RID: 297
		private ReadOnlyCollection<Expression> expressions;
	}
}
