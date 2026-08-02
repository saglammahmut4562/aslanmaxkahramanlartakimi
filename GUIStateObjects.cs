using System;
using System.Collections.Generic;
using System.Security;

namespace UnityEngine
{
	// Token: 0x02000072 RID: 114
	internal class GUIStateObjects
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x0000F40C File Offset: 0x0000D60C
		[SecuritySafeCritical]
		internal static object GetStateObject(Type t, int controlID)
		{
			object obj;
			if (!GUIStateObjects.s_StateCache.TryGetValue(controlID, out obj) || obj.GetType() != t)
			{
				obj = Activator.CreateInstance(t);
				GUIStateObjects.s_StateCache[controlID] = obj;
			}
			return obj;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000F44C File Offset: 0x0000D64C
		internal static object QueryStateObject(Type t, int controlID)
		{
			object obj = GUIStateObjects.s_StateCache[controlID];
			if (t.IsInstanceOfType(obj))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x04000150 RID: 336
		private static Dictionary<int, object> s_StateCache = new Dictionary<int, object>();
	}
}
