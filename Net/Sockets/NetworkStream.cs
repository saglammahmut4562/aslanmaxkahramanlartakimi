using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x02000096 RID: 150
	public class NetworkStream : Stream, IDisposable
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0000F694 File Offset: 0x0000D894
		public NetworkStream(Socket socket, bool owns_socket)
			: this(socket, FileAccess.ReadWrite, owns_socket)
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		public NetworkStream(Socket socket, FileAccess access, bool owns_socket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket is null");
			}
			if (socket.SocketType != SocketType.Stream)
			{
				throw new ArgumentException("Socket is not of type Stream", "socket");
			}
			if (!socket.Connected)
			{
				throw new IOException("Not connected");
			}
			if (!socket.Blocking)
			{
				throw new IOException("Operation not allowed on a non-blocking socket.");
			}
			this.socket = socket;
			this.owns_socket = owns_socket;
			this.access = access;
			this.readable = this.CanRead;
			this.writeable = this.CanWrite;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000F73C File Offset: 0x0000D93C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000F74C File Offset: 0x0000D94C
		public override bool CanRead
		{
			get
			{
				return this.access == FileAccess.ReadWrite || this.access == FileAccess.Read;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000F768 File Offset: 0x0000D968
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000F76C File Offset: 0x0000D96C
		public override bool CanWrite
		{
			get
			{
				return this.access == FileAccess.ReadWrite || this.access == FileAccess.Write;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000F788 File Offset: 0x0000D988
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000F790 File Offset: 0x0000D990
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000F798 File Offset: 0x0000D998
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
		public override int ReadTimeout
		{
			get
			{
				return this.socket.ReceiveTimeout;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
		public override int WriteTimeout
		{
			get
			{
				return this.socket.SendTimeout;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer is null");
			}
			int num = buffer.Length;
			if (offset < 0 || offset > num)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (size < 0 || offset + size > num)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.socket.BeginReceive(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch (Exception ex)
			{
				throw new IOException("BeginReceive failure", ex);
			}
			return asyncResult;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000F858 File Offset: 0x0000DA58
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer is null");
			}
			int num = buffer.Length;
			if (offset < 0 || offset > num)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (size < 0 || offset + size > num)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.socket.BeginSend(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch
			{
				throw new IOException("BeginWrite failure");
			}
			return asyncResult;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		~NetworkStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000F920 File Offset: 0x0000DB20
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.owns_socket)
			{
				Socket socket = this.socket;
				if (socket != null)
				{
					socket.Close();
				}
			}
			this.socket = null;
			this.access = (FileAccess)0;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000F96C File Offset: 0x0000DB6C
		public override int EndRead(IAsyncResult ar)
		{
			this.CheckDisposed();
			if (ar == null)
			{
				throw new ArgumentNullException("async result is null");
			}
			int num;
			try
			{
				num = this.socket.EndReceive(ar);
			}
			catch (Exception ex)
			{
				throw new IOException("EndRead failure", ex);
			}
			return num;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
		public override void EndWrite(IAsyncResult ar)
		{
			this.CheckDisposed();
			if (ar == null)
			{
				throw new ArgumentNullException("async result is null");
			}
			try
			{
				this.socket.EndSend(ar);
			}
			catch (Exception ex)
			{
				throw new IOException("EndWrite failure", ex);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000FA20 File Offset: 0x0000DC20
		public override void Flush()
		{
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000FA24 File Offset: 0x0000DC24
		public override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer is null");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			int num;
			try
			{
				num = this.socket.Receive(buffer, offset, size, SocketFlags.None);
			}
			catch (Exception ex)
			{
				throw new IOException("Read failure", ex);
			}
			return num;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000FAC0 File Offset: 0x0000DCC0
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset exceeds the size of buffer");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("offset+size exceeds the size of buffer");
			}
			try
			{
				int num = 0;
				while (size - num > 0)
				{
					num += this.socket.Send(buffer, offset + num, size - num, SocketFlags.None);
				}
			}
			catch (Exception ex)
			{
				throw new IOException("Write failure", ex);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000FB74 File Offset: 0x0000DD74
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x040001DA RID: 474
		private FileAccess access;

		// Token: 0x040001DB RID: 475
		private Socket socket;

		// Token: 0x040001DC RID: 476
		private bool owns_socket;

		// Token: 0x040001DD RID: 477
		private bool readable;

		// Token: 0x040001DE RID: 478
		private bool writeable;

		// Token: 0x040001DF RID: 479
		private bool disposed;
	}
}
