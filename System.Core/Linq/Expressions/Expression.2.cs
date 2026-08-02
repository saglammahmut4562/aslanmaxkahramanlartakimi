using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000027 RID: 39
	public sealed class Expression<TDelegate> : LambdaExpression
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x000051F8 File Offset: 0x000033F8
		internal Expression(Expression body, ReadOnlyCollection<ParameterExpression> parameters)
			: base(typeof(TDelegate), body, parameters)
		{
		}
	}
}
