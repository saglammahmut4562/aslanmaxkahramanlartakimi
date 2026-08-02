using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Collections.Specialized
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public abstract class NameObjectCollectionBase : ICollection, IEnumerable, IDeserializationCallback, ISerializable
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00003990 File Offset: 0x00001B90
		protected NameObjectCollectionBase()
		{
			this.m_readonly = false;
			this.m_hashprovider = CaseInsensitiveHashCodeProvider.DefaultInvariant;
			this.m_comparer = CaseInsensitiveComparer.DefaultInvariant;
			this.m_defCapacity = 0;
			this.Init();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000039C4 File Offset: 0x00001BC4
		protected NameObjectCollectionBase(SerializationInfo info, StreamingContext context)
		{
			this.infoCopy = info;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000039D4 File Offset: 0x00001BD4
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000039D8 File Offset: 0x00001BD8
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000039DC File Offset: 0x00001BDC
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.Keys).CopyTo(array, index);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000039EC File Offset: 0x00001BEC
		private void Init()
		{
			if (this.equality_comparer != null)
			{
				this.m_ItemsContainer = new Hashtable(this.m_defCapacity, this.equality_comparer);
			}
			else
			{
				this.m_ItemsContainer = new Hashtable(this.m_defCapacity, this.m_hashprovider, this.m_comparer);
			}
			this.m_ItemsArray = new ArrayList();
			this.m_NullKeyItem = null;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003A50 File Offset: 0x00001C50
		public virtual NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				if (this.keyscoll == null)
				{
					this.keyscoll = new NameObjectCollectionBase.KeysCollection(this);
				}
				return this.keyscoll;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003A70 File Offset: 0x00001C70
		public virtual IEnumerator GetEnumerator()
		{
			return new NameObjectCollectionBase._KeysEnumerator(this);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003A78 File Offset: 0x00001C78
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			int count = this.Count;
			string[] array = new string[count];
			object[] array2 = new object[count];
			int num = 0;
			foreach (object obj in this.m_ItemsArray)
			{
				NameObjectCollectionBase._Item item = (NameObjectCollectionBase._Item)obj;
				array[num] = item.key;
				array2[num] = item.value;
				num++;
			}
			if (this.equality_comparer != null)
			{
				info.AddValue("KeyComparer", this.equality_comparer, typeof(IEqualityComparer));
				info.AddValue("Version", 4, typeof(int));
			}
			else
			{
				info.AddValue("HashProvider", this.m_hashprovider, typeof(IHashCodeProvider));
				info.AddValue("Comparer", this.m_comparer, typeof(IComparer));
				info.AddValue("Version", 2, typeof(int));
			}
			info.AddValue("ReadOnly", this.m_readonly);
			info.AddValue("Count", count);
			info.AddValue("Keys", array, typeof(string[]));
			info.AddValue("Values", array2, typeof(object[]));
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003BFC File Offset: 0x00001DFC
		public virtual int Count
		{
			get
			{
				return this.m_ItemsArray.Count;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003C0C File Offset: 0x00001E0C
		public virtual void OnDeserialization(object sender)
		{
			SerializationInfo serializationInfo = this.infoCopy;
			if (serializationInfo == null)
			{
				return;
			}
			this.infoCopy = null;
			this.m_hashprovider = (IHashCodeProvider)serializationInfo.GetValue("HashProvider", typeof(IHashCodeProvider));
			if (this.m_hashprovider == null)
			{
				this.equality_comparer = (IEqualityComparer)serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
			}
			else
			{
				this.m_comparer = (IComparer)serializationInfo.GetValue("Comparer", typeof(IComparer));
				if (this.m_comparer == null)
				{
					throw new SerializationException("The comparer is null");
				}
			}
			this.m_readonly = serializationInfo.GetBoolean("ReadOnly");
			string[] array = (string[])serializationInfo.GetValue("Keys", typeof(string[]));
			if (array == null)
			{
				throw new SerializationException("keys is null");
			}
			object[] array2 = (object[])serializationInfo.GetValue("Values", typeof(object[]));
			if (array2 == null)
			{
				throw new SerializationException("values is null");
			}
			this.Init();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				this.BaseAdd(array[i], array2[i]);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003D4C File Offset: 0x00001F4C
		protected bool IsReadOnly
		{
			get
			{
				return this.m_readonly;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003D54 File Offset: 0x00001F54
		protected void BaseAdd(string name, object value)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only");
			}
			NameObjectCollectionBase._Item item = new NameObjectCollectionBase._Item(name, value);
			if (name == null)
			{
				if (this.m_NullKeyItem == null)
				{
					this.m_NullKeyItem = item;
				}
			}
			else if (this.m_ItemsContainer[name] == null)
			{
				this.m_ItemsContainer.Add(name, item);
			}
			this.m_ItemsArray.Add(item);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003DC8 File Offset: 0x00001FC8
		protected object BaseGet(int index)
		{
			return ((NameObjectCollectionBase._Item)this.m_ItemsArray[index]).value;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003DE0 File Offset: 0x00001FE0
		protected object BaseGet(string name)
		{
			NameObjectCollectionBase._Item item = this.FindFirstMatchedItem(name);
			if (item == null)
			{
				return null;
			}
			return item.value;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003E04 File Offset: 0x00002004
		protected string BaseGetKey(int index)
		{
			return ((NameObjectCollectionBase._Item)this.m_ItemsArray[index]).key;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003E1C File Offset: 0x0000201C
		protected void BaseRemove(string name)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only");
			}
			if (name != null)
			{
				this.m_ItemsContainer.Remove(name);
			}
			else
			{
				this.m_NullKeyItem = null;
			}
			int num = this.m_ItemsArray.Count;
			int i = 0;
			while (i < num)
			{
				string text = this.BaseGetKey(i);
				if (this.Equals(text, name))
				{
					this.m_ItemsArray.RemoveAt(i);
					num--;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003EA8 File Offset: 0x000020A8
		protected void BaseSet(string name, object value)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only");
			}
			NameObjectCollectionBase._Item item = this.FindFirstMatchedItem(name);
			if (item != null)
			{
				item.value = value;
			}
			else
			{
				this.BaseAdd(name, value);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003EF0 File Offset: 0x000020F0
		[global::System.MonoTODO]
		private NameObjectCollectionBase._Item FindFirstMatchedItem(string name)
		{
			if (name != null)
			{
				return (NameObjectCollectionBase._Item)this.m_ItemsContainer[name];
			}
			return this.m_NullKeyItem;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003F10 File Offset: 0x00002110
		internal bool Equals(string s1, string s2)
		{
			if (this.m_comparer != null)
			{
				return this.m_comparer.Compare(s1, s2) == 0;
			}
			return this.equality_comparer.Equals(s1, s2);
		}

		// Token: 0x0400004A RID: 74
		private Hashtable m_ItemsContainer;

		// Token: 0x0400004B RID: 75
		private NameObjectCollectionBase._Item m_NullKeyItem;

		// Token: 0x0400004C RID: 76
		private ArrayList m_ItemsArray;

		// Token: 0x0400004D RID: 77
		private IHashCodeProvider m_hashprovider;

		// Token: 0x0400004E RID: 78
		private IComparer m_comparer;

		// Token: 0x0400004F RID: 79
		private int m_defCapacity;

		// Token: 0x04000050 RID: 80
		private bool m_readonly;

		// Token: 0x04000051 RID: 81
		private SerializationInfo infoCopy;

		// Token: 0x04000052 RID: 82
		private NameObjectCollectionBase.KeysCollection keyscoll;

		// Token: 0x04000053 RID: 83
		private IEqualityComparer equality_comparer;

		// Token: 0x02000018 RID: 24
		internal class _Item
		{
			// Token: 0x060000A9 RID: 169 RVA: 0x00003F3C File Offset: 0x0000213C
			public _Item(string key, object value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x04000054 RID: 84
			public string key;

			// Token: 0x04000055 RID: 85
			public object value;
		}

		// Token: 0x02000019 RID: 25
		[Serializable]
		internal class _KeysEnumerator : IEnumerator
		{
			// Token: 0x060000AA RID: 170 RVA: 0x00003F54 File Offset: 0x00002154
			internal _KeysEnumerator(NameObjectCollectionBase collection)
			{
				this.m_collection = collection;
				this.Reset();
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060000AB RID: 171 RVA: 0x00003F6C File Offset: 0x0000216C
			public object Current
			{
				get
				{
					if (this.m_position < this.m_collection.Count || this.m_position < 0)
					{
						return this.m_collection.BaseGetKey(this.m_position);
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060000AC RID: 172 RVA: 0x00003FA8 File Offset: 0x000021A8
			public bool MoveNext()
			{
				return ++this.m_position < this.m_collection.Count;
			}

			// Token: 0x060000AD RID: 173 RVA: 0x00003FD4 File Offset: 0x000021D4
			public void Reset()
			{
				this.m_position = -1;
			}

			// Token: 0x04000056 RID: 86
			private NameObjectCollectionBase m_collection;

			// Token: 0x04000057 RID: 87
			private int m_position;
		}

		// Token: 0x0200001A RID: 26
		[DefaultMember("Item")]
		[Serializable]
		public class KeysCollection : ICollection, IEnumerable
		{
			// Token: 0x060000AE RID: 174 RVA: 0x00003FE0 File Offset: 0x000021E0
			internal KeysCollection(NameObjectCollectionBase collection)
			{
				this.m_collection = collection;
			}

			// Token: 0x060000AF RID: 175 RVA: 0x00003FF0 File Offset: 0x000021F0
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				ArrayList itemsArray = this.m_collection.m_ItemsArray;
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				if (array.Length > 0 && arrayIndex >= array.Length)
				{
					throw new ArgumentException("arrayIndex is equal to or greater than array.Length");
				}
				if (arrayIndex + itemsArray.Count > array.Length)
				{
					throw new ArgumentException("Not enough room from arrayIndex to end of array for this KeysCollection");
				}
				if (array != null && array.Rank > 1)
				{
					throw new ArgumentException("array is multidimensional");
				}
				object[] array2 = (object[])array;
				int i = 0;
				while (i < itemsArray.Count)
				{
					array2[arrayIndex] = ((NameObjectCollectionBase._Item)itemsArray[i]).key;
					i++;
					arrayIndex++;
				}
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x000040C4 File Offset: 0x000022C4
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x000040C8 File Offset: 0x000022C8
			object ICollection.SyncRoot
			{
				get
				{
					return this.m_collection;
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000B2 RID: 178 RVA: 0x000040D0 File Offset: 0x000022D0
			public int Count
			{
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x000040E0 File Offset: 0x000022E0
			public IEnumerator GetEnumerator()
			{
				return new NameObjectCollectionBase._KeysEnumerator(this.m_collection);
			}

			// Token: 0x04000058 RID: 88
			private NameObjectCollectionBase m_collection;
		}
	}
}
