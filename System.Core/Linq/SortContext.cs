using System;

namespace System.Linq
{
	// Token: 0x02000043 RID: 67
	internal abstract class SortContext<TElement>
	{
		// Token: 0x0600016D RID: 365 RVA: 0x000067C8 File Offset: 0x000049C8
		protected SortContext(SortDirection direction, SortContext<TElement> child_context)
		{
			this.direction = direction;
			this.child_context = child_context;
		}

		// Token: 0x0600016E RID: 366
		public abstract void Initialize(TElement[] elements);

		// Token: 0x0600016F RID: 367
		public abstract int Compare(int first_index, int second_index);

		// Token: 0x04000145 RID: 325
		protected SortDirection direction;

		// Token: 0x04000146 RID: 326
		protected SortContext<TElement> child_context;
	}
}
