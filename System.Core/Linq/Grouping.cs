using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200003C RID: 60
	internal class Grouping<K, T> : IEnumerable<T>, IGrouping<K, T>, IEnumerable
	{
		// Token: 0x06000151 RID: 337 RVA: 0x000062F0 File Offset: 0x000044F0
		public Grouping(K key, IEnumerable<T> group)
		{
			this.group = group;
			this.key = key;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006308 File Offset: 0x00004508
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.group.GetEnumerator();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006318 File Offset: 0x00004518
		public IEnumerator<T> GetEnumerator()
		{
			return this.group.GetEnumerator();
		}

		// Token: 0x04000133 RID: 307
		private K key;

		// Token: 0x04000134 RID: 308
		private IEnumerable<T> group;
	}
}
