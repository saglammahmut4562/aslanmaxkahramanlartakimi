using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Net
{
	// Token: 0x0200006C RID: 108
	[Serializable]
	public sealed class CookieCollection : ICollection, IEnumerable
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00008F7C File Offset: 0x0000717C
		internal IList<Cookie> List
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00008F84 File Offset: 0x00007184
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00008F94 File Offset: 0x00007194
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00008F98 File Offset: 0x00007198
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00008F9C File Offset: 0x0000719C
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.list).CopyTo(array, index);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008FAC File Offset: 0x000071AC
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00008FC0 File Offset: 0x000071C0
		public void Add(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			int num = this.SearchCookie(cookie);
			if (num == -1)
			{
				this.list.Add(cookie);
			}
			else
			{
				this.list[num] = cookie;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000900C File Offset: 0x0000720C
		internal void Sort()
		{
			if (this.list.Count > 0)
			{
				this.list.Sort(CookieCollection.Comparer);
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009030 File Offset: 0x00007230
		private int SearchCookie(Cookie cookie)
		{
			string name = cookie.Name;
			string domain = cookie.Domain;
			string path = cookie.Path;
			for (int i = this.list.Count - 1; i >= 0; i--)
			{
				Cookie cookie2 = this.list[i];
				if (cookie2.Version == cookie.Version)
				{
					if (string.Compare(domain, cookie2.Domain, true, CultureInfo.InvariantCulture) == 0)
					{
						if (string.Compare(name, cookie2.Name, true, CultureInfo.InvariantCulture) == 0)
						{
							if (string.Compare(path, cookie2.Path, true, CultureInfo.InvariantCulture) == 0)
							{
								return i;
							}
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x1700009B RID: 155
		public Cookie this[int index]
		{
			get
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.list[index];
			}
		}

		// Token: 0x040000D5 RID: 213
		private List<Cookie> list = new List<Cookie>();

		// Token: 0x040000D6 RID: 214
		private static CookieCollection.CookieCollectionComparer Comparer = new CookieCollection.CookieCollectionComparer();

		// Token: 0x0200006D RID: 109
		private sealed class CookieCollectionComparer : IComparer<Cookie>
		{
			// Token: 0x0600027D RID: 637 RVA: 0x0000912C File Offset: 0x0000732C
			public int Compare(Cookie x, Cookie y)
			{
				if (x == null || y == null)
				{
					return 0;
				}
				int num = x.Name.Length + x.Value.Length;
				int num2 = y.Name.Length + y.Value.Length;
				return num - num2;
			}
		}
	}
}
