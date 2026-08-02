using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200003F RID: 63
	internal abstract class OrderedEnumerable<TElement> : IEnumerable<TElement>, IOrderedEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00006328 File Offset: 0x00004528
		protected OrderedEnumerable(IEnumerable<TElement> source)
		{
			this.source = source;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006338 File Offset: 0x00004538
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006340 File Offset: 0x00004540
		public IEnumerator<TElement> GetEnumerator()
		{
			return this.Sort(this.source).GetEnumerator();
		}

		// Token: 0x06000157 RID: 343
		public abstract SortContext<TElement> CreateContext(SortContext<TElement> current);

		// Token: 0x06000158 RID: 344
		protected abstract IEnumerable<TElement> Sort(IEnumerable<TElement> source);

		// Token: 0x04000135 RID: 309
		private IEnumerable<TElement> source;
	}
}
