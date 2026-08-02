using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x02000099 RID: 153
	public class Socket : IDisposable
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0000FB94 File Offset: 0x0000DD94
		public Socket(AddressFamily family, SocketType type, ProtocolType proto)
		{
			if (family == AddressFamily.Unspecified)
			{
				throw new ArgumentException("family");
			}
			this.address_family = family;
			this.socket_type = type;
			this.protocol_type = proto;
			int num;
			this.socket = this.Socket_internal(family, type, proto, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000FC28 File Offset: 0x0000DE28
		static Socket()
		{
			Socket.CheckProtocolSupport();
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000FC3C File Offset: 0x0000DE3C
		public SocketType SocketType
		{
			get
			{
				return this.socket_type;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000FC44 File Offset: 0x0000DE44
		public int SendTimeout
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000FC84 File Offset: 0x0000DE84
		public int ReceiveTimeout
		{
			get
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				return (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socket_flags, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Queue queue = this.readQ;
			Socket.SocketAsyncResult socketAsyncResult;
			lock (queue)
			{
				socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.Receive);
				socketAsyncResult.Buffer = buffer;
				socketAsyncResult.Offset = offset;
				socketAsyncResult.Size = size;
				socketAsyncResult.SockFlags = socket_flags;
				this.readQ.Enqueue(socketAsyncResult);
				if (this.readQ.Count == 1)
				{
					Socket.Worker worker = new Socket.Worker(socketAsyncResult);
					Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(worker.Receive);
					socketAsyncCall.BeginInvoke(null, socketAsyncResult);
				}
			}
			return socketAsyncResult;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000FDD0 File Offset: 0x0000DFD0
		public IAsyncResult BeginSend(byte[] buffer, int offset, int size, SocketFlags socket_flags, AsyncCallback callback, object state)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "offset must be >= 0");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size", "size must be >= 0");
			}
			if (offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset, size", "offset + size exceeds the buffer length");
			}
			if (!this.connected)
			{
				throw new SocketException(10057);
			}
			Queue queue = this.writeQ;
			Socket.SocketAsyncResult socketAsyncResult;
			lock (queue)
			{
				socketAsyncResult = new Socket.SocketAsyncResult(this, state, callback, Socket.SocketOperation.Send);
				socketAsyncResult.Buffer = buffer;
				socketAsyncResult.Offset = offset;
				socketAsyncResult.Size = size;
				socketAsyncResult.SockFlags = socket_flags;
				this.writeQ.Enqueue(socketAsyncResult);
				if (this.writeQ.Count == 1)
				{
					Socket.Worker worker = new Socket.Worker(socketAsyncResult);
					Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(worker.Send);
					socketAsyncCall.BeginInvoke(null, socketAsyncResult);
				}
			}
			return socketAsyncResult;
		}

		// Token: 0x060003A5 RID: 933
		[MethodImpl(4096)]
		private static extern void Bind_internal(IntPtr sock, SocketAddress sa, out int error);

		// Token: 0x060003A6 RID: 934 RVA: 0x0000FF04 File Offset: 0x0000E104
		public void Bind(EndPoint local_end)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (local_end == null)
			{
				throw new ArgumentNullException("local_end");
			}
			if (Environment.SocketSecurityEnabled && Socket.current_bind_count >= this.max_bind_count)
			{
				throw new SecurityException("Too many sockets are bound, maximum count in the webplayer is " + this.max_bind_count);
			}
			int num;
			Socket.Bind_internal(this.socket, local_end.Serialize(), out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (num == 0)
			{
				this.isbound = true;
			}
			if (Environment.SocketSecurityEnabled)
			{
				Socket.current_bind_count++;
			}
			this.seed_endpoint = local_end;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
		public int EndReceive(IAsyncResult result)
		{
			SocketError socketError;
			return this.EndReceive(result, out socketError);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public int EndReceive(IAsyncResult asyncResult, out SocketError errorCode)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket.SocketAsyncResult socketAsyncResult = asyncResult as Socket.SocketAsyncResult;
			if (socketAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			if (Interlocked.CompareExchange(ref socketAsyncResult.EndCalled, 1, 0) == 1)
			{
				throw this.InvalidAsyncOp("EndReceive");
			}
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			errorCode = socketAsyncResult.ErrorCode;
			socketAsyncResult.CheckIfThrowDelayedException();
			return socketAsyncResult.Total;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001008C File Offset: 0x0000E28C
		public int EndSend(IAsyncResult result)
		{
			SocketError socketError;
			return this.EndSend(result, out socketError);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000100A4 File Offset: 0x0000E2A4
		public int EndSend(IAsyncResult asyncResult, out SocketError errorCode)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket.SocketAsyncResult socketAsyncResult = asyncResult as Socket.SocketAsyncResult;
			if (socketAsyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "result");
			}
			if (Interlocked.CompareExchange(ref socketAsyncResult.EndCalled, 1, 0) == 1)
			{
				throw this.InvalidAsyncOp("EndSend");
			}
			if (!asyncResult.IsCompleted)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			errorCode = socketAsyncResult.ErrorCode;
			socketAsyncResult.CheckIfThrowDelayedException();
			return socketAsyncResult.Total;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00010150 File Offset: 0x0000E350
		private Exception InvalidAsyncOp(string method)
		{
			return new InvalidOperationException(method + " can only be called once per asynchronous operation");
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00010164 File Offset: 0x0000E364
		public bool Poll(int time_us, SelectMode mode)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (mode != SelectMode.SelectRead && mode != SelectMode.SelectWrite && mode != SelectMode.SelectError)
			{
				throw new NotSupportedException("'mode' parameter is not valid.");
			}
			int num;
			bool flag = Socket.Poll_internal(this.socket, mode, time_us, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (mode == SelectMode.SelectWrite && flag && !this.connected && (int)this.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error) == 0)
			{
				this.connected = true;
			}
			return flag;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00010210 File Offset: 0x0000E410
		public int Receive(byte[] buffer, int offset, int size, SocketFlags flags)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			SocketError socketError;
			int num = this.Receive_nochecks(buffer, offset, size, flags, out socketError);
			if (socketError == SocketError.Success)
			{
				return num;
			}
			if (socketError == SocketError.WouldBlock && this.blocking)
			{
				throw new SocketException((int)socketError, "Operation timed out.");
			}
			throw new SocketException((int)socketError);
		}

		// Token: 0x060003AE RID: 942
		[MethodImpl(4096)]
		private static extern int RecvFrom_internal(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, ref SocketAddress sockaddr, out int error);

		// Token: 0x060003AF RID: 943 RVA: 0x000102CC File Offset: 0x0000E4CC
		internal int ReceiveFrom_nochecks(byte[] buf, int offset, int size, SocketFlags flags, ref EndPoint remote_end)
		{
			int num;
			return this.ReceiveFrom_nochecks_exc(buf, offset, size, flags, ref remote_end, true, out num);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000102EC File Offset: 0x0000E4EC
		internal int ReceiveFrom_nochecks_exc(byte[] buf, int offset, int size, SocketFlags flags, ref EndPoint remote_end, bool throwOnError, out int error)
		{
			SocketAddress socketAddress = remote_end.Serialize();
			int num = Socket.RecvFrom_internal(this.socket, buf, offset, size, flags, ref socketAddress, out error);
			SocketError socketError = (SocketError)error;
			if (socketError != SocketError.Success)
			{
				if (socketError != SocketError.WouldBlock && socketError != SocketError.InProgress)
				{
					this.connected = false;
				}
				else if (socketError == SocketError.WouldBlock && this.blocking)
				{
					if (throwOnError)
					{
						throw new SocketException(10060, "Operation timed out");
					}
					error = 10060;
					return 0;
				}
				if (throwOnError)
				{
					throw new SocketException(error);
				}
				return 0;
			}
			else
			{
				if (Environment.SocketSecurityEnabled && !Socket.CheckEndPoint(socketAddress))
				{
					buf.Initialize();
					throw new SecurityException("Unable to connect, as no valid crossdomain policy was found");
				}
				this.connected = true;
				this.isbound = true;
				if (socketAddress != null)
				{
					remote_end = remote_end.Create(socketAddress);
				}
				this.seed_endpoint = remote_end;
				return num;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000103DC File Offset: 0x0000E5DC
		public int Send(byte[] buf, int offset, int size, SocketFlags flags)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (buf == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buf.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buf.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			SocketError socketError;
			int num = this.Send_nochecks(buf, offset, size, flags, out socketError);
			if (socketError != SocketError.Success)
			{
				throw new SocketException((int)socketError);
			}
			return num;
		}

		// Token: 0x060003B2 RID: 946
		[MethodImpl(4096)]
		private static extern int SendTo_internal_real(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, SocketAddress sa, out int error);

		// Token: 0x060003B3 RID: 947 RVA: 0x00010478 File Offset: 0x0000E678
		private static int SendTo_internal(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, SocketAddress sa, out int error)
		{
			if (Environment.SocketSecurityEnabled && !Socket.CheckEndPoint(sa))
			{
				SecurityException ex = new SecurityException("SendTo request refused by Unity webplayer security model");
				Console.WriteLine("Throwing the following security exception: " + ex);
				throw ex;
			}
			return Socket.SendTo_internal_real(sock, buffer, offset, count, flags, sa, out error);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000104CC File Offset: 0x0000E6CC
		internal int SendTo_nochecks(byte[] buffer, int offset, int size, SocketFlags flags, EndPoint remote_end)
		{
			SocketAddress socketAddress = remote_end.Serialize();
			int num2;
			int num = Socket.SendTo_internal(this.socket, buffer, offset, size, flags, socketAddress, out num2);
			SocketError socketError = (SocketError)num2;
			if (socketError != SocketError.Success)
			{
				if (socketError != SocketError.WouldBlock && socketError != SocketError.InProgress)
				{
					this.connected = false;
				}
				throw new SocketException(num2);
			}
			this.connected = true;
			this.isbound = true;
			this.seed_endpoint = remote_end;
			return num;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00010538 File Offset: 0x0000E738
		internal static void CheckProtocolSupport()
		{
			if (Socket.ipv4Supported == -1)
			{
				try
				{
					Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					socket.Close();
					Socket.ipv4Supported = 1;
				}
				catch
				{
					Socket.ipv4Supported = 0;
				}
			}
			if (Socket.ipv6Supported == -1 && Socket.ipv6Supported != 0)
			{
				try
				{
					Socket socket2 = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
					socket2.Close();
					Socket.ipv6Supported = 1;
				}
				catch
				{
					Socket.ipv6Supported = 0;
				}
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000105D0 File Offset: 0x0000E7D0
		public static bool SupportsIPv4
		{
			get
			{
				Socket.CheckProtocolSupport();
				return Socket.ipv4Supported == 1;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x000105E0 File Offset: 0x0000E7E0
		[Obsolete("Use OSSupportsIPv6 instead")]
		public static bool SupportsIPv6
		{
			get
			{
				Socket.CheckProtocolSupport();
				return Socket.ipv6Supported == 1;
			}
		}

		// Token: 0x060003B8 RID: 952
		[MethodImpl(4096)]
		private extern IntPtr Socket_internal(AddressFamily family, SocketType type, ProtocolType proto, out int error);

		// Token: 0x060003B9 RID: 953 RVA: 0x000105F0 File Offset: 0x0000E7F0
		~Socket()
		{
			this.Dispose(false);
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00010620 File Offset: 0x0000E820
		public bool Blocking
		{
			get
			{
				return this.blocking;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00010628 File Offset: 0x0000E828
		public bool Connected
		{
			get
			{
				return this.connected;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00010630 File Offset: 0x0000E830
		public bool NoDelay
		{
			set
			{
				if (this.disposed && this.closed)
				{
					throw new ObjectDisposedException(base.GetType().ToString());
				}
				this.ThrowIfUpd();
				this.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.Debug, (!value) ? 0 : 1);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00010680 File Offset: 0x0000E880
		private void Linger(IntPtr handle)
		{
			if (!this.connected || this.linger_timeout <= 0)
			{
				return;
			}
			int num;
			Socket.Shutdown_internal(handle, SocketShutdown.Receive, out num);
			if (num != 0)
			{
				return;
			}
			int num2 = this.linger_timeout / 1000;
			int num3 = this.linger_timeout % 1000;
			if (num3 > 0)
			{
				Socket.Poll_internal(handle, SelectMode.SelectRead, num3 * 1000, out num);
				if (num != 0)
				{
					return;
				}
			}
			if (num2 > 0)
			{
				LingerOption lingerOption = new LingerOption(true, num2);
				Socket.SetSocketOption_internal(handle, SocketOptionLevel.Socket, SocketOptionName.Linger, lingerOption, null, 0, out num);
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00010714 File Offset: 0x0000E914
		protected virtual void Dispose(bool explicitDisposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			bool flag = this.connected;
			this.connected = false;
			if ((int)this.socket != -1)
			{
				if (Environment.SocketSecurityEnabled && Socket.current_bind_count > 0)
				{
					Socket.current_bind_count--;
				}
				this.closed = true;
				IntPtr intPtr = this.socket;
				this.socket = (IntPtr)(-1);
				Thread thread = this.blocking_thread;
				if (thread != null)
				{
					thread.Abort();
					this.blocking_thread = null;
				}
				if (flag)
				{
					this.Linger(intPtr);
				}
				int num;
				Socket.Close_internal(intPtr, out num);
				if (num != 0)
				{
					throw new SocketException(num);
				}
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000107CC File Offset: 0x0000E9CC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003C0 RID: 960
		[MethodImpl(4096)]
		private static extern void Close_internal(IntPtr socket, out int error);

		// Token: 0x060003C1 RID: 961 RVA: 0x000107DC File Offset: 0x0000E9DC
		public void Close()
		{
			this.linger_timeout = 0;
			((IDisposable)this).Dispose();
		}

		// Token: 0x060003C2 RID: 962
		[MethodImpl(4096)]
		private static extern void Connect_internal_real(IntPtr sock, SocketAddress sa, out int error);

		// Token: 0x060003C3 RID: 963 RVA: 0x000107EC File Offset: 0x0000E9EC
		private static void Connect_internal(IntPtr sock, SocketAddress sa, out int error, bool requireSocketPolicyFile)
		{
			if (requireSocketPolicyFile && !Socket.CheckEndPoint(sa))
			{
				throw new SecurityException("Unable to connect, as no valid crossdomain policy was found");
			}
			Socket.Connect_internal_real(sock, sa, out error);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00010814 File Offset: 0x0000EA14
		internal static bool CheckEndPoint(SocketAddress sa)
		{
			if (!Environment.SocketSecurityEnabled)
			{
				return true;
			}
			bool flag;
			try
			{
				IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Loopback, 123);
				IPEndPoint ipendPoint2 = (IPEndPoint)ipendPoint.Create(sa);
				if (Socket.check_socket_policy == null)
				{
					Socket.check_socket_policy = Socket.GetUnityCrossDomainHelperMethod("CheckSocketEndPoint");
				}
				flag = (bool)Socket.check_socket_policy.Invoke(null, new object[]
				{
					ipendPoint2.Address.ToString(),
					ipendPoint2.Port
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unexpected error while trying to CheckEndPoint() : " + ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000108CC File Offset: 0x0000EACC
		private static MethodInfo GetUnityCrossDomainHelperMethod(string methodname)
		{
			Type type = Type.GetType("UnityEngine.UnityCrossDomainHelper, CrossDomainPolicyParser, Version=1.0.0.0, Culture=neutral");
			if (type == null)
			{
				throw new SecurityException("Cant find type UnityCrossDomainHelper");
			}
			MethodInfo method = type.GetMethod(methodname);
			if (method == null)
			{
				throw new SecurityException("Cant find " + methodname);
			}
			return method;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00010918 File Offset: 0x0000EB18
		internal void Connect(EndPoint remoteEP, bool requireSocketPolicy)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			IPEndPoint ipendPoint = remoteEP as IPEndPoint;
			if (ipendPoint != null && (ipendPoint.Address.Equals(IPAddress.Any) || ipendPoint.Address.Equals(IPAddress.IPv6Any)))
			{
				throw new SocketException(10049);
			}
			if (this.islistening)
			{
				throw new InvalidOperationException();
			}
			SocketAddress socketAddress = remoteEP.Serialize();
			int num = 0;
			this.blocking_thread = Thread.CurrentThread;
			try
			{
				Socket.Connect_internal(this.socket, socketAddress, out num, requireSocketPolicy);
			}
			catch (ThreadAbortException)
			{
				if (this.disposed)
				{
					Thread.ResetAbort();
					num = 10004;
				}
			}
			finally
			{
				this.blocking_thread = null;
			}
			if (num != 0)
			{
				throw new SocketException(num);
			}
			this.connected = true;
			this.isbound = true;
			this.seed_endpoint = remoteEP;
		}

		// Token: 0x060003C7 RID: 967
		[MethodImpl(4096)]
		private static extern bool Poll_internal(IntPtr socket, SelectMode mode, int timeout, out int error);

		// Token: 0x060003C8 RID: 968
		[MethodImpl(4096)]
		private static extern int Receive_internal(IntPtr sock, byte[] buffer, int offset, int count, SocketFlags flags, out int error);

		// Token: 0x060003C9 RID: 969 RVA: 0x00010A3C File Offset: 0x0000EC3C
		internal int Receive_nochecks(byte[] buf, int offset, int size, SocketFlags flags, out SocketError error)
		{
			if (this.protocol_type == ProtocolType.Udp)
			{
				EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
				int num = 0;
				int num2 = this.ReceiveFrom_nochecks_exc(buf, offset, size, flags, ref endPoint, false, out num);
				error = (SocketError)num;
				return num2;
			}
			int num4;
			int num3 = Socket.Receive_internal(this.socket, buf, offset, size, flags, out num4);
			error = (SocketError)num4;
			if (error != SocketError.Success && error != SocketError.WouldBlock && error != SocketError.InProgress)
			{
				this.connected = false;
			}
			else
			{
				this.connected = true;
			}
			return num3;
		}

		// Token: 0x060003CA RID: 970
		[MethodImpl(4096)]
		private static extern void GetSocketOption_obj_internal(IntPtr socket, SocketOptionLevel level, SocketOptionName name, out object obj_val, out int error);

		// Token: 0x060003CB RID: 971
		[MethodImpl(4096)]
		private static extern int Send_internal(IntPtr sock, byte[] buf, int offset, int count, SocketFlags flags, out int error);

		// Token: 0x060003CC RID: 972 RVA: 0x00010ACC File Offset: 0x0000ECCC
		internal int Send_nochecks(byte[] buf, int offset, int size, SocketFlags flags, out SocketError error)
		{
			if (size == 0)
			{
				error = SocketError.Success;
				return 0;
			}
			int num2;
			int num = Socket.Send_internal(this.socket, buf, offset, size, flags, out num2);
			error = (SocketError)num2;
			if (error != SocketError.Success && error != SocketError.WouldBlock && error != SocketError.InProgress)
			{
				this.connected = false;
			}
			else
			{
				this.connected = true;
			}
			return num;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00010B34 File Offset: 0x0000ED34
		public object GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			object obj;
			int num;
			Socket.GetSocketOption_obj_internal(this.socket, optionLevel, optionName, out obj, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
			if (optionName == SocketOptionName.Linger)
			{
				return (LingerOption)obj;
			}
			if (optionName == SocketOptionName.AddMembership || optionName == SocketOptionName.DropMembership)
			{
				return (MulticastOption)obj;
			}
			if (obj is int)
			{
				return (int)obj;
			}
			return obj;
		}

		// Token: 0x060003CE RID: 974
		[MethodImpl(4096)]
		private static extern void Shutdown_internal(IntPtr socket, SocketShutdown how, out int error);

		// Token: 0x060003CF RID: 975
		[MethodImpl(4096)]
		private static extern void SetSocketOption_internal(IntPtr socket, SocketOptionLevel level, SocketOptionName name, object obj_val, byte[] byte_val, int int_val, out int error);

		// Token: 0x060003D0 RID: 976 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue)
		{
			if (this.disposed && this.closed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			int num;
			Socket.SetSocketOption_internal(this.socket, optionLevel, optionName, null, null, optionValue, out num);
			if (num != 0)
			{
				throw new SocketException(num);
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00010C1C File Offset: 0x0000EE1C
		private void ThrowIfUpd()
		{
		}

		// Token: 0x040001FE RID: 510
		private Queue readQ = new Queue(2);

		// Token: 0x040001FF RID: 511
		private Queue writeQ = new Queue(2);

		// Token: 0x04000200 RID: 512
		private bool islistening;

		// Token: 0x04000201 RID: 513
		private bool useoverlappedIO;

		// Token: 0x04000202 RID: 514
		private readonly int MinListenPort = 7100;

		// Token: 0x04000203 RID: 515
		private readonly int MaxListenPort = 7150;

		// Token: 0x04000204 RID: 516
		private static int ipv4Supported = -1;

		// Token: 0x04000205 RID: 517
		private static int ipv6Supported = -1;

		// Token: 0x04000206 RID: 518
		private int linger_timeout;

		// Token: 0x04000207 RID: 519
		private IntPtr socket;

		// Token: 0x04000208 RID: 520
		private AddressFamily address_family;

		// Token: 0x04000209 RID: 521
		private SocketType socket_type;

		// Token: 0x0400020A RID: 522
		private ProtocolType protocol_type;

		// Token: 0x0400020B RID: 523
		internal bool blocking = true;

		// Token: 0x0400020C RID: 524
		private Thread blocking_thread;

		// Token: 0x0400020D RID: 525
		private bool isbound;

		// Token: 0x0400020E RID: 526
		private static int current_bind_count;

		// Token: 0x0400020F RID: 527
		private readonly int max_bind_count = 50;

		// Token: 0x04000210 RID: 528
		private bool connected;

		// Token: 0x04000211 RID: 529
		private bool closed;

		// Token: 0x04000212 RID: 530
		internal bool disposed;

		// Token: 0x04000213 RID: 531
		internal EndPoint seed_endpoint;

		// Token: 0x04000214 RID: 532
		private static MethodInfo check_socket_policy;

		// Token: 0x0200009A RID: 154
		// (Invoke) Token: 0x060003D3 RID: 979
		private delegate void SocketAsyncCall();

		// Token: 0x0200009B RID: 155
		[StructLayout(0)]
		private sealed class SocketAsyncResult : IAsyncResult
		{
			// Token: 0x060003D6 RID: 982 RVA: 0x00010C20 File Offset: 0x0000EE20
			public SocketAsyncResult(Socket sock, object state, AsyncCallback callback, Socket.SocketOperation operation)
			{
				this.Sock = sock;
				this.blocking = sock.blocking;
				this.handle = sock.socket;
				this.state = state;
				this.callback = callback;
				this.operation = operation;
				this.SockFlags = SocketFlags.None;
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x00010C70 File Offset: 0x0000EE70
			public void CheckIfThrowDelayedException()
			{
				if (this.delayedException != null)
				{
					this.Sock.connected = false;
					throw this.delayedException;
				}
				if (this.error != 0)
				{
					this.Sock.connected = false;
					throw new SocketException(this.error);
				}
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x00010CC0 File Offset: 0x0000EEC0
			private void CompleteAllOnDispose(Queue queue)
			{
				object[] array = queue.ToArray();
				queue.Clear();
				foreach (Socket.SocketAsyncResult socketAsyncResult in array)
				{
					WaitCallback waitCallback = new WaitCallback(socketAsyncResult.CompleteDisposed);
					ThreadPool.QueueUserWorkItem(waitCallback, null);
				}
				if (array.Length == 0)
				{
					this.Buffer = null;
				}
			}

			// Token: 0x060003D9 RID: 985 RVA: 0x00010D1C File Offset: 0x0000EF1C
			private void CompleteDisposed(object unused)
			{
				this.Complete();
			}

			// Token: 0x060003DA RID: 986 RVA: 0x00010D24 File Offset: 0x0000EF24
			public void Complete()
			{
				if (this.operation != Socket.SocketOperation.Receive && this.Sock.disposed)
				{
					this.delayedException = new ObjectDisposedException(this.Sock.GetType().ToString());
				}
				this.IsCompleted = true;
				Queue queue = null;
				if (this.operation == Socket.SocketOperation.Receive || this.operation == Socket.SocketOperation.ReceiveFrom || this.operation == Socket.SocketOperation.ReceiveGeneric)
				{
					queue = this.Sock.readQ;
				}
				else if (this.operation == Socket.SocketOperation.Send || this.operation == Socket.SocketOperation.SendTo || this.operation == Socket.SocketOperation.SendGeneric)
				{
					queue = this.Sock.writeQ;
				}
				if (queue != null)
				{
					Socket.SocketAsyncCall socketAsyncCall = null;
					Socket.SocketAsyncResult socketAsyncResult = null;
					Queue queue2 = queue;
					lock (queue2)
					{
						queue.Dequeue();
						if (queue.Count > 0)
						{
							socketAsyncResult = (Socket.SocketAsyncResult)queue.Peek();
							if (!this.Sock.disposed)
							{
								Socket.Worker worker = new Socket.Worker(socketAsyncResult);
								socketAsyncCall = this.GetDelegate(worker, socketAsyncResult.operation);
							}
							else
							{
								this.CompleteAllOnDispose(queue);
							}
						}
					}
					if (socketAsyncCall != null)
					{
						socketAsyncCall.BeginInvoke(null, socketAsyncResult);
					}
				}
				if (this.callback != null)
				{
					this.callback(this);
				}
				this.Buffer = null;
			}

			// Token: 0x060003DB RID: 987 RVA: 0x00010E88 File Offset: 0x0000F088
			private Socket.SocketAsyncCall GetDelegate(Socket.Worker worker, Socket.SocketOperation op)
			{
				switch (op)
				{
				case Socket.SocketOperation.Receive:
					return new Socket.SocketAsyncCall(worker.Receive);
				case Socket.SocketOperation.ReceiveFrom:
					return new Socket.SocketAsyncCall(worker.ReceiveFrom);
				case Socket.SocketOperation.Send:
					return new Socket.SocketAsyncCall(worker.Send);
				case Socket.SocketOperation.SendTo:
					return new Socket.SocketAsyncCall(worker.SendTo);
				default:
					return null;
				}
			}

			// Token: 0x060003DC RID: 988 RVA: 0x00010EEC File Offset: 0x0000F0EC
			public void Complete(int total)
			{
				this.total = total;
				this.Complete();
			}

			// Token: 0x060003DD RID: 989 RVA: 0x00010EFC File Offset: 0x0000F0FC
			public void Complete(Exception e)
			{
				this.delayedException = e;
				this.Complete();
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x060003DE RID: 990 RVA: 0x00010F0C File Offset: 0x0000F10C
			public object AsyncState
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x060003DF RID: 991 RVA: 0x00010F14 File Offset: 0x0000F114
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					lock (this)
					{
						if (this.waithandle == null)
						{
							this.waithandle = new ManualResetEvent(this.completed);
						}
					}
					return this.waithandle;
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x060003E0 RID: 992 RVA: 0x00010F68 File Offset: 0x0000F168
			// (set) Token: 0x060003E1 RID: 993 RVA: 0x00010F70 File Offset: 0x0000F170
			public bool IsCompleted
			{
				get
				{
					return this.completed;
				}
				set
				{
					this.completed = value;
					lock (this)
					{
						if (this.waithandle != null && value)
						{
							((ManualResetEvent)this.waithandle).Set();
						}
					}
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x060003E2 RID: 994 RVA: 0x00010FCC File Offset: 0x0000F1CC
			// (set) Token: 0x060003E3 RID: 995 RVA: 0x00010FD4 File Offset: 0x0000F1D4
			public int Total
			{
				get
				{
					return this.total;
				}
				set
				{
					this.total = value;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x060003E4 RID: 996 RVA: 0x00010FE0 File Offset: 0x0000F1E0
			public SocketError ErrorCode
			{
				get
				{
					SocketException ex = this.delayedException as SocketException;
					if (ex != null)
					{
						return ex.SocketErrorCode;
					}
					if (this.error != 0)
					{
						return (SocketError)this.error;
					}
					return SocketError.Success;
				}
			}

			// Token: 0x04000215 RID: 533
			public Socket Sock;

			// Token: 0x04000216 RID: 534
			public IntPtr handle;

			// Token: 0x04000217 RID: 535
			private object state;

			// Token: 0x04000218 RID: 536
			private AsyncCallback callback;

			// Token: 0x04000219 RID: 537
			private WaitHandle waithandle;

			// Token: 0x0400021A RID: 538
			private Exception delayedException;

			// Token: 0x0400021B RID: 539
			public EndPoint EndPoint;

			// Token: 0x0400021C RID: 540
			public byte[] Buffer;

			// Token: 0x0400021D RID: 541
			public int Offset;

			// Token: 0x0400021E RID: 542
			public int Size;

			// Token: 0x0400021F RID: 543
			public SocketFlags SockFlags;

			// Token: 0x04000220 RID: 544
			public Socket AcceptSocket;

			// Token: 0x04000221 RID: 545
			public IPAddress[] Addresses;

			// Token: 0x04000222 RID: 546
			public int Port;

			// Token: 0x04000223 RID: 547
			public IList<ArraySegment<byte>> Buffers;

			// Token: 0x04000224 RID: 548
			public bool ReuseSocket;

			// Token: 0x04000225 RID: 549
			private Socket acc_socket;

			// Token: 0x04000226 RID: 550
			private int total;

			// Token: 0x04000227 RID: 551
			private bool completed_sync;

			// Token: 0x04000228 RID: 552
			private bool completed;

			// Token: 0x04000229 RID: 553
			public bool blocking;

			// Token: 0x0400022A RID: 554
			internal int error;

			// Token: 0x0400022B RID: 555
			private Socket.SocketOperation operation;

			// Token: 0x0400022C RID: 556
			public object ares;

			// Token: 0x0400022D RID: 557
			public int EndCalled;
		}

		// Token: 0x0200009C RID: 156
		private enum SocketOperation
		{
			// Token: 0x0400022F RID: 559
			Accept,
			// Token: 0x04000230 RID: 560
			Connect,
			// Token: 0x04000231 RID: 561
			Receive,
			// Token: 0x04000232 RID: 562
			ReceiveFrom,
			// Token: 0x04000233 RID: 563
			Send,
			// Token: 0x04000234 RID: 564
			SendTo,
			// Token: 0x04000235 RID: 565
			UsedInManaged1,
			// Token: 0x04000236 RID: 566
			UsedInManaged2,
			// Token: 0x04000237 RID: 567
			UsedInProcess,
			// Token: 0x04000238 RID: 568
			UsedInConsole2,
			// Token: 0x04000239 RID: 569
			Disconnect,
			// Token: 0x0400023A RID: 570
			AcceptReceive,
			// Token: 0x0400023B RID: 571
			ReceiveGeneric,
			// Token: 0x0400023C RID: 572
			SendGeneric
		}

		// Token: 0x0200009D RID: 157
		private sealed class Worker
		{
			// Token: 0x060003E5 RID: 997 RVA: 0x0001101C File Offset: 0x0000F21C
			public Worker(Socket.SocketAsyncResult ares)
				: this(ares, true)
			{
			}

			// Token: 0x060003E6 RID: 998 RVA: 0x00011028 File Offset: 0x0000F228
			public Worker(Socket.SocketAsyncResult ares, bool requireSocketSecurity)
			{
				this.result = ares;
				this.requireSocketSecurity = requireSocketSecurity;
			}

			// Token: 0x060003E7 RID: 999 RVA: 0x00011040 File Offset: 0x0000F240
			public void Receive()
			{
				this.result.Complete();
			}

			// Token: 0x060003E8 RID: 1000 RVA: 0x00011050 File Offset: 0x0000F250
			public void ReceiveFrom()
			{
				int num = 0;
				try
				{
					num = this.result.Sock.ReceiveFrom_nochecks(this.result.Buffer, this.result.Offset, this.result.Size, this.result.SockFlags, ref this.result.EndPoint);
				}
				catch (Exception ex)
				{
					this.result.Complete(ex);
					return;
				}
				this.result.Complete(num);
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x000110E0 File Offset: 0x0000F2E0
			private void UpdateSendValues(int last_sent)
			{
				if (this.result.error == 0)
				{
					this.send_so_far += last_sent;
					this.result.Offset += last_sent;
					this.result.Size -= last_sent;
				}
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x00011134 File Offset: 0x0000F334
			public void Send()
			{
				if (this.result.error == 0)
				{
					this.UpdateSendValues(this.result.Total);
					if (this.result.Sock.disposed)
					{
						this.result.Complete();
						return;
					}
					if (this.result.Size > 0)
					{
						Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(this.Send);
						socketAsyncCall.BeginInvoke(null, this.result);
						return;
					}
					this.result.Total = this.send_so_far;
				}
				this.result.Complete();
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x000111CC File Offset: 0x0000F3CC
			public void SendTo()
			{
				try
				{
					int num = this.result.Sock.SendTo_nochecks(this.result.Buffer, this.result.Offset, this.result.Size, this.result.SockFlags, this.result.EndPoint);
					this.UpdateSendValues(num);
					if (this.result.Size > 0)
					{
						Socket.SocketAsyncCall socketAsyncCall = new Socket.SocketAsyncCall(this.SendTo);
						socketAsyncCall.BeginInvoke(null, this.result);
						return;
					}
					this.result.Total = this.send_so_far;
				}
				catch (Exception ex)
				{
					this.result.Complete(ex);
					return;
				}
				this.result.Complete();
			}

			// Token: 0x0400023D RID: 573
			private Socket.SocketAsyncResult result;

			// Token: 0x0400023E RID: 574
			private bool requireSocketSecurity;

			// Token: 0x0400023F RID: 575
			private int send_so_far;
		}
	}
}
