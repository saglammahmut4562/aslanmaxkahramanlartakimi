using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000073 RID: 115
	internal class DigestClient : IAuthenticationModule
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00009BC0 File Offset: 0x00007DC0
		private static Hashtable Cache
		{
			get
			{
				object syncRoot = DigestClient.cache.SyncRoot;
				lock (syncRoot)
				{
					DigestClient.CheckExpired(DigestClient.cache.Count);
				}
				return DigestClient.cache;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009C10 File Offset: 0x00007E10
		private static void CheckExpired(int count)
		{
			if (count < 10)
			{
				return;
			}
			DateTime dateTime = DateTime.MaxValue;
			DateTime now = DateTime.Now;
			ArrayList arrayList = null;
			foreach (object obj in DigestClient.cache.Keys)
			{
				int num = (int)obj;
				DigestSession digestSession = (DigestSession)DigestClient.cache[num];
				if (digestSession.LastUse < dateTime && (digestSession.LastUse - now).Ticks > 6000000000L)
				{
					dateTime = digestSession.LastUse;
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(num);
				}
			}
			if (arrayList != null)
			{
				foreach (object obj2 in arrayList)
				{
					int num2 = (int)obj2;
					DigestClient.cache.Remove(num2);
				}
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009D64 File Offset: 0x00007F64
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			string text = challenge.Trim();
			if (text.ToLower().IndexOf("digest") == -1)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			int num = httpWebRequest.Address.GetHashCode() ^ credentials.GetHashCode();
			DigestSession digestSession = (DigestSession)DigestClient.Cache[num];
			bool flag = digestSession == null;
			if (flag)
			{
				digestSession = new DigestSession();
			}
			if (!digestSession.Parse(challenge))
			{
				return null;
			}
			if (flag)
			{
				DigestClient.Cache.Add(num, digestSession);
			}
			return digestSession.Authenticate(webRequest, credentials);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009E18 File Offset: 0x00008018
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			if (credentials == null)
			{
				return null;
			}
			int num = httpWebRequest.Address.GetHashCode() ^ credentials.GetHashCode();
			DigestSession digestSession = (DigestSession)DigestClient.Cache[num];
			if (digestSession == null)
			{
				return null;
			}
			return digestSession.Authenticate(webRequest, credentials);
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00009E78 File Offset: 0x00008078
		public string AuthenticationType
		{
			get
			{
				return "Digest";
			}
		}

		// Token: 0x040000E6 RID: 230
		private static readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());
	}
}
