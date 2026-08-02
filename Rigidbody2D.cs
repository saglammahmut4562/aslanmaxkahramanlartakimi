using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000CB RID: 203
	public sealed class Rigidbody2D : Component
	{
		// Token: 0x060007A4 RID: 1956 RVA: 0x00012120 File Offset: 0x00010320
		[ExcludeFromDocs]
		public void AddForce(Vector2 force)
		{
			ForceMode2D forceMode2D = ForceMode2D.Force;
			Rigidbody2D.INTERNAL_CALL_AddForce(this, ref force, forceMode2D);
		}

		// Token: 0x060007A5 RID: 1957
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_AddForce(Rigidbody2D self, ref Vector2 force, ForceMode2D mode);

		// Token: 0x060007A6 RID: 1958
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void AddTorque(float torque, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x060007A7 RID: 1959 RVA: 0x00012138 File Offset: 0x00010338
		[ExcludeFromDocs]
		public void AddTorque(float torque)
		{
			ForceMode2D forceMode2D = ForceMode2D.Force;
			this.AddTorque(torque, forceMode2D);
		}
	}
}
