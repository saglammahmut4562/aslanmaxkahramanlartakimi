using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200002F RID: 47
	public sealed class MemberAssignment : MemberBinding
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000061FC File Offset: 0x000043FC
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x04000119 RID: 281
		private Expression expression;
	}
}
