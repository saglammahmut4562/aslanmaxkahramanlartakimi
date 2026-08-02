using System;
using System.Globalization;
using System.Text;

namespace System.Net
{
	// Token: 0x0200006E RID: 110
	[Serializable]
	public sealed class CookieContainer
	{
		// Token: 0x0600027F RID: 639 RVA: 0x000091A4 File Offset: 0x000073A4
		private void AddCookie(Cookie cookie)
		{
			if (this.cookies == null)
			{
				this.cookies = new CookieCollection();
			}
			if (this.cookies.Count >= this.capacity)
			{
				this.RemoveOldest(null);
			}
			if (this.cookies.Count >= this.perDomainCapacity && this.CountDomain(cookie.Domain) >= this.perDomainCapacity)
			{
				this.RemoveOldest(cookie.Domain);
			}
			Cookie cookie2 = new Cookie(cookie.Name, cookie.Value);
			cookie2.Path = ((cookie.Path.Length != 0) ? cookie.Path : "/");
			cookie2.Domain = cookie.Domain;
			cookie2.ExactDomain = cookie.ExactDomain;
			cookie2.Version = cookie.Version;
			cookie2.Expires = cookie.Expires;
			cookie2.CommentUri = cookie.CommentUri;
			cookie2.Comment = cookie.Comment;
			cookie2.Discard = cookie.Discard;
			cookie2.HttpOnly = cookie.HttpOnly;
			cookie2.Secure = cookie.Secure;
			this.cookies.Add(cookie2);
			this.CheckExpiration();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000092D4 File Offset: 0x000074D4
		private int CountDomain(string domain)
		{
			int num = 0;
			foreach (object obj in this.cookies)
			{
				Cookie cookie = (Cookie)obj;
				if (CookieContainer.CheckDomain(domain, cookie.Domain, true))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000934C File Offset: 0x0000754C
		private void RemoveOldest(string domain)
		{
			int num = 0;
			DateTime dateTime = DateTime.MaxValue;
			for (int i = 0; i < this.cookies.Count; i++)
			{
				Cookie cookie = this.cookies[i];
				if (cookie.TimeStamp < dateTime && (domain == null || domain == cookie.Domain))
				{
					dateTime = cookie.TimeStamp;
					num = i;
				}
			}
			this.cookies.List.RemoveAt(num);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000093CC File Offset: 0x000075CC
		private void CheckExpiration()
		{
			if (this.cookies == null)
			{
				return;
			}
			for (int i = this.cookies.Count - 1; i >= 0; i--)
			{
				Cookie cookie = this.cookies[i];
				if (cookie.Expired)
				{
					this.cookies.List.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000942C File Offset: 0x0000762C
		private void Cook(global::System.Uri uri, Cookie cookie)
		{
			if (CookieContainer.IsNullOrEmpty(cookie.Name))
			{
				throw new CookieException("Invalid cookie: name");
			}
			if (cookie.Value == null)
			{
				throw new CookieException("Invalid cookie: value");
			}
			if (uri != null && cookie.Domain.Length == 0)
			{
				cookie.Domain = uri.Host;
			}
			if (cookie.Version == 0 && CookieContainer.IsNullOrEmpty(cookie.Path))
			{
				if (uri != null)
				{
					cookie.Path = uri.AbsolutePath;
				}
				else
				{
					cookie.Path = "/";
				}
			}
			if (cookie.Port.Length == 0 && uri != null && !uri.IsDefaultPort)
			{
				cookie.Port = "\"" + uri.Port.ToString() + "\"";
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009520 File Offset: 0x00007720
		public void Add(global::System.Uri uri, Cookie cookie)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			if (!cookie.Expired)
			{
				this.Cook(uri, cookie);
				this.AddCookie(cookie);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009570 File Offset: 0x00007770
		public string GetCookieHeader(global::System.Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			CookieCollection cookieCollection = this.GetCookies(uri);
			if (cookieCollection.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in cookieCollection)
			{
				Cookie cookie = (Cookie)obj;
				stringBuilder.Append(cookie.ToString(uri));
				stringBuilder.Append("; ");
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length -= 2;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000963C File Offset: 0x0000783C
		private static bool CheckDomain(string domain, string host, bool exact)
		{
			if (domain.Length == 0)
			{
				return false;
			}
			if (exact)
			{
				return string.Compare(host, domain, true, CultureInfo.InvariantCulture) == 0;
			}
			if (!CultureInfo.InvariantCulture.CompareInfo.IsSuffix(host, domain, CompareOptions.IgnoreCase))
			{
				return false;
			}
			if (domain[0] == '.')
			{
				return true;
			}
			int num = host.Length - domain.Length - 1;
			return num >= 0 && host[num] == '.';
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000096BC File Offset: 0x000078BC
		public CookieCollection GetCookies(global::System.Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.CheckExpiration();
			CookieCollection cookieCollection = new CookieCollection();
			if (this.cookies == null)
			{
				return cookieCollection;
			}
			foreach (object obj in this.cookies)
			{
				Cookie cookie = (Cookie)obj;
				string domain = cookie.Domain;
				if (CookieContainer.CheckDomain(domain, uri.Host, cookie.ExactDomain))
				{
					if (cookie.Port.Length <= 0 || cookie.Ports == null || uri.Port == -1 || Array.IndexOf<int>(cookie.Ports, uri.Port) != -1)
					{
						string path = cookie.Path;
						string absolutePath = uri.AbsolutePath;
						if (path != string.Empty && path != "/" && absolutePath != path)
						{
							if (!absolutePath.StartsWith(path))
							{
								continue;
							}
							if (path[path.Length - 1] != '/' && absolutePath.Length > path.Length && absolutePath[path.Length] != '/')
							{
								continue;
							}
						}
						if (!cookie.Secure || !(uri.Scheme != "https"))
						{
							cookieCollection.Add(cookie);
						}
					}
				}
			}
			cookieCollection.Sort();
			return cookieCollection;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009888 File Offset: 0x00007A88
		private static bool IsNullOrEmpty(string s)
		{
			return s == null || s.Length == 0;
		}

		// Token: 0x040000D7 RID: 215
		public const int DefaultCookieLengthLimit = 4096;

		// Token: 0x040000D8 RID: 216
		public const int DefaultCookieLimit = 300;

		// Token: 0x040000D9 RID: 217
		public const int DefaultPerDomainCookieLimit = 20;

		// Token: 0x040000DA RID: 218
		private int capacity = 300;

		// Token: 0x040000DB RID: 219
		private int perDomainCapacity = 20;

		// Token: 0x040000DC RID: 220
		private int maxCookieSize = 4096;

		// Token: 0x040000DD RID: 221
		private CookieCollection cookies;
	}
}
