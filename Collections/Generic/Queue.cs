using System;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	// Token: 0x0200000D RID: 13
	[ComVisible(false)]
	[Serializable]
	public class Queue<T> : IEnumerable<T>, ICollection, IEnumerable
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000029D4 File Offset: 0x00000BD4
		public Queue()
		{
			this._array = new T[0];
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000029E8 File Offset: 0x00000BE8
		void ICollection.CopyTo(Array array, int idx)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (idx > array.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (array.Length - idx < this._size)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this._size == 0)
			{
				return;
			}
			try
			{
				int num = this._array.Length;
				int num2 = num - this._head;
				Array.Copy(this._array, this._head, array, idx, Math.Min(this._size, num2));
				if (this._size > num2)
				{
					Array.Copy(this._array, 0, array, idx + num2, this._size - num2);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002AB0 File Offset: 0x00000CB0
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002AB4 File Offset: 0x00000CB4
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002AB8 File Offset: 0x00000CB8
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002AC8 File Offset: 0x00000CC8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public void CopyTo(T[] array, int idx)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			((ICollection)this).CopyTo(array, idx);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public T Dequeue()
		{
			T t = this.Peek();
			this._array[this._head] = default(T);
			if (++this._head == this._array.Length)
			{
				this._head = 0;
			}
			this._size--;
			this._version++;
			return t;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B60 File Offset: 0x00000D60
		public T Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException();
			}
			return this._array[this._head];
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B84 File Offset: 0x00000D84
		public void Enqueue(T item)
		{
			if (this._size == this._array.Length || this._tail == this._array.Length)
			{
				this.SetCapacity(Math.Max(Math.Max(this._size, this._tail) * 2, 4));
			}
			this._array[this._tail] = item;
			if (++this._tail == this._array.Length)
			{
				this._tail = 0;
			}
			this._size++;
			this._version++;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C2C File Offset: 0x00000E2C
		private void SetCapacity(int new_size)
		{
			if (new_size == this._array.Length)
			{
				return;
			}
			if (new_size < this._size)
			{
				throw new InvalidOperationException("shouldnt happen");
			}
			T[] array = new T[new_size];
			if (this._size > 0)
			{
				this.CopyTo(array, 0);
			}
			this._array = array;
			this._tail = this._size;
			this._head = 0;
			this._version++;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002CAC File Offset: 0x00000EAC
		public Queue<T>.Enumerator GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x04000024 RID: 36
		private T[] _array;

		// Token: 0x04000025 RID: 37
		private int _head;

		// Token: 0x04000026 RID: 38
		private int _tail;

		// Token: 0x04000027 RID: 39
		private int _size;

		// Token: 0x04000028 RID: 40
		private int _version;

		// Token: 0x0200000E RID: 14
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000040 RID: 64 RVA: 0x00002CB4 File Offset: 0x00000EB4
			internal Enumerator(Queue<T> q)
			{
				this.q = q;
				this.idx = -2;
				this.ver = q._version;
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00002CD4 File Offset: 0x00000ED4
			void IEnumerator.Reset()
			{
				if (this.ver != this.q._version)
				{
					throw new InvalidOperationException();
				}
				this.idx = -2;
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000042 RID: 66 RVA: 0x00002CFC File Offset: 0x00000EFC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000043 RID: 67 RVA: 0x00002D0C File Offset: 0x00000F0C
			public void Dispose()
			{
				this.idx = -2;
			}

			// Token: 0x06000044 RID: 68 RVA: 0x00002D18 File Offset: 0x00000F18
			public bool MoveNext()
			{
				if (this.ver != this.q._version)
				{
					throw new InvalidOperationException();
				}
				if (this.idx == -2)
				{
					this.idx = this.q._size;
				}
				return this.idx != -1 && --this.idx != -1;
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000045 RID: 69 RVA: 0x00002D88 File Offset: 0x00000F88
			public T Current
			{
				get
				{
					if (this.idx < 0)
					{
						throw new InvalidOperationException();
					}
					return this.q._array[(this.q._size - 1 - this.idx + this.q._head) % this.q._array.Length];
				}
			}

			// Token: 0x04000029 RID: 41
			private const int NOT_STARTED = -2;

			// Token: 0x0400002A RID: 42
			private const int FINISHED = -1;

			// Token: 0x0400002B RID: 43
			private Queue<T> q;

			// Token: 0x0400002C RID: 44
			private int idx;

			// Token: 0x0400002D RID: 45
			private int ver;
		}
	}
}
