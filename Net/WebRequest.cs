using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Security;
using System.Runtime.Serialization;

namespace System.Net
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	public abstract class WebRequest : MarshalByRefObject, ISerializable
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x00015B64 File Offset: 0x00013D64
		protected WebRequest()
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00015B74 File Offset: 0x00013D74
		protected WebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00015B84 File Offset: 0x00013D84
		static WebRequest()
		{
			WebRequest.AddDynamicPrefix("http", "HttpRequestCreator");
			WebRequest.AddDynamicPrefix("https", "HttpRequestCreator");
			WebRequest.AddDynamicPrefix("file", "FileWebRequestCreator");
			WebRequest.AddDynamicPrefix("ftp", "FtpRequestCreator");
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00015BE4 File Offset: 0x00013DE4
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00015BEC File Offset: 0x00013DEC
		private static void AddDynamicPrefix(string protocol, string implementor)
		{
			Type type = typeof(WebRequest).Assembly.GetType("System.Net." + implementor);
			if (type == null)
			{
				return;
			}
			WebRequest.AddPrefix(protocol, type);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00015C28 File Offset: 0x00013E28
		private static Exception GetMustImplement()
		{
			return new NotImplementedException("This method must be implemented in derived classes");
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00015C34 File Offset: 0x00013E34
		public virtual long ContentLength
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00015C3C File Offset: 0x00013E3C
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x00015C44 File Offset: 0x00013E44
		public virtual ICredentials Credentials
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
			set
			{
				throw WebRequest.GetMustImplement();
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00015C4C File Offset: 0x00013E4C
		public virtual WebHeaderCollection Headers
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00015C54 File Offset: 0x00013E54
		public virtual string Method
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00015C5C File Offset: 0x00013E5C
		public virtual IWebProxy Proxy
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00015C64 File Offset: 0x00013E64
		public virtual global::System.Uri RequestUri
		{
			get
			{
				throw WebRequest.GetMustImplement();
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015C6C File Offset: 0x00013E6C
		public virtual void Abort()
		{
			throw WebRequest.GetMustImplement();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00015C74 File Offset: 0x00013E74
		public virtual IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			throw WebRequest.GetMustImplement();
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00015C7C File Offset: 0x00013E7C
		public static WebRequest Create(string requestUriString)
		{
			if (requestUriString == null)
			{
				throw new ArgumentNullException("requestUriString");
			}
			return WebRequest.Create(new global::System.Uri(requestUriString));
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00015C9C File Offset: 0x00013E9C
		public static WebRequest Create(global::System.Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			return WebRequest.GetCreator(requestUri.AbsoluteUri).Create(requestUri);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00015CC8 File Offset: 0x00013EC8
		public virtual WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			throw WebRequest.GetMustImplement();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00015CD0 File Offset: 0x00013ED0
		public virtual WebResponse GetResponse()
		{
			throw WebRequest.GetMustImplement();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00015CD8 File Offset: 0x00013ED8
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw WebRequest.GetMustImplement();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00015CE0 File Offset: 0x00013EE0
		private static IWebRequestCreate GetCreator(string prefix)
		{
			int num = -1;
			IWebRequestCreate webRequestCreate = null;
			prefix = prefix.ToLower(CultureInfo.InvariantCulture);
			IDictionaryEnumerator enumerator = WebRequest.prefixes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text = enumerator.Key as string;
				if (text.Length > num)
				{
					if (prefix.StartsWith(text))
					{
						num = text.Length;
						webRequestCreate = (IWebRequestCreate)enumerator.Value;
					}
				}
			}
			if (webRequestCreate == null)
			{
				throw new NotSupportedException(prefix);
			}
			return webRequestCreate;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00015D68 File Offset: 0x00013F68
		internal static void AddPrefix(string prefix, Type type)
		{
			object obj = Activator.CreateInstance(type, true);
			WebRequest.prefixes[prefix] = obj;
		}

		// Token: 0x0400032E RID: 814
		private static global::System.Collections.Specialized.HybridDictionary prefixes = new global::System.Collections.Specialized.HybridDictionary();

		// Token: 0x0400032F RID: 815
		private static bool isDefaultWebProxySet;

		// Token: 0x04000330 RID: 816
		private static IWebProxy defaultWebProxy;

		// Token: 0x04000331 RID: 817
		private global::System.Net.Security.AuthenticationLevel authentication_level = global::System.Net.Security.AuthenticationLevel.MutualAuthRequested;

		// Token: 0x04000332 RID: 818
		private static readonly object lockobj = new object();
	}
}
