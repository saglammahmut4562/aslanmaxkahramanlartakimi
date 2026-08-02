using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x020000A9 RID: 169
	internal class WebConnectionGroup
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x00013878 File Offset: 0x00011A78
		public WebConnectionGroup(ServicePoint sPoint, string name)
		{
			this.sPoint = sPoint;
			this.name = name;
			this.connections = new ArrayList(1);
			this.queue = new Queue();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000138A8 File Offset: 0x00011AA8
		public WebConnection GetConnection(HttpWebRequest request)
		{
			WebConnection webConnection = null;
			ArrayList arrayList = this.connections;
			lock (arrayList)
			{
				int count = this.connections.Count;
				ArrayList arrayList2 = null;
				for (int i = 0; i < count; i++)
				{
					WeakReference weakReference = (WeakReference)this.connections[i];
					webConnection = weakReference.Target as WebConnection;
					if (webConnection == null)
					{
						if (arrayList2 == null)
						{
							arrayList2 = new ArrayList(1);
						}
						arrayList2.Add(i);
					}
				}
				if (arrayList2 != null)
				{
					for (int j = arrayList2.Count - 1; j >= 0; j--)
					{
						this.connections.RemoveAt((int)arrayList2[j]);
					}
				}
				webConnection = this.CreateOrReuseConnection(request);
			}
			return webConnection;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00013994 File Offset: 0x00011B94
		private static void PrepareSharingNtlm(WebConnection cnc, HttpWebRequest request)
		{
			if (!cnc.NtlmAuthenticated)
			{
				return;
			}
			bool flag = false;
			NetworkCredential ntlmCredential = cnc.NtlmCredential;
			NetworkCredential credential = request.Credentials.GetCredential(request.RequestUri, "NTLM");
			if (ntlmCredential.Domain != credential.Domain || ntlmCredential.UserName != credential.UserName || ntlmCredential.Password != credential.Password)
			{
				flag = true;
			}
			if (!flag)
			{
				bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
				bool unsafeAuthenticatedConnectionSharing2 = cnc.UnsafeAuthenticatedConnectionSharing;
				flag = !unsafeAuthenticatedConnectionSharing || unsafeAuthenticatedConnectionSharing != unsafeAuthenticatedConnectionSharing2;
			}
			if (flag)
			{
				cnc.Close(false);
				cnc.ResetNtlm();
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013A4C File Offset: 0x00011C4C
		private WebConnection CreateOrReuseConnection(HttpWebRequest request)
		{
			int num = this.connections.Count;
			WebConnection webConnection;
			for (int i = 0; i < num; i++)
			{
				WeakReference weakReference = this.connections[i] as WeakReference;
				webConnection = weakReference.Target as WebConnection;
				if (webConnection == null)
				{
					this.connections.RemoveAt(i);
					num--;
					i--;
				}
				else if (!webConnection.Busy)
				{
					WebConnectionGroup.PrepareSharingNtlm(webConnection, request);
					return webConnection;
				}
			}
			if (this.sPoint.ConnectionLimit > num)
			{
				webConnection = new WebConnection(this, this.sPoint);
				this.connections.Add(new WeakReference(webConnection));
				return webConnection;
			}
			if (this.rnd == null)
			{
				this.rnd = new Random();
			}
			int num2 = ((num <= 1) ? 0 : this.rnd.Next(0, num - 1));
			WeakReference weakReference2 = (WeakReference)this.connections[num2];
			webConnection = weakReference2.Target as WebConnection;
			if (webConnection == null)
			{
				webConnection = new WebConnection(this, this.sPoint);
				this.connections.RemoveAt(num2);
				this.connections.Add(new WeakReference(webConnection));
			}
			return webConnection;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00013B88 File Offset: 0x00011D88
		internal Queue Queue
		{
			get
			{
				return this.queue;
			}
		}

		// Token: 0x040002F1 RID: 753
		private ServicePoint sPoint;

		// Token: 0x040002F2 RID: 754
		private string name;

		// Token: 0x040002F3 RID: 755
		private ArrayList connections;

		// Token: 0x040002F4 RID: 756
		private Random rnd;

		// Token: 0x040002F5 RID: 757
		private Queue queue;
	}
}
