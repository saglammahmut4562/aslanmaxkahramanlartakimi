using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000066 RID: 102
	public sealed class GUILayer : Behaviour
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x0000C0DC File Offset: 0x0000A2DC
		public GUIElement HitTest(Vector3 screenPosition)
		{
			return GUILayer.INTERNAL_CALL_HitTest(this, ref screenPosition);
		}

		// Token: 0x060004FB RID: 1275
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern GUIElement INTERNAL_CALL_HitTest(GUILayer self, ref Vector3 screenPosition);
	}
}
