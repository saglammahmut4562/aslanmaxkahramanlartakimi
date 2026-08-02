using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020000A4 RID: 164
	[StructLayout(0)]
	public class Object
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x000111AC File Offset: 0x0000F3AC
		public override bool Equals(object o)
		{
			return Object.CompareBaseObjects(this, o as Object);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000111BC File Offset: 0x0000F3BC
		public override int GetHashCode()
		{
			return this.GetInstanceID();
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000111C4 File Offset: 0x0000F3C4
		private static bool CompareBaseObjects(Object lhs, Object rhs)
		{
			return Object.CompareBaseObjectsInternal(lhs, rhs);
		}

		// Token: 0x060006CA RID: 1738
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool CompareBaseObjectsInternal([Writable] Object lhs, [Writable] Object rhs);

		// Token: 0x060006CB RID: 1739 RVA: 0x000111D0 File Offset: 0x0000F3D0
		[NotRenamed]
		public int GetInstanceID()
		{
			return this.m_UnityRuntimeReferenceData.instanceID;
		}

		// Token: 0x060006CC RID: 1740
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Object Internal_CloneSingle(Object data);

		// Token: 0x060006CD RID: 1741 RVA: 0x000111E0 File Offset: 0x0000F3E0
		private static Object Internal_InstantiateSingle(Object data, Vector3 pos, Quaternion rot)
		{
			return Object.INTERNAL_CALL_Internal_InstantiateSingle(data, ref pos, ref rot);
		}

		// Token: 0x060006CE RID: 1742
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Object INTERNAL_CALL_Internal_InstantiateSingle(Object data, ref Vector3 pos, ref Quaternion rot);

		// Token: 0x060006CF RID: 1743 RVA: 0x000111EC File Offset: 0x0000F3EC
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
		{
			Object.CheckNullArgument(original, "The prefab you want to instantiate is null.");
			return Object.Internal_InstantiateSingle(original, position, rotation);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00011204 File Offset: 0x0000F404
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original)
		{
			Object.CheckNullArgument(original, "The thing you want to instantiate is null.");
			return Object.Internal_CloneSingle(original);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00011218 File Offset: 0x0000F418
		private static void CheckNullArgument(object arg, string message)
		{
			if (arg == null)
			{
				throw new ArgumentException(message);
			}
		}

		// Token: 0x060006D2 RID: 1746
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void Destroy(Object obj, [DefaultValue("0.0F")] float t);

		// Token: 0x060006D3 RID: 1747 RVA: 0x00011228 File Offset: 0x0000F428
		[ExcludeFromDocs]
		public static void Destroy(Object obj)
		{
			float num = 0f;
			Object.Destroy(obj, num);
		}

		// Token: 0x060006D4 RID: 1748
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void DestroyImmediate(Object obj, [DefaultValue("false")] bool allowDestroyingAssets);

		// Token: 0x060006D5 RID: 1749 RVA: 0x00011244 File Offset: 0x0000F444
		[ExcludeFromDocs]
		public static void DestroyImmediate(Object obj)
		{
			bool flag = false;
			Object.DestroyImmediate(obj, flag);
		}

		// Token: 0x060006D6 RID: 1750
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Object[] FindObjectsOfType(Type type);

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001125C File Offset: 0x0000F45C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type)
		{
			Object[] array = Object.FindObjectsOfType(type);
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006D8 RID: 1752
		// (set) Token: 0x060006D9 RID: 1753
		public extern string name
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060006DA RID: 1754
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void DontDestroyOnLoad(Object target);

		// Token: 0x17000158 RID: 344
		// (set) Token: 0x060006DB RID: 1755
		public extern HideFlags hideFlags
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060006DC RID: 1756
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void DestroyObject(Object obj, [DefaultValue("0.0F")] float t);

		// Token: 0x060006DD RID: 1757 RVA: 0x00011280 File Offset: 0x0000F480
		[ExcludeFromDocs]
		public static void DestroyObject(Object obj)
		{
			float num = 0f;
			Object.DestroyObject(obj, num);
		}

		// Token: 0x060006DE RID: 1758
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public override extern string ToString();

		// Token: 0x060006DF RID: 1759 RVA: 0x0001129C File Offset: 0x0000F49C
		public static implicit operator bool(Object exists)
		{
			return !Object.CompareBaseObjects(exists, null);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000112A8 File Offset: 0x0000F4A8
		public static bool operator ==(Object x, Object y)
		{
			return Object.CompareBaseObjects(x, y);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000112B4 File Offset: 0x0000F4B4
		public static bool operator !=(Object x, Object y)
		{
			return !Object.CompareBaseObjects(x, y);
		}

		// Token: 0x040002C8 RID: 712
		private ReferenceData m_UnityRuntimeReferenceData;
	}
}
