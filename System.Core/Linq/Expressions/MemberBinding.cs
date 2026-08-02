using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000030 RID: 48
	public abstract class MemberBinding
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006204 File Offset: 0x00004404
		public MemberBindingType BindingType
		{
			get
			{
				return this.binding_type;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000620C File Offset: 0x0000440C
		public MemberInfo Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006214 File Offset: 0x00004414
		public override string ToString()
		{
			return ExpressionPrinter.ToString(this);
		}

		// Token: 0x0400011A RID: 282
		private MemberBindingType binding_type;

		// Token: 0x0400011B RID: 283
		private MemberInfo member;
	}
}
