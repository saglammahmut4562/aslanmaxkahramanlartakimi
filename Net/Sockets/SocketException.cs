using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	public class SocketException : global::System.ComponentModel.Win32Exception
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x000112A4 File Offset: 0x0000F4A4
		public SocketException()
			: base(SocketException.WSAGetLastError_internal())
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000112B4 File Offset: 0x0000F4B4
		public SocketException(int error)
			: base(error)
		{
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000112C0 File Offset: 0x0000F4C0
		protected SocketException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000112CC File Offset: 0x0000F4CC
		internal SocketException(int error, string message)
			: base(error, message)
		{
		}

		// Token: 0x060003F0 RID: 1008
		[MethodImpl(4096)]
		private static extern int WSAGetLastError_internal();

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public SocketError SocketErrorCode
		{
			get
			{
				return (SocketError)base.NativeErrorCode;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000112E0 File Offset: 0x0000F4E0
		public override string Message
		{
			get
			{
				return base.Message;
			}
		}
	}
}
