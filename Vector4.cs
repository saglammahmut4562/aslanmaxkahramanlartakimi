using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x0200011E RID: 286
	[DefaultMember("Item")]
	public struct Vector4
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x00018160 File Offset: 0x00016360
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00018180 File Offset: 0x00016380
		public Vector4(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
			this.w = 0f;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000181A8 File Offset: 0x000163A8
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2) ^ (this.w.GetHashCode() >> 1);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000181E0 File Offset: 0x000163E0
		public override bool Equals(object other)
		{
			if (!(other is Vector4))
			{
				return false;
			}
			Vector4 vector = (Vector4)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z) && this.w.Equals(vector.w);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001825C File Offset: 0x0001645C
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", new object[] { this.x, this.y, this.z, this.w });
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000182B4 File Offset: 0x000164B4
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00018300 File Offset: 0x00016500
		public static float SqrMagnitude(Vector4 a)
		{
			return Vector4.Dot(a, a);
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0001830C File Offset: 0x0001650C
		public static Vector4 zero
		{
			get
			{
				return new Vector4(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00018328 File Offset: 0x00016528
		public static Vector4 one
		{
			get
			{
				return new Vector4(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00018344 File Offset: 0x00016544
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00018394 File Offset: 0x00016594
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000183E4 File Offset: 0x000165E4
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, -a.w);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001840C File Offset: 0x0001660C
		public static Vector4 operator *(Vector4 a, float d)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00018438 File Offset: 0x00016638
		public static Vector4 operator *(float d, Vector4 a)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00018464 File Offset: 0x00016664
		public static bool operator ==(Vector4 lhs, Vector4 rhs)
		{
			return Vector4.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001847C File Offset: 0x0001667C
		public static bool operator !=(Vector4 lhs, Vector4 rhs)
		{
			return Vector4.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00018494 File Offset: 0x00016694
		public static implicit operator Vector4(Vector3 v)
		{
			return new Vector4(v.x, v.y, v.z, 0f);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000184B8 File Offset: 0x000166B8
		public static implicit operator Vector3(Vector4 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000184D4 File Offset: 0x000166D4
		public static implicit operator Vector4(Vector2 v)
		{
			return new Vector4(v.x, v.y, 0f, 0f);
		}

		// Token: 0x040004C7 RID: 1223
		public const float kEpsilon = 1E-05f;

		// Token: 0x040004C8 RID: 1224
		public float x;

		// Token: 0x040004C9 RID: 1225
		public float y;

		// Token: 0x040004CA RID: 1226
		public float z;

		// Token: 0x040004CB RID: 1227
		public float w;
	}
}
