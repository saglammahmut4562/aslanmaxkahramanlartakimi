using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200011D RID: 285
	[DefaultMember("Item")]
	public struct Vector3
	{
		// Token: 0x0600097E RID: 2430 RVA: 0x00017AE0 File Offset: 0x00015CE0
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00017AF8 File Offset: 0x00015CF8
		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00017B14 File Offset: 0x00015D14
		public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00017B7C File Offset: 0x00015D7C
		public static Vector3 Slerp(Vector3 from, Vector3 to, float t)
		{
			return Vector3.INTERNAL_CALL_Slerp(ref from, ref to, t);
		}

		// Token: 0x06000982 RID: 2434
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_Slerp(ref Vector3 from, ref Vector3 to, float t);

		// Token: 0x06000983 RID: 2435 RVA: 0x00017B88 File Offset: 0x00015D88
		public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			Vector3 vector = target - current;
			float magnitude = vector.magnitude;
			if (magnitude <= maxDistanceDelta || magnitude == 0f)
			{
				return target;
			}
			return current + vector / magnitude * maxDistanceDelta;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00017BCC File Offset: 0x00015DCC
		public void Set(float new_x, float new_y, float new_z)
		{
			this.x = new_x;
			this.y = new_y;
			this.z = new_z;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00017BE4 File Offset: 0x00015DE4
		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00017C18 File Offset: 0x00015E18
		public void Scale(Vector3 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00017C58 File Offset: 0x00015E58
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00017CC8 File Offset: 0x00015EC8
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00017CF4 File Offset: 0x00015EF4
		public override bool Equals(object other)
		{
			if (!(other is Vector3))
			{
				return false;
			}
			Vector3 vector = (Vector3)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00017D58 File Offset: 0x00015F58
		public static Vector3 Normalize(Vector3 value)
		{
			float num = Vector3.Magnitude(value);
			if (num > 1E-05f)
			{
				return value / num;
			}
			return Vector3.zero;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00017D84 File Offset: 0x00015F84
		public void Normalize()
		{
			float num = Vector3.Magnitude(this);
			if (num > 1E-05f)
			{
				this /= num;
			}
			else
			{
				this = Vector3.zero;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00017DCC File Offset: 0x00015FCC
		public Vector3 normalized
		{
			get
			{
				return Vector3.Normalize(this);
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00017DDC File Offset: 0x00015FDC
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1}, {2:F1})", new object[] { this.x, this.y, this.z });
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00017E18 File Offset: 0x00016018
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00017E4C File Offset: 0x0001604C
		public static float Angle(Vector3 from, Vector3 to)
		{
			return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00017E7C File Offset: 0x0001607C
		public static float Magnitude(Vector3 a)
		{
			return Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00017EB4 File Offset: 0x000160B4
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00017EE4 File Offset: 0x000160E4
		public static float SqrMagnitude(Vector3 a)
		{
			return a.x * a.x + a.y * a.y + a.z * a.z;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00017F18 File Offset: 0x00016118
		public float sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00017F44 File Offset: 0x00016144
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00017F84 File Offset: 0x00016184
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00017FC4 File Offset: 0x000161C4
		public static Vector3 zero
		{
			get
			{
				return new Vector3(0f, 0f, 0f);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00017FDC File Offset: 0x000161DC
		public static Vector3 one
		{
			get
			{
				return new Vector3(1f, 1f, 1f);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00017FF4 File Offset: 0x000161F4
		public static Vector3 forward
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0001800C File Offset: 0x0001620C
		public static Vector3 up
		{
			get
			{
				return new Vector3(0f, 1f, 0f);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00018024 File Offset: 0x00016224
		public static Vector3 right
		{
			get
			{
				return new Vector3(1f, 0f, 0f);
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001803C File Offset: 0x0001623C
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00018070 File Offset: 0x00016270
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000180A4 File Offset: 0x000162A4
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000180C4 File Offset: 0x000162C4
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x000180E8 File Offset: 0x000162E8
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001810C File Offset: 0x0001630C
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00018130 File Offset: 0x00016330
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00018148 File Offset: 0x00016348
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x040004C3 RID: 1219
		public const float kEpsilon = 1E-05f;

		// Token: 0x040004C4 RID: 1220
		public float x;

		// Token: 0x040004C5 RID: 1221
		public float y;

		// Token: 0x040004C6 RID: 1222
		public float z;
	}
}
