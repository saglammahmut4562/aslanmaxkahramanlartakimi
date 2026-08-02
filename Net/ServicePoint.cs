using System;
using System.Collections;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x0200008E RID: 142
	public class ServicePoint
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000E38C File Offset: 0x0000C58C
		internal ServicePoint(global::System.Uri uri, int connectionLimit, int maxIdleTime)
		{
			this.uri = uri;
			this.connectionLimit = connectionLimit;
			this.maxIdleTime = maxIdleTime;
			this.currentConnections = 0;
			this.idleSince = DateTime.Now;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000E3E4 File Offset: 0x0000C5E4
		public global::System.Uri Address
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000E3EC File Offset: 0x0000C5EC
		public int ConnectionLimit
		{
			get
			{
				return this.connectionLimit;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		public int CurrentConnections
		{
			get
			{
				return this.currentConnections;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000E404 File Offset: 0x0000C604
		public DateTime IdleSince
		{
			get
			{
				return this.idleSince;
			}
			internal set
			{
				object obj = this.locker;
				lock (obj)
				{
					this.idleSince = value;
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000E444 File Offset: 0x0000C644
		public virtual Version ProtocolVersion
		{
			get
			{
				return this.protocolVersion;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000E44C File Offset: 0x0000C64C
		public bool Expect100Continue
		{
			set
			{
				this.SendContinue = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000E458 File Offset: 0x0000C658
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000E460 File Offset: 0x0000C660
		public bool UseNagleAlgorithm
		{
			get
			{
				return this.useNagle;
			}
			set
			{
				this.useNagle = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000E46C File Offset: 0x0000C66C
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		internal bool SendContinue
		{
			get
			{
				return this.sendContinue && (this.protocolVersion == null || this.protocolVersion == HttpVersion.Version11);
			}
			set
			{
				this.sendContinue = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000E4AC File Offset: 0x0000C6AC
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		internal bool UsesProxy
		{
			get
			{
				return this.usesProxy;
			}
			set
			{
				this.usesProxy = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		internal bool UseConnect
		{
			get
			{
				return this.useConnect;
			}
			set
			{
				this.useConnect = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		internal bool AvailableForRecycling
		{
			get
			{
				return this.CurrentConnections == 0 && this.maxIdleTime != -1 && DateTime.Now >= this.IdleSince.AddMilliseconds((double)this.maxIdleTime);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000E51C File Offset: 0x0000C71C
		internal Hashtable Groups
		{
			get
			{
				if (this.groups == null)
				{
					this.groups = new Hashtable();
				}
				return this.groups;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000E53C File Offset: 0x0000C73C
		internal IPHostEntry HostEntry
		{
			get
			{
				object obj = this.hostE;
				lock (obj)
				{
					if (this.host != null)
					{
						return this.host;
					}
					string text = this.uri.Host;
					if (this.uri.HostNameType == global::System.UriHostNameType.IPv6 || this.uri.HostNameType == global::System.UriHostNameType.IPv4)
					{
						if (this.uri.HostNameType == global::System.UriHostNameType.IPv6)
						{
							text = text.Substring(1, text.Length - 2);
						}
						this.host = new IPHostEntry();
						this.host.AddressList = new IPAddress[] { IPAddress.Parse(text) };
						return this.host;
					}
					try
					{
						this.host = Dns.GetHostByName(text);
					}
					catch
					{
						return null;
					}
				}
				return this.host;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000E63C File Offset: 0x0000C83C
		internal void SetVersion(Version version)
		{
			this.protocolVersion = version;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000E648 File Offset: 0x0000C848
		private WebConnectionGroup GetConnectionGroup(string name)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			WebConnectionGroup webConnectionGroup = this.Groups[name] as WebConnectionGroup;
			if (webConnectionGroup != null)
			{
				return webConnectionGroup;
			}
			webConnectionGroup = new WebConnectionGroup(this, name);
			this.Groups[name] = webConnectionGroup;
			return webConnectionGroup;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000E694 File Offset: 0x0000C894
		internal EventHandler SendRequest(HttpWebRequest request, string groupName)
		{
			object obj = this.locker;
			WebConnection connection;
			lock (obj)
			{
				WebConnectionGroup connectionGroup = this.GetConnectionGroup(groupName);
				connection = connectionGroup.GetConnection(request);
			}
			return connection.SendRequest(request);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		internal void SetCertificates(X509Certificate client, X509Certificate server)
		{
			this.certificate = server;
			this.clientCertificate = client;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		internal bool CallEndPointDelegate(global::System.Net.Sockets.Socket sock, IPEndPoint remote)
		{
			if (this.endPointCallback == null)
			{
				return true;
			}
			int num = 0;
			checked
			{
				for (;;)
				{
					IPEndPoint ipendPoint = null;
					try
					{
						ipendPoint = this.endPointCallback(this, remote, num);
					}
					catch
					{
						return false;
					}
					if (ipendPoint == null)
					{
						break;
					}
					try
					{
						sock.Bind(ipendPoint);
					}
					catch (global::System.Net.Sockets.SocketException)
					{
						num++;
						continue;
					}
					return true;
				}
				return true;
			}
		}

		// Token: 0x04000191 RID: 401
		private global::System.Uri uri;

		// Token: 0x04000192 RID: 402
		private int connectionLimit;

		// Token: 0x04000193 RID: 403
		private int maxIdleTime;

		// Token: 0x04000194 RID: 404
		private int currentConnections;

		// Token: 0x04000195 RID: 405
		private DateTime idleSince;

		// Token: 0x04000196 RID: 406
		private Version protocolVersion;

		// Token: 0x04000197 RID: 407
		private X509Certificate certificate;

		// Token: 0x04000198 RID: 408
		private X509Certificate clientCertificate;

		// Token: 0x04000199 RID: 409
		private IPHostEntry host;

		// Token: 0x0400019A RID: 410
		private bool usesProxy;

		// Token: 0x0400019B RID: 411
		private Hashtable groups;

		// Token: 0x0400019C RID: 412
		private bool sendContinue = true;

		// Token: 0x0400019D RID: 413
		private bool useConnect;

		// Token: 0x0400019E RID: 414
		private object locker = new object();

		// Token: 0x0400019F RID: 415
		private object hostE = new object();

		// Token: 0x040001A0 RID: 416
		private bool useNagle;

		// Token: 0x040001A1 RID: 417
		private BindIPEndPoint endPointCallback;
	}
}
