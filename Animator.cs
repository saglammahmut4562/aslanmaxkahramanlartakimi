using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	public sealed class Animator : Behaviour
	{
		// Token: 0x060001CE RID: 462 RVA: 0x00006964 File Offset: 0x00004B64
		[ExcludeFromDocs]
		public void CrossFade(string stateName, float transitionDuration)
		{
			float negativeInfinity = float.NegativeInfinity;
			int num = -1;
			this.CrossFade(stateName, transitionDuration, num, negativeInfinity);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006984 File Offset: 0x00004B84
		public void CrossFade(string stateName, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.CrossFade(Animator.StringToHash(stateName), transitionDuration, layer, normalizedTime);
		}

		// Token: 0x060001D0 RID: 464
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void CrossFade(int stateNameHash, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime);

		// Token: 0x060001D1 RID: 465
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int StringToHash(string name);
	}
}
