using System;

namespace UnityEngineInternal
{
	// Token: 0x02000128 RID: 296
	[AttributeUsage(AttributeTargets.Method)]
	[Serializable]
	public class TypeInferenceRuleAttribute : Attribute
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x0001876C File Offset: 0x0001696C
		public TypeInferenceRuleAttribute(TypeInferenceRules rule)
			: this(rule.ToString())
		{
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00018780 File Offset: 0x00016980
		public TypeInferenceRuleAttribute(string rule)
		{
			this._rule = rule;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00018790 File Offset: 0x00016990
		public override string ToString()
		{
			return this._rule;
		}

		// Token: 0x040004D5 RID: 1237
		private readonly string _rule;
	}
}
