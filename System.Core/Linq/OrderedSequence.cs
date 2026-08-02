using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000040 RID: 64
	internal class OrderedSequence<TElement, TKey> : OrderedEnumerable<TElement>
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00006354 File Offset: 0x00004554
		internal OrderedSequence(IEnumerable<TElement> source, Func<TElement, TKey> key_selector, IComparer<TKey> comparer, SortDirection direction)
			: base(source)
		{
			this.selector = key_selector;
			this.comparer = comparer ?? Comparer<TKey>.Default;
			this.direction = direction;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006380 File Offset: 0x00004580
		public override SortContext<TElement> CreateContext(SortContext<TElement> current)
		{
			SortContext<TElement> sortContext = new SortSequenceContext<TElement, TKey>(this.selector, this.comparer, this.direction, current);
			if (this.parent != null)
			{
				return this.parent.CreateContext(sortContext);
			}
			return sortContext;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000063C0 File Offset: 0x000045C0
		protected override IEnumerable<TElement> Sort(IEnumerable<TElement> source)
		{
			return QuickSort<TElement>.Sort(source, this.CreateContext(null));
		}

		// Token: 0x04000136 RID: 310
		private OrderedEnumerable<TElement> parent;

		// Token: 0x04000137 RID: 311
		private Func<TElement, TKey> selector;

		// Token: 0x04000138 RID: 312
		private IComparer<TKey> comparer;

		// Token: 0x04000139 RID: 313
		private SortDirection direction;
	}
}
