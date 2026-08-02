using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x020000D7 RID: 215
	internal class SetupCoroutine
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x00012780 File Offset: 0x00010980
		public static object InvokeMember(object behaviour, string name, object variable)
		{
			object[] array = null;
			if (variable != null)
			{
				array = new object[] { variable };
			}
			return behaviour.GetType().InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, behaviour, array, null, null, null);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000127B8 File Offset: 0x000109B8
		public static object InvokeStatic(Type klass, string name, object variable)
		{
			object[] array = null;
			if (variable != null)
			{
				array = new object[] { variable };
			}
			return klass.InvokeMember(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, null, array, null, null, null);
		}
	}
}
