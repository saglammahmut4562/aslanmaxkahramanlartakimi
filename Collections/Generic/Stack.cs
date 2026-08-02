using System;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	// Token: 0x0200000F RID: 15
	[ComVisible(false)]
	[Serializable]
	public class Stack<T> : IEnumerable<T>, ICollection, IEnumerable
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002DF0 File Offset: 0x00000FF0
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002DF4 File Offset: 0x00000FF4
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002DF8 File Offset: 0x00000FF8
		void ICollection.CopyTo(Array dest, int idx)
		{
			try
			{
				if (this._array != null)
				{
					this._array.CopyTo(dest, idx);
					Array.Reverse(dest, idx, this._size);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E4C File Offset: 0x0000104C
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E5C File Offset: 0x0000105C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E6C File Offset: 0x0000106C
		public bool Contains(T t)
		{
			return this._array != null && Array.IndexOf<T>(this._array, t, 0, this._size) != -1;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E98 File Offset: 0x00001098
		public T Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException();
			}
			return this._array[this._size - 1];
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002EC0 File Offset: 0x000010C0
		public T Pop()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException();
			}
			this._version++;
			T t = this._array[--this._size];
			this._array[this._size] = default(T);
			return t;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F24 File Offset: 0x00001124
		public void Push(T t)
		{
			if (this._array == null || this._size == this._array.Length)
			{
				Array.Resize<T>(ref this._array, (this._size != 0) ? (2 * this._size) : 16);
			}
			this._version++;
			this._array[this._size++] = t;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002FA0 File Offset: 0x000011A0
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002FA8 File Offset: 0x000011A8
		public Stack<T>.Enumerator GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x0400002E RID: 46
		private const int INITIAL_SIZE = 16;

		// Token: 0x0400002F RID: 47
		private T[] _array;

		// Token: 0x04000030 RID: 48
		private int _size;

		// Token: 0x04000031 RID: 49
		private int _version;

		// Token: 0x02000010 RID: 16
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000052 RID: 82 RVA: 0x00002FB0 File Offset: 0x000011B0
			internal Enumerator(Stack<T> t)
			{
				this.parent = t;
				this.idx = -2;
				this._version = t._version;
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00002FD0 File Offset: 0x000011D0
			void IEnumerator.Reset()
			{
				if (this._version != this.parent._version)
				{
					throw new InvalidOperationException();
				}
				this.idx = -2;
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000054 RID: 84 RVA: 0x00002FF8 File Offset: 0x000011F8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000055 RID: 85 RVA: 0x00003008 File Offset: 0x00001208
			public void Dispose()
			{
				this.idx = -2;
			}

			// Token: 0x06000056 RID: 86 RVA: 0x00003014 File Offset: 0x00001214
			public bool MoveNext()
			{
				if (this._version != this.parent._version)
				{
					throw new InvalidOperationException();
				}
				if (this.idx == -2)
				{
					this.idx = this.parent._size;
				}
				return this.idx != -1 && --this.idx != -1;
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000057 RID: 87 RVA: 0x00003084 File Offset: 0x00001284
			public T Current
			{
				get
				{
					if (this.idx < 0)
					{
						throw new InvalidOperationException();
					}
					return this.parent._array[this.idx];
				}
			}

			// Token: 0x04000032 RID: 50
			private const int NOT_STARTED = -2;

			// Token: 0x04000033 RID: 51
			private const int FINISHED = -1;

			// Token: 0x04000034 RID: 52
			private Stack<T> parent;

			// Token: 0x04000035 RID: 53
			private int idx;

			// Token: 0x04000036 RID: 54
			private int _version;
		}
	}
}
