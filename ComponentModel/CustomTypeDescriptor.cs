using System;

namespace System.ComponentModel
{
	// Token: 0x02000026 RID: 38
	public abstract class CustomTypeDescriptor : ICustomTypeDescriptor
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00004B74 File Offset: 0x00002D74
		public virtual AttributeCollection GetAttributes()
		{
			if (this._parent != null)
			{
				return this._parent.GetAttributes();
			}
			return AttributeCollection.Empty;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004B94 File Offset: 0x00002D94
		public virtual string GetClassName()
		{
			if (this._parent != null)
			{
				return this._parent.GetClassName();
			}
			return null;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004BB0 File Offset: 0x00002DB0
		public virtual string GetComponentName()
		{
			if (this._parent != null)
			{
				return this._parent.GetComponentName();
			}
			return null;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004BCC File Offset: 0x00002DCC
		public virtual TypeConverter GetConverter()
		{
			if (this._parent != null)
			{
				return this._parent.GetConverter();
			}
			return new TypeConverter();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004BEC File Offset: 0x00002DEC
		public virtual EventDescriptor GetDefaultEvent()
		{
			if (this._parent != null)
			{
				return this._parent.GetDefaultEvent();
			}
			return null;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004C08 File Offset: 0x00002E08
		public virtual PropertyDescriptor GetDefaultProperty()
		{
			if (this._parent != null)
			{
				return this._parent.GetDefaultProperty();
			}
			return null;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004C24 File Offset: 0x00002E24
		public virtual object GetEditor(Type editorBaseType)
		{
			if (this._parent != null)
			{
				return this._parent.GetEditor(editorBaseType);
			}
			return null;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004C40 File Offset: 0x00002E40
		public virtual EventDescriptorCollection GetEvents()
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents();
			}
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004C60 File Offset: 0x00002E60
		public virtual EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents(attributes);
			}
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004C80 File Offset: 0x00002E80
		public virtual PropertyDescriptorCollection GetProperties()
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties();
			}
			return PropertyDescriptorCollection.Empty;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties(attributes);
			}
			return PropertyDescriptorCollection.Empty;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004CC0 File Offset: 0x00002EC0
		public virtual object GetPropertyOwner(PropertyDescriptor pd)
		{
			if (this._parent != null)
			{
				return this._parent.GetPropertyOwner(pd);
			}
			return null;
		}

		// Token: 0x0400005F RID: 95
		private ICustomTypeDescriptor _parent;
	}
}
