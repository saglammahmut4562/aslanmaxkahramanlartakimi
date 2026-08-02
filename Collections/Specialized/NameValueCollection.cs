using System;
using System.Runtime.Serialization;
using System.Text;

namespace System.Collections.Specialized
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class NameValueCollection : NameObjectCollectionBase
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000040F0 File Offset: 0x000022F0
		public NameValueCollection()
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000040F8 File Offset: 0x000022F8
		protected NameValueCollection(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000039 RID: 57
		public string this[string name]
		{
			get
			{
				return this.Get(name);
			}
			set
			{
				this.Set(name, value);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000411C File Offset: 0x0000231C
		public virtual void Add(string name, string val)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only");
			}
			this.InvalidateCachedArrays();
			ArrayList arrayList = (ArrayList)base.BaseGet(name);
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				if (val != null)
				{
					arrayList.Add(val);
				}
				base.BaseAdd(name, arrayList);
			}
			else if (val != null)
			{
				arrayList.Add(val);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004188 File Offset: 0x00002388
		public virtual string Get(int index)
		{
			ArrayList arrayList = (ArrayList)base.BaseGet(index);
			return NameValueCollection.AsSingleString(arrayList);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000041A8 File Offset: 0x000023A8
		public virtual string Get(string name)
		{
			ArrayList arrayList = (ArrayList)base.BaseGet(name);
			return NameValueCollection.AsSingleString(arrayList);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000041C8 File Offset: 0x000023C8
		private static string AsSingleString(ArrayList values)
		{
			if (values == null)
			{
				return null;
			}
			int count = values.Count;
			switch (count)
			{
			case 0:
				return null;
			case 1:
				return (string)values[0];
			case 2:
				return (string)values[0] + ',' + (string)values[1];
			default:
			{
				int num = count;
				for (int i = 0; i < count; i++)
				{
					num += ((string)values[i]).Length;
				}
				StringBuilder stringBuilder = new StringBuilder((string)values[0], num);
				for (int j = 1; j < count; j++)
				{
					stringBuilder.Append(',');
					stringBuilder.Append(values[j]);
				}
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000042A8 File Offset: 0x000024A8
		public virtual string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000042B4 File Offset: 0x000024B4
		public virtual string[] GetValues(string name)
		{
			ArrayList arrayList = (ArrayList)base.BaseGet(name);
			return NameValueCollection.AsStringArray(arrayList);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000042D4 File Offset: 0x000024D4
		private static string[] AsStringArray(ArrayList values)
		{
			if (values == null)
			{
				return null;
			}
			int count = values.Count;
			if (count == 0)
			{
				return null;
			}
			string[] array = new string[count];
			values.CopyTo(array);
			return array;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004308 File Offset: 0x00002508
		public virtual void Remove(string name)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only");
			}
			this.InvalidateCachedArrays();
			base.BaseRemove(name);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004330 File Offset: 0x00002530
		public virtual void Set(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only");
			}
			this.InvalidateCachedArrays();
			ArrayList arrayList = new ArrayList();
			if (value != null)
			{
				arrayList.Add(value);
				base.BaseSet(name, arrayList);
			}
			else
			{
				base.BaseSet(name, null);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004384 File Offset: 0x00002584
		protected void InvalidateCachedArrays()
		{
			this.cachedAllKeys = null;
			this.cachedAll = null;
		}

		// Token: 0x04000059 RID: 89
		private string[] cachedAllKeys;

		// Token: 0x0400005A RID: 90
		private string[] cachedAll;
	}
}
