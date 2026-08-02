using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000039 RID: 57
	public sealed class ParameterExpression : Expression
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000062BC File Offset: 0x000044BC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400012D RID: 301
		private string name;
	}
}
