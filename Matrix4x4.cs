using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000098 RID: 152
	public struct Matrix4x4
	{
		// Token: 0x1700013D RID: 317
		public float this[int row, int column]
		{
			get
			{
				return this[row + column * 4];
			}
			set
			{
				this[row + column * 4] = value;
			}
		}

		// Token: 0x1700013E RID: 318
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.m00;
				case 1:
					return this.m10;
				case 2:
					return this.m20;
				case 3:
					return this.m30;
				case 4:
					return this.m01;
				case 5:
					return this.m11;
				case 6:
					return this.m21;
				case 7:
					return this.m31;
				case 8:
					return this.m02;
				case 9:
					return this.m12;
				case 10:
					return this.m22;
				case 11:
					return this.m32;
				case 12:
					return this.m03;
				case 13:
					return this.m13;
				case 14:
					return this.m23;
				case 15:
					return this.m33;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.m00 = value;
					break;
				case 1:
					this.m10 = value;
					break;
				case 2:
					this.m20 = value;
					break;
				case 3:
					this.m30 = value;
					break;
				case 4:
					this.m01 = value;
					break;
				case 5:
					this.m11 = value;
					break;
				case 6:
					this.m21 = value;
					break;
				case 7:
					this.m31 = value;
					break;
				case 8:
					this.m02 = value;
					break;
				case 9:
					this.m12 = value;
					break;
				case 10:
					this.m22 = value;
					break;
				case 11:
					this.m32 = value;
					break;
				case 12:
					this.m03 = value;
					break;
				case 13:
					this.m13 = value;
					break;
				case 14:
					this.m23 = value;
					break;
				case 15:
					this.m33 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00010680 File Offset: 0x0000E880
		public override int GetHashCode()
		{
			return this.GetColumn(0).GetHashCode() ^ (this.GetColumn(1).GetHashCode() << 2) ^ (this.GetColumn(2).GetHashCode() >> 2) ^ (this.GetColumn(3).GetHashCode() >> 1);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000106D4 File Offset: 0x0000E8D4
		public override bool Equals(object other)
		{
			if (!(other is Matrix4x4))
			{
				return false;
			}
			Matrix4x4 matrix4x = (Matrix4x4)other;
			return this.GetColumn(0).Equals(matrix4x.GetColumn(0)) && this.GetColumn(1).Equals(matrix4x.GetColumn(1)) && this.GetColumn(2).Equals(matrix4x.GetColumn(2)) && this.GetColumn(3).Equals(matrix4x.GetColumn(3));
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00010778 File Offset: 0x0000E978
		public static Matrix4x4 Inverse(Matrix4x4 m)
		{
			return Matrix4x4.INTERNAL_CALL_Inverse(ref m);
		}

		// Token: 0x0600066D RID: 1645
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Matrix4x4 INTERNAL_CALL_Inverse(ref Matrix4x4 m);

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00010784 File Offset: 0x0000E984
		public Matrix4x4 inverse
		{
			get
			{
				return Matrix4x4.Inverse(this);
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00010794 File Offset: 0x0000E994
		public Vector4 GetColumn(int i)
		{
			return new Vector4(this[0, i], this[1, i], this[2, i], this[3, i]);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000107BC File Offset: 0x0000E9BC
		public void SetRow(int i, Vector4 v)
		{
			this[i, 0] = v.x;
			this[i, 1] = v.y;
			this[i, 2] = v.z;
			this[i, 3] = v.w;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000107FC File Offset: 0x0000E9FC
		public Vector3 MultiplyPoint(Vector3 v)
		{
			Vector3 vector;
			vector.x = this.m00 * v.x + this.m01 * v.y + this.m02 * v.z + this.m03;
			vector.y = this.m10 * v.x + this.m11 * v.y + this.m12 * v.z + this.m13;
			vector.z = this.m20 * v.x + this.m21 * v.y + this.m22 * v.z + this.m23;
			float num = this.m30 * v.x + this.m31 * v.y + this.m32 * v.z + this.m33;
			num = 1f / num;
			vector.x *= num;
			vector.y *= num;
			vector.z *= num;
			return vector;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00010924 File Offset: 0x0000EB24
		public Vector3 MultiplyVector(Vector3 v)
		{
			Vector3 vector;
			vector.x = this.m00 * v.x + this.m01 * v.y + this.m02 * v.z;
			vector.y = this.m10 * v.x + this.m11 * v.y + this.m12 * v.z;
			vector.z = this.m20 * v.x + this.m21 * v.y + this.m22 * v.z;
			return vector;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x000109CC File Offset: 0x0000EBCC
		public static Matrix4x4 identity
		{
			get
			{
				return new Matrix4x4
				{
					m00 = 1f,
					m01 = 0f,
					m02 = 0f,
					m03 = 0f,
					m10 = 0f,
					m11 = 1f,
					m12 = 0f,
					m13 = 0f,
					m20 = 0f,
					m21 = 0f,
					m22 = 1f,
					m23 = 0f,
					m30 = 0f,
					m31 = 0f,
					m32 = 0f,
					m33 = 1f
				};
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00010AA4 File Offset: 0x0000ECA4
		public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			this = Matrix4x4.TRS(pos, q, s);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			return Matrix4x4.INTERNAL_CALL_TRS(ref pos, ref q, ref s);
		}

		// Token: 0x06000676 RID: 1654
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Matrix4x4 INTERNAL_CALL_TRS(ref Vector3 pos, ref Quaternion q, ref Vector3 s);

		// Token: 0x06000677 RID: 1655 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		public override string ToString()
		{
			return UnityString.Format("{0:F5}\t{1:F5}\t{2:F5}\t{3:F5}\n{4:F5}\t{5:F5}\t{6:F5}\t{7:F5}\n{8:F5}\t{9:F5}\t{10:F5}\t{11:F5}\n{12:F5}\t{13:F5}\t{14:F5}\t{15:F5}\n", new object[]
			{
				this.m00, this.m01, this.m02, this.m03, this.m10, this.m11, this.m12, this.m13, this.m20, this.m21,
				this.m22, this.m23, this.m30, this.m31, this.m32, this.m33
			});
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00010BCC File Offset: 0x0000EDCC
		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return new Matrix4x4
			{
				m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30,
				m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31,
				m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32,
				m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33,
				m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30,
				m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31,
				m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32,
				m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33,
				m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30,
				m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31,
				m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32,
				m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33,
				m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30,
				m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31,
				m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32,
				m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33
			};
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00011044 File Offset: 0x0000F244
		public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000110B8 File Offset: 0x0000F2B8
		public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040002AE RID: 686
		public float m00;

		// Token: 0x040002AF RID: 687
		public float m10;

		// Token: 0x040002B0 RID: 688
		public float m20;

		// Token: 0x040002B1 RID: 689
		public float m30;

		// Token: 0x040002B2 RID: 690
		public float m01;

		// Token: 0x040002B3 RID: 691
		public float m11;

		// Token: 0x040002B4 RID: 692
		public float m21;

		// Token: 0x040002B5 RID: 693
		public float m31;

		// Token: 0x040002B6 RID: 694
		public float m02;

		// Token: 0x040002B7 RID: 695
		public float m12;

		// Token: 0x040002B8 RID: 696
		public float m22;

		// Token: 0x040002B9 RID: 697
		public float m32;

		// Token: 0x040002BA RID: 698
		public float m03;

		// Token: 0x040002BB RID: 699
		public float m13;

		// Token: 0x040002BC RID: 700
		public float m23;

		// Token: 0x040002BD RID: 701
		public float m33;
	}
}
