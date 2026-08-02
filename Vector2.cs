using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x0200011C RID: 284
	[DefaultMember("Item")]
	public struct Vector2
	{
		// Token: 0x06000967 RID: 2407 RVA: 0x000177D8 File Offset: 0x000159D8
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x000177E8 File Offset: 0x000159E8
		public void Set(float new_x, float new_y)
		{
			this.x = new_x;
			this.y = new_y;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x000177F8 File Offset: 0x000159F8
		public static Vector2 Scale(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00017820 File Offset: 0x00015A20
		public void Normalize()
		{
			float magnitude = this.magnitude;
			if (magnitude > 1E-05f)
			{
				this /= magnitude;
			}
			else
			{
				this = Vector2.zero;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00017864 File Offset: 0x00015A64
		public Vector2 normalized
		{
			get
			{
				Vector2 vector = new Vector2(this.x, this.y);
				vector.Normalize();
				return vector;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001788C File Offset: 0x00015A8C
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1})", new object[] { this.x, this.y });
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000178BC File Offset: 0x00015ABC
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000178D8 File Offset: 0x00015AD8
		public override bool Equals(object other)
		{
			if (!(other is Vector2))
			{
				return false;
			}
			Vector2 vector = (Vector2)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00017928 File Offset: 0x00015B28
		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0001794C File Offset: 0x00015B4C
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(this.x * this.x + this.y * this.y);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00017970 File Offset: 0x00015B70
		public float sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00017990 File Offset: 0x00015B90
		public static float SqrMagnitude(Vector2 a)
		{
			return a.x * a.x + a.y * a.y;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x000179B4 File Offset: 0x00015BB4
		public static Vector2 zero
		{
			get
			{
				return new Vector2(0f, 0f);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x000179C8 File Offset: 0x00015BC8
		public static Vector2 one
		{
			get
			{
				return new Vector2(1f, 1f);
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000179DC File Offset: 0x00015BDC
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00017A04 File Offset: 0x00015C04
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00017A2C File Offset: 0x00015C2C
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00017A44 File Offset: 0x00015C44
		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00017A60 File Offset: 0x00015C60
		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00017A7C File Offset: 0x00015C7C
		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00017A94 File Offset: 0x00015C94
		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00017AAC File Offset: 0x00015CAC
		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00017AC4 File Offset: 0x00015CC4
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		// Token: 0x040004C0 RID: 1216
		public const float kEpsilon = 1E-05f;

		// Token: 0x040004C1 RID: 1217
		public float x;

		// Token: 0x040004C2 RID: 1218
		public float y;
	}
}
