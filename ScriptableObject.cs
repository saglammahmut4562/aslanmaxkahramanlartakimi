using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020000D1 RID: 209
	[StructLayout(0)]
	public class ScriptableObject : Object
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x00012150 File Offset: 0x00010350
		public ScriptableObject()
		{
			ScriptableObject.Internal_CreateScriptableObject(this);
		}

		// Token: 0x060007AC RID: 1964
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateScriptableObject([Writable] ScriptableObject self);

		// Token: 0x060007AD RID: 1965 RVA: 0x00012160 File Offset: 0x00010360
		public static ScriptableObject CreateInstance(Type type)
		{
			return ScriptableObject.CreateInstanceFromType(type);
		}

		// Token: 0x060007AE RID: 1966
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern ScriptableObject CreateInstanceFromType(Type type);

		// Token: 0x060007AF RID: 1967 RVA: 0x00012168 File Offset: 0x00010368
		public static T CreateInstance<T>() where T : ScriptableObject
		{
			return (T)((object)ScriptableObject.CreateInstance(typeof(T)));
		}
	}
}
