using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x02000023 RID: 35
	[ComVisible(true)]
	public class ComponentCollection : ReadOnlyCollectionBase
	{
		// Token: 0x17000041 RID: 65
		public virtual IComponent this[string name]
		{
			get
			{
				foreach (object obj in base.InnerList)
				{
					IComponent component = (IComponent)obj;
					if (component.Site != null && component.Site.Name == name)
					{
						return component;
					}
				}
				return null;
			}
		}
	}
}
