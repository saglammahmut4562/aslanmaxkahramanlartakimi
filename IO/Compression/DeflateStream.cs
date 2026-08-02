using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.IO.Compression
{
	// Token: 0x0200005E RID: 94
	public class DeflateStream : Stream
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x00007394 File Offset: 0x00005594
		public DeflateStream(Stream compressedStream, CompressionMode mode)
			: this(compressedStream, mode, false, false)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000073A0 File Offset: 0x000055A0
		internal DeflateStream(Stream compressedStream, CompressionMode mode, bool leaveOpen, bool gzip)
		{
			if (compressedStream == null)
			{
				throw new ArgumentNullException("compressedStream");
			}
			if (mode != CompressionMode.Compress && mode != CompressionMode.Decompress)
			{
				throw new ArgumentException("mode");
			}
			this.data = GCHandle.Alloc(this);
			this.base_stream = compressedStream;
			this.feeder = ((mode != CompressionMode.Compress) ? new DeflateStream.UnmanagedReadOrWrite(DeflateStream.UnmanagedRead) : new DeflateStream.UnmanagedReadOrWrite(DeflateStream.UnmanagedWrite));
			this.z_stream = DeflateStream.CreateZStream(mode, gzip, this.feeder, GCHandle.ToIntPtr(this.data));
			if (this.z_stream == IntPtr.Zero)
			{
				this.base_stream = null;
				this.feeder = null;
				throw new NotImplementedException("Failed to initialize zlib. You probably have an old zlib installed. Version 1.2.0.4 or later is required.");
			}
			this.mode = mode;
			this.leaveOpen = leaveOpen;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007474 File Offset: 0x00005674
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				IntPtr intPtr = this.z_stream;
				this.z_stream = IntPtr.Zero;
				int num = 0;
				if (intPtr != IntPtr.Zero)
				{
					num = DeflateStream.CloseZStream(intPtr);
				}
				this.io_buffer = null;
				if (!this.leaveOpen)
				{
					Stream stream = this.base_stream;
					if (stream != null)
					{
						stream.Close();
					}
					this.base_stream = null;
				}
				DeflateStream.CheckResult(num, "Dispose");
			}
			if (this.data.IsAllocated)
			{
				this.data.Free();
				this.data = default(GCHandle);
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000752C File Offset: 0x0000572C
		private static int UnmanagedRead(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStream deflateStream = GCHandle.FromIntPtr(data).Target as DeflateStream;
			if (deflateStream == null)
			{
				return -1;
			}
			return deflateStream.UnmanagedRead(buffer, length);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007560 File Offset: 0x00005760
		private unsafe int UnmanagedRead(IntPtr buffer, int length)
		{
			int num = 0;
			int num2 = 1;
			while (length > 0 && num2 > 0)
			{
				if (this.io_buffer == null)
				{
					this.io_buffer = new byte[4096];
				}
				int num3 = Math.Min(length, this.io_buffer.Length);
				num2 = this.base_stream.Read(this.io_buffer, 0, num3);
				if (num2 > 0)
				{
					Marshal.Copy(this.io_buffer, 0, buffer, num2);
					buffer = new IntPtr((void*)((byte*)buffer.ToPointer() + num2));
					length -= num2;
					num += num2;
				}
			}
			return num;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000075F4 File Offset: 0x000057F4
		private static int UnmanagedWrite(IntPtr buffer, int length, IntPtr data)
		{
			DeflateStream deflateStream = GCHandle.FromIntPtr(data).Target as DeflateStream;
			if (deflateStream == null)
			{
				return -1;
			}
			return deflateStream.UnmanagedWrite(buffer, length);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007628 File Offset: 0x00005828
		private unsafe int UnmanagedWrite(IntPtr buffer, int length)
		{
			int num = 0;
			while (length > 0)
			{
				if (this.io_buffer == null)
				{
					this.io_buffer = new byte[4096];
				}
				int num2 = Math.Min(length, this.io_buffer.Length);
				Marshal.Copy(buffer, this.io_buffer, 0, num2);
				this.base_stream.Write(this.io_buffer, 0, num2);
				buffer = new IntPtr((void*)((byte*)buffer.ToPointer() + num2));
				length -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000076A8 File Offset: 0x000058A8
		private unsafe int ReadInternal(byte[] array, int offset, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			int num;
			fixed (byte* ptr = (ref array != null && array.Length != 0 ? ref array[0] : ref *null))
			{
				IntPtr intPtr = new IntPtr((void*)(ptr + offset));
				num = DeflateStream.ReadZStream(this.z_stream, intPtr, count);
			}
			DeflateStream.CheckResult(num, "ReadInternal");
			return num;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007704 File Offset: 0x00005904
		public override int Read(byte[] dest, int dest_offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (dest == null)
			{
				throw new ArgumentNullException("Destination array is null.");
			}
			if (!this.CanRead)
			{
				throw new InvalidOperationException("Stream does not support reading.");
			}
			int num = dest.Length;
			if (dest_offset < 0 || count < 0)
			{
				throw new ArgumentException("Dest or count is negative.");
			}
			if (dest_offset > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (dest_offset + count > num)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			return this.ReadInternal(dest, dest_offset, count);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000077A0 File Offset: 0x000059A0
		private unsafe void WriteInternal(byte[] array, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			int num;
			fixed (byte* ptr = (ref array != null && array.Length != 0 ? ref array[0] : ref *null))
			{
				IntPtr intPtr = new IntPtr((void*)(ptr + offset));
				num = DeflateStream.WriteZStream(this.z_stream, intPtr, count);
			}
			DeflateStream.CheckResult(num, "WriteInternal");
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000077FC File Offset: 0x000059FC
		public override void Write(byte[] src, int src_offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			if (src_offset < 0)
			{
				throw new ArgumentOutOfRangeException("src_offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			this.WriteInternal(src, src_offset, count);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000787C File Offset: 0x00005A7C
		private static void CheckResult(int result, string where)
		{
			if (result >= 0)
			{
				return;
			}
			string text;
			switch (result + 11)
			{
			case 0:
				text = "IO error";
				goto IL_00A7;
			case 1:
				text = "Invalid argument(s)";
				goto IL_00A7;
			case 5:
				text = "Invalid version";
				goto IL_00A7;
			case 6:
				text = "Internal error (no progress possible)";
				goto IL_00A7;
			case 7:
				text = "Not enough memory";
				goto IL_00A7;
			case 8:
				text = "Corrupted data";
				goto IL_00A7;
			case 9:
				text = "Internal error";
				goto IL_00A7;
			case 10:
				text = "Unknown error";
				goto IL_00A7;
			}
			text = "Unknown error";
			IL_00A7:
			throw new IOException(text + " " + where);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007944 File Offset: 0x00005B44
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.CanWrite)
			{
				int num = DeflateStream.Flush(this.z_stream);
				DeflateStream.CheckResult(num, "Flush");
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007990 File Offset: 0x00005B90
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream does not support reading");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentException("Buffer too small. count/offset wrong.");
			}
			DeflateStream.ReadMethod readMethod = new DeflateStream.ReadMethod(this.ReadInternal);
			return readMethod.BeginInvoke(buffer, offset, count, cback, state);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007A40 File Offset: 0x00005C40
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanWrite)
			{
				throw new InvalidOperationException("This stream does not support writing");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentException("Buffer too small. count/offset wrong.");
			}
			DeflateStream.WriteMethod writeMethod = new DeflateStream.WriteMethod(this.WriteInternal);
			return writeMethod.BeginInvoke(buffer, offset, count, cback, state);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public override int EndRead(IAsyncResult async_result)
		{
			if (async_result == null)
			{
				throw new ArgumentNullException("async_result");
			}
			AsyncResult asyncResult = async_result as AsyncResult;
			if (asyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "async_result");
			}
			DeflateStream.ReadMethod readMethod = asyncResult.AsyncDelegate as DeflateStream.ReadMethod;
			if (readMethod == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "async_result");
			}
			return readMethod.EndInvoke(async_result);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007B54 File Offset: 0x00005D54
		public override void EndWrite(IAsyncResult async_result)
		{
			if (async_result == null)
			{
				throw new ArgumentNullException("async_result");
			}
			AsyncResult asyncResult = async_result as AsyncResult;
			if (asyncResult == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "async_result");
			}
			DeflateStream.WriteMethod writeMethod = asyncResult.AsyncDelegate as DeflateStream.WriteMethod;
			if (writeMethod == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "async_result");
			}
			writeMethod.EndInvoke(async_result);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007BB8 File Offset: 0x00005DB8
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007BC0 File Offset: 0x00005DC0
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public override bool CanRead
		{
			get
			{
				return !this.disposed && this.mode == CompressionMode.Decompress && this.base_stream.CanRead;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00007BF0 File Offset: 0x00005DF0
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00007BF4 File Offset: 0x00005DF4
		public override bool CanWrite
		{
			get
			{
				return !this.disposed && this.mode == CompressionMode.Compress && this.base_stream.CanWrite;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00007C1C File Offset: 0x00005E1C
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00007C24 File Offset: 0x00005E24
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00007C2C File Offset: 0x00005E2C
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

		// Token: 0x060001FD RID: 509
		[DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CreateZStream(CompressionMode compress, bool gzip, DeflateStream.UnmanagedReadOrWrite feeder, IntPtr data);

		// Token: 0x060001FE RID: 510
		[DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
		private static extern int CloseZStream(IntPtr stream);

		// Token: 0x060001FF RID: 511
		[DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
		private static extern int Flush(IntPtr stream);

		// Token: 0x06000200 RID: 512
		[DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
		private static extern int ReadZStream(IntPtr stream, IntPtr buffer, int length);

		// Token: 0x06000201 RID: 513
		[DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
		private static extern int WriteZStream(IntPtr stream, IntPtr buffer, int length);

		// Token: 0x0400009F RID: 159
		private const int BufferSize = 4096;

		// Token: 0x040000A0 RID: 160
		private const string LIBNAME = "__Internal";

		// Token: 0x040000A1 RID: 161
		private Stream base_stream;

		// Token: 0x040000A2 RID: 162
		private CompressionMode mode;

		// Token: 0x040000A3 RID: 163
		private bool leaveOpen;

		// Token: 0x040000A4 RID: 164
		private bool disposed;

		// Token: 0x040000A5 RID: 165
		private DeflateStream.UnmanagedReadOrWrite feeder;

		// Token: 0x040000A6 RID: 166
		private IntPtr z_stream;

		// Token: 0x040000A7 RID: 167
		private byte[] io_buffer;

		// Token: 0x040000A8 RID: 168
		private GCHandle data;

		// Token: 0x0200005F RID: 95
		// (Invoke) Token: 0x06000203 RID: 515
		private delegate int ReadMethod(byte[] array, int offset, int count);

		// Token: 0x02000060 RID: 96
		// (Invoke) Token: 0x06000207 RID: 519
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int UnmanagedReadOrWrite(IntPtr buffer, int length, IntPtr data);

		// Token: 0x02000061 RID: 97
		// (Invoke) Token: 0x0600020B RID: 523
		private delegate void WriteMethod(byte[] array, int offset, int count);
	}
}
