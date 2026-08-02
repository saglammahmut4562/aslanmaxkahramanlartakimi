using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000CA RID: 202
	public sealed class Rigidbody : Component
	{
		// Token: 0x06000793 RID: 1939
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x06000794 RID: 1940
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_velocity(ref Vector3 value);

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00012098 File Offset: 0x00010298
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x000120B0 File Offset: 0x000102B0
		public Vector3 velocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_velocity(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_velocity(ref value);
			}
		}

		// Token: 0x06000797 RID: 1943
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_angularVelocity(ref Vector3 value);

		// Token: 0x17000190 RID: 400
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x000120BC File Offset: 0x000102BC
		public Vector3 angularVelocity
		{
			set
			{
				this.INTERNAL_set_angularVelocity(ref value);
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000120C8 File Offset: 0x000102C8
		public void SetDensity(float density)
		{
			Rigidbody.INTERNAL_CALL_SetDensity(this, density);
		}

		// Token: 0x0600079A RID: 1946
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetDensity(Rigidbody self, float density);

		// Token: 0x17000191 RID: 401
		// (set) Token: 0x0600079B RID: 1947
		public extern bool isKinematic
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000120D4 File Offset: 0x000102D4
		public void AddForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddForce(this, ref force, mode);
		}

		// Token: 0x0600079D RID: 1949
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_AddForce(Rigidbody self, ref Vector3 force, ForceMode mode);

		// Token: 0x0600079E RID: 1950 RVA: 0x000120E0 File Offset: 0x000102E0
		public void AddForce(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForce(new Vector3(x, y, z), mode);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000120F4 File Offset: 0x000102F4
		public void AddTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddTorque(this, ref torque, mode);
		}

		// Token: 0x060007A0 RID: 1952
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_AddTorque(Rigidbody self, ref Vector3 torque, ForceMode mode);

		// Token: 0x060007A1 RID: 1953 RVA: 0x00012100 File Offset: 0x00010300
		public void AddTorque(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddTorque(new Vector3(x, y, z), mode);
		}

		// Token: 0x060007A2 RID: 1954
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_centerOfMass(ref Vector3 value);

		// Token: 0x17000192 RID: 402
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00012114 File Offset: 0x00010314
		public Vector3 centerOfMass
		{
			set
			{
				this.INTERNAL_set_centerOfMass(ref value);
			}
		}
	}
}
