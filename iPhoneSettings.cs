using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200008B RID: 139
	public sealed class iPhoneSettings
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000623 RID: 1571
		[Obsolete("internetReachability property is deprecated. Please use Application.internetReachability instead.")]
		public static extern iPhoneNetworkReachability internetReachability
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}
	}
}
