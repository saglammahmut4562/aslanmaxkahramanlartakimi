using System;

namespace System.Collections.Specialized
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class ListDictionary : ICollection, IDictionary, IEnumerable
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000032B8 File Offset: 0x000014B8
		public ListDictionary()
		{
			this.count = 0;
			this.version = 0;
			this.comparer = null;
			this.head = null;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000032DC File Offset: 0x000014DC
		public ListDictionary(IComparer comparer)
			: this()
		{
			this.comparer = comparer;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000032EC File Offset: 0x000014EC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionary.DictionaryNodeEnumerator(this);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000032F4 File Offset: 0x000014F4
		private ListDictionary.DictionaryNode FindEntry(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Attempted lookup for a null key.");
			}
			ListDictionary.DictionaryNode dictionaryNode = this.head;
			if (this.comparer == null)
			{
				while (dictionaryNode != null)
				{
					if (key.Equals(dictionaryNode.key))
					{
						break;
					}
					dictionaryNode = dictionaryNode.next;
				}
			}
			else
			{
				while (dictionaryNode != null)
				{
					if (this.comparer.Compare(key, dictionaryNode.key) == 0)
					{
						break;
					}
					dictionaryNode = dictionaryNode.next;
				}
			}
			return dictionaryNode;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003388 File Offset: 0x00001588
		private ListDictionary.DictionaryNode FindEntry(object key, out ListDictionary.DictionaryNode prev)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Attempted lookup for a null key.");
			}
			ListDictionary.DictionaryNode dictionaryNode = this.head;
			prev = null;
			if (this.comparer == null)
			{
				while (dictionaryNode != null)
				{
					if (key.Equals(dictionaryNode.key))
					{
						break;
					}
					prev = dictionaryNode;
					dictionaryNode = dictionaryNode.next;
				}
			}
			else
			{
				while (dictionaryNode != null)
				{
					if (this.comparer.Compare(key, dictionaryNode.key) == 0)
					{
						break;
					}
					prev = dictionaryNode;
					dictionaryNode = dictionaryNode.next;
				}
			}
			return dictionaryNode;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003424 File Offset: 0x00001624
		private void AddImpl(object key, object value, ListDictionary.DictionaryNode prev)
		{
			if (prev == null)
			{
				this.head = new ListDictionary.DictionaryNode(key, value, this.head);
			}
			else
			{
				prev.next = new ListDictionary.DictionaryNode(key, value, prev.next);
			}
			this.count++;
			this.version++;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003480 File Offset: 0x00001680
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003488 File Offset: 0x00001688
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000348C File Offset: 0x0000168C
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003490 File Offset: 0x00001690
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Array cannot be null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "index is less than 0");
			}
			if (index > array.Length)
			{
				throw new IndexOutOfRangeException("index is too large");
			}
			if (this.Count > array.Length - index)
			{
				throw new ArgumentException("Not enough room in the array");
			}
			foreach (object obj in this)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array.SetValue(dictionaryEntry, index++);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000355C File Offset: 0x0000175C
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003560 File Offset: 0x00001760
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000024 RID: 36
		public object this[object key]
		{
			get
			{
				ListDictionary.DictionaryNode dictionaryNode = this.FindEntry(key);
				return (dictionaryNode != null) ? dictionaryNode.value : null;
			}
			set
			{
				ListDictionary.DictionaryNode dictionaryNode2;
				ListDictionary.DictionaryNode dictionaryNode = this.FindEntry(key, out dictionaryNode2);
				if (dictionaryNode != null)
				{
					dictionaryNode.value = value;
				}
				else
				{
					this.AddImpl(key, value, dictionaryNode2);
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000035C0 File Offset: 0x000017C0
		public ICollection Keys
		{
			get
			{
				return new ListDictionary.DictionaryNodeCollection(this, true);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000035CC File Offset: 0x000017CC
		public ICollection Values
		{
			get
			{
				return new ListDictionary.DictionaryNodeCollection(this, false);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000035D8 File Offset: 0x000017D8
		public void Add(object key, object value)
		{
			ListDictionary.DictionaryNode dictionaryNode2;
			ListDictionary.DictionaryNode dictionaryNode = this.FindEntry(key, out dictionaryNode2);
			if (dictionaryNode != null)
			{
				throw new ArgumentException("key", "Duplicate key in add.");
			}
			this.AddImpl(key, value, dictionaryNode2);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003610 File Offset: 0x00001810
		public void Clear()
		{
			this.head = null;
			this.count = 0;
			this.version++;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003630 File Offset: 0x00001830
		public bool Contains(object key)
		{
			return this.FindEntry(key) != null;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003640 File Offset: 0x00001840
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionary.DictionaryNodeEnumerator(this);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003648 File Offset: 0x00001848
		public void Remove(object key)
		{
			ListDictionary.DictionaryNode dictionaryNode2;
			ListDictionary.DictionaryNode dictionaryNode = this.FindEntry(key, out dictionaryNode2);
			if (dictionaryNode == null)
			{
				return;
			}
			if (dictionaryNode2 == null)
			{
				this.head = dictionaryNode.next;
			}
			else
			{
				dictionaryNode2.next = dictionaryNode.next;
			}
			dictionaryNode.value = null;
			this.count--;
			this.version++;
		}

		// Token: 0x0400003B RID: 59
		private int count;

		// Token: 0x0400003C RID: 60
		private int version;

		// Token: 0x0400003D RID: 61
		private ListDictionary.DictionaryNode head;

		// Token: 0x0400003E RID: 62
		private IComparer comparer;

		// Token: 0x02000013 RID: 19
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x06000081 RID: 129 RVA: 0x000036AC File Offset: 0x000018AC
			public DictionaryNode(object key, object value, ListDictionary.DictionaryNode next)
			{
				this.key = key;
				this.value = value;
				this.next = next;
			}

			// Token: 0x0400003F RID: 63
			public object key;

			// Token: 0x04000040 RID: 64
			public object value;

			// Token: 0x04000041 RID: 65
			public ListDictionary.DictionaryNode next;
		}

		// Token: 0x02000014 RID: 20
		private class DictionaryNodeCollection : ICollection, IEnumerable
		{
			// Token: 0x06000082 RID: 130 RVA: 0x000036CC File Offset: 0x000018CC
			public DictionaryNodeCollection(ListDictionary dict, bool isKeyList)
			{
				this.dict = dict;
				this.isKeyList = isKeyList;
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000083 RID: 131 RVA: 0x000036E4 File Offset: 0x000018E4
			public int Count
			{
				get
				{
					return this.dict.Count;
				}
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000084 RID: 132 RVA: 0x000036F4 File Offset: 0x000018F4
			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x06000085 RID: 133 RVA: 0x000036F8 File Offset: 0x000018F8
			public object SyncRoot
			{
				get
				{
					return this.dict.SyncRoot;
				}
			}

			// Token: 0x06000086 RID: 134 RVA: 0x00003708 File Offset: 0x00001908
			public void CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array", "Array cannot be null.");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", "index is less than 0");
				}
				if (index > array.Length)
				{
					throw new IndexOutOfRangeException("index is too large");
				}
				if (this.Count > array.Length - index)
				{
					throw new ArgumentException("Not enough room in the array");
				}
				foreach (object obj in this)
				{
					array.SetValue(obj, index++);
				}
			}

			// Token: 0x06000087 RID: 135 RVA: 0x000037CC File Offset: 0x000019CC
			public IEnumerator GetEnumerator()
			{
				return new ListDictionary.DictionaryNodeCollection.DictionaryNodeCollectionEnumerator(this.dict.GetEnumerator(), this.isKeyList);
			}

			// Token: 0x04000042 RID: 66
			private ListDictionary dict;

			// Token: 0x04000043 RID: 67
			private bool isKeyList;

			// Token: 0x02000015 RID: 21
			private class DictionaryNodeCollectionEnumerator : IEnumerator
			{
				// Token: 0x06000088 RID: 136 RVA: 0x000037E4 File Offset: 0x000019E4
				public DictionaryNodeCollectionEnumerator(IDictionaryEnumerator inner, bool isKeyList)
				{
					this.inner = inner;
					this.isKeyList = isKeyList;
				}

				// Token: 0x1700002A RID: 42
				// (get) Token: 0x06000089 RID: 137 RVA: 0x000037FC File Offset: 0x000019FC
				public object Current
				{
					get
					{
						return (!this.isKeyList) ? this.inner.Value : this.inner.Key;
					}
				}

				// Token: 0x0600008A RID: 138 RVA: 0x00003824 File Offset: 0x00001A24
				public bool MoveNext()
				{
					return this.inner.MoveNext();
				}

				// Token: 0x0600008B RID: 139 RVA: 0x00003834 File Offset: 0x00001A34
				public void Reset()
				{
					this.inner.Reset();
				}

				// Token: 0x04000044 RID: 68
				private IDictionaryEnumerator inner;

				// Token: 0x04000045 RID: 69
				private bool isKeyList;
			}
		}

		// Token: 0x02000016 RID: 22
		private class DictionaryNodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x0600008C RID: 140 RVA: 0x00003844 File Offset: 0x00001A44
			public DictionaryNodeEnumerator(ListDictionary dict)
			{
				this.dict = dict;
				this.version = dict.version;
				this.Reset();
			}

			// Token: 0x0600008D RID: 141 RVA: 0x00003868 File Offset: 0x00001A68
			private void FailFast()
			{
				if (this.version != this.dict.version)
				{
					throw new InvalidOperationException("The ListDictionary's contents changed after this enumerator was instantiated.");
				}
			}

			// Token: 0x0600008E RID: 142 RVA: 0x0000388C File Offset: 0x00001A8C
			public bool MoveNext()
			{
				this.FailFast();
				if (this.current == null && !this.isAtStart)
				{
					return false;
				}
				this.current = ((!this.isAtStart) ? this.current.next : this.dict.head);
				this.isAtStart = false;
				return this.current != null;
			}

			// Token: 0x0600008F RID: 143 RVA: 0x000038F8 File Offset: 0x00001AF8
			public void Reset()
			{
				this.FailFast();
				this.isAtStart = true;
				this.current = null;
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x06000090 RID: 144 RVA: 0x00003910 File Offset: 0x00001B10
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x06000091 RID: 145 RVA: 0x00003920 File Offset: 0x00001B20
			private ListDictionary.DictionaryNode DictionaryNode
			{
				get
				{
					this.FailFast();
					if (this.current == null)
					{
						throw new InvalidOperationException("Enumerator is positioned before the collection's first element or after the last element.");
					}
					return this.current;
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000092 RID: 146 RVA: 0x00003944 File Offset: 0x00001B44
			public DictionaryEntry Entry
			{
				get
				{
					object key = this.DictionaryNode.key;
					return new DictionaryEntry(key, this.current.value);
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000093 RID: 147 RVA: 0x00003970 File Offset: 0x00001B70
			public object Key
			{
				get
				{
					return this.DictionaryNode.key;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000094 RID: 148 RVA: 0x00003980 File Offset: 0x00001B80
			public object Value
			{
				get
				{
					return this.DictionaryNode.value;
				}
			}

			// Token: 0x04000046 RID: 70
			private ListDictionary dict;

			// Token: 0x04000047 RID: 71
			private bool isAtStart;

			// Token: 0x04000048 RID: 72
			private ListDictionary.DictionaryNode current;

			// Token: 0x04000049 RID: 73
			private int version;
		}
	}
}
