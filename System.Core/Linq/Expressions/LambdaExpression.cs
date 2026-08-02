using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200002D RID: 45
	public class LambdaExpression : Expression
	{
		// Token: 0x06000135 RID: 309 RVA: 0x000061C0 File Offset: 0x000043C0
		internal LambdaExpression(Type delegateType, Expression body, ReadOnlyCollection<ParameterExpression> parameters)
			: base(ExpressionType.Lambda, delegateType)
		{
			this.body = body;
			this.parameters = parameters;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000061DC File Offset: 0x000043DC
		public Expression Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000061E4 File Offset: 0x000043E4
		public ReadOnlyCollection<ParameterExpression> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04000115 RID: 277
		private Expression body;

		// Token: 0x04000116 RID: 278
		private ReadOnlyCollection<ParameterExpression> parameters;
	}
}
