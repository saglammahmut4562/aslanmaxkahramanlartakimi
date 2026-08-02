using System;

namespace UnityEngine
{
	// Token: 0x020000BA RID: 186
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class RangeAttribute : PropertyAttribute
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x00011A08 File Offset: 0x0000FC08
		public RangeAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040002FB RID: 763
		public readonly float min;

		// Token: 0x040002FC RID: 764
		public readonly float max;
	}
}
