using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x0200000A RID: 10
	[ComVisible(false)]
	[Serializable]
	public class LinkedList<T> : ICollection<T>, IEnumerable<T>, ICollection, IEnumerable, IDeserializationCallback, ISerializable
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022C0 File Offset: 0x000004C0
		public LinkedList()
		{
			this.syncRoot = new object();
			this.first = null;
			this.count = (this.version = 0U);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022F8 File Offset: 0x000004F8
		protected LinkedList(SerializationInfo info, StreamingContext context)
			: this()
		{
			this.si = info;
			this.syncRoot = new object();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002314 File Offset: 0x00000514
		void ICollection<T>.Add(T value)
		{
			this.AddLast(value);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002320 File Offset: 0x00000520
		void ICollection.CopyTo(Array array, int index)
		{
			T[] array2 = array as T[];
			if (array2 == null)
			{
				throw new ArgumentException("array");
			}
			this.CopyTo(array2, index);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002350 File Offset: 0x00000550
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002360 File Offset: 0x00000560
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002370 File Offset: 0x00000570
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002374 File Offset: 0x00000574
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002378 File Offset: 0x00000578
		object ICollection.SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002380 File Offset: 0x00000580
		private void VerifyReferencedNode(LinkedListNode<T> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.List != this)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023A8 File Offset: 0x000005A8
		public LinkedListNode<T> AddLast(T value)
		{
			LinkedListNode<T> linkedListNode;
			if (this.first == null)
			{
				linkedListNode = new LinkedListNode<T>(this, value);
				this.first = linkedListNode;
			}
			else
			{
				linkedListNode = new LinkedListNode<T>(this, value, this.first.back, this.first);
			}
			this.count += 1U;
			this.version += 1U;
			return linkedListNode;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000240C File Offset: 0x0000060C
		public void Clear()
		{
			this.count = 0U;
			this.first = null;
			this.version += 1U;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000242C File Offset: 0x0000062C
		public bool Contains(T value)
		{
			LinkedListNode<T> forward = this.first;
			if (forward == null)
			{
				return false;
			}
			while (!value.Equals(forward.Value))
			{
				forward = forward.forward;
				if (forward == this.first)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000247C File Offset: 0x0000067C
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < array.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("array", "Array is multidimensional");
			}
			if ((long)(array.Length - index + array.GetLowerBound(0)) < (long)((ulong)this.count))
			{
				throw new ArgumentException("number of items exceeds capacity");
			}
			LinkedListNode<T> forward = this.first;
			if (this.first == null)
			{
				return;
			}
			do
			{
				array[index] = forward.Value;
				index++;
				forward = forward.forward;
			}
			while (forward != this.first);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000252C File Offset: 0x0000072C
		public LinkedListNode<T> Find(T value)
		{
			LinkedListNode<T> forward = this.first;
			if (forward == null)
			{
				return null;
			}
			while ((value != null || forward.Value != null) && (value == null || !value.Equals(forward.Value)))
			{
				forward = forward.forward;
				if (forward == this.first)
				{
					return null;
				}
			}
			return forward;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025A4 File Offset: 0x000007A4
		public LinkedList<T>.Enumerator GetEnumerator()
		{
			return new LinkedList<T>.Enumerator(this);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025AC File Offset: 0x000007AC
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			T[] array = new T[this.count];
			this.CopyTo(array, 0);
			info.AddValue("DataArray", array, typeof(T[]));
			info.AddValue("version", this.version);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025F8 File Offset: 0x000007F8
		public virtual void OnDeserialization(object sender)
		{
			if (this.si != null)
			{
				T[] array = (T[])this.si.GetValue("DataArray", typeof(T[]));
				if (array != null)
				{
					foreach (T t in array)
					{
						this.AddLast(t);
					}
				}
				this.version = this.si.GetUInt32("version");
				this.si = null;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000267C File Offset: 0x0000087C
		public bool Remove(T value)
		{
			LinkedListNode<T> linkedListNode = this.Find(value);
			if (linkedListNode == null)
			{
				return false;
			}
			this.Remove(linkedListNode);
			return true;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026A4 File Offset: 0x000008A4
		public void Remove(LinkedListNode<T> node)
		{
			this.VerifyReferencedNode(node);
			this.count -= 1U;
			if (this.count == 0U)
			{
				this.first = null;
			}
			if (node == this.first)
			{
				this.first = this.first.forward;
			}
			this.version += 1U;
			node.Detach();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000270C File Offset: 0x0000090C
		public void RemoveFirst()
		{
			if (this.first != null)
			{
				this.Remove(this.first);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002728 File Offset: 0x00000928
		public int Count
		{
			get
			{
				return (int)this.count;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002730 File Offset: 0x00000930
		public LinkedListNode<T> First
		{
			get
			{
				return this.first;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002738 File Offset: 0x00000938
		public LinkedListNode<T> Last
		{
			get
			{
				return (this.first == null) ? null : this.first.back;
			}
		}

		// Token: 0x04000012 RID: 18
		private const string DataArrayKey = "DataArray";

		// Token: 0x04000013 RID: 19
		private const string VersionKey = "version";

		// Token: 0x04000014 RID: 20
		private uint count;

		// Token: 0x04000015 RID: 21
		private uint version;

		// Token: 0x04000016 RID: 22
		private object syncRoot;

		// Token: 0x04000017 RID: 23
		internal LinkedListNode<T> first;

		// Token: 0x04000018 RID: 24
		internal SerializationInfo si;

		// Token: 0x0200000B RID: 11
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000027 RID: 39 RVA: 0x00002758 File Offset: 0x00000958
			internal Enumerator(LinkedList<T> parent)
			{
				this.list = parent;
				this.current = null;
				this.index = -1;
				this.version = parent.version;
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000028 RID: 40 RVA: 0x0000277C File Offset: 0x0000097C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000029 RID: 41 RVA: 0x0000278C File Offset: 0x0000098C
			void IEnumerator.Reset()
			{
				if (this.list == null)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("list modified");
				}
				this.current = null;
				this.index = -1;
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600002A RID: 42 RVA: 0x000027DC File Offset: 0x000009DC
			public T Current
			{
				get
				{
					if (this.list == null)
					{
						throw new ObjectDisposedException(null);
					}
					if (this.current == null)
					{
						throw new InvalidOperationException();
					}
					return this.current.Value;
				}
			}

			// Token: 0x0600002B RID: 43 RVA: 0x0000280C File Offset: 0x00000A0C
			public bool MoveNext()
			{
				if (this.list == null)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("list modified");
				}
				if (this.current == null)
				{
					this.current = this.list.first;
				}
				else
				{
					this.current = this.current.forward;
					if (this.current == this.list.first)
					{
						this.current = null;
					}
				}
				if (this.current == null)
				{
					this.index = -1;
					return false;
				}
				this.index++;
				return true;
			}

			// Token: 0x0600002C RID: 44 RVA: 0x000028C0 File Offset: 0x00000AC0
			public void Dispose()
			{
				if (this.list == null)
				{
					throw new ObjectDisposedException(null);
				}
				this.current = null;
				this.list = null;
			}

			// Token: 0x04000019 RID: 25
			private const string VersionKey = "version";

			// Token: 0x0400001A RID: 26
			private const string IndexKey = "index";

			// Token: 0x0400001B RID: 27
			private const string ListKey = "list";

			// Token: 0x0400001C RID: 28
			private LinkedList<T> list;

			// Token: 0x0400001D RID: 29
			private LinkedListNode<T> current;

			// Token: 0x0400001E RID: 30
			private int index;

			// Token: 0x0400001F RID: 31
			private uint version;
		}
	}
}
