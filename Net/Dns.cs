using System;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace System.Net
{
	// Token: 0x02000076 RID: 118
	public static class Dns
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x0000A728 File Offset: 0x00008928
		static Dns()
		{
			global::System.Net.Sockets.Socket.CheckProtocolSupport();
		}

		// Token: 0x060002B8 RID: 696
		[MethodImpl(4096)]
		private static extern bool GetHostByName_internal(string host, out string h_name, out string[] h_aliases, out string[] h_addr_list);

		// Token: 0x060002B9 RID: 697 RVA: 0x0000A730 File Offset: 0x00008930
		private static IPHostEntry hostent_to_IPHostEntry(string h_name, string[] h_aliases, string[] h_addrlist)
		{
			IPHostEntry iphostEntry = new IPHostEntry();
			ArrayList arrayList = new ArrayList();
			iphostEntry.HostName = h_name;
			iphostEntry.Aliases = h_aliases;
			for (int i = 0; i < h_addrlist.Length; i++)
			{
				try
				{
					IPAddress ipaddress = IPAddress.Parse(h_addrlist[i]);
					if ((global::System.Net.Sockets.Socket.SupportsIPv6 && ipaddress.AddressFamily == global::System.Net.Sockets.AddressFamily.InterNetworkV6) || (global::System.Net.Sockets.Socket.SupportsIPv4 && ipaddress.AddressFamily == global::System.Net.Sockets.AddressFamily.InterNetwork))
					{
						arrayList.Add(ipaddress);
					}
				}
				catch (ArgumentNullException)
				{
				}
			}
			if (arrayList.Count == 0)
			{
				throw new global::System.Net.Sockets.SocketException(11001);
			}
			iphostEntry.AddressList = arrayList.ToArray(typeof(IPAddress)) as IPAddress[];
			return iphostEntry;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000A7F8 File Offset: 0x000089F8
		[Obsolete("Use GetHostEntry instead")]
		public static IPHostEntry GetHostByName(string hostName)
		{
			if (hostName == null)
			{
				throw new ArgumentNullException("hostName");
			}
			string text;
			string[] array;
			string[] array2;
			if (!Dns.GetHostByName_internal(hostName, out text, out array, out array2))
			{
				throw new global::System.Net.Sockets.SocketException(11001);
			}
			return Dns.hostent_to_IPHostEntry(text, array, array2);
		}
	}
}
