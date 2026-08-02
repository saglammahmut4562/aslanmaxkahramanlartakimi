using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000023 RID: 35
	public sealed class ConditionalExpression : Expression
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004CCC File Offset: 0x00002ECC
		public Expression Test
		{
			get
			{
				return this.test;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public Expression IfTrue
		{
			get
			{
				return this.if_true;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004CDC File Offset: 0x00002EDC
		public Expression IfFalse
		{
			get
			{
				return this.if_false;
			}
		}

		// Token: 0x040000CA RID: 202
		private Expression test;

		// Token: 0x040000CB RID: 203
		private Expression if_true;

		// Token: 0x040000CC RID: 204
		private Expression if_false;
	}
}
