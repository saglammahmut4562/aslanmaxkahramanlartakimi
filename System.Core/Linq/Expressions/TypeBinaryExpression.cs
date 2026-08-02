using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200003A RID: 58
	public sealed class TypeBinaryExpression : Expression
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000062C4 File Offset: 0x000044C4
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000062CC File Offset: 0x000044CC
		public Type TypeOperand
		{
			get
			{
				return this.type_operand;
			}
		}

		// Token: 0x0400012E RID: 302
		private Expression expression;

		// Token: 0x0400012F RID: 303
		private Type type_operand;
	}
}
