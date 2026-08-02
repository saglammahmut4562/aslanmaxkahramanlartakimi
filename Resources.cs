using System;
using System.Runtime.CompilerServices;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020000C9 RID: 201
	public sealed class Resources
	{
		// Token: 0x0600078E RID: 1934 RVA: 0x0001206C File Offset: 0x0001026C
		public static Object Load(string path)
		{
			return Resources.Load(path, typeof(Object));
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00012080 File Offset: 0x00010280
		public static T Load<T>(string path) where T : Object
		{
			return (T)((object)Resources.Load(path, typeof(T)));
		}

		// Token: 0x06000790 RID: 1936
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Object Load(string path, Type systemTypeInstance);

		// Token: 0x06000791 RID: 1937
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void UnloadAsset(Object assetToUnload);

		// Token: 0x06000792 RID: 1938
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern AsyncOperation UnloadUnusedAssets();
	}
}
