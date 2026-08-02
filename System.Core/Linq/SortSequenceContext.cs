using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000045 RID: 69
	internal class SortSequenceContext<TElement, TKey> : SortContext<TElement>
	{
		// Token: 0x06000170 RID: 368 RVA: 0x000067E0 File Offset: 0x000049E0
		public SortSequenceContext(Func<TElement, TKey> selector, IComparer<TKey> comparer, SortDirection direction, SortContext<TElement> child_context)
			: base(direction, child_context)
		{
			this.selector = selector;
			this.comparer = comparer;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000067FC File Offset: 0x000049FC
		public override void Initialize(TElement[] elements)
		{
			if (this.child_context != null)
			{
				this.child_context.Initialize(elements);
			}
			this.keys = new TKey[elements.Length];
			for (int i = 0; i < this.keys.Length; i++)
			{
				this.keys[i] = this.selector(elements[i]);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006868 File Offset: 0x00004A68
		public override int Compare(int first_index, int second_index)
		{
			int num = this.comparer.Compare(this.keys[first_index], this.keys[second_index]);
			if (num == 0)
			{
				if (this.child_context != null)
				{
					return this.child_context.Compare(first_index, second_index);
				}
				num = ((this.direction != SortDirection.Descending) ? (first_index - second_index) : (second_index - first_index));
			}
			return (this.direction != SortDirection.Descending) ? num : (-num);
		}

		// Token: 0x0400014A RID: 330
		private Func<TElement, TKey> selector;

		// Token: 0x0400014B RID: 331
		private IComparer<TKey> comparer;

		// Token: 0x0400014C RID: 332
		private TKey[] keys;
	}
}
