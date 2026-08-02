using System;
using System.Runtime.Serialization;

namespace System.Net
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	public class CookieException : FormatException, ISerializable
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0000989C File Offset: 0x00007A9C
		public CookieException()
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000098A4 File Offset: 0x00007AA4
		internal CookieException(string msg)
			: base(msg)
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000098B0 File Offset: 0x00007AB0
		internal CookieException(string msg, Exception e)
			: base(msg, e)
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000098BC File Offset: 0x00007ABC
		protected CookieException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000098C8 File Offset: 0x00007AC8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000098D4 File Offset: 0x00007AD4
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
