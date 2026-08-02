using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Mono.Security.Protocol.Tls;

namespace System.Net
{
	// Token: 0x020000A6 RID: 166
	internal class WebConnection
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x000116F0 File Offset: 0x0000F8F0
		public WebConnection(WebConnectionGroup group, ServicePoint sPoint)
		{
			this.sPoint = sPoint;
			this.buffer = new byte[4096];
			this.readState = ReadState.None;
			this.Data = new WebConnectionData();
			this.initConn = new WaitCallback(this.InitConnection);
			this.queue = group.Queue;
			this.abortHelper = new WebConnection.AbortHelper();
			this.abortHelper.Connection = this;
			this.abortHandler = new EventHandler(this.abortHelper.Abort);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000117A4 File Offset: 0x0000F9A4
		private bool CanReuse()
		{
			return !this.socket.Poll(0, global::System.Net.Sockets.SelectMode.SelectRead);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000117B8 File Offset: 0x0000F9B8
		private void LoggedThrow(Exception e)
		{
			Console.WriteLine("Throwing this exception: " + e);
			throw e;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000117CC File Offset: 0x0000F9CC
		private void CheckUnityWebSecurity(HttpWebRequest request)
		{
			if (!Environment.SocketSecurityEnabled)
			{
				return;
			}
			Console.WriteLine("CheckingSecurityForUrl: " + request.RequestUri.AbsoluteUri);
			global::System.Uri requestUri = request.RequestUri;
			string text = string.Empty;
			if (!requestUri.IsDefaultPort)
			{
				text = ":" + requestUri.Port;
			}
			if (requestUri.ToString() == string.Concat(new string[] { requestUri.Scheme, "://", requestUri.Host, text, "/crossdomain.xml" }))
			{
				return;
			}
			try
			{
				if (WebConnection.method_GetSecurityPolicyFromNonMainThread == null)
				{
					Type type = Type.GetType("UnityEngine.UnityCrossDomainHelper, CrossDomainPolicyParser, Version=1.0.0.0, Culture=neutral");
					if (type == null)
					{
						this.LoggedThrow(new SecurityException("Cant find type UnityCrossDomainHelper"));
					}
					WebConnection.method_GetSecurityPolicyFromNonMainThread = type.GetMethod("GetSecurityPolicyForDotNetWebRequest");
					if (WebConnection.method_GetSecurityPolicyFromNonMainThread == null)
					{
						this.LoggedThrow(new SecurityException("Cant find GetSecurityPolicyFromNonMainThread"));
					}
				}
				MethodInfo method = typeof(WebConnection).GetMethod("DownloadPolicy", BindingFlags.Static | BindingFlags.NonPublic);
				if (method == null)
				{
					this.LoggedThrow(new SecurityException("Cannot find method DownloadPolicy"));
				}
				if (!(bool)WebConnection.method_GetSecurityPolicyFromNonMainThread.Invoke(null, new object[]
				{
					request.RequestUri.ToString(),
					method
				}))
				{
					this.LoggedThrow(new SecurityException("Webrequest was denied"));
				}
			}
			catch (Exception ex)
			{
				this.LoggedThrow(new SecurityException("Unexpected error while trying to call method_GetSecurityPolicyBlocking : " + ex));
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00011964 File Offset: 0x0000FB64
		private void Connect(HttpWebRequest request)
		{
			object obj = this.socketLock;
			lock (obj)
			{
				if (this.socket != null && this.socket.Connected && this.status == WebExceptionStatus.Success && this.CanReuse() && this.CompleteChunkedRead())
				{
					this.reused = true;
				}
				else
				{
					this.reused = false;
					if (this.socket != null)
					{
						this.socket.Close();
						this.socket = null;
					}
					this.chunkStream = null;
					IPHostEntry hostEntry = this.sPoint.HostEntry;
					if (hostEntry == null)
					{
						this.status = ((!this.sPoint.UsesProxy) ? WebExceptionStatus.NameResolutionFailure : WebExceptionStatus.ProxyNameResolutionFailure);
					}
					else
					{
						WebConnectionData data = this.Data;
						foreach (IPAddress ipaddress in hostEntry.AddressList)
						{
							this.socket = new global::System.Net.Sockets.Socket(ipaddress.AddressFamily, global::System.Net.Sockets.SocketType.Stream, global::System.Net.Sockets.ProtocolType.Tcp);
							IPEndPoint ipendPoint = new IPEndPoint(ipaddress, this.sPoint.Address.Port);
							this.socket.SetSocketOption(global::System.Net.Sockets.SocketOptionLevel.Tcp, global::System.Net.Sockets.SocketOptionName.Debug, (!this.sPoint.UseNagleAlgorithm) ? 1 : 0);
							this.socket.NoDelay = !this.sPoint.UseNagleAlgorithm;
							if (!this.sPoint.CallEndPointDelegate(this.socket, ipendPoint))
							{
								this.socket.Close();
								this.socket = null;
								this.status = WebExceptionStatus.ConnectFailure;
							}
							else
							{
								try
								{
									if (request.Aborted)
									{
										break;
									}
									this.CheckUnityWebSecurity(request);
									this.socket.Connect(ipendPoint, false);
									this.status = WebExceptionStatus.Success;
									break;
								}
								catch (ThreadAbortException)
								{
									global::System.Net.Sockets.Socket socket = this.socket;
									this.socket = null;
									if (socket != null)
									{
										socket.Close();
									}
									break;
								}
								catch (ObjectDisposedException ex)
								{
									break;
								}
								catch (Exception ex2)
								{
									global::System.Net.Sockets.Socket socket2 = this.socket;
									this.socket = null;
									if (socket2 != null)
									{
										socket2.Close();
									}
									if (!request.Aborted)
									{
										this.status = WebExceptionStatus.ConnectFailure;
									}
									this.connect_exception = ex2;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00011C0C File Offset: 0x0000FE0C
		private static void EnsureSSLStreamAvailable()
		{
			object obj = WebConnection.classLock;
			lock (obj)
			{
				if (WebConnection.sslStream == null)
				{
					WebConnection.sslStream = typeof(HttpsClientStream);
					WebConnection.piClient = WebConnection.sslStream.GetProperty("SelectedClientCertificate");
					WebConnection.piServer = WebConnection.sslStream.GetProperty("ServerCertificate");
					WebConnection.piTrustFailure = WebConnection.sslStream.GetProperty("TrustFailure");
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00011C9C File Offset: 0x0000FE9C
		private bool CreateTunnel(HttpWebRequest request, Stream stream, out byte[] buffer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CONNECT ");
			stringBuilder.Append(request.Address.Host);
			stringBuilder.Append(':');
			stringBuilder.Append(request.Address.Port);
			stringBuilder.Append(" HTTP/");
			if (request.ServicePoint.ProtocolVersion == HttpVersion.Version11)
			{
				stringBuilder.Append("1.1");
			}
			else
			{
				stringBuilder.Append("1.0");
			}
			stringBuilder.Append("\r\nHost: ");
			stringBuilder.Append(request.Address.Authority);
			string challenge = this.Data.Challenge;
			this.Data.Challenge = null;
			bool flag = request.Headers["Proxy-Authorization"] != null;
			if (flag)
			{
				stringBuilder.Append("\r\nProxy-Authorization: ");
				stringBuilder.Append(request.Headers["Proxy-Authorization"]);
			}
			else if (challenge != null && this.Data.StatusCode == 407)
			{
				flag = true;
				ICredentials credentials = request.Proxy.Credentials;
				Authorization authorization = AuthenticationManager.Authenticate(challenge, request, credentials);
				if (authorization != null)
				{
					stringBuilder.Append("\r\nProxy-Authorization: ");
					stringBuilder.Append(authorization.Message);
				}
			}
			stringBuilder.Append("\r\n\r\n");
			this.Data.StatusCode = 0;
			byte[] bytes = Encoding.Default.GetBytes(stringBuilder.ToString());
			stream.Write(bytes, 0, bytes.Length);
			int num;
			WebHeaderCollection webHeaderCollection = this.ReadHeaders(request, stream, out buffer, out num);
			if (!flag && webHeaderCollection != null && num == 407)
			{
				this.Data.StatusCode = num;
				this.Data.Challenge = webHeaderCollection["Proxy-Authenticate"];
				return false;
			}
			if (num != 200)
			{
				string text = string.Format("The remote server returned a {0} status code.", num);
				this.HandleError(WebExceptionStatus.SecureChannelFailure, null, text);
				return false;
			}
			return webHeaderCollection != null;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00011EB4 File Offset: 0x000100B4
		private WebHeaderCollection ReadHeaders(HttpWebRequest request, Stream stream, out byte[] retBuffer, out int status)
		{
			retBuffer = null;
			status = 200;
			byte[] array = new byte[1024];
			MemoryStream memoryStream = new MemoryStream();
			bool flag = false;
			int num2;
			WebHeaderCollection webHeaderCollection;
			for (;;)
			{
				int num = stream.Read(array, 0, 1024);
				if (num == 0)
				{
					break;
				}
				memoryStream.Write(array, 0, num);
				num2 = 0;
				string text = null;
				webHeaderCollection = new WebHeaderCollection();
				while (WebConnection.ReadLine(memoryStream.GetBuffer(), ref num2, (int)memoryStream.Length, ref text))
				{
					if (text == null)
					{
						goto Block_2;
					}
					if (flag)
					{
						webHeaderCollection.Add(text);
					}
					else
					{
						int num3 = text.IndexOf(' ');
						if (num3 == -1)
						{
							goto Block_5;
						}
						status = (int)uint.Parse(text.Substring(num3 + 1, 3));
						flag = true;
					}
				}
			}
			this.HandleError(WebExceptionStatus.ServerProtocolViolation, null, "ReadHeaders");
			return null;
			Block_2:
			if (memoryStream.Length - (long)num2 > 0L)
			{
				retBuffer = new byte[memoryStream.Length - (long)num2];
				Buffer.BlockCopy(memoryStream.GetBuffer(), num2, retBuffer, 0, retBuffer.Length);
			}
			return webHeaderCollection;
			Block_5:
			this.HandleError(WebExceptionStatus.ServerProtocolViolation, null, "ReadHeaders2");
			return null;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00011FD0 File Offset: 0x000101D0
		private bool CreateStream(HttpWebRequest request)
		{
			try
			{
				global::System.Net.Sockets.NetworkStream networkStream = new global::System.Net.Sockets.NetworkStream(this.socket, false);
				if (request.Address.Scheme == global::System.Uri.UriSchemeHttps)
				{
					this.ssl = true;
					WebConnection.EnsureSSLStreamAvailable();
					if (!this.reused || this.nstream == null || this.nstream.GetType() != WebConnection.sslStream)
					{
						byte[] array = null;
						if (this.sPoint.UseConnect && !this.CreateTunnel(request, networkStream, out array))
						{
							return false;
						}
						object[] array2 = new object[] { networkStream, request.ClientCertificates, request, array };
						this.nstream = (Stream)Activator.CreateInstance(WebConnection.sslStream, array2);
						SslClientStream sslClientStream = (SslClientStream)this.nstream;
						ServicePointManager.ChainValidationHelper chainValidationHelper = new ServicePointManager.ChainValidationHelper(request);
						sslClientStream.ServerCertValidation2 += chainValidationHelper.ValidateChain;
						this.certsAvailable = false;
					}
				}
				else
				{
					this.ssl = false;
					this.nstream = networkStream;
				}
			}
			catch (Exception)
			{
				if (!request.Aborted)
				{
					this.status = WebExceptionStatus.ConnectFailure;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00012120 File Offset: 0x00010320
		private void HandleError(WebExceptionStatus st, Exception e, string where)
		{
			this.status = st;
			lock (this)
			{
				if (st == WebExceptionStatus.RequestCanceled)
				{
					this.Data = new WebConnectionData();
				}
			}
			if (e == null)
			{
				try
				{
					throw new Exception(new StackTrace().ToString());
				}
				catch (Exception ex)
				{
					e = ex;
				}
			}
			HttpWebRequest httpWebRequest = null;
			if (this.Data != null && this.Data.request != null)
			{
				httpWebRequest = this.Data.request;
			}
			this.Close(true);
			if (httpWebRequest != null)
			{
				httpWebRequest.FinishedReading = true;
				httpWebRequest.SetResponseError(st, e, where);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000121E0 File Offset: 0x000103E0
		private static void ReadDone(IAsyncResult result)
		{
			WebConnection webConnection = (WebConnection)result.AsyncState;
			WebConnectionData data = webConnection.Data;
			Stream stream = webConnection.nstream;
			if (stream == null)
			{
				webConnection.Close(true);
				return;
			}
			int num = -1;
			try
			{
				num = stream.EndRead(result);
			}
			catch (Exception ex)
			{
				webConnection.HandleError(WebExceptionStatus.ReceiveFailure, ex, "ReadDone1");
				return;
			}
			if (num == 0)
			{
				webConnection.HandleError(WebExceptionStatus.ReceiveFailure, null, "ReadDone2");
				return;
			}
			if (num < 0)
			{
				webConnection.HandleError(WebExceptionStatus.ServerProtocolViolation, null, "ReadDone3");
				return;
			}
			int num2 = -1;
			num += webConnection.position;
			if (webConnection.readState == ReadState.None)
			{
				Exception ex2 = null;
				try
				{
					num2 = webConnection.GetResponse(webConnection.buffer, num);
				}
				catch (Exception ex3)
				{
					ex2 = ex3;
				}
				if (ex2 != null)
				{
					webConnection.HandleError(WebExceptionStatus.ServerProtocolViolation, ex2, "ReadDone4");
					return;
				}
			}
			if (webConnection.readState != ReadState.Content)
			{
				int num3 = num * 2;
				int num4 = ((num3 >= webConnection.buffer.Length) ? num3 : webConnection.buffer.Length);
				byte[] array = new byte[num4];
				Buffer.BlockCopy(webConnection.buffer, 0, array, 0, num);
				webConnection.buffer = array;
				webConnection.position = num;
				webConnection.readState = ReadState.None;
				WebConnection.InitRead(webConnection);
				return;
			}
			webConnection.position = 0;
			WebConnectionStream webConnectionStream = new WebConnectionStream(webConnection);
			string text = data.Headers["Transfer-Encoding"];
			webConnection.chunkedRead = text != null && text.ToLower().IndexOf("chunked") != -1;
			if (!webConnection.chunkedRead)
			{
				webConnectionStream.ReadBuffer = webConnection.buffer;
				webConnectionStream.ReadBufferOffset = num2;
				webConnectionStream.ReadBufferSize = num;
				webConnectionStream.CheckResponseInBuffer();
			}
			else if (webConnection.chunkStream == null)
			{
				try
				{
					webConnection.chunkStream = new ChunkStream(webConnection.buffer, num2, num, data.Headers);
				}
				catch (Exception ex4)
				{
					webConnection.HandleError(WebExceptionStatus.ServerProtocolViolation, ex4, "ReadDone5");
					return;
				}
			}
			else
			{
				webConnection.chunkStream.ResetBuffer();
				try
				{
					webConnection.chunkStream.Write(webConnection.buffer, num2, num);
				}
				catch (Exception ex5)
				{
					webConnection.HandleError(WebExceptionStatus.ServerProtocolViolation, ex5, "ReadDone6");
					return;
				}
			}
			data.stream = webConnectionStream;
			if (!WebConnection.ExpectContent(data.StatusCode) || data.request.Method == "HEAD")
			{
				webConnectionStream.ForceCompletion();
			}
			data.request.SetResponseData(data);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000124A0 File Offset: 0x000106A0
		private static bool ExpectContent(int statusCode)
		{
			return statusCode >= 200 && statusCode != 204 && statusCode != 304;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000124C8 File Offset: 0x000106C8
		internal void GetCertificates()
		{
			X509Certificate x509Certificate = (X509Certificate)WebConnection.piClient.GetValue(this.nstream, null);
			X509Certificate x509Certificate2 = (X509Certificate)WebConnection.piServer.GetValue(this.nstream, null);
			this.sPoint.SetCertificates(x509Certificate, x509Certificate2);
			this.certsAvailable = x509Certificate2 != null;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00012520 File Offset: 0x00010720
		internal static void InitRead(object state)
		{
			WebConnection webConnection = (WebConnection)state;
			Stream stream = webConnection.nstream;
			try
			{
				int num = webConnection.buffer.Length - webConnection.position;
				stream.BeginRead(webConnection.buffer, webConnection.position, num, WebConnection.readDoneDelegate, webConnection);
			}
			catch (Exception ex)
			{
				webConnection.HandleError(WebExceptionStatus.ReceiveFailure, ex, "InitRead");
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00012590 File Offset: 0x00010790
		private int GetResponse(byte[] buffer, int max)
		{
			int num = 0;
			string text = null;
			bool flag = false;
			bool flag2 = false;
			for (;;)
			{
				if (this.readState != ReadState.None)
				{
					goto IL_0114;
				}
				if (!WebConnection.ReadLine(buffer, ref num, max, ref text))
				{
					break;
				}
				if (text == null)
				{
					flag2 = true;
				}
				else
				{
					flag2 = false;
					this.readState = ReadState.Status;
					string[] array = text.Split(new char[] { ' ' });
					if (array.Length < 2)
					{
						return -1;
					}
					if (string.Compare(array[0], "HTTP/1.1", true) == 0)
					{
						this.Data.Version = HttpVersion.Version11;
						this.sPoint.SetVersion(HttpVersion.Version11);
					}
					else
					{
						this.Data.Version = HttpVersion.Version10;
						this.sPoint.SetVersion(HttpVersion.Version10);
					}
					this.Data.StatusCode = (int)uint.Parse(array[1]);
					if (array.Length >= 3)
					{
						this.Data.StatusDescription = string.Join(" ", array, 2, array.Length - 2);
					}
					else
					{
						this.Data.StatusDescription = string.Empty;
					}
					if (num >= max)
					{
						return num;
					}
					goto IL_0114;
				}
				IL_02CA:
				if (!flag2 && !flag)
				{
					return -1;
				}
				continue;
				IL_0114:
				flag2 = false;
				if (this.readState != ReadState.Status)
				{
					goto IL_02CA;
				}
				this.readState = ReadState.Headers;
				this.Data.Headers = new WebHeaderCollection();
				ArrayList arrayList = new ArrayList();
				bool flag3 = false;
				while (!flag3)
				{
					if (!WebConnection.ReadLine(buffer, ref num, max, ref text))
					{
						break;
					}
					if (text == null)
					{
						flag3 = true;
					}
					else if (text.Length > 0 && (text[0] == ' ' || text[0] == '\t'))
					{
						int num2 = arrayList.Count - 1;
						if (num2 < 0)
						{
							break;
						}
						string text2 = (string)arrayList[num2] + text;
						arrayList[num2] = text2;
					}
					else
					{
						arrayList.Add(text);
					}
				}
				if (!flag3)
				{
					return -1;
				}
				foreach (object obj in arrayList)
				{
					string text3 = (string)obj;
					this.Data.Headers.SetInternal(text3);
				}
				if (this.Data.StatusCode != 100)
				{
					goto IL_02C1;
				}
				this.sPoint.SendContinue = true;
				if (num >= max)
				{
					return num;
				}
				if (this.Data.request.ExpectContinue)
				{
					this.Data.request.DoContinueDelegate(this.Data.StatusCode, this.Data.Headers);
					this.Data.request.ExpectContinue = false;
				}
				this.readState = ReadState.None;
				flag = true;
				goto IL_02CA;
			}
			return -1;
			IL_02C1:
			this.readState = ReadState.Content;
			return num;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00012888 File Offset: 0x00010A88
		private void InitConnection(object state)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)state;
			httpWebRequest.WebConnection = this;
			if (httpWebRequest.Aborted)
			{
				return;
			}
			this.keepAlive = httpWebRequest.KeepAlive;
			this.Data = new WebConnectionData();
			this.Data.request = httpWebRequest;
			WebExceptionStatus webExceptionStatus;
			for (;;)
			{
				this.Connect(httpWebRequest);
				if (httpWebRequest.Aborted)
				{
					break;
				}
				if (this.status != WebExceptionStatus.Success)
				{
					goto Block_3;
				}
				if (this.CreateStream(httpWebRequest))
				{
					goto IL_00D2;
				}
				if (httpWebRequest.Aborted)
				{
					return;
				}
				webExceptionStatus = this.status;
				if (this.Data.Challenge == null)
				{
					goto IL_00B4;
				}
			}
			return;
			Block_3:
			if (!httpWebRequest.Aborted)
			{
				httpWebRequest.SetWriteStreamError(this.status, this.connect_exception);
				this.Close(true);
			}
			return;
			IL_00B4:
			Exception ex = this.connect_exception;
			this.connect_exception = null;
			httpWebRequest.SetWriteStreamError(webExceptionStatus, ex);
			this.Close(true);
			return;
			IL_00D2:
			this.readState = ReadState.None;
			httpWebRequest.SetWriteStream(new WebConnectionStream(this, httpWebRequest));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001297C File Offset: 0x00010B7C
		internal EventHandler SendRequest(HttpWebRequest request)
		{
			if (request.Aborted)
			{
				return null;
			}
			lock (this)
			{
				if (!this.busy)
				{
					this.busy = true;
					this.status = WebExceptionStatus.Success;
					ThreadPool.QueueUserWorkItem(this.initConn, request);
				}
				else
				{
					Queue queue = this.queue;
					lock (queue)
					{
						this.queue.Enqueue(request);
					}
				}
			}
			return this.abortHandler;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00012A1C File Offset: 0x00010C1C
		private void SendNext()
		{
			Queue queue = this.queue;
			lock (queue)
			{
				if (this.queue.Count > 0)
				{
					this.SendRequest((HttpWebRequest)this.queue.Dequeue());
				}
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00012A7C File Offset: 0x00010C7C
		internal void NextRead()
		{
			lock (this)
			{
				this.Data.request.FinishedReading = true;
				string text = ((!this.sPoint.UsesProxy) ? "Connection" : "Proxy-Connection");
				string text2 = ((this.Data.Headers == null) ? null : this.Data.Headers[text]);
				bool flag = this.Data.Version == HttpVersion.Version11 && this.keepAlive;
				if (text2 != null)
				{
					text2 = text2.ToLower();
					flag = this.keepAlive && text2.IndexOf("keep-alive") != -1;
				}
				if ((this.socket != null && !this.socket.Connected) || !flag || (text2 != null && text2.IndexOf("close") != -1))
				{
					this.Close(false);
				}
				this.busy = false;
				if (this.priority_request != null)
				{
					this.SendRequest(this.priority_request);
					this.priority_request = null;
				}
				else
				{
					this.SendNext();
				}
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00012BD4 File Offset: 0x00010DD4
		private static bool ReadLine(byte[] buffer, ref int start, int max, ref string output)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (start < max)
			{
				num = (int)buffer[start++];
				if (num == 10)
				{
					if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\r')
					{
						stringBuilder.Length--;
					}
					flag = false;
					break;
				}
				if (flag)
				{
					stringBuilder.Length--;
					break;
				}
				if (num == 13)
				{
					flag = true;
				}
				stringBuilder.Append((char)num);
			}
			if (num != 10 && num != 13)
			{
				return false;
			}
			if (stringBuilder.Length == 0)
			{
				output = null;
				return num == 10 || num == 13;
			}
			if (flag)
			{
				stringBuilder.Length--;
			}
			output = stringBuilder.ToString();
			return true;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00012CBC File Offset: 0x00010EBC
		internal IAsyncResult BeginRead(HttpWebRequest request, byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(global::System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					return null;
				}
			}
			IAsyncResult asyncResult = null;
			if (this.chunkedRead)
			{
				if (!this.chunkStream.WantMore)
				{
					goto IL_009A;
				}
			}
			try
			{
				asyncResult = this.nstream.BeginRead(buffer, offset, size, cb, state);
				cb = null;
			}
			catch (Exception)
			{
				this.HandleError(WebExceptionStatus.ReceiveFailure, null, "chunked BeginRead");
				throw;
			}
			IL_009A:
			if (this.chunkedRead)
			{
				WebAsyncResult webAsyncResult = new WebAsyncResult(cb, state, buffer, offset, size);
				webAsyncResult.InnerAsyncResult = asyncResult;
				if (asyncResult == null)
				{
					webAsyncResult.SetCompleted(true, null);
					webAsyncResult.DoCallback();
				}
				return webAsyncResult;
			}
			return asyncResult;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00012DB8 File Offset: 0x00010FB8
		internal int EndRead(HttpWebRequest request, IAsyncResult result)
		{
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(global::System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					throw new ObjectDisposedException(typeof(global::System.Net.Sockets.NetworkStream).FullName);
				}
			}
			int num = 0;
			WebAsyncResult webAsyncResult = null;
			IAsyncResult innerAsyncResult = ((WebAsyncResult)result).InnerAsyncResult;
			if (this.chunkedRead && innerAsyncResult is WebAsyncResult)
			{
				webAsyncResult = (WebAsyncResult)innerAsyncResult;
				IAsyncResult innerAsyncResult2 = webAsyncResult.InnerAsyncResult;
				if (innerAsyncResult2 != null && !(innerAsyncResult2 is WebAsyncResult))
				{
					num = this.nstream.EndRead(innerAsyncResult2);
				}
			}
			else if (!(innerAsyncResult is WebAsyncResult))
			{
				num = this.nstream.EndRead(innerAsyncResult);
				webAsyncResult = (WebAsyncResult)result;
			}
			if (this.chunkedRead)
			{
				bool flag = num == 0;
				try
				{
					this.chunkStream.WriteAndReadBack(webAsyncResult.Buffer, webAsyncResult.Offset, webAsyncResult.Size, ref num);
					if (!flag && num == 0 && this.chunkStream.WantMore)
					{
						num = this.EnsureRead(webAsyncResult.Buffer, webAsyncResult.Offset, webAsyncResult.Size);
					}
				}
				catch (Exception ex)
				{
					if (ex is WebException)
					{
						throw ex;
					}
					throw new WebException("Invalid chunked data.", ex, WebExceptionStatus.ServerProtocolViolation, null);
				}
				if ((flag || num == 0) && this.chunkStream.ChunkLeft != 0)
				{
					this.HandleError(WebExceptionStatus.ReceiveFailure, null, "chunked EndRead");
					throw new WebException("Read error", null, WebExceptionStatus.ReceiveFailure, null);
				}
			}
			return (num == 0) ? (-1) : num;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00012F8C File Offset: 0x0001118C
		private int EnsureRead(byte[] buffer, int offset, int size)
		{
			byte[] array = null;
			int num = 0;
			while (num == 0 && this.chunkStream.WantMore)
			{
				int num2 = this.chunkStream.ChunkLeft;
				if (num2 <= 0)
				{
					num2 = 1024;
				}
				else if (num2 > 16384)
				{
					num2 = 16384;
				}
				if (array == null || array.Length < num2)
				{
					array = new byte[num2];
				}
				int num3 = this.nstream.Read(array, 0, num2);
				if (num3 <= 0)
				{
					return 0;
				}
				this.chunkStream.Write(array, 0, num3);
				num += this.chunkStream.Read(buffer, offset + num, size - num);
			}
			return num;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001303C File Offset: 0x0001123C
		private bool CompleteChunkedRead()
		{
			if (!this.chunkedRead || this.chunkStream == null)
			{
				return true;
			}
			while (this.chunkStream.WantMore)
			{
				int num = this.nstream.Read(this.buffer, 0, this.buffer.Length);
				if (num <= 0)
				{
					return false;
				}
				this.chunkStream.Write(this.buffer, 0, num);
			}
			return true;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000130B0 File Offset: 0x000112B0
		internal IAsyncResult BeginWrite(HttpWebRequest request, byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(global::System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					return null;
				}
			}
			IAsyncResult asyncResult = null;
			try
			{
				asyncResult = this.nstream.BeginWrite(buffer, offset, size, cb, state);
			}
			catch (Exception)
			{
				this.status = WebExceptionStatus.SendFailure;
				throw;
			}
			return asyncResult;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00013154 File Offset: 0x00011354
		internal void EndWrite2(HttpWebRequest request, IAsyncResult result)
		{
			if (request.FinishedReading)
			{
				return;
			}
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(global::System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					throw new ObjectDisposedException(typeof(global::System.Net.Sockets.NetworkStream).FullName);
				}
			}
			try
			{
				this.nstream.EndWrite(result);
			}
			catch (Exception ex)
			{
				this.status = WebExceptionStatus.SendFailure;
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				throw;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00013218 File Offset: 0x00011418
		internal bool EndWrite(HttpWebRequest request, IAsyncResult result)
		{
			if (request.FinishedReading)
			{
				return true;
			}
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(global::System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					throw new ObjectDisposedException(typeof(global::System.Net.Sockets.NetworkStream).FullName);
				}
			}
			bool flag;
			try
			{
				this.nstream.EndWrite(result);
				flag = true;
			}
			catch
			{
				this.status = WebExceptionStatus.SendFailure;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000132D8 File Offset: 0x000114D8
		internal int Read(HttpWebRequest request, byte[] buffer, int offset, int size)
		{
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(global::System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					return 0;
				}
			}
			int num = 0;
			try
			{
				bool flag = false;
				if (!this.chunkedRead)
				{
					num = this.nstream.Read(buffer, offset, size);
					flag = num == 0;
				}
				if (this.chunkedRead)
				{
					try
					{
						this.chunkStream.WriteAndReadBack(buffer, offset, size, ref num);
						if (!flag && num == 0 && this.chunkStream.WantMore)
						{
							num = this.EnsureRead(buffer, offset, size);
						}
					}
					catch (Exception ex)
					{
						this.HandleError(WebExceptionStatus.ReceiveFailure, ex, "chunked Read1");
						throw;
					}
					if ((flag || num == 0) && this.chunkStream.WantMore)
					{
						this.HandleError(WebExceptionStatus.ReceiveFailure, null, "chunked Read2");
						throw new WebException("Read error", null, WebExceptionStatus.ReceiveFailure, null);
					}
				}
			}
			catch (Exception ex2)
			{
				this.HandleError(WebExceptionStatus.ReceiveFailure, ex2, "Read");
			}
			return num;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00013430 File Offset: 0x00011630
		internal bool Write(HttpWebRequest request, byte[] buffer, int offset, int size, ref string err_msg)
		{
			err_msg = null;
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(global::System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					return false;
				}
			}
			try
			{
				this.nstream.Write(buffer, offset, size);
				if (this.ssl && !this.certsAvailable)
				{
					this.GetCertificates();
				}
			}
			catch (Exception ex)
			{
				err_msg = ex.Message;
				WebExceptionStatus webExceptionStatus = WebExceptionStatus.SendFailure;
				string text = "Write: " + err_msg;
				if (ex is WebException)
				{
					this.HandleError(webExceptionStatus, ex, text);
					return false;
				}
				if (this.ssl && (bool)WebConnection.piTrustFailure.GetValue(this.nstream, null))
				{
					webExceptionStatus = WebExceptionStatus.TrustFailure;
					text = "Trust failure";
				}
				this.HandleError(webExceptionStatus, ex, text);
				return false;
			}
			return true;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001355C File Offset: 0x0001175C
		internal void Close(bool sendNext)
		{
			lock (this)
			{
				if (this.nstream != null)
				{
					try
					{
						this.nstream.Close();
					}
					catch
					{
					}
					this.nstream = null;
				}
				if (this.socket != null)
				{
					try
					{
						this.socket.Close();
					}
					catch
					{
					}
					this.socket = null;
				}
				this.busy = false;
				this.Data = new WebConnectionData();
				if (sendNext)
				{
					this.SendNext();
				}
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00013614 File Offset: 0x00011814
		private void Abort(object sender, EventArgs args)
		{
			lock (this)
			{
				Queue queue = this.queue;
				lock (queue)
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)sender;
					if (this.Data.request == httpWebRequest)
					{
						if (!httpWebRequest.FinishedReading)
						{
							this.status = WebExceptionStatus.RequestCanceled;
							this.Close(false);
							if (this.queue.Count > 0)
							{
								this.Data.request = (HttpWebRequest)this.queue.Dequeue();
								this.SendRequest(this.Data.request);
							}
						}
					}
					else
					{
						httpWebRequest.FinishedReading = true;
						httpWebRequest.SetResponseError(WebExceptionStatus.RequestCanceled, null, "User aborted");
						if (this.queue.Count > 0 && this.queue.Peek() == sender)
						{
							this.queue.Dequeue();
						}
						else if (this.queue.Count > 0)
						{
							object[] array = this.queue.ToArray();
							this.queue.Clear();
							for (int i = array.Length - 1; i >= 0; i--)
							{
								if (array[i] != sender)
								{
									this.queue.Enqueue(array[i]);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00013798 File Offset: 0x00011998
		internal void ResetNtlm()
		{
			this.ntlm_authenticated = false;
			this.ntlm_credentials = null;
			this.unsafe_sharing = false;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000137B0 File Offset: 0x000119B0
		internal bool Busy
		{
			get
			{
				bool flag;
				lock (this)
				{
					flag = this.busy;
				}
				return flag;
			}
		}

		// Token: 0x17000109 RID: 265
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x000137F0 File Offset: 0x000119F0
		internal HttpWebRequest PriorityRequest
		{
			set
			{
				this.priority_request = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x000137FC File Offset: 0x000119FC
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00013804 File Offset: 0x00011A04
		internal bool NtlmAuthenticated
		{
			get
			{
				return this.ntlm_authenticated;
			}
			set
			{
				this.ntlm_authenticated = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00013810 File Offset: 0x00011A10
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x00013818 File Offset: 0x00011A18
		internal NetworkCredential NtlmCredential
		{
			get
			{
				return this.ntlm_credentials;
			}
			set
			{
				this.ntlm_credentials = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00013824 File Offset: 0x00011A24
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0001382C File Offset: 0x00011A2C
		internal bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_sharing;
			}
			set
			{
				this.unsafe_sharing = value;
			}
		}

		// Token: 0x040002C9 RID: 713
		private ServicePoint sPoint;

		// Token: 0x040002CA RID: 714
		private Stream nstream;

		// Token: 0x040002CB RID: 715
		private global::System.Net.Sockets.Socket socket;

		// Token: 0x040002CC RID: 716
		private object socketLock = new object();

		// Token: 0x040002CD RID: 717
		private WebExceptionStatus status;

		// Token: 0x040002CE RID: 718
		private WaitCallback initConn;

		// Token: 0x040002CF RID: 719
		private bool keepAlive;

		// Token: 0x040002D0 RID: 720
		private byte[] buffer;

		// Token: 0x040002D1 RID: 721
		private static AsyncCallback readDoneDelegate = new AsyncCallback(WebConnection.ReadDone);

		// Token: 0x040002D2 RID: 722
		private EventHandler abortHandler;

		// Token: 0x040002D3 RID: 723
		private WebConnection.AbortHelper abortHelper;

		// Token: 0x040002D4 RID: 724
		private ReadState readState;

		// Token: 0x040002D5 RID: 725
		internal WebConnectionData Data;

		// Token: 0x040002D6 RID: 726
		private bool chunkedRead;

		// Token: 0x040002D7 RID: 727
		private ChunkStream chunkStream;

		// Token: 0x040002D8 RID: 728
		private Queue queue;

		// Token: 0x040002D9 RID: 729
		private bool reused;

		// Token: 0x040002DA RID: 730
		private int position;

		// Token: 0x040002DB RID: 731
		private bool busy;

		// Token: 0x040002DC RID: 732
		private HttpWebRequest priority_request;

		// Token: 0x040002DD RID: 733
		private NetworkCredential ntlm_credentials;

		// Token: 0x040002DE RID: 734
		private bool ntlm_authenticated;

		// Token: 0x040002DF RID: 735
		private bool unsafe_sharing;

		// Token: 0x040002E0 RID: 736
		private bool ssl;

		// Token: 0x040002E1 RID: 737
		private bool certsAvailable;

		// Token: 0x040002E2 RID: 738
		private Exception connect_exception;

		// Token: 0x040002E3 RID: 739
		private static object classLock = new object();

		// Token: 0x040002E4 RID: 740
		private static Type sslStream;

		// Token: 0x040002E5 RID: 741
		private static PropertyInfo piClient;

		// Token: 0x040002E6 RID: 742
		private static PropertyInfo piServer;

		// Token: 0x040002E7 RID: 743
		private static PropertyInfo piTrustFailure;

		// Token: 0x040002E8 RID: 744
		private static MethodInfo method_GetSecurityPolicyFromNonMainThread;

		// Token: 0x020000A7 RID: 167
		private class AbortHelper
		{
			// Token: 0x06000435 RID: 1077 RVA: 0x00013840 File Offset: 0x00011A40
			public void Abort(object sender, EventArgs args)
			{
				WebConnection webConnection = ((HttpWebRequest)sender).WebConnection;
				if (webConnection == null)
				{
					webConnection = this.Connection;
				}
				webConnection.Abort(sender, args);
			}

			// Token: 0x040002E9 RID: 745
			public WebConnection Connection;
		}
	}
}
