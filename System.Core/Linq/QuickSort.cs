using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000041 RID: 65
	internal class QuickSort<TElement>
	{
		// Token: 0x0600015C RID: 348 RVA: 0x000063D0 File Offset: 0x000045D0
		private QuickSort(IEnumerable<TElement> source, SortContext<TElement> context)
		{
			this.elements = source.ToArray<TElement>();
			this.indexes = QuickSort<TElement>.CreateIndexes(this.elements.Length);
			this.context = context;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006400 File Offset: 0x00004600
		private static int[] CreateIndexes(int length)
		{
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = i;
			}
			return array;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000642C File Offset: 0x0000462C
		private void PerformSort()
		{
			if (this.elements.Length <= 1)
			{
				return;
			}
			this.context.Initialize(this.elements);
			this.Sort(0, this.indexes.Length - 1);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006460 File Offset: 0x00004660
		private int CompareItems(int first_index, int second_index)
		{
			return this.context.Compare(first_index, second_index);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006470 File Offset: 0x00004670
		private int MedianOfThree(int left, int right)
		{
			int num = (left + right) / 2;
			if (this.CompareItems(this.indexes[num], this.indexes[left]) < 0)
			{
				this.Swap(left, num);
			}
			if (this.CompareItems(this.indexes[right], this.indexes[left]) < 0)
			{
				this.Swap(left, right);
			}
			if (this.CompareItems(this.indexes[right], this.indexes[num]) < 0)
			{
				this.Swap(num, right);
			}
			this.Swap(num, right - 1);
			return this.indexes[right - 1];
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006504 File Offset: 0x00004704
		private void Sort(int left, int right)
		{
			if (left + 3 <= right)
			{
				int num = left;
				int num2 = right - 1;
				int num3 = this.MedianOfThree(left, right);
				for (;;)
				{
					while (this.CompareItems(this.indexes[++num], num3) < 0)
					{
					}
					while (this.CompareItems(this.indexes[--num2], num3) > 0)
					{
					}
					if (num >= num2)
					{
						break;
					}
					this.Swap(num, num2);
				}
				this.Swap(num, right - 1);
				this.Sort(left, num - 1);
				this.Sort(num + 1, right);
			}
			else
			{
				this.InsertionSort(left, right);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000065B0 File Offset: 0x000047B0
		private void InsertionSort(int left, int right)
		{
			for (int i = left + 1; i <= right; i++)
			{
				int num = this.indexes[i];
				int num2 = i;
				while (num2 > left && this.CompareItems(num, this.indexes[num2 - 1]) < 0)
				{
					this.indexes[num2] = this.indexes[num2 - 1];
					num2--;
				}
				this.indexes[num2] = num;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006620 File Offset: 0x00004820
		private void Swap(int left, int right)
		{
			int num = this.indexes[right];
			this.indexes[right] = this.indexes[left];
			this.indexes[left] = num;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006650 File Offset: 0x00004850
		public static IEnumerable<TElement> Sort(IEnumerable<TElement> source, SortContext<TElement> context)
		{
			QuickSort<TElement> sorter = new QuickSort<TElement>(source, context);
			sorter.PerformSort();
			for (int i = 0; i < sorter.indexes.Length; i++)
			{
				yield return sorter.elements[sorter.indexes[i]];
			}
			yield break;
		}

		// Token: 0x0400013A RID: 314
		private TElement[] elements;

		// Token: 0x0400013B RID: 315
		private int[] indexes;

		// Token: 0x0400013C RID: 316
		private SortContext<TElement> context;
	}
}
