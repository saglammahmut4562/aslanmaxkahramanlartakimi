using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200002C RID: 44
	public sealed class InvocationExpression : Expression
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000061B0 File Offset: 0x000043B0
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000061B8 File Offset: 0x000043B8
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x04000113 RID: 275
		private Expression expression;

		// Token: 0x04000114 RID: 276
		private ReadOnlyCollection<Expression> arguments;
	}
}
