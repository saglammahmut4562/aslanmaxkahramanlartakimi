using System;
using System.Net.Sockets;

namespace System.Net
{
	// Token: 0x02000077 RID: 119
	[Serializable]
	public abstract class EndPoint
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000A844 File Offset: 0x00008A44
		public virtual global::System.Net.Sockets.AddressFamily AddressFamily
		{
			get
			{
				throw EndPoint.NotImplemented();
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A84C File Offset: 0x00008A4C
		public virtual EndPoint Create(SocketAddress address)
		{
			throw EndPoint.NotImplemented();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000A854 File Offset: 0x00008A54
		public virtual SocketAddress Serialize()
		{
			throw EndPoint.NotImplemented();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000A85C File Offset: 0x00008A5C
		private static Exception NotImplemented()
		{
			return new NotImplementedException();
		}
	}
}
