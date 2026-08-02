using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class HashSet<T> : IEnumerable<T>, ICollection<T>, IEnumerable, IDeserializationCallback, ISerializable
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002050 File Offset: 0x00000250
		public HashSet()
		{
			this.Init(10, null);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002064 File Offset: 0x00000264
		public HashSet(IEqualityComparer<T> comparer)
		{
			this.Init(10, comparer);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002078 File Offset: 0x00000278
		public HashSet(IEnumerable<T> collection)
			: this(collection, null)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002084 File Offset: 0x00000284
		public HashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			int num = 0;
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				num = collection2.Count;
			}
			this.Init(num, comparer);
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000210C File Offset: 0x0000030C
		protected HashSet(SerializationInfo info, StreamingContext context)
		{
			this.si = info;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000211C File Offset: 0x0000031C
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000212C File Offset: 0x0000032C
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002130 File Offset: 0x00000330
		void ICollection<T>.CopyTo(T[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000213C File Offset: 0x0000033C
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002148 File Offset: 0x00000348
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002158 File Offset: 0x00000358
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002160 File Offset: 0x00000360
		private void Init(int capacity, IEqualityComparer<T> comparer)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.comparer = comparer ?? EqualityComparer<T>.Default;
			if (capacity == 0)
			{
				capacity = 10;
			}
			capacity = (int)((float)capacity / 0.9f) + 1;
			this.InitArrays(capacity);
			this.generation = 0;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021B8 File Offset: 0x000003B8
		private void InitArrays(int size)
		{
			this.table = new int[size];
			this.links = new HashSet<T>.Link[size];
			this.empty_slot = -1;
			this.slots = new T[size];
			this.touched = 0;
			this.threshold = (int)((float)this.table.Length * 0.9f);
			if (this.threshold == 0 && this.table.Length > 0)
			{
				this.threshold = 1;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002230 File Offset: 0x00000430
		private bool SlotsContainsAt(int index, int hash, T item)
		{
			HashSet<T>.Link link;
			for (int num = this.table[index] - 1; num != -1; num = link.Next)
			{
				link = this.links[num];
				if (link.HashCode == hash && ((hash != -2147483648 || (item != null && this.slots[num] != null)) ? this.comparer.Equals(item, this.slots[num]) : (item == null && null == this.slots[num])))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022F0 File Offset: 0x000004F0
		public void CopyTo(T[] array, int index)
		{
			this.CopyTo(array, index, this.count);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002300 File Offset: 0x00000500
		public void CopyTo(T[] array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index > array.Length)
			{
				throw new ArgumentException("index larger than largest valid index of array");
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException("Destination array cannot hold the requested elements!");
			}
			int num = 0;
			int num2 = 0;
			while (num < this.touched && num2 < count)
			{
				if (this.GetLinkHashCode(num) != 0)
				{
					array[index++] = this.slots[num];
				}
				num++;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023A0 File Offset: 0x000005A0
		private void Resize()
		{
			int num = HashSet<T>.PrimeHelper.ToPrime((this.table.Length << 1) | 1);
			int[] array = new int[num];
			HashSet<T>.Link[] array2 = new HashSet<T>.Link[num];
			for (int i = 0; i < this.table.Length; i++)
			{
				for (int num2 = this.table[i] - 1; num2 != -1; num2 = this.links[num2].Next)
				{
					int num3 = (array2[num2].HashCode = this.GetItemHashCode(this.slots[num2]));
					int num4 = (num3 & int.MaxValue) % num;
					array2[num2].Next = array[num4] - 1;
					array[num4] = num2 + 1;
				}
			}
			this.table = array;
			this.links = array2;
			T[] array3 = new T[num];
			Array.Copy(this.slots, 0, array3, 0, this.touched);
			this.slots = array3;
			this.threshold = (int)((float)num * 0.9f);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024A4 File Offset: 0x000006A4
		private int GetLinkHashCode(int index)
		{
			return this.links[index].HashCode & int.MinValue;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024C0 File Offset: 0x000006C0
		private int GetItemHashCode(T item)
		{
			if (item == null)
			{
				return int.MinValue;
			}
			return this.comparer.GetHashCode(item) | int.MinValue;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024E8 File Offset: 0x000006E8
		public bool Add(T item)
		{
			int itemHashCode = this.GetItemHashCode(item);
			int num = (itemHashCode & int.MaxValue) % this.table.Length;
			if (this.SlotsContainsAt(num, itemHashCode, item))
			{
				return false;
			}
			if (++this.count > this.threshold)
			{
				this.Resize();
				num = (itemHashCode & int.MaxValue) % this.table.Length;
			}
			int num2 = this.empty_slot;
			if (num2 == -1)
			{
				num2 = this.touched++;
			}
			else
			{
				this.empty_slot = this.links[num2].Next;
			}
			this.links[num2].HashCode = itemHashCode;
			this.links[num2].Next = this.table[num] - 1;
			this.table[num] = num2 + 1;
			this.slots[num2] = item;
			this.generation++;
			return true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025E4 File Offset: 0x000007E4
		public void Clear()
		{
			this.count = 0;
			Array.Clear(this.table, 0, this.table.Length);
			Array.Clear(this.slots, 0, this.slots.Length);
			Array.Clear(this.links, 0, this.links.Length);
			this.empty_slot = -1;
			this.touched = 0;
			this.generation++;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002650 File Offset: 0x00000850
		public bool Contains(T item)
		{
			int itemHashCode = this.GetItemHashCode(item);
			int num = (itemHashCode & int.MaxValue) % this.table.Length;
			return this.SlotsContainsAt(num, itemHashCode, item);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002680 File Offset: 0x00000880
		public bool Remove(T item)
		{
			int itemHashCode = this.GetItemHashCode(item);
			int num = (itemHashCode & int.MaxValue) % this.table.Length;
			int num2 = this.table[num] - 1;
			if (num2 == -1)
			{
				return false;
			}
			int num3 = -1;
			do
			{
				HashSet<T>.Link link = this.links[num2];
				if (link.HashCode == itemHashCode && ((itemHashCode != -2147483648 || (item != null && this.slots[num2] != null)) ? this.comparer.Equals(this.slots[num2], item) : (item == null && null == this.slots[num2])))
				{
					break;
				}
				num3 = num2;
				num2 = link.Next;
			}
			while (num2 != -1);
			if (num2 == -1)
			{
				return false;
			}
			this.count--;
			if (num3 == -1)
			{
				this.table[num] = this.links[num2].Next + 1;
			}
			else
			{
				this.links[num3].Next = this.links[num2].Next;
			}
			this.links[num2].Next = this.empty_slot;
			this.empty_slot = num2;
			this.links[num2].HashCode = 0;
			this.slots[num2] = default(T);
			this.generation++;
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002818 File Offset: 0x00000A18
		[MonoTODO]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002820 File Offset: 0x00000A20
		[MonoTODO]
		public virtual void OnDeserialization(object sender)
		{
			if (this.si == null)
			{
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002834 File Offset: 0x00000A34
		public HashSet<T>.Enumerator GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x0400000D RID: 13
		private const int INITIAL_SIZE = 10;

		// Token: 0x0400000E RID: 14
		private const float DEFAULT_LOAD_FACTOR = 0.9f;

		// Token: 0x0400000F RID: 15
		private const int NO_SLOT = -1;

		// Token: 0x04000010 RID: 16
		private const int HASH_FLAG = -2147483648;

		// Token: 0x04000011 RID: 17
		private int[] table;

		// Token: 0x04000012 RID: 18
		private HashSet<T>.Link[] links;

		// Token: 0x04000013 RID: 19
		private T[] slots;

		// Token: 0x04000014 RID: 20
		private int touched;

		// Token: 0x04000015 RID: 21
		private int empty_slot;

		// Token: 0x04000016 RID: 22
		private int count;

		// Token: 0x04000017 RID: 23
		private int threshold;

		// Token: 0x04000018 RID: 24
		private IEqualityComparer<T> comparer;

		// Token: 0x04000019 RID: 25
		private SerializationInfo si;

		// Token: 0x0400001A RID: 26
		private int generation;

		// Token: 0x0200000B RID: 11
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000027 RID: 39 RVA: 0x0000283C File Offset: 0x00000A3C
			internal Enumerator(HashSet<T> hashset)
			{
				this.hashset = hashset;
				this.stamp = hashset.generation;
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000028 RID: 40 RVA: 0x00002854 File Offset: 0x00000A54
			object IEnumerator.Current
			{
				get
				{
					this.CheckState();
					if (this.next <= 0)
					{
						throw new InvalidOperationException("Current is not valid");
					}
					return this.current;
				}
			}

			// Token: 0x06000029 RID: 41 RVA: 0x00002880 File Offset: 0x00000A80
			void IEnumerator.Reset()
			{
				this.CheckState();
				this.next = 0;
			}

			// Token: 0x0600002A RID: 42 RVA: 0x00002890 File Offset: 0x00000A90
			public bool MoveNext()
			{
				this.CheckState();
				if (this.next < 0)
				{
					return false;
				}
				while (this.next < this.hashset.touched)
				{
					int num = this.next++;
					if (this.hashset.GetLinkHashCode(num) != 0)
					{
						this.current = this.hashset.slots[num];
						return true;
					}
				}
				this.next = -1;
				return false;
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002910 File Offset: 0x00000B10
			public T Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00002918 File Offset: 0x00000B18
			public void Dispose()
			{
				this.hashset = null;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00002924 File Offset: 0x00000B24
			private void CheckState()
			{
				if (this.hashset == null)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.hashset.generation != this.stamp)
				{
					throw new InvalidOperationException("HashSet have been modified while it was iterated over");
				}
			}

			// Token: 0x0400001B RID: 27
			private HashSet<T> hashset;

			// Token: 0x0400001C RID: 28
			private int next;

			// Token: 0x0400001D RID: 29
			private int stamp;

			// Token: 0x0400001E RID: 30
			private T current;
		}

		// Token: 0x0200000C RID: 12
		private struct Link
		{
			// Token: 0x0400001F RID: 31
			public int HashCode;

			// Token: 0x04000020 RID: 32
			public int Next;
		}

		// Token: 0x0200000D RID: 13
		private static class PrimeHelper
		{
			// Token: 0x0600002F RID: 47 RVA: 0x00002978 File Offset: 0x00000B78
			private static bool TestPrime(int x)
			{
				if ((x & 1) != 0)
				{
					int num = (int)Math.Sqrt((double)x);
					for (int i = 3; i < num; i += 2)
					{
						if (x % i == 0)
						{
							return false;
						}
					}
					return true;
				}
				return x == 2;
			}

			// Token: 0x06000030 RID: 48 RVA: 0x000029B8 File Offset: 0x00000BB8
			private static int CalcPrime(int x)
			{
				for (int i = (x & -2) - 1; i < 2147483647; i += 2)
				{
					if (HashSet<T>.PrimeHelper.TestPrime(i))
					{
						return i;
					}
				}
				return x;
			}

			// Token: 0x06000031 RID: 49 RVA: 0x000029F0 File Offset: 0x00000BF0
			public static int ToPrime(int x)
			{
				for (int i = 0; i < HashSet<T>.PrimeHelper.primes_table.Length; i++)
				{
					if (x <= HashSet<T>.PrimeHelper.primes_table[i])
					{
						return HashSet<T>.PrimeHelper.primes_table[i];
					}
				}
				return HashSet<T>.PrimeHelper.CalcPrime(x);
			}

			// Token: 0x04000021 RID: 33
			private static readonly int[] primes_table = new int[]
			{
				11, 19, 37, 73, 109, 163, 251, 367, 557, 823,
				1237, 1861, 2777, 4177, 6247, 9371, 14057, 21089, 31627, 47431,
				71143, 106721, 160073, 240101, 360163, 540217, 810343, 1215497, 1823231, 2734867,
				4102283, 6153409, 9230113, 13845163
			};
		}
	}
}
