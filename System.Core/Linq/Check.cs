using System;

namespace System.Linq
{
	// Token: 0x02000012 RID: 18
	internal static class Check
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002A50 File Offset: 0x00000C50
		public static void Source(object source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A64 File Offset: 0x00000C64
		public static void SourceAndSelector(object source, object selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A88 File Offset: 0x00000C88
		public static void SourceAndPredicate(object source, object predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AAC File Offset: 0x00000CAC
		public static void FirstAndSecond(object first, object second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public static void SourceAndKeySelector(object source, object keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public static void SourceAndKeyElementSelectors(object source, object keySelector, object elementSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
		}
	}
}
