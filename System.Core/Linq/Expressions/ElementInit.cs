using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000025 RID: 37
	public sealed class ElementInit
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004D00 File Offset: 0x00002F00
		public MethodInfo AddMethod
		{
			get
			{
				return this.add_method;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004D08 File Offset: 0x00002F08
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004D10 File Offset: 0x00002F10
		public override string ToString()
		{
			return ExpressionPrinter.ToString(this);
		}

		// Token: 0x040000CE RID: 206
		private MethodInfo add_method;

		// Token: 0x040000CF RID: 207
		private ReadOnlyCollection<Expression> arguments;
	}
}
