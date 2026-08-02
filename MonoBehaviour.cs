using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200009E RID: 158
	public class MonoBehaviour : Behaviour
	{
		// Token: 0x060006B9 RID: 1721
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern MonoBehaviour();

		// Token: 0x060006BA RID: 1722
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Invoke(string methodName, float time);

		// Token: 0x060006BB RID: 1723
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void InvokeRepeating(string methodName, float time, float repeatRate);

		// Token: 0x060006BC RID: 1724 RVA: 0x00011160 File Offset: 0x0000F360
		public Coroutine StartCoroutine(IEnumerator routine)
		{
			return this.StartCoroutine_Auto(routine);
		}

		// Token: 0x060006BD RID: 1725
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Coroutine StartCoroutine_Auto(IEnumerator routine);

		// Token: 0x060006BE RID: 1726
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value);

		// Token: 0x060006BF RID: 1727 RVA: 0x0001116C File Offset: 0x0000F36C
		[ExcludeFromDocs]
		public Coroutine StartCoroutine(string methodName)
		{
			object obj = null;
			return this.StartCoroutine(methodName, obj);
		}

		// Token: 0x060006C0 RID: 1728
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void StopCoroutine(string methodName);

		// Token: 0x060006C1 RID: 1729
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void StopAllCoroutines();

		// Token: 0x060006C2 RID: 1730 RVA: 0x00011184 File Offset: 0x0000F384
		public static void print(object message)
		{
			Debug.Log(message);
		}
	}
}
