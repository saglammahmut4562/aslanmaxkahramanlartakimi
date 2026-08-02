using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000038 RID: 56
	public sealed class NewExpression : Expression
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000062AC File Offset: 0x000044AC
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000062B4 File Offset: 0x000044B4
		public ReadOnlyCollection<MemberInfo> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x0400012A RID: 298
		private ConstructorInfo constructor;

		// Token: 0x0400012B RID: 299
		private ReadOnlyCollection<Expression> arguments;

		// Token: 0x0400012C RID: 300
		private ReadOnlyCollection<MemberInfo> members;
	}
}
