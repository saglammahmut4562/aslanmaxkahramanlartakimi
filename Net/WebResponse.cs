using System;
using System.IO;
using System.Runtime.Serialization;

namespace System.Net
{
	// Token: 0x020000AF RID: 175
	[Serializable]
	public abstract class WebResponse : MarshalByRefObject, IDisposable, ISerializable
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x00015D8C File Offset: 0x00013F8C
		protected WebResponse()
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00015D94 File Offset: 0x00013F94
		protected WebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00015DA4 File Offset: 0x00013FA4
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00015DAC File Offset: 0x00013FAC
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00015DB4 File Offset: 0x00013FB4
		public virtual WebHeaderCollection Headers
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00015DBC File Offset: 0x00013FBC
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00015DC4 File Offset: 0x00013FC4
		public virtual void Close()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00015DCC File Offset: 0x00013FCC
		public virtual Stream GetResponseStream()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00015DD4 File Offset: 0x00013FD4
		[global::System.MonoTODO]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw WebResponse.GetMustImplement();
		}
	}
}
