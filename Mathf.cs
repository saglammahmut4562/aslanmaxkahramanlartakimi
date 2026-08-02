using System;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000097 RID: 151
	public struct Mathf
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x00010170 File Offset: 0x0000E370
		public static float Sin(float f)
		{
			return (float)Math.Sin((double)f);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001017C File Offset: 0x0000E37C
		public static float Cos(float f)
		{
			return (float)Math.Cos((double)f);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00010188 File Offset: 0x0000E388
		public static float Tan(float f)
		{
			return (float)Math.Tan((double)f);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00010194 File Offset: 0x0000E394
		public static float Acos(float f)
		{
			return (float)Math.Acos((double)f);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000101A0 File Offset: 0x0000E3A0
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000101AC File Offset: 0x0000E3AC
		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt((double)f);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000101B8 File Offset: 0x0000E3B8
		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000101C4 File Offset: 0x0000E3C4
		public static float Min(float a, float b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000101D4 File Offset: 0x0000E3D4
		public static int Min(int a, int b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public static float Max(float a, float b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000101F4 File Offset: 0x0000E3F4
		public static float Max(params float[] values)
		{
			int num = values.Length;
			if (num == 0)
			{
				return 0f;
			}
			float num2 = values[0];
			for (int i = 1; i < num; i++)
			{
				if (values[i] > num2)
				{
					num2 = values[i];
				}
			}
			return num2;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00010238 File Offset: 0x0000E438
		public static int Max(int a, int b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00010248 File Offset: 0x0000E448
		public static float Pow(float f, float p)
		{
			return (float)Math.Pow((double)f, (double)p);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00010254 File Offset: 0x0000E454
		public static float Log(float f, float p)
		{
			return (float)Math.Log((double)f, (double)p);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00010260 File Offset: 0x0000E460
		public static float Log(float f)
		{
			return (float)Math.Log((double)f);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001026C File Offset: 0x0000E46C
		public static float Ceil(float f)
		{
			return (float)Math.Ceiling((double)f);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00010278 File Offset: 0x0000E478
		public static float Floor(float f)
		{
			return (float)Math.Floor((double)f);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00010284 File Offset: 0x0000E484
		public static float Round(float f)
		{
			return (float)Math.Round((double)f);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00010290 File Offset: 0x0000E490
		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling((double)f);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001029C File Offset: 0x0000E49C
		public static int FloorToInt(float f)
		{
			return (int)Math.Floor((double)f);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000102A8 File Offset: 0x0000E4A8
		public static float Sign(float f)
		{
			return (f < 0f) ? (-1f) : 1f;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000102C4 File Offset: 0x0000E4C4
		public static float Clamp(float value, float min, float max)
		{
			if (value < min)
			{
				value = min;
			}
			else if (value > max)
			{
				value = max;
			}
			return value;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000102E0 File Offset: 0x0000E4E0
		public static int Clamp(int value, int min, int max)
		{
			if (value < min)
			{
				value = min;
			}
			else if (value > max)
			{
				value = max;
			}
			return value;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000102FC File Offset: 0x0000E4FC
		public static float Clamp01(float value)
		{
			if (value < 0f)
			{
				return 0f;
			}
			if (value > 1f)
			{
				return 1f;
			}
			return value;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00010324 File Offset: 0x0000E524
		public static float Lerp(float from, float to, float t)
		{
			return from + (to - from) * Mathf.Clamp01(t);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00010334 File Offset: 0x0000E534
		public static float MoveTowards(float current, float target, float maxDelta)
		{
			if (Mathf.Abs(target - current) <= maxDelta)
			{
				return target;
			}
			return current + Mathf.Sign(target - current) * maxDelta;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00010354 File Offset: 0x0000E554
		public static float SmoothStep(float from, float to, float t)
		{
			t = Mathf.Clamp01(t);
			t = -2f * t * t * t + 3f * t * t;
			return to * t + from * (1f - t);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00010384 File Offset: 0x0000E584
		public static bool Approximately(float a, float b)
		{
			return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), 1.1E-44f);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current - target;
			float num5 = target;
			float num6 = maxSpeed * smoothTime;
			num4 = Mathf.Clamp(num4, -num6, num6);
			target = current - num4;
			float num7 = (currentVelocity + num * num4) * deltaTime;
			currentVelocity = (currentVelocity - num * num7) * num3;
			float num8 = target + (num4 + num7) * num3;
			if (num5 - current > 0f == num8 > num5)
			{
				num8 = num5;
				currentVelocity = (num8 - num5) / deltaTime;
			}
			return num8;
		}

		// Token: 0x040002A8 RID: 680
		public const float PI = 3.1415927f;

		// Token: 0x040002A9 RID: 681
		public const float Infinity = float.PositiveInfinity;

		// Token: 0x040002AA RID: 682
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x040002AB RID: 683
		public const float Deg2Rad = 0.017453292f;

		// Token: 0x040002AC RID: 684
		public const float Rad2Deg = 57.29578f;

		// Token: 0x040002AD RID: 685
		public const float Epsilon = 1E-45f;
	}
}
