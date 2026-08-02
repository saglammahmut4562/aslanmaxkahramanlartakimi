using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200003B RID: 59
	public sealed class UnaryExpression : Expression
	{
		// Token: 0x0600014F RID: 335 RVA: 0x000062D4 File Offset: 0x000044D4
		internal UnaryExpression(ExpressionType node_type, Expression operand, Type type)
			: base(node_type, type)
		{
			this.operand = operand;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000062E8 File Offset: 0x000044E8
		public Expression Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x04000130 RID: 304
		private Expression operand;

		// Token: 0x04000131 RID: 305
		private MethodInfo method;

		// Token: 0x04000132 RID: 306
		private bool is_lifted;
	}
}
