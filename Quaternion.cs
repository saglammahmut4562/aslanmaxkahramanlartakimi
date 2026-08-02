using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000B7 RID: 183
	[DefaultMember("Item")]
	public struct Quaternion
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x000115C8 File Offset: 0x0000F7C8
		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x000115E8 File Offset: 0x0000F7E8
		public static Quaternion identity
		{
			get
			{
				return new Quaternion(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00011604 File Offset: 0x0000F804
		public static Quaternion AngleAxis(float angle, Vector3 axis)
		{
			return Quaternion.INTERNAL_CALL_AngleAxis(angle, ref axis);
		}

		// Token: 0x06000713 RID: 1811
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Quaternion INTERNAL_CALL_AngleAxis(float angle, ref Vector3 axis);

		// Token: 0x06000714 RID: 1812 RVA: 0x00011610 File Offset: 0x0000F810
		public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			return Quaternion.INTERNAL_CALL_FromToRotation(ref fromDirection, ref toDirection);
		}

		// Token: 0x06000715 RID: 1813
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Quaternion INTERNAL_CALL_FromToRotation(ref Vector3 fromDirection, ref Vector3 toDirection);

		// Token: 0x06000716 RID: 1814 RVA: 0x0001161C File Offset: 0x0000F81C
		[ExcludeFromDocs]
		public static Quaternion LookRotation(Vector3 forward)
		{
			Vector3 up = Vector3.up;
			return Quaternion.INTERNAL_CALL_LookRotation(ref forward, ref up);
		}

		// Token: 0x06000717 RID: 1815
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Quaternion INTERNAL_CALL_LookRotation(ref Vector3 forward, ref Vector3 upwards);

		// Token: 0x06000718 RID: 1816 RVA: 0x00011638 File Offset: 0x0000F838
		public static Quaternion Inverse(Quaternion rotation)
		{
			return Quaternion.INTERNAL_CALL_Inverse(ref rotation);
		}

		// Token: 0x06000719 RID: 1817
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Quaternion INTERNAL_CALL_Inverse(ref Quaternion rotation);

		// Token: 0x0600071A RID: 1818 RVA: 0x00011644 File Offset: 0x0000F844
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", new object[] { this.x, this.y, this.z, this.w });
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001169C File Offset: 0x0000F89C
		public Vector3 eulerAngles
		{
			get
			{
				return Quaternion.Internal_ToEulerRad(this) * 57.29578f;
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000116B4 File Offset: 0x0000F8B4
		public static Quaternion Euler(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z) * 0.017453292f);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000116D0 File Offset: 0x0000F8D0
		public static Quaternion Euler(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler * 0.017453292f);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000116E4 File Offset: 0x0000F8E4
		private static Vector3 Internal_ToEulerRad(Quaternion rotation)
		{
			return Quaternion.INTERNAL_CALL_Internal_ToEulerRad(ref rotation);
		}

		// Token: 0x0600071F RID: 1823
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_Internal_ToEulerRad(ref Quaternion rotation);

		// Token: 0x06000720 RID: 1824 RVA: 0x000116F0 File Offset: 0x0000F8F0
		private static Quaternion Internal_FromEulerRad(Vector3 euler)
		{
			return Quaternion.INTERNAL_CALL_Internal_FromEulerRad(ref euler);
		}

		// Token: 0x06000721 RID: 1825
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Quaternion INTERNAL_CALL_Internal_FromEulerRad(ref Vector3 euler);

		// Token: 0x06000722 RID: 1826 RVA: 0x000116FC File Offset: 0x0000F8FC
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2) ^ (this.w.GetHashCode() >> 1);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00011734 File Offset: 0x0000F934
		public override bool Equals(object other)
		{
			if (!(other is Quaternion))
			{
				return false;
			}
			Quaternion quaternion = (Quaternion)other;
			return this.x.Equals(quaternion.x) && this.y.Equals(quaternion.y) && this.z.Equals(quaternion.z) && this.w.Equals(quaternion.w);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000117B0 File Offset: 0x0000F9B0
		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000118C0 File Offset: 0x0000FAC0
		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			float num = rotation.x * 2f;
			float num2 = rotation.y * 2f;
			float num3 = rotation.z * 2f;
			float num4 = rotation.x * num;
			float num5 = rotation.y * num2;
			float num6 = rotation.z * num3;
			float num7 = rotation.x * num2;
			float num8 = rotation.x * num3;
			float num9 = rotation.y * num3;
			float num10 = rotation.w * num;
			float num11 = rotation.w * num2;
			float num12 = rotation.w * num3;
			Vector3 vector;
			vector.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			vector.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			vector.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return vector;
		}

		// Token: 0x040002F3 RID: 755
		public const float kEpsilon = 1E-06f;

		// Token: 0x040002F4 RID: 756
		public float x;

		// Token: 0x040002F5 RID: 757
		public float y;

		// Token: 0x040002F6 RID: 758
		public float z;

		// Token: 0x040002F7 RID: 759
		public float w;
	}
}
