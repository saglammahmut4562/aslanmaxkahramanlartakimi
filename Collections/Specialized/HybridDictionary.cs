using System;

namespace System.Collections.Specialized
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class HybridDictionary : ICollection, IDictionary, IEnumerable
	{
		// Token: 0x06000058 RID: 88 RVA: 0x000030B0 File Offset: 0x000012B0
		public HybridDictionary()
			: this(0, false)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000030BC File Offset: 0x000012BC
		public HybridDictionary(int initialSize, bool caseInsensitive)
		{
			this.caseInsensitive = caseInsensitive;
			IComparer comparer = ((!caseInsensitive) ? null : CaseInsensitiveComparer.DefaultInvariant);
			IHashCodeProvider hashCodeProvider = ((!caseInsensitive) ? null : CaseInsensitiveHashCodeProvider.DefaultInvariant);
			if (initialSize <= 10)
			{
				this.list = new ListDictionary(comparer);
			}
			else
			{
				this.hashtable = new Hashtable(initialSize, hashCodeProvider, comparer);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003124 File Offset: 0x00001324
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000312C File Offset: 0x0000132C
		private IDictionary inner
		{
			get
			{
				IDictionary dictionary2;
				if (this.list == null)
				{
					IDictionary dictionary = this.hashtable;
					dictionary2 = dictionary;
				}
				else
				{
					dictionary2 = this.list;
				}
				return dictionary2;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003158 File Offset: 0x00001358
		public int Count
		{
			get
			{
				return this.inner.Count;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003168 File Offset: 0x00001368
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000316C File Offset: 0x0000136C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003170 File Offset: 0x00001370
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001B RID: 27
		public object this[object key]
		{
			get
			{
				return this.inner[key];
			}
			set
			{
				this.inner[key] = value;
				if (this.list != null && this.Count > 10)
				{
					this.Switch();
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000031B4 File Offset: 0x000013B4
		public ICollection Keys
		{
			get
			{
				return this.inner.Keys;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000031C4 File Offset: 0x000013C4
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000031C8 File Offset: 0x000013C8
		public ICollection Values
		{
			get
			{
				return this.inner.Values;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000031D8 File Offset: 0x000013D8
		public void Add(object key, object value)
		{
			this.inner.Add(key, value);
			if (this.list != null && this.Count > 10)
			{
				this.Switch();
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003208 File Offset: 0x00001408
		public void Clear()
		{
			this.inner.Clear();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003218 File Offset: 0x00001418
		public bool Contains(object key)
		{
			return this.inner.Contains(key);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003228 File Offset: 0x00001428
		public void CopyTo(Array array, int index)
		{
			this.inner.CopyTo(array, index);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003238 File Offset: 0x00001438
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.inner.GetEnumerator();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003248 File Offset: 0x00001448
		public void Remove(object key)
		{
			this.inner.Remove(key);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003258 File Offset: 0x00001458
		private void Switch()
		{
			IComparer comparer = ((!this.caseInsensitive) ? null : CaseInsensitiveComparer.DefaultInvariant);
			IHashCodeProvider hashCodeProvider = ((!this.caseInsensitive) ? null : CaseInsensitiveHashCodeProvider.DefaultInvariant);
			this.hashtable = new Hashtable(this.list, hashCodeProvider, comparer);
			this.list.Clear();
			this.list = null;
		}

		// Token: 0x04000037 RID: 55
		private const int switchAfter = 10;

		// Token: 0x04000038 RID: 56
		private bool caseInsensitive;

		// Token: 0x04000039 RID: 57
		private Hashtable hashtable;

		// Token: 0x0400003A RID: 58
		private ListDictionary list;
	}
}
