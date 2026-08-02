using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x0200001D RID: 29
	[ComVisible(true)]
	public class AttributeCollection : ICollection, IEnumerable
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x000043F0 File Offset: 0x000025F0
		internal AttributeCollection(ArrayList attributes)
		{
			if (attributes != null)
			{
				this.attrList = attributes;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004410 File Offset: 0x00002610
		public AttributeCollection(params Attribute[] attributes)
		{
			if (attributes != null)
			{
				for (int i = 0; i < attributes.Length; i++)
				{
					this.attrList.Add(attributes[i]);
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004468 File Offset: 0x00002668
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004470 File Offset: 0x00002670
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.attrList.IsSynchronized;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004480 File Offset: 0x00002680
		object ICollection.SyncRoot
		{
			get
			{
				return this.attrList.SyncRoot;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004490 File Offset: 0x00002690
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004498 File Offset: 0x00002698
		public void CopyTo(Array array, int index)
		{
			this.attrList.CopyTo(array, index);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000044A8 File Offset: 0x000026A8
		public IEnumerator GetEnumerator()
		{
			return this.attrList.GetEnumerator();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000044B8 File Offset: 0x000026B8
		protected Attribute GetDefaultAttribute(Type attributeType)
		{
			Attribute attribute = null;
			BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public;
			FieldInfo field = attributeType.GetField("Default", bindingFlags);
			if (field == null)
			{
				ConstructorInfo constructor = attributeType.GetConstructor(Type.EmptyTypes);
				if (constructor != null)
				{
					attribute = constructor.Invoke(null) as Attribute;
				}
				if (attribute != null && !attribute.IsDefaultAttribute())
				{
					attribute = null;
				}
			}
			else
			{
				attribute = (Attribute)field.GetValue(null);
			}
			return attribute;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004524 File Offset: 0x00002724
		public int Count
		{
			get
			{
				return (this.attrList == null) ? 0 : this.attrList.Count;
			}
		}

		// Token: 0x1700003E RID: 62
		public virtual Attribute this[Type type]
		{
			get
			{
				Attribute attribute = null;
				if (this.attrList != null)
				{
					foreach (object obj in this.attrList)
					{
						Attribute attribute2 = (Attribute)obj;
						if (type.IsAssignableFrom(attribute2.GetType()))
						{
							attribute = attribute2;
							break;
						}
					}
				}
				if (attribute == null)
				{
					attribute = this.GetDefaultAttribute(type);
				}
				return attribute;
			}
		}

		// Token: 0x0400005B RID: 91
		private ArrayList attrList = new ArrayList();

		// Token: 0x0400005C RID: 92
		public static readonly AttributeCollection Empty = new AttributeCollection(null);
	}
}
