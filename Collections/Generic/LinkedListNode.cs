using System;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	// Token: 0x0200000C RID: 12
	[ComVisible(false)]
	public sealed class LinkedListNode<T>
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000028E4 File Offset: 0x00000AE4
		internal LinkedListNode(LinkedList<T> list, T value)
		{
			this.container = list;
			this.item = value;
			this.forward = this;
			this.back = this;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002918 File Offset: 0x00000B18
		internal LinkedListNode(LinkedList<T> list, T value, LinkedListNode<T> previousNode, LinkedListNode<T> nextNode)
		{
			this.container = list;
			this.item = value;
			this.back = previousNode;
			this.forward = nextNode;
			previousNode.forward = this;
			nextNode.back = this;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000294C File Offset: 0x00000B4C
		internal void Detach()
		{
			this.back.forward = this.forward;
			this.forward.back = this.back;
			this.forward = (this.back = null);
			this.container = null;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002994 File Offset: 0x00000B94
		public LinkedList<T> List
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000299C File Offset: 0x00000B9C
		public LinkedListNode<T> Next
		{
			get
			{
				return (this.container == null || this.forward == this.container.first) ? null : this.forward;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000029CC File Offset: 0x00000BCC
		public T Value
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04000020 RID: 32
		private T item;

		// Token: 0x04000021 RID: 33
		private LinkedList<T> container;

		// Token: 0x04000022 RID: 34
		internal LinkedListNode<T> forward;

		// Token: 0x04000023 RID: 35
		internal LinkedListNode<T> back;
	}
}
