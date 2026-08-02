using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000036 RID: 54
	public sealed class MethodCallExpression : Expression
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00006268 File Offset: 0x00004468
		internal MethodCallExpression(Expression obj, MethodInfo method, ReadOnlyCollection<Expression> arguments)
			: base(ExpressionType.Call, method.ReturnType)
		{
			this.obj = obj;
			this.method = method;
			this.arguments = arguments;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000628C File Offset: 0x0000448C
		public Expression Object
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006294 File Offset: 0x00004494
		public MethodInfo Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000629C File Offset: 0x0000449C
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x04000126 RID: 294
		private Expression obj;

		// Token: 0x04000127 RID: 295
		private MethodInfo method;

		// Token: 0x04000128 RID: 296
		private ReadOnlyCollection<Expression> arguments;
	}
}
