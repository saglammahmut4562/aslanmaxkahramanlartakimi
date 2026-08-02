using System;
using System.Collections;
using System.ComponentModel.Design;

namespace System.ComponentModel
{
	// Token: 0x0200003A RID: 58
	internal abstract class Info
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00005BD8 File Offset: 0x00003DD8
		public Info(Type infoType)
		{
			this._infoType = infoType;
		}

		// Token: 0x06000145 RID: 325
		public abstract AttributeCollection GetAttributes();

		// Token: 0x06000146 RID: 326 RVA: 0x00005BE8 File Offset: 0x00003DE8
		protected AttributeCollection GetAttributes(IComponent comp)
		{
			if (this._attributes != null)
			{
				return this._attributes;
			}
			bool flag = true;
			ArrayList arrayList = new ArrayList();
			foreach (Attribute attribute in this._infoType.GetCustomAttributes(false))
			{
				arrayList.Add(attribute);
			}
			Type type = this._infoType.BaseType;
			while (type != null && type != typeof(object))
			{
				foreach (Attribute attribute2 in type.GetCustomAttributes(false))
				{
					arrayList.Add(attribute2);
				}
				type = type.BaseType;
			}
			foreach (Type type2 in this._infoType.GetInterfaces())
			{
				foreach (object obj in TypeDescriptor.GetAttributes(type2))
				{
					Attribute attribute3 = (Attribute)obj;
					arrayList.Add(attribute3);
				}
			}
			Hashtable hashtable = new Hashtable();
			for (int l = arrayList.Count - 1; l >= 0; l--)
			{
				Attribute attribute4 = (Attribute)arrayList[l];
				hashtable[attribute4.TypeId] = attribute4;
			}
			if (comp != null && comp.Site != null)
			{
				global::System.ComponentModel.Design.ITypeDescriptorFilterService typeDescriptorFilterService = (global::System.ComponentModel.Design.ITypeDescriptorFilterService)comp.Site.GetService(typeof(global::System.ComponentModel.Design.ITypeDescriptorFilterService));
				if (typeDescriptorFilterService != null)
				{
					flag = typeDescriptorFilterService.FilterAttributes(comp, hashtable);
				}
			}
			Attribute[] array = new Attribute[hashtable.Values.Count];
			hashtable.Values.CopyTo(array, 0);
			AttributeCollection attributeCollection = new AttributeCollection(array);
			if (flag)
			{
				this._attributes = attributeCollection;
			}
			return attributeCollection;
		}

		// Token: 0x04000070 RID: 112
		private Type _infoType;

		// Token: 0x04000071 RID: 113
		private EventDescriptor _defaultEvent;

		// Token: 0x04000072 RID: 114
		private bool _gotDefaultEvent;

		// Token: 0x04000073 RID: 115
		private PropertyDescriptor _defaultProperty;

		// Token: 0x04000074 RID: 116
		private bool _gotDefaultProperty;

		// Token: 0x04000075 RID: 117
		private AttributeCollection _attributes;
	}
}
