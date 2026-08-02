using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;

namespace System.Net
{
	// Token: 0x0200007C RID: 124
	[Serializable]
	public class HttpWebResponse : WebResponse, IDisposable, ISerializable
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x0000C53C File Offset: 0x0000A73C
		internal HttpWebResponse(global::System.Uri uri, string method, WebConnectionData data, CookieContainer container)
		{
			this.uri = uri;
			this.method = method;
			this.webHeaders = data.Headers;
			this.version = data.Version;
			this.statusCode = (HttpStatusCode)data.StatusCode;
			this.statusDescription = data.StatusDescription;
			this.stream = data.stream;
			this.contentLength = -1L;
			try
			{
				string text = this.webHeaders["Content-Length"];
				if (string.IsNullOrEmpty(text) || !long.TryParse(text, out this.contentLength))
				{
					this.contentLength = -1L;
				}
			}
			catch (Exception)
			{
				this.contentLength = -1L;
			}
			if (container != null)
			{
				this.cookie_container = container;
				this.FillCookies();
			}
			string text2 = this.webHeaders["Content-Encoding"];
			if (text2 == "gzip" && (data.request.AutomaticDecompression & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				this.stream = new global::System.IO.Compression.GZipStream(this.stream, global::System.IO.Compression.CompressionMode.Decompress);
			}
			else if (text2 == "deflate" && (data.request.AutomaticDecompression & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				this.stream = new global::System.IO.Compression.DeflateStream(this.stream, global::System.IO.Compression.CompressionMode.Decompress);
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		[Obsolete("Serialization is obsoleted for this type", false)]
		protected HttpWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.uri = (global::System.Uri)serializationInfo.GetValue("uri", typeof(global::System.Uri));
			this.contentLength = serializationInfo.GetInt64("contentLength");
			this.contentType = serializationInfo.GetString("contentType");
			this.method = serializationInfo.GetString("method");
			this.statusDescription = serializationInfo.GetString("statusDescription");
			this.cookieCollection = (CookieCollection)serializationInfo.GetValue("cookieCollection", typeof(CookieCollection));
			this.version = (Version)serializationInfo.GetValue("version", typeof(Version));
			this.statusCode = (HttpStatusCode)((int)serializationInfo.GetValue("statusCode", typeof(HttpStatusCode)));
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000C7BC File Offset: 0x0000A9BC
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000C7CC File Offset: 0x0000A9CC
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
		public HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		public string StatusDescription
		{
			get
			{
				this.CheckDisposed();
				return this.statusDescription;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		internal void ReadAll()
		{
			WebConnectionStream webConnectionStream = this.stream as WebConnectionStream;
			if (webConnectionStream == null)
			{
				return;
			}
			try
			{
				webConnectionStream.ReadAll();
			}
			catch
			{
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000C830 File Offset: 0x0000AA30
		public override Stream GetResponseStream()
		{
			this.CheckDisposed();
			if (this.stream == null)
			{
				return Stream.Null;
			}
			if (string.Compare(this.method, "HEAD", true) == 0)
			{
				return Stream.Null;
			}
			return this.stream;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000C86C File Offset: 0x0000AA6C
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("uri", this.uri);
			serializationInfo.AddValue("contentLength", this.contentLength);
			serializationInfo.AddValue("contentType", this.contentType);
			serializationInfo.AddValue("method", this.method);
			serializationInfo.AddValue("statusDescription", this.statusDescription);
			serializationInfo.AddValue("cookieCollection", this.cookieCollection);
			serializationInfo.AddValue("version", this.version);
			serializationInfo.AddValue("statusCode", this.statusCode);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C908 File Offset: 0x0000AB08
		public override void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000C910 File Offset: 0x0000AB10
		private void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (disposing)
			{
				this.uri = null;
				this.cookieCollection = null;
				this.method = null;
				this.version = null;
				this.statusDescription = null;
			}
			Stream stream = this.stream;
			this.stream = null;
			if (stream != null)
			{
				stream.Close();
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000C974 File Offset: 0x0000AB74
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000C994 File Offset: 0x0000AB94
		private void FillCookies()
		{
			if (this.webHeaders == null)
			{
				return;
			}
			string[] array = this.webHeaders.GetValues("Set-Cookie");
			if (array != null)
			{
				foreach (string text in array)
				{
					this.SetCookie(text);
				}
			}
			array = this.webHeaders.GetValues("Set-Cookie2");
			if (array != null)
			{
				foreach (string text2 in array)
				{
					this.SetCookie2(text2);
				}
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000CA28 File Offset: 0x0000AC28
		private void SetCookie(string header)
		{
			Cookie cookie = null;
			CookieParser cookieParser = new CookieParser(header);
			string text;
			string text2;
			while (cookieParser.GetNextNameValue(out text, out text2))
			{
				if ((text != null && !(text == string.Empty)) || cookie != null)
				{
					if (cookie == null)
					{
						cookie = new Cookie(text, text2);
					}
					else
					{
						text = text.ToUpper();
						string text3 = text;
						switch (text3)
						{
						case "COMMENT":
							if (cookie.Comment == null)
							{
								cookie.Comment = text2;
							}
							break;
						case "COMMENTURL":
							if (cookie.CommentUri == null)
							{
								cookie.CommentUri = new global::System.Uri(text2);
							}
							break;
						case "DISCARD":
							cookie.Discard = true;
							break;
						case "DOMAIN":
							if (cookie.Domain == string.Empty)
							{
								cookie.Domain = text2;
							}
							break;
						case "HTTPONLY":
							cookie.HttpOnly = true;
							break;
						case "MAX-AGE":
							if (cookie.Expires == DateTime.MinValue)
							{
								try
								{
									cookie.Expires = cookie.TimeStamp.AddSeconds(uint.Parse(text2));
								}
								catch
								{
								}
							}
							break;
						case "EXPIRES":
							if (!(cookie.Expires != DateTime.MinValue))
							{
								cookie.Expires = this.TryParseCookieExpires(text2);
							}
							break;
						case "PATH":
							cookie.Path = text2;
							break;
						case "PORT":
							if (cookie.Port == null)
							{
								cookie.Port = text2;
							}
							break;
						case "SECURE":
							cookie.Secure = true;
							break;
						case "VERSION":
							try
							{
								cookie.Version = (int)uint.Parse(text2);
							}
							catch
							{
							}
							break;
						}
					}
				}
			}
			if (cookie == null)
			{
				return;
			}
			if (this.cookieCollection == null)
			{
				this.cookieCollection = new CookieCollection();
			}
			if (cookie.Domain == string.Empty)
			{
				cookie.Domain = this.uri.Host;
			}
			this.cookieCollection.Add(cookie);
			if (this.cookie_container != null)
			{
				this.cookie_container.Add(this.uri, cookie);
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000CD40 File Offset: 0x0000AF40
		private void SetCookie2(string cookies_str)
		{
			string[] array = cookies_str.Split(new char[] { ',' });
			foreach (string text in array)
			{
				this.SetCookie(text);
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000CD80 File Offset: 0x0000AF80
		private DateTime TryParseCookieExpires(string value)
		{
			if (value == null || value.Length == 0)
			{
				return DateTime.MinValue;
			}
			for (int i = 0; i < this.cookieExpiresFormats.Length; i++)
			{
				try
				{
					DateTime dateTime = DateTime.ParseExact(value, this.cookieExpiresFormats[i], CultureInfo.InvariantCulture);
					dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
					return TimeZone.CurrentTimeZone.ToLocalTime(dateTime);
				}
				catch
				{
				}
			}
			return DateTime.MinValue;
		}

		// Token: 0x04000157 RID: 343
		private global::System.Uri uri;

		// Token: 0x04000158 RID: 344
		private WebHeaderCollection webHeaders;

		// Token: 0x04000159 RID: 345
		private CookieCollection cookieCollection;

		// Token: 0x0400015A RID: 346
		private string method;

		// Token: 0x0400015B RID: 347
		private Version version;

		// Token: 0x0400015C RID: 348
		private HttpStatusCode statusCode;

		// Token: 0x0400015D RID: 349
		private string statusDescription;

		// Token: 0x0400015E RID: 350
		private long contentLength;

		// Token: 0x0400015F RID: 351
		private string contentType;

		// Token: 0x04000160 RID: 352
		private CookieContainer cookie_container;

		// Token: 0x04000161 RID: 353
		private bool disposed;

		// Token: 0x04000162 RID: 354
		private Stream stream;

		// Token: 0x04000163 RID: 355
		private string[] cookieExpiresFormats = new string[] { "r", "ddd, dd'-'MMM'-'yyyy HH':'mm':'ss 'GMT'", "ddd, dd'-'MMM'-'yy HH':'mm':'ss 'GMT'" };
	}
}
