using System;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200007B RID: 123
	[Serializable]
	public class HttpWebRequest : WebRequest, ISerializable
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000A880 File Offset: 0x00008A80
		[Obsolete("Serialization is obsoleted for this type", false)]
		protected HttpWebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.requestUri = (global::System.Uri)serializationInfo.GetValue("requestUri", typeof(global::System.Uri));
			this.actualUri = (global::System.Uri)serializationInfo.GetValue("actualUri", typeof(global::System.Uri));
			this.allowAutoRedirect = serializationInfo.GetBoolean("allowAutoRedirect");
			this.allowBuffering = serializationInfo.GetBoolean("allowBuffering");
			this.certificates = (global::System.Security.Cryptography.X509Certificates.X509CertificateCollection)serializationInfo.GetValue("certificates", typeof(global::System.Security.Cryptography.X509Certificates.X509CertificateCollection));
			this.connectionGroup = serializationInfo.GetString("connectionGroup");
			this.contentLength = serializationInfo.GetInt64("contentLength");
			this.webHeaders = (WebHeaderCollection)serializationInfo.GetValue("webHeaders", typeof(WebHeaderCollection));
			this.keepAlive = serializationInfo.GetBoolean("keepAlive");
			this.maxAutoRedirect = serializationInfo.GetInt32("maxAutoRedirect");
			this.mediaType = serializationInfo.GetString("mediaType");
			this.method = serializationInfo.GetString("method");
			this.initialMethod = serializationInfo.GetString("initialMethod");
			this.pipelined = serializationInfo.GetBoolean("pipelined");
			this.version = (Version)serializationInfo.GetValue("version", typeof(Version));
			this.proxy = (IWebProxy)serializationInfo.GetValue("proxy", typeof(IWebProxy));
			this.sendChunked = serializationInfo.GetBoolean("sendChunked");
			this.timeout = serializationInfo.GetInt32("timeout");
			this.redirects = serializationInfo.GetInt32("redirects");
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		public global::System.Uri Address
		{
			get
			{
				return this.actualUri;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		public DecompressionMethods AutomaticDecompression
		{
			get
			{
				return this.auto_decomp;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		internal bool InternalAllowBuffering
		{
			get
			{
				return this.allowBuffering && (this.method != "HEAD" && this.method != "GET" && this.method != "MKCOL" && this.method != "CONNECT" && this.method != "DELETE") && this.method != "TRACE";
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000AB78 File Offset: 0x00008D78
		public global::System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.certificates == null)
				{
					this.certificates = new global::System.Security.Cryptography.X509Certificates.X509CertificateCollection();
				}
				return this.certificates;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000AB98 File Offset: 0x00008D98
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		internal long InternalContentLength
		{
			set
			{
				this.contentLength = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000ABAC File Offset: 0x00008DAC
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		public override ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public bool KeepAlive
		{
			get
			{
				return this.keepAlive;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public int ReadWriteTimeout
		{
			get
			{
				return this.readWriteTimeout;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public override string Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		public override IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public override global::System.Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		public bool SendChunked
		{
			get
			{
				return this.sendChunked;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		public ServicePoint ServicePoint
		{
			get
			{
				return this.GetServicePoint();
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000AC00 File Offset: 0x00008E00
		public string TransferEncoding
		{
			get
			{
				return this.webHeaders["Transfer-Encoding"];
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000AC14 File Offset: 0x00008E14
		public bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_auth_blah;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000AC1C File Offset: 0x00008E1C
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000AC24 File Offset: 0x00008E24
		internal bool ExpectContinue
		{
			get
			{
				return this.expectContinue;
			}
			set
			{
				this.expectContinue = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000AC30 File Offset: 0x00008E30
		internal global::System.Uri AuthUri
		{
			get
			{
				return this.actualUri;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000AC38 File Offset: 0x00008E38
		internal bool ProxyQuery
		{
			get
			{
				return this.servicePoint.UsesProxy && !this.servicePoint.UseConnect;
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000AC5C File Offset: 0x00008E5C
		internal ServicePoint GetServicePoint()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (this.hostChanged || this.servicePoint == null)
				{
					this.servicePoint = ServicePointManager.FindServicePoint(this.actualUri, this.proxy);
					this.hostChanged = false;
				}
			}
			return this.servicePoint;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000ACCC File Offset: 0x00008ECC
		private void CheckIfForceWrite()
		{
			if (this.writeStream == null || this.writeStream.RequestWritten || this.contentLength < 0L || !this.InternalAllowBuffering)
			{
				return;
			}
			if ((long)this.writeStream.WriteBufferLength == this.contentLength)
			{
				this.writeStream.WriteRequest();
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000AD30 File Offset: 0x00008F30
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			if (this.Aborted)
			{
				throw new WebException("The request was canceled.", WebExceptionStatus.RequestCanceled);
			}
			if (this.method == null)
			{
				throw new ProtocolViolationException("Method is null.");
			}
			string transferEncoding = this.TransferEncoding;
			if (!this.sendChunked && transferEncoding != null && transferEncoding.Trim() != string.Empty)
			{
				throw new ProtocolViolationException("SendChunked should be true.");
			}
			Monitor.Enter(this.locker);
			this.getResponseCalled = true;
			if (this.asyncRead != null && !this.haveResponse)
			{
				Monitor.Exit(this.locker);
				throw new InvalidOperationException("Cannot re-call start of asynchronous method while a previous call is still in progress.");
			}
			this.CheckIfForceWrite();
			this.asyncRead = new WebAsyncResult(this, callback, state);
			WebAsyncResult webAsyncResult = this.asyncRead;
			this.initialMethod = this.method;
			if (this.haveResponse)
			{
				Exception ex = this.saved_exc;
				if (this.webResponse != null)
				{
					Monitor.Exit(this.locker);
					if (ex == null)
					{
						webAsyncResult.SetCompleted(true, this.webResponse);
					}
					else
					{
						webAsyncResult.SetCompleted(true, ex);
					}
					webAsyncResult.DoCallback();
					return webAsyncResult;
				}
				if (ex != null)
				{
					Monitor.Exit(this.locker);
					webAsyncResult.SetCompleted(true, ex);
					webAsyncResult.DoCallback();
					return webAsyncResult;
				}
			}
			if (!this.requestSent)
			{
				this.requestSent = true;
				this.redirects = 0;
				this.servicePoint = this.GetServicePoint();
				this.abortHandler = this.servicePoint.SendRequest(this, this.connectionGroup);
			}
			Monitor.Exit(this.locker);
			return webAsyncResult;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000AEC4 File Offset: 0x000090C4
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			WebAsyncResult webAsyncResult = asyncResult as WebAsyncResult;
			if (webAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			if (!webAsyncResult.WaitUntilComplete(this.timeout, false))
			{
				this.Abort();
				throw new WebException("The request timed out", WebExceptionStatus.Timeout);
			}
			if (webAsyncResult.GotException)
			{
				throw webAsyncResult.Exception;
			}
			return webAsyncResult.Response;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000AF3C File Offset: 0x0000913C
		public override WebResponse GetResponse()
		{
			WebAsyncResult webAsyncResult = (WebAsyncResult)this.BeginGetResponse(null, null);
			return this.EndGetResponse(webAsyncResult);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000AF60 File Offset: 0x00009160
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000AF68 File Offset: 0x00009168
		internal bool FinishedReading
		{
			get
			{
				return this.finished_reading;
			}
			set
			{
				this.finished_reading = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000AF74 File Offset: 0x00009174
		internal bool Aborted
		{
			get
			{
				return Interlocked.CompareExchange(ref this.aborted, 0, 0) == 1;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000AF88 File Offset: 0x00009188
		public override void Abort()
		{
			if (Interlocked.CompareExchange(ref this.aborted, 1, 0) == 1)
			{
				return;
			}
			if (this.haveResponse && this.finished_reading)
			{
				return;
			}
			this.haveResponse = true;
			if (this.abortHandler != null)
			{
				try
				{
					this.abortHandler(this, EventArgs.Empty);
				}
				catch (Exception)
				{
				}
				this.abortHandler = null;
			}
			if (this.asyncWrite != null)
			{
				WebAsyncResult webAsyncResult = this.asyncWrite;
				if (!webAsyncResult.IsCompleted)
				{
					try
					{
						WebException ex = new WebException("Aborted.", WebExceptionStatus.RequestCanceled);
						webAsyncResult.SetCompleted(false, ex);
						webAsyncResult.DoCallback();
					}
					catch
					{
					}
				}
				this.asyncWrite = null;
			}
			if (this.asyncRead != null)
			{
				WebAsyncResult webAsyncResult2 = this.asyncRead;
				if (!webAsyncResult2.IsCompleted)
				{
					try
					{
						WebException ex2 = new WebException("Aborted.", WebExceptionStatus.RequestCanceled);
						webAsyncResult2.SetCompleted(false, ex2);
						webAsyncResult2.DoCallback();
					}
					catch
					{
					}
				}
				this.asyncRead = null;
			}
			if (this.writeStream != null)
			{
				try
				{
					this.writeStream.Close();
					this.writeStream = null;
				}
				catch
				{
				}
			}
			if (this.webResponse != null)
			{
				try
				{
					this.webResponse.Close();
					this.webResponse = null;
				}
				catch
				{
				}
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B118 File Offset: 0x00009318
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("requestUri", this.requestUri, typeof(global::System.Uri));
			serializationInfo.AddValue("actualUri", this.actualUri, typeof(global::System.Uri));
			serializationInfo.AddValue("allowAutoRedirect", this.allowAutoRedirect);
			serializationInfo.AddValue("allowBuffering", this.allowBuffering);
			serializationInfo.AddValue("certificates", this.certificates, typeof(global::System.Security.Cryptography.X509Certificates.X509CertificateCollection));
			serializationInfo.AddValue("connectionGroup", this.connectionGroup);
			serializationInfo.AddValue("contentLength", this.contentLength);
			serializationInfo.AddValue("webHeaders", this.webHeaders, typeof(WebHeaderCollection));
			serializationInfo.AddValue("keepAlive", this.keepAlive);
			serializationInfo.AddValue("maxAutoRedirect", this.maxAutoRedirect);
			serializationInfo.AddValue("mediaType", this.mediaType);
			serializationInfo.AddValue("method", this.method);
			serializationInfo.AddValue("initialMethod", this.initialMethod);
			serializationInfo.AddValue("pipelined", this.pipelined);
			serializationInfo.AddValue("version", this.version, typeof(Version));
			serializationInfo.AddValue("proxy", this.proxy, typeof(IWebProxy));
			serializationInfo.AddValue("sendChunked", this.sendChunked);
			serializationInfo.AddValue("timeout", this.timeout);
			serializationInfo.AddValue("redirects", this.redirects);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000B2A8 File Offset: 0x000094A8
		internal void DoContinueDelegate(int statusCode, WebHeaderCollection headers)
		{
			if (this.continueDelegate != null)
			{
				this.continueDelegate(statusCode, headers);
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B2C4 File Offset: 0x000094C4
		private bool Redirect(WebAsyncResult result, HttpStatusCode code)
		{
			this.redirects++;
			Exception ex = null;
			string text = null;
			switch (code)
			{
			case HttpStatusCode.MultipleChoices:
				ex = new WebException("Ambiguous redirect.");
				goto IL_00E4;
			case HttpStatusCode.MovedPermanently:
			case HttpStatusCode.Found:
			case HttpStatusCode.TemporaryRedirect:
				this.contentLength = -1L;
				this.bodyBufferLength = 0;
				this.bodyBuffer = null;
				this.method = "GET";
				text = this.webResponse.Headers["Location"];
				goto IL_00E4;
			case HttpStatusCode.SeeOther:
				this.method = "GET";
				text = this.webResponse.Headers["Location"];
				goto IL_00E4;
			case HttpStatusCode.NotModified:
				return false;
			case HttpStatusCode.UseProxy:
				ex = new NotImplementedException("Proxy support not available.");
				goto IL_00E4;
			}
			ex = new ProtocolViolationException("Invalid status code: " + (int)code);
			IL_00E4:
			if (ex != null)
			{
				throw ex;
			}
			if (text == null)
			{
				throw new WebException("No Location header found for " + (int)code, WebExceptionStatus.ProtocolError);
			}
			global::System.Uri uri = this.actualUri;
			try
			{
				this.actualUri = new global::System.Uri(this.actualUri, text);
			}
			catch (Exception)
			{
				throw new WebException(string.Format("Invalid URL ({0}) for {1}", text, (int)code), WebExceptionStatus.ProtocolError);
			}
			this.hostChanged = this.actualUri.Scheme != uri.Scheme || this.actualUri.Host != uri.Host || this.actualUri.Port != uri.Port;
			return true;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000B47C File Offset: 0x0000967C
		private string GetHeaders()
		{
			bool flag = false;
			if (this.sendChunked)
			{
				flag = true;
				this.webHeaders.RemoveAndAdd("Transfer-Encoding", "chunked");
				this.webHeaders.RemoveInternal("Content-Length");
			}
			else if (this.contentLength != -1L)
			{
				if (this.contentLength > 0L)
				{
					flag = true;
				}
				this.webHeaders.SetInternal("Content-Length", this.contentLength.ToString());
				this.webHeaders.RemoveInternal("Transfer-Encoding");
			}
			if (this.actualVersion == HttpVersion.Version11 && flag && this.servicePoint.SendContinue)
			{
				this.webHeaders.RemoveAndAdd("Expect", "100-continue");
				this.expectContinue = true;
			}
			else
			{
				this.webHeaders.RemoveInternal("Expect");
				this.expectContinue = false;
			}
			bool proxyQuery = this.ProxyQuery;
			string text = ((!proxyQuery) ? "Connection" : "Proxy-Connection");
			this.webHeaders.RemoveInternal(proxyQuery ? "Connection" : "Proxy-Connection");
			Version protocolVersion = this.servicePoint.ProtocolVersion;
			bool flag2 = protocolVersion == null || protocolVersion == HttpVersion.Version10;
			if (this.keepAlive && (this.version == HttpVersion.Version10 || flag2))
			{
				this.webHeaders.RemoveAndAdd(text, "keep-alive");
			}
			else if (!this.keepAlive && this.version == HttpVersion.Version11)
			{
				this.webHeaders.RemoveAndAdd(text, "close");
			}
			this.webHeaders.SetInternal("Host", this.actualUri.Authority);
			if (this.cookieContainer != null)
			{
				string cookieHeader = this.cookieContainer.GetCookieHeader(this.actualUri);
				if (cookieHeader != string.Empty)
				{
					this.webHeaders.SetInternal("Cookie", cookieHeader);
				}
			}
			string text2 = null;
			if ((this.auto_decomp & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				text2 = "gzip";
			}
			if ((this.auto_decomp & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				text2 = ((text2 == null) ? "deflate" : "gzip, deflate");
			}
			if (text2 != null)
			{
				this.webHeaders.RemoveAndAdd("Accept-Encoding", text2);
			}
			if (!this.usedPreAuth && this.preAuthenticate)
			{
				this.DoPreAuthenticate();
			}
			return this.webHeaders.ToString();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000B718 File Offset: 0x00009918
		private void DoPreAuthenticate()
		{
			bool flag = this.proxy != null && !this.proxy.IsBypassed(this.actualUri);
			ICredentials credentials2;
			if (!flag || this.credentials != null)
			{
				ICredentials credentials = this.credentials;
				credentials2 = credentials;
			}
			else
			{
				credentials2 = this.proxy.Credentials;
			}
			ICredentials credentials3 = credentials2;
			Authorization authorization = AuthenticationManager.PreAuthenticate(this, credentials3);
			if (authorization == null)
			{
				return;
			}
			this.webHeaders.RemoveInternal("Proxy-Authorization");
			this.webHeaders.RemoveInternal("Authorization");
			string text = ((!flag || this.credentials != null) ? "Authorization" : "Proxy-Authorization");
			this.webHeaders[text] = authorization.Message;
			this.usedPreAuth = true;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000B7E0 File Offset: 0x000099E0
		internal void SetWriteStreamError(WebExceptionStatus status, Exception exc)
		{
			if (this.Aborted)
			{
				return;
			}
			WebAsyncResult webAsyncResult = this.asyncWrite;
			if (webAsyncResult == null)
			{
				webAsyncResult = this.asyncRead;
			}
			if (webAsyncResult != null)
			{
				WebException ex;
				if (exc == null)
				{
					string text = "Error: " + status;
					ex = new WebException(text, status);
				}
				else
				{
					string text = string.Format("Error: {0} ({1})", status, exc.Message);
					ex = new WebException(text, exc, status);
				}
				webAsyncResult.SetCompleted(false, ex);
				webAsyncResult.DoCallback();
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000B868 File Offset: 0x00009A68
		internal void SendRequestHeaders(bool propagate_error)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			if (!this.ProxyQuery)
			{
				text = this.actualUri.PathAndQuery;
			}
			else if (this.actualUri.IsDefaultPort)
			{
				text = string.Format("{0}://{1}{2}", this.actualUri.Scheme, this.actualUri.Host, this.actualUri.PathAndQuery);
			}
			else
			{
				text = string.Format("{0}://{1}:{2}{3}", new object[]
				{
					this.actualUri.Scheme,
					this.actualUri.Host,
					this.actualUri.Port,
					this.actualUri.PathAndQuery
				});
			}
			if (this.servicePoint.ProtocolVersion != null && this.servicePoint.ProtocolVersion < this.version)
			{
				this.actualVersion = this.servicePoint.ProtocolVersion;
			}
			else
			{
				this.actualVersion = this.version;
			}
			stringBuilder.AppendFormat("{0} {1} HTTP/{2}.{3}\r\n", new object[]
			{
				this.method,
				text,
				this.actualVersion.Major,
				this.actualVersion.Minor
			});
			stringBuilder.Append(this.GetHeaders());
			string text2 = stringBuilder.ToString();
			byte[] bytes = Encoding.UTF8.GetBytes(text2);
			try
			{
				this.writeStream.SetHeaders(bytes);
			}
			catch (WebException ex)
			{
				this.SetWriteStreamError(ex.Status, ex);
				if (propagate_error)
				{
					throw;
				}
			}
			catch (Exception ex2)
			{
				this.SetWriteStreamError(WebExceptionStatus.SendFailure, ex2);
				if (propagate_error)
				{
					throw;
				}
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000BA44 File Offset: 0x00009C44
		internal void SetWriteStream(WebConnectionStream stream)
		{
			if (this.Aborted)
			{
				return;
			}
			this.writeStream = stream;
			if (this.bodyBuffer != null)
			{
				this.webHeaders.RemoveInternal("Transfer-Encoding");
				this.contentLength = (long)this.bodyBufferLength;
				this.writeStream.SendChunked = false;
			}
			this.SendRequestHeaders(false);
			this.haveRequest = true;
			if (this.bodyBuffer != null)
			{
				this.writeStream.Write(this.bodyBuffer, 0, this.bodyBufferLength);
				this.bodyBuffer = null;
				this.writeStream.Close();
			}
			else if (this.method != "HEAD" && this.method != "GET" && this.method != "MKCOL" && this.method != "CONNECT" && this.method != "DELETE" && this.method != "TRACE" && this.getResponseCalled && !this.writeStream.RequestWritten)
			{
				this.writeStream.WriteRequest();
			}
			if (this.asyncWrite != null)
			{
				this.asyncWrite.SetCompleted(false, stream);
				this.asyncWrite.DoCallback();
				this.asyncWrite = null;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		internal void SetResponseError(WebExceptionStatus status, Exception e, string where)
		{
			if (this.Aborted)
			{
				return;
			}
			object obj = this.locker;
			lock (obj)
			{
				string text = string.Format("Error getting response stream ({0}): {1}", where, status);
				WebAsyncResult webAsyncResult = this.asyncRead;
				if (webAsyncResult == null)
				{
					webAsyncResult = this.asyncWrite;
				}
				WebException ex;
				if (e is WebException)
				{
					ex = (WebException)e;
				}
				else
				{
					ex = new WebException(text, e, status, null);
				}
				if (webAsyncResult != null)
				{
					if (!webAsyncResult.IsCompleted)
					{
						webAsyncResult.SetCompleted(false, ex);
						webAsyncResult.DoCallback();
					}
					else if (webAsyncResult == this.asyncWrite)
					{
						this.saved_exc = ex;
					}
					this.haveResponse = true;
					this.asyncRead = null;
					this.asyncWrite = null;
				}
				else
				{
					this.haveResponse = true;
					this.saved_exc = ex;
				}
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000BC98 File Offset: 0x00009E98
		private void CheckSendError(WebConnectionData data)
		{
			int statusCode = data.StatusCode;
			if (statusCode < 400 || statusCode == 401 || statusCode == 407)
			{
				return;
			}
			if (this.writeStream != null && this.asyncRead == null && !this.writeStream.CompleteRequestWritten)
			{
				this.saved_exc = new WebException(data.StatusDescription, null, WebExceptionStatus.ProtocolError, this.webResponse);
				this.webResponse.ReadAll();
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000BD18 File Offset: 0x00009F18
		private void HandleNtlmAuth(WebAsyncResult r)
		{
			WebConnectionStream webConnectionStream = this.webResponse.GetResponseStream() as WebConnectionStream;
			if (webConnectionStream != null)
			{
				WebConnection connection = webConnectionStream.Connection;
				connection.PriorityRequest = this;
				ICredentials credentials2;
				if (this.proxy == null || this.proxy.IsBypassed(this.actualUri))
				{
					ICredentials credentials = this.credentials;
					credentials2 = credentials;
				}
				else
				{
					credentials2 = this.proxy.Credentials;
				}
				ICredentials credentials3 = credentials2;
				if (credentials3 != null)
				{
					connection.NtlmCredential = credentials3.GetCredential(this.requestUri, "NTLM");
					connection.UnsafeAuthenticatedConnectionSharing = this.unsafe_auth_blah;
				}
			}
			r.Reset();
			this.haveResponse = false;
			this.webResponse.ReadAll();
			this.webResponse = null;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000BDD8 File Offset: 0x00009FD8
		internal void SetResponseData(WebConnectionData data)
		{
			object obj = this.locker;
			lock (obj)
			{
				if (this.Aborted)
				{
					if (data.stream != null)
					{
						data.stream.Close();
					}
				}
				else
				{
					WebException ex = null;
					try
					{
						this.webResponse = new HttpWebResponse(this.actualUri, this.method, data, this.cookieContainer);
					}
					catch (Exception ex2)
					{
						ex = new WebException(ex2.Message, ex2, WebExceptionStatus.ProtocolError, null);
						if (data.stream != null)
						{
							data.stream.Close();
						}
					}
					if (ex == null && (this.method == "POST" || this.method == "PUT"))
					{
						object obj2 = this.locker;
						lock (obj2)
						{
							this.CheckSendError(data);
							if (this.saved_exc != null)
							{
								ex = (WebException)this.saved_exc;
							}
						}
					}
					WebAsyncResult webAsyncResult = this.asyncRead;
					bool flag = false;
					if (webAsyncResult == null && this.webResponse != null)
					{
						flag = true;
						webAsyncResult = new WebAsyncResult(null, null);
						webAsyncResult.SetCompleted(false, this.webResponse);
					}
					if (webAsyncResult != null)
					{
						if (ex != null)
						{
							webAsyncResult.SetCompleted(false, ex);
							webAsyncResult.DoCallback();
						}
						else
						{
							try
							{
								if (!this.CheckFinalStatus(webAsyncResult))
								{
									if (this.is_ntlm_auth && this.authCompleted && this.webResponse != null && this.webResponse.StatusCode < HttpStatusCode.BadRequest)
									{
										WebConnectionStream webConnectionStream = this.webResponse.GetResponseStream() as WebConnectionStream;
										if (webConnectionStream != null)
										{
											WebConnection connection = webConnectionStream.Connection;
											connection.NtlmAuthenticated = true;
										}
									}
									if (this.writeStream != null)
									{
										this.writeStream.KillBuffer();
									}
									this.haveResponse = true;
									webAsyncResult.SetCompleted(false, this.webResponse);
									webAsyncResult.DoCallback();
								}
								else
								{
									if (this.webResponse != null)
									{
										if (this.is_ntlm_auth)
										{
											this.HandleNtlmAuth(webAsyncResult);
											return;
										}
										this.webResponse.Close();
									}
									this.finished_reading = false;
									this.haveResponse = false;
									this.webResponse = null;
									webAsyncResult.Reset();
									this.servicePoint = this.GetServicePoint();
									this.abortHandler = this.servicePoint.SendRequest(this, this.connectionGroup);
								}
							}
							catch (WebException ex3)
							{
								if (flag)
								{
									this.saved_exc = ex3;
									this.haveResponse = true;
								}
								webAsyncResult.SetCompleted(false, ex3);
								webAsyncResult.DoCallback();
							}
							catch (Exception ex4)
							{
								ex = new WebException(ex4.Message, ex4, WebExceptionStatus.ProtocolError, null);
								if (flag)
								{
									this.saved_exc = ex;
									this.haveResponse = true;
								}
								webAsyncResult.SetCompleted(false, ex);
								webAsyncResult.DoCallback();
							}
						}
					}
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000C140 File Offset: 0x0000A340
		private bool CheckAuthorization(WebResponse response, HttpStatusCode code)
		{
			this.authCompleted = false;
			if (code == HttpStatusCode.Unauthorized && this.credentials == null)
			{
				return false;
			}
			bool flag = code == HttpStatusCode.ProxyAuthenticationRequired;
			if (flag && (this.proxy == null || this.proxy.Credentials == null))
			{
				return false;
			}
			string[] values = response.Headers.GetValues((!flag) ? "WWW-Authenticate" : "Proxy-Authenticate");
			if (values == null || values.Length == 0)
			{
				return false;
			}
			ICredentials credentials2;
			if (!flag)
			{
				ICredentials credentials = this.credentials;
				credentials2 = credentials;
			}
			else
			{
				credentials2 = this.proxy.Credentials;
			}
			ICredentials credentials3 = credentials2;
			Authorization authorization = null;
			foreach (string text in values)
			{
				authorization = AuthenticationManager.Authenticate(text, this, credentials3);
				if (authorization != null)
				{
					break;
				}
			}
			if (authorization == null)
			{
				return false;
			}
			this.webHeaders[(!flag) ? "Authorization" : "Proxy-Authorization"] = authorization.Message;
			this.authCompleted = authorization.Complete;
			this.is_ntlm_auth = authorization.Module.AuthenticationType == "NTLM";
			return true;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000C27C File Offset: 0x0000A47C
		private bool CheckFinalStatus(WebAsyncResult result)
		{
			if (result.GotException)
			{
				throw result.Exception;
			}
			Exception ex = result.Exception;
			this.bodyBuffer = null;
			HttpWebResponse response = result.Response;
			WebExceptionStatus webExceptionStatus = WebExceptionStatus.ProtocolError;
			HttpStatusCode httpStatusCode = (HttpStatusCode)0;
			if (ex == null && this.webResponse != null)
			{
				httpStatusCode = this.webResponse.StatusCode;
				if (!this.authCompleted && ((httpStatusCode == HttpStatusCode.Unauthorized && this.credentials != null) || (this.ProxyQuery && httpStatusCode == HttpStatusCode.ProxyAuthenticationRequired)) && !this.usedPreAuth && this.CheckAuthorization(this.webResponse, httpStatusCode))
				{
					if (this.InternalAllowBuffering)
					{
						this.bodyBuffer = this.writeStream.WriteBuffer;
						this.bodyBufferLength = this.writeStream.WriteBufferLength;
						return true;
					}
					if (this.method != "PUT" && this.method != "POST")
					{
						return true;
					}
					this.writeStream.InternalClose();
					this.writeStream = null;
					this.webResponse.Close();
					this.webResponse = null;
					throw new WebException("This request requires buffering of data for authentication or redirection to be sucessful.");
				}
				else if (httpStatusCode >= HttpStatusCode.BadRequest)
				{
					string text = string.Format("The remote server returned an error: ({0}) {1}.", (int)httpStatusCode, this.webResponse.StatusDescription);
					ex = new WebException(text, null, webExceptionStatus, this.webResponse);
					this.webResponse.ReadAll();
				}
				else if (httpStatusCode == HttpStatusCode.NotModified && this.allowAutoRedirect)
				{
					string text2 = string.Format("The remote server returned an error: ({0}) {1}.", (int)httpStatusCode, this.webResponse.StatusDescription);
					ex = new WebException(text2, null, webExceptionStatus, this.webResponse);
				}
				else if (httpStatusCode >= HttpStatusCode.MultipleChoices && this.allowAutoRedirect && this.redirects >= this.maxAutoRedirect)
				{
					ex = new WebException("Max. redirections exceeded.", null, webExceptionStatus, this.webResponse);
					this.webResponse.ReadAll();
				}
			}
			if (ex == null)
			{
				bool flag = false;
				int num = (int)httpStatusCode;
				if (this.allowAutoRedirect && num >= 300)
				{
					if (this.InternalAllowBuffering && this.writeStream.WriteBufferLength > 0)
					{
						this.bodyBuffer = this.writeStream.WriteBuffer;
						this.bodyBufferLength = this.writeStream.WriteBufferLength;
					}
					flag = this.Redirect(result, httpStatusCode);
				}
				if (response != null && num >= 300 && num != 304)
				{
					response.ReadAll();
				}
				return flag;
			}
			if (this.writeStream != null)
			{
				this.writeStream.InternalClose();
				this.writeStream = null;
			}
			this.webResponse = null;
			throw ex;
		}

		// Token: 0x04000123 RID: 291
		private global::System.Uri requestUri;

		// Token: 0x04000124 RID: 292
		private global::System.Uri actualUri;

		// Token: 0x04000125 RID: 293
		private bool hostChanged;

		// Token: 0x04000126 RID: 294
		private bool allowAutoRedirect = true;

		// Token: 0x04000127 RID: 295
		private bool allowBuffering = true;

		// Token: 0x04000128 RID: 296
		private global::System.Security.Cryptography.X509Certificates.X509CertificateCollection certificates;

		// Token: 0x04000129 RID: 297
		private string connectionGroup;

		// Token: 0x0400012A RID: 298
		private long contentLength = -1L;

		// Token: 0x0400012B RID: 299
		private HttpContinueDelegate continueDelegate;

		// Token: 0x0400012C RID: 300
		private CookieContainer cookieContainer;

		// Token: 0x0400012D RID: 301
		private ICredentials credentials;

		// Token: 0x0400012E RID: 302
		private bool haveResponse;

		// Token: 0x0400012F RID: 303
		private bool haveRequest;

		// Token: 0x04000130 RID: 304
		private bool requestSent;

		// Token: 0x04000131 RID: 305
		private WebHeaderCollection webHeaders = new WebHeaderCollection(true);

		// Token: 0x04000132 RID: 306
		private bool keepAlive = true;

		// Token: 0x04000133 RID: 307
		private int maxAutoRedirect = 50;

		// Token: 0x04000134 RID: 308
		private string mediaType = string.Empty;

		// Token: 0x04000135 RID: 309
		private string method = "GET";

		// Token: 0x04000136 RID: 310
		private string initialMethod = "GET";

		// Token: 0x04000137 RID: 311
		private bool pipelined = true;

		// Token: 0x04000138 RID: 312
		private bool preAuthenticate;

		// Token: 0x04000139 RID: 313
		private bool usedPreAuth;

		// Token: 0x0400013A RID: 314
		private Version version = HttpVersion.Version11;

		// Token: 0x0400013B RID: 315
		private Version actualVersion;

		// Token: 0x0400013C RID: 316
		private IWebProxy proxy;

		// Token: 0x0400013D RID: 317
		private bool sendChunked;

		// Token: 0x0400013E RID: 318
		private ServicePoint servicePoint;

		// Token: 0x0400013F RID: 319
		private int timeout = 100000;

		// Token: 0x04000140 RID: 320
		private WebConnectionStream writeStream;

		// Token: 0x04000141 RID: 321
		private HttpWebResponse webResponse;

		// Token: 0x04000142 RID: 322
		private WebAsyncResult asyncWrite;

		// Token: 0x04000143 RID: 323
		private WebAsyncResult asyncRead;

		// Token: 0x04000144 RID: 324
		private EventHandler abortHandler;

		// Token: 0x04000145 RID: 325
		private int aborted;

		// Token: 0x04000146 RID: 326
		private bool gotRequestStream;

		// Token: 0x04000147 RID: 327
		private int redirects;

		// Token: 0x04000148 RID: 328
		private bool expectContinue;

		// Token: 0x04000149 RID: 329
		private bool authCompleted;

		// Token: 0x0400014A RID: 330
		private byte[] bodyBuffer;

		// Token: 0x0400014B RID: 331
		private int bodyBufferLength;

		// Token: 0x0400014C RID: 332
		private bool getResponseCalled;

		// Token: 0x0400014D RID: 333
		private Exception saved_exc;

		// Token: 0x0400014E RID: 334
		private object locker = new object();

		// Token: 0x0400014F RID: 335
		private bool is_ntlm_auth;

		// Token: 0x04000150 RID: 336
		private bool finished_reading;

		// Token: 0x04000151 RID: 337
		internal WebConnection WebConnection;

		// Token: 0x04000152 RID: 338
		private DecompressionMethods auto_decomp;

		// Token: 0x04000153 RID: 339
		private int maxResponseHeadersLength;

		// Token: 0x04000154 RID: 340
		private static int defaultMaxResponseHeadersLength = 65536;

		// Token: 0x04000155 RID: 341
		private int readWriteTimeout = 300000;

		// Token: 0x04000156 RID: 342
		private bool unsafe_auth_blah;
	}
}
