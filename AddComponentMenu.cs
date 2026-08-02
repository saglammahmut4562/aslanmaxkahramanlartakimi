using System;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public sealed class AddComponentMenu : Attribute
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000033C4 File Offset: 0x000015C4
		public AddComponentMenu(string menuName)
		{
			this.m_AddComponentMenu = menuName;
			this.m_Ordering = 0;
		}

		// Token: 0x04000001 RID: 1
		private string m_AddComponentMenu;

		// Token: 0x04000002 RID: 2
		private int m_Ordering;
	}
}
