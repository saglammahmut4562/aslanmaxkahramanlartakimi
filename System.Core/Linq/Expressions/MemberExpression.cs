using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000032 RID: 50
	public sealed class MemberExpression : Expression
	{
		// Token: 0x0600013E RID: 318 RVA: 0x0000621C File Offset: 0x0000441C
		internal MemberExpression(Expression expression, MemberInfo member, Type type)
			: base(ExpressionType.MemberAccess, type)
		{
			this.expression = expression;
			this.member = member;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006238 File Offset: 0x00004438
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006240 File Offset: 0x00004440
		public MemberInfo Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x04000120 RID: 288
		private Expression expression;

		// Token: 0x04000121 RID: 289
		private MemberInfo member;
	}
}
