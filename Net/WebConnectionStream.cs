using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace System.Net
{
	// Token: 0x020000AA RID: 170
	internal class WebConnectionStream : Stream
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x00013B90 File Offset: 0x00011D90
		public WebConnectionStream(WebConnection cnc)
		{
			this.isRead = true;
			this.pending = new ManualResetEvent(true);
			this.request = cnc.Data.request;
			this.read_timeout = this.request.ReadWriteTimeout;
			this.write_timeout = this.read_timeout;
			this.cnc = cnc;
			string text = cnc.Data.Headers["Transfer-Encoding"];
			bool flag = text != null && text.ToLower().IndexOf("chunked") != -1;
			string text2 = cnc.Data.Headers["Content-Length"];
			if (!flag && text2 != null && text2 != string.Empty)
			{
				try
				{
					this.contentLength = int.Parse(text2);
					if (this.contentLength == 0 && !this.IsNtlmAuth())
					{
						this.ReadAll();
					}
				}
				catch
				{
					this.contentLength = int.MaxValue;
				}
			}
			else
			{
				this.contentLength = int.MaxValue;
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00013CBC File Offset: 0x00011EBC
		public WebConnectionStream(WebConnection cnc, HttpWebRequest request)
		{
			this.read_timeout = request.ReadWriteTimeout;
			this.write_timeout = this.read_timeout;
			this.isRead = false;
			this.cnc = cnc;
			this.request = request;
			this.allowBuffering = request.InternalAllowBuffering;
			this.sendChunked = request.SendChunked;
			if (this.sendChunked)
			{
				this.pending = new ManualResetEvent(true);
			}
			else if (this.allowBuffering)
			{
				this.writeBuffer = new MemoryStream();
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00013D6C File Offset: 0x00011F6C
		private bool IsNtlmAuth()
		{
			bool flag = this.request.Proxy != null && !this.request.Proxy.IsBypassed(this.request.Address);
			string text = ((!flag) ? "WWW-Authenticate" : "Proxy-Authenticate");
			string text2 = this.cnc.Data.Headers[text];
			return text2 != null && text2.IndexOf("NTLM") != -1;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00013DF4 File Offset: 0x00011FF4
		internal void CheckResponseInBuffer()
		{
			if (this.contentLength > 0 && this.readBufferSize - this.readBufferOffset >= this.contentLength && !this.IsNtlmAuth())
			{
				this.ReadAll();
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00013E2C File Offset: 0x0001202C
		internal WebConnection Connection
		{
			get
			{
				return this.cnc;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00013E34 File Offset: 0x00012034
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00013E3C File Offset: 0x0001203C
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00013E44 File Offset: 0x00012044
		internal bool CompleteRequestWritten
		{
			get
			{
				return this.complete_request_written;
			}
		}

		// Token: 0x17000112 RID: 274
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x00013E4C File Offset: 0x0001204C
		internal bool SendChunked
		{
			set
			{
				this.sendChunked = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x00013E58 File Offset: 0x00012058
		internal byte[] ReadBuffer
		{
			set
			{
				this.readBuffer = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00013E64 File Offset: 0x00012064
		internal int ReadBufferOffset
		{
			set
			{
				this.readBufferOffset = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00013E70 File Offset: 0x00012070
		internal int ReadBufferSize
		{
			set
			{
				this.readBufferSize = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00013E7C File Offset: 0x0001207C
		internal byte[] WriteBuffer
		{
			get
			{
				return this.writeBuffer.GetBuffer();
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00013E8C File Offset: 0x0001208C
		internal int WriteBufferLength
		{
			get
			{
				return (this.writeBuffer == null) ? (-1) : ((int)this.writeBuffer.Length);
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00013EAC File Offset: 0x000120AC
		internal void ForceCompletion()
		{
			if (!this.nextReadCalled)
			{
				if (this.contentLength == 2147483647)
				{
					this.contentLength = 0;
				}
				this.nextReadCalled = true;
				this.cnc.NextRead();
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00013EE4 File Offset: 0x000120E4
		internal void CheckComplete()
		{
			if (!this.nextReadCalled && this.readBufferSize - this.readBufferOffset == this.contentLength)
			{
				this.nextReadCalled = true;
				this.cnc.NextRead();
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00013F28 File Offset: 0x00012128
		internal void ReadAll()
		{
			if (!this.isRead || this.read_eof || this.totalRead >= this.contentLength || this.nextReadCalled)
			{
				if (this.isRead && !this.nextReadCalled)
				{
					this.nextReadCalled = true;
					this.cnc.NextRead();
				}
				return;
			}
			this.pending.WaitOne();
			object obj = this.locker;
			lock (obj)
			{
				if (this.totalRead >= this.contentLength)
				{
					return;
				}
				int num = this.readBufferSize - this.readBufferOffset;
				byte[] array2;
				int num3;
				if (this.contentLength == 2147483647)
				{
					MemoryStream memoryStream = new MemoryStream();
					byte[] array = null;
					if (this.readBuffer != null && num > 0)
					{
						memoryStream.Write(this.readBuffer, this.readBufferOffset, num);
						if (this.readBufferSize >= 8192)
						{
							array = this.readBuffer;
						}
					}
					if (array == null)
					{
						array = new byte[8192];
					}
					int num2;
					while ((num2 = this.cnc.Read(this.request, array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, num2);
					}
					array2 = memoryStream.GetBuffer();
					num3 = (int)memoryStream.Length;
					this.contentLength = num3;
				}
				else
				{
					num3 = this.contentLength - this.totalRead;
					array2 = new byte[num3];
					if (this.readBuffer != null && num > 0)
					{
						if (num > num3)
						{
							num = num3;
						}
						Buffer.BlockCopy(this.readBuffer, this.readBufferOffset, array2, 0, num);
					}
					int num4 = num3 - num;
					int num5 = -1;
					while (num4 > 0 && num5 != 0)
					{
						num5 = this.cnc.Read(this.request, array2, num, num4);
						num4 -= num5;
						num += num5;
					}
				}
				this.readBuffer = array2;
				this.readBufferOffset = 0;
				this.readBufferSize = num3;
				this.totalRead = 0;
				this.nextReadCalled = true;
			}
			this.cnc.NextRead();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00014164 File Offset: 0x00012364
		private void WriteCallbackWrapper(IAsyncResult r)
		{
			WebAsyncResult webAsyncResult = r as WebAsyncResult;
			if (webAsyncResult != null && webAsyncResult.AsyncWriteAll)
			{
				return;
			}
			if (r.AsyncState != null)
			{
				webAsyncResult = (WebAsyncResult)r.AsyncState;
				webAsyncResult.InnerAsyncResult = r;
				webAsyncResult.DoCallback();
			}
			else
			{
				this.EndWrite(r);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000141BC File Offset: 0x000123BC
		private void ReadCallbackWrapper(IAsyncResult r)
		{
			if (r.AsyncState != null)
			{
				WebAsyncResult webAsyncResult = (WebAsyncResult)r.AsyncState;
				webAsyncResult.InnerAsyncResult = r;
				webAsyncResult.DoCallback();
			}
			else
			{
				this.EndRead(r);
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000141FC File Offset: 0x000123FC
		public override int Read(byte[] buffer, int offset, int size)
		{
			AsyncCallback asyncCallback = new AsyncCallback(this.ReadCallbackWrapper);
			WebAsyncResult webAsyncResult = (WebAsyncResult)this.BeginRead(buffer, offset, size, asyncCallback, null);
			if (!webAsyncResult.IsCompleted && !webAsyncResult.WaitUntilComplete(this.ReadTimeout, false))
			{
				this.nextReadCalled = true;
				this.cnc.Close(true);
				throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
			}
			return this.EndRead(webAsyncResult);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001426C File Offset: 0x0001246C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			if (!this.isRead)
			{
				throw new NotSupportedException("this stream does not allow reading");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			object obj = this.locker;
			lock (obj)
			{
				this.pendingReads++;
				this.pending.Reset();
			}
			WebAsyncResult webAsyncResult = new WebAsyncResult(cb, state, buffer, offset, size);
			if (this.totalRead >= this.contentLength)
			{
				webAsyncResult.SetCompleted(true, -1);
				webAsyncResult.DoCallback();
				return webAsyncResult;
			}
			int num2 = this.readBufferSize - this.readBufferOffset;
			if (num2 > 0)
			{
				int num3 = ((num2 <= size) ? num2 : size);
				Buffer.BlockCopy(this.readBuffer, this.readBufferOffset, buffer, offset, num3);
				this.readBufferOffset += num3;
				offset += num3;
				size -= num3;
				this.totalRead += num3;
				if (size == 0 || this.totalRead >= this.contentLength)
				{
					webAsyncResult.SetCompleted(true, num3);
					webAsyncResult.DoCallback();
					return webAsyncResult;
				}
				webAsyncResult.NBytes = num3;
			}
			if (cb != null)
			{
				cb = new AsyncCallback(this.ReadCallbackWrapper);
			}
			if (this.contentLength != 2147483647 && this.contentLength - this.totalRead < size)
			{
				size = this.contentLength - this.totalRead;
			}
			if (!this.read_eof)
			{
				webAsyncResult.InnerAsyncResult = this.cnc.BeginRead(this.request, buffer, offset, size, cb, webAsyncResult);
			}
			else
			{
				webAsyncResult.SetCompleted(true, webAsyncResult.NBytes);
				webAsyncResult.DoCallback();
			}
			return webAsyncResult;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00014468 File Offset: 0x00012668
		public override int EndRead(IAsyncResult r)
		{
			WebAsyncResult webAsyncResult = (WebAsyncResult)r;
			if (webAsyncResult.EndCalled)
			{
				int nbytes = webAsyncResult.NBytes;
				return (nbytes < 0) ? 0 : nbytes;
			}
			webAsyncResult.EndCalled = true;
			if (!webAsyncResult.IsCompleted)
			{
				int num = -1;
				try
				{
					num = this.cnc.EndRead(this.request, webAsyncResult);
				}
				catch (Exception ex)
				{
					object obj = this.locker;
					lock (obj)
					{
						this.pendingReads--;
						if (this.pendingReads == 0)
						{
							this.pending.Set();
						}
					}
					this.nextReadCalled = true;
					this.cnc.Close(true);
					webAsyncResult.SetCompleted(false, ex);
					webAsyncResult.DoCallback();
					throw;
				}
				if (num < 0)
				{
					num = 0;
					this.read_eof = true;
				}
				this.totalRead += num;
				webAsyncResult.SetCompleted(false, num + webAsyncResult.NBytes);
				webAsyncResult.DoCallback();
				if (num == 0)
				{
					this.contentLength = this.totalRead;
				}
			}
			object obj2 = this.locker;
			lock (obj2)
			{
				this.pendingReads--;
				if (this.pendingReads == 0)
				{
					this.pending.Set();
				}
			}
			if (this.totalRead >= this.contentLength && !this.nextReadCalled)
			{
				this.ReadAll();
			}
			int nbytes2 = webAsyncResult.NBytes;
			return (nbytes2 < 0) ? 0 : nbytes2;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001461C File Offset: 0x0001281C
		private void WriteRequestAsyncCB(IAsyncResult r)
		{
			WebAsyncResult webAsyncResult = (WebAsyncResult)r.AsyncState;
			try
			{
				this.cnc.EndWrite2(this.request, r);
				webAsyncResult.SetCompleted(false, 0);
				if (!this.initRead)
				{
					this.initRead = true;
					WebConnection.InitRead(this.cnc);
				}
			}
			catch (Exception ex)
			{
				this.KillBuffer();
				this.nextReadCalled = true;
				this.cnc.Close(true);
				if (ex is global::System.Net.Sockets.SocketException)
				{
					ex = new IOException("Error writing request", ex);
				}
				webAsyncResult.SetCompleted(false, ex);
			}
			this.complete_request_written = true;
			webAsyncResult.DoCallback();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000146CC File Offset: 0x000128CC
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			if (this.request.Aborted)
			{
				throw new WebException("The request was canceled.", null, WebExceptionStatus.RequestCanceled);
			}
			if (this.isRead)
			{
				throw new NotSupportedException("this stream does not allow writing");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (this.sendChunked)
			{
				object obj = this.locker;
				lock (obj)
				{
					this.pendingWrites++;
					this.pending.Reset();
				}
			}
			WebAsyncResult webAsyncResult = new WebAsyncResult(cb, state);
			if (!this.sendChunked)
			{
				this.CheckWriteOverflow(this.request.ContentLength, this.totalWritten, (long)size);
			}
			if (this.allowBuffering && !this.sendChunked)
			{
				if (this.writeBuffer == null)
				{
					this.writeBuffer = new MemoryStream();
				}
				this.writeBuffer.Write(buffer, offset, size);
				this.totalWritten += (long)size;
				if (this.request.ContentLength > 0L && this.totalWritten == this.request.ContentLength)
				{
					try
					{
						webAsyncResult.AsyncWriteAll = true;
						webAsyncResult.InnerAsyncResult = this.WriteRequestAsync(new AsyncCallback(this.WriteRequestAsyncCB), webAsyncResult);
						if (webAsyncResult.InnerAsyncResult == null)
						{
							if (!webAsyncResult.IsCompleted)
							{
								webAsyncResult.SetCompleted(true, 0);
							}
							webAsyncResult.DoCallback();
						}
					}
					catch (Exception ex)
					{
						webAsyncResult.SetCompleted(true, ex);
						webAsyncResult.DoCallback();
					}
				}
				else
				{
					webAsyncResult.SetCompleted(true, 0);
					webAsyncResult.DoCallback();
				}
				return webAsyncResult;
			}
			AsyncCallback asyncCallback = null;
			if (cb != null)
			{
				asyncCallback = new AsyncCallback(this.WriteCallbackWrapper);
			}
			if (this.sendChunked)
			{
				this.WriteRequest();
				string text = string.Format("{0:X}\r\n", size);
				byte[] bytes = Encoding.ASCII.GetBytes(text);
				int num2 = 2 + size + bytes.Length;
				byte[] array = new byte[num2];
				Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
				Buffer.BlockCopy(buffer, offset, array, bytes.Length, size);
				Buffer.BlockCopy(WebConnectionStream.crlf, 0, array, bytes.Length + size, WebConnectionStream.crlf.Length);
				buffer = array;
				offset = 0;
				size = num2;
			}
			webAsyncResult.InnerAsyncResult = this.cnc.BeginWrite(this.request, buffer, offset, size, asyncCallback, webAsyncResult);
			this.totalWritten += (long)size;
			return webAsyncResult;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001498C File Offset: 0x00012B8C
		private void CheckWriteOverflow(long contentLength, long totalWritten, long size)
		{
			if (contentLength == -1L)
			{
				return;
			}
			long num = contentLength - totalWritten;
			if (size > num)
			{
				this.KillBuffer();
				this.nextReadCalled = true;
				this.cnc.Close(true);
				throw new ProtocolViolationException("The number of bytes to be written is greater than the specified ContentLength.");
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000149D4 File Offset: 0x00012BD4
		public override void EndWrite(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			WebAsyncResult webAsyncResult = r as WebAsyncResult;
			if (webAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult");
			}
			if (webAsyncResult.EndCalled)
			{
				return;
			}
			webAsyncResult.EndCalled = true;
			if (webAsyncResult.AsyncWriteAll)
			{
				webAsyncResult.WaitUntilComplete();
				if (webAsyncResult.GotException)
				{
					throw webAsyncResult.Exception;
				}
				return;
			}
			else
			{
				if (this.allowBuffering && !this.sendChunked)
				{
					return;
				}
				if (webAsyncResult.GotException)
				{
					throw webAsyncResult.Exception;
				}
				try
				{
					this.cnc.EndWrite2(this.request, webAsyncResult.InnerAsyncResult);
					webAsyncResult.SetCompleted(false, 0);
					webAsyncResult.DoCallback();
				}
				catch (Exception ex)
				{
					webAsyncResult.SetCompleted(false, ex);
					webAsyncResult.DoCallback();
					throw;
				}
				finally
				{
					if (this.sendChunked)
					{
						object obj = this.locker;
						lock (obj)
						{
							this.pendingWrites--;
							if (this.pendingWrites == 0)
							{
								this.pending.Set();
							}
						}
					}
				}
				return;
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00014B1C File Offset: 0x00012D1C
		public override void Write(byte[] buffer, int offset, int size)
		{
			AsyncCallback asyncCallback = new AsyncCallback(this.WriteCallbackWrapper);
			WebAsyncResult webAsyncResult = (WebAsyncResult)this.BeginWrite(buffer, offset, size, asyncCallback, null);
			if (!webAsyncResult.IsCompleted && !webAsyncResult.WaitUntilComplete(this.WriteTimeout, false))
			{
				this.KillBuffer();
				this.nextReadCalled = true;
				this.cnc.Close(true);
				throw new IOException("Write timed out.");
			}
			this.EndWrite(webAsyncResult);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00014B90 File Offset: 0x00012D90
		public override void Flush()
		{
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00014B94 File Offset: 0x00012D94
		internal void SetHeaders(byte[] buffer)
		{
			if (this.headersSent)
			{
				return;
			}
			this.headers = buffer;
			long num = this.request.ContentLength;
			string method = this.request.Method;
			bool flag = method == "GET" || method == "CONNECT" || method == "HEAD" || method == "TRACE" || method == "DELETE";
			if (this.sendChunked || num > -1L || flag)
			{
				this.WriteHeaders();
				if (!this.initRead)
				{
					this.initRead = true;
					WebConnection.InitRead(this.cnc);
				}
				if (!this.sendChunked && num == 0L)
				{
					this.requestWritten = true;
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00014C70 File Offset: 0x00012E70
		internal bool RequestWritten
		{
			get
			{
				return this.requestWritten;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00014C78 File Offset: 0x00012E78
		private IAsyncResult WriteRequestAsync(AsyncCallback cb, object state)
		{
			this.requestWritten = true;
			byte[] buffer = this.writeBuffer.GetBuffer();
			int num = (int)this.writeBuffer.Length;
			IAsyncResult asyncResult2;
			if (num > 0)
			{
				IAsyncResult asyncResult = this.cnc.BeginWrite(this.request, buffer, 0, num, cb, state);
				asyncResult2 = asyncResult;
			}
			else
			{
				asyncResult2 = null;
			}
			return asyncResult2;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00014CCC File Offset: 0x00012ECC
		private void WriteHeaders()
		{
			if (this.headersSent)
			{
				return;
			}
			this.headersSent = true;
			string text = null;
			if (!this.cnc.Write(this.request, this.headers, 0, this.headers.Length, ref text))
			{
				throw new WebException("Error writing request: " + text, null, WebExceptionStatus.SendFailure, null);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00014D2C File Offset: 0x00012F2C
		internal void WriteRequest()
		{
			if (this.requestWritten)
			{
				return;
			}
			this.requestWritten = true;
			if (this.sendChunked)
			{
				return;
			}
			if (!this.allowBuffering || this.writeBuffer == null)
			{
				return;
			}
			byte[] buffer = this.writeBuffer.GetBuffer();
			int num = (int)this.writeBuffer.Length;
			if (this.request.ContentLength != -1L && this.request.ContentLength < (long)num)
			{
				this.nextReadCalled = true;
				this.cnc.Close(true);
				throw new WebException("Specified Content-Length is less than the number of bytes to write", null, WebExceptionStatus.ServerProtocolViolation, null);
			}
			if (!this.headersSent)
			{
				string method = this.request.Method;
				if (!(method == "GET") && !(method == "CONNECT") && !(method == "HEAD") && !(method == "TRACE") && !(method == "DELETE"))
				{
					this.request.InternalContentLength = (long)num;
				}
				this.request.SendRequestHeaders(true);
			}
			this.WriteHeaders();
			if (this.cnc.Data.StatusCode != 0 && this.cnc.Data.StatusCode != 100)
			{
				return;
			}
			IAsyncResult asyncResult = null;
			if (num > 0)
			{
				asyncResult = this.cnc.BeginWrite(this.request, buffer, 0, num, null, null);
			}
			if (!this.initRead)
			{
				this.initRead = true;
				WebConnection.InitRead(this.cnc);
			}
			if (num > 0)
			{
				this.complete_request_written = this.cnc.EndWrite(this.request, asyncResult);
			}
			else
			{
				this.complete_request_written = true;
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00014EF4 File Offset: 0x000130F4
		internal void InternalClose()
		{
			this.disposed = true;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00014F00 File Offset: 0x00013100
		public override void Close()
		{
			if (this.sendChunked)
			{
				if (this.disposed)
				{
					return;
				}
				this.disposed = true;
				this.pending.WaitOne();
				byte[] bytes = Encoding.ASCII.GetBytes("0\r\n\r\n");
				string text = null;
				this.cnc.Write(this.request, bytes, 0, bytes.Length, ref text);
				return;
			}
			else
			{
				if (this.isRead)
				{
					if (!this.nextReadCalled)
					{
						this.CheckComplete();
						if (!this.nextReadCalled)
						{
							this.nextReadCalled = true;
							this.cnc.Close(true);
						}
					}
					return;
				}
				if (!this.allowBuffering)
				{
					this.complete_request_written = true;
					if (!this.initRead)
					{
						this.initRead = true;
						WebConnection.InitRead(this.cnc);
					}
					return;
				}
				if (this.disposed || this.requestWritten)
				{
					return;
				}
				long num = this.request.ContentLength;
				if (!this.sendChunked && num != -1L && this.totalWritten != num)
				{
					IOException ex = new IOException("Cannot close the stream until all bytes are written");
					this.nextReadCalled = true;
					this.cnc.Close(true);
					throw new WebException("Request was cancelled.", ex, WebExceptionStatus.RequestCanceled);
				}
				this.WriteRequest();
				this.disposed = true;
				return;
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00015048 File Offset: 0x00013248
		internal void KillBuffer()
		{
			this.writeBuffer = null;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00015054 File Offset: 0x00013254
		public override long Seek(long a, SeekOrigin b)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001505C File Offset: 0x0001325C
		public override void SetLength(long a)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00015064 File Offset: 0x00013264
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00015068 File Offset: 0x00013268
		public override bool CanRead
		{
			get
			{
				return !this.disposed && this.isRead;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00015080 File Offset: 0x00013280
		public override bool CanWrite
		{
			get
			{
				return !this.disposed && !this.isRead;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001509C File Offset: 0x0001329C
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000150A4 File Offset: 0x000132A4
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x000150AC File Offset: 0x000132AC
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040002F6 RID: 758
		private static byte[] crlf = new byte[] { 13, 10 };

		// Token: 0x040002F7 RID: 759
		private bool isRead;

		// Token: 0x040002F8 RID: 760
		private WebConnection cnc;

		// Token: 0x040002F9 RID: 761
		private HttpWebRequest request;

		// Token: 0x040002FA RID: 762
		private byte[] readBuffer;

		// Token: 0x040002FB RID: 763
		private int readBufferOffset;

		// Token: 0x040002FC RID: 764
		private int readBufferSize;

		// Token: 0x040002FD RID: 765
		private int contentLength;

		// Token: 0x040002FE RID: 766
		private int totalRead;

		// Token: 0x040002FF RID: 767
		private long totalWritten;

		// Token: 0x04000300 RID: 768
		private bool nextReadCalled;

		// Token: 0x04000301 RID: 769
		private int pendingReads;

		// Token: 0x04000302 RID: 770
		private int pendingWrites;

		// Token: 0x04000303 RID: 771
		private ManualResetEvent pending;

		// Token: 0x04000304 RID: 772
		private bool allowBuffering;

		// Token: 0x04000305 RID: 773
		private bool sendChunked;

		// Token: 0x04000306 RID: 774
		private MemoryStream writeBuffer;

		// Token: 0x04000307 RID: 775
		private bool requestWritten;

		// Token: 0x04000308 RID: 776
		private byte[] headers;

		// Token: 0x04000309 RID: 777
		private bool disposed;

		// Token: 0x0400030A RID: 778
		private bool headersSent;

		// Token: 0x0400030B RID: 779
		private object locker = new object();

		// Token: 0x0400030C RID: 780
		private bool initRead;

		// Token: 0x0400030D RID: 781
		private bool read_eof;

		// Token: 0x0400030E RID: 782
		private bool complete_request_written;

		// Token: 0x0400030F RID: 783
		private int read_timeout;

		// Token: 0x04000310 RID: 784
		private int write_timeout;
	}
}
