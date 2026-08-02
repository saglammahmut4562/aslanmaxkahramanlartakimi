using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000024 RID: 36
	public sealed class ConstantExpression : Expression
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00004CE4 File Offset: 0x00002EE4
		internal ConstantExpression(object value, Type type)
			: base(ExpressionType.Constant, type)
		{
			this.value = value;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040000CD RID: 205
		private object value;
	}
}
