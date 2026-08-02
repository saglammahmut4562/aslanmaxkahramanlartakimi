using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x02000034 RID: 52
	[DefaultMember("Item")]
	[ComVisible(true)]
	public class EventDescriptorCollection : ICollection, IEnumerable, IList
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00005850 File Offset: 0x00003A50
		public EventDescriptorCollection(EventDescriptor[] events, bool readOnly)
		{
			this.isReadOnly = readOnly;
			if (events == null)
			{
				return;
			}
			for (int i = 0; i < events.Length; i++)
			{
				this.Add(events[i]);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000058AC File Offset: 0x00003AAC
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000058B4 File Offset: 0x00003AB4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000058BC File Offset: 0x00003ABC
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000058C8 File Offset: 0x00003AC8
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000058D0 File Offset: 0x00003AD0
		int IList.Add(object value)
		{
			return this.Add((EventDescriptor)value);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000058E0 File Offset: 0x00003AE0
		bool IList.Contains(object value)
		{
			return this.Contains((EventDescriptor)value);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000058F0 File Offset: 0x00003AF0
		int IList.IndexOf(object value)
		{
			return this.IndexOf((EventDescriptor)value);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005900 File Offset: 0x00003B00
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (EventDescriptor)value);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005910 File Offset: 0x00003B10
		void IList.Remove(object value)
		{
			this.Remove((EventDescriptor)value);
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005920 File Offset: 0x00003B20
		bool IList.IsFixedSize
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005928 File Offset: 0x00003B28
		bool IList.IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x1700004C RID: 76
		object IList.this[int index]
		{
			get
			{
				return this.eventList[index];
			}
			set
			{
				if (this.isReadOnly)
				{
					throw new NotSupportedException("The collection is read-only");
				}
				this.eventList[index] = value;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005968 File Offset: 0x00003B68
		void ICollection.CopyTo(Array array, int index)
		{
			this.eventList.CopyTo(array, index);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005978 File Offset: 0x00003B78
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000597C File Offset: 0x00003B7C
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005980 File Offset: 0x00003B80
		public int Add(EventDescriptor value)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException("The collection is read-only");
			}
			return this.eventList.Add(value);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000059A4 File Offset: 0x00003BA4
		public void Clear()
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException("The collection is read-only");
			}
			this.eventList.Clear();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000059C8 File Offset: 0x00003BC8
		public bool Contains(EventDescriptor value)
		{
			return this.eventList.Contains(value);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000059D8 File Offset: 0x00003BD8
		public IEnumerator GetEnumerator()
		{
			return this.eventList.GetEnumerator();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000059E8 File Offset: 0x00003BE8
		public int IndexOf(EventDescriptor value)
		{
			return this.eventList.IndexOf(value);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000059F8 File Offset: 0x00003BF8
		public void Insert(int index, EventDescriptor value)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException("The collection is read-only");
			}
			this.eventList.Insert(index, value);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005A20 File Offset: 0x00003C20
		public void Remove(EventDescriptor value)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException("The collection is read-only");
			}
			this.eventList.Remove(value);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005A44 File Offset: 0x00003C44
		public void RemoveAt(int index)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException("The collection is read-only");
			}
			this.eventList.RemoveAt(index);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005A68 File Offset: 0x00003C68
		public int Count
		{
			get
			{
				return this.eventList.Count;
			}
		}

		// Token: 0x0400006D RID: 109
		private ArrayList eventList = new ArrayList();

		// Token: 0x0400006E RID: 110
		private bool isReadOnly;

		// Token: 0x0400006F RID: 111
		public static readonly EventDescriptorCollection Empty = new EventDescriptorCollection(null, true);
	}
}
