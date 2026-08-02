using System;

namespace UnityEngine
{
	// Token: 0x020000C7 RID: 199
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class RequireComponent : Attribute
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x0001205C File Offset: 0x0001025C
		public RequireComponent(Type requiredComponent)
		{
			this.m_Type0 = requiredComponent;
		}

		// Token: 0x0400032B RID: 811
		public Type m_Type0;

		// Token: 0x0400032C RID: 812
		public Type m_Type1;

		// Token: 0x0400032D RID: 813
		public Type m_Type2;
	}
}
