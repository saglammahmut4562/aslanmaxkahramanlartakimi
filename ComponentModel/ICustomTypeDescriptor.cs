using System;

namespace System.ComponentModel
{
	// Token: 0x02000039 RID: 57
	public interface ICustomTypeDescriptor
	{
		// Token: 0x06000138 RID: 312
		AttributeCollection GetAttributes();

		// Token: 0x06000139 RID: 313
		string GetClassName();

		// Token: 0x0600013A RID: 314
		string GetComponentName();

		// Token: 0x0600013B RID: 315
		TypeConverter GetConverter();

		// Token: 0x0600013C RID: 316
		EventDescriptor GetDefaultEvent();

		// Token: 0x0600013D RID: 317
		PropertyDescriptor GetDefaultProperty();

		// Token: 0x0600013E RID: 318
		object GetEditor(Type editorBaseType);

		// Token: 0x0600013F RID: 319
		EventDescriptorCollection GetEvents();

		// Token: 0x06000140 RID: 320
		EventDescriptorCollection GetEvents(Attribute[] arr);

		// Token: 0x06000141 RID: 321
		PropertyDescriptorCollection GetProperties();

		// Token: 0x06000142 RID: 322
		PropertyDescriptorCollection GetProperties(Attribute[] arr);

		// Token: 0x06000143 RID: 323
		object GetPropertyOwner(PropertyDescriptor pd);
	}
}
