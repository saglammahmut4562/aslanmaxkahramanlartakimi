using System;
using System.Runtime.Serialization;

namespace System.Net
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	public class WebException : InvalidOperationException, ISerializable
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x000150B4 File Offset: 0x000132B4
		public WebException()
		{
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000150C4 File Offset: 0x000132C4
		public WebException(string message)
			: base(message)
		{
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000150D8 File Offset: 0x000132D8
		protected WebException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000150EC File Offset: 0x000132EC
		public WebException(string message, WebExceptionStatus status)
			: base(message)
		{
			this.status = status;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00015104 File Offset: 0x00013304
		internal WebException(string message, Exception innerException, WebExceptionStatus status)
			: base(message, innerException)
		{
			this.status = status;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00015120 File Offset: 0x00013320
		public WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response)
			: base(message, innerException)
		{
			this.status = status;
			this.response = response;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00015144 File Offset: 0x00013344
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00015150 File Offset: 0x00013350
		public WebExceptionStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00015158 File Offset: 0x00013358
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x04000311 RID: 785
		private WebResponse response;

		// Token: 0x04000312 RID: 786
		private WebExceptionStatus status = WebExceptionStatus.UnknownError;
	}
}
