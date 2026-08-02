using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000022 RID: 34
	public sealed class BinaryExpression : Expression
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004CB4 File Offset: 0x00002EB4
		public Expression Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004CBC File Offset: 0x00002EBC
		public Expression Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004CC4 File Offset: 0x00002EC4
		public LambdaExpression Conversion
		{
			get
			{
				return this.conversion;
			}
		}

		// Token: 0x040000C4 RID: 196
		private Expression left;

		// Token: 0x040000C5 RID: 197
		private Expression right;

		// Token: 0x040000C6 RID: 198
		private LambdaExpression conversion;

		// Token: 0x040000C7 RID: 199
		private MethodInfo method;

		// Token: 0x040000C8 RID: 200
		private bool lift_to_null;

		// Token: 0x040000C9 RID: 201
		private bool is_lifted;
	}
}
