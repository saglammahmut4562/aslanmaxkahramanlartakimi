using System;

namespace System.IO.Compression
{
	// Token: 0x02000062 RID: 98
	public class GZipStream : Stream
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00007C34 File Offset: 0x00005E34
		public GZipStream(Stream compressedStream, CompressionMode mode)
			: this(compressedStream, mode, false)
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007C40 File Offset: 0x00005E40
		public GZipStream(Stream compressedStream, CompressionMode mode, bool leaveOpen)
		{
			this.deflateStream = new DeflateStream(compressedStream, mode, leaveOpen, true);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007C58 File Offset: 0x00005E58
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.deflateStream.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007C74 File Offset: 0x00005E74
		public override int Read(byte[] dest, int dest_offset, int count)
		{
			return this.deflateStream.Read(dest, dest_offset, count);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007C84 File Offset: 0x00005E84
		public override void Write(byte[] src, int src_offset, int count)
		{
			this.deflateStream.Write(src, src_offset, count);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007C94 File Offset: 0x00005E94
		public override void Flush()
		{
			this.deflateStream.Flush();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007CA4 File Offset: 0x00005EA4
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.deflateStream.Seek(offset, origin);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007CB4 File Offset: 0x00005EB4
		public override void SetLength(long value)
		{
			this.deflateStream.SetLength(value);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			return this.deflateStream.BeginRead(buffer, offset, count, cback, state);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007CD8 File Offset: 0x00005ED8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			return this.deflateStream.BeginWrite(buffer, offset, count, cback, state);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007CEC File Offset: 0x00005EEC
		public override int EndRead(IAsyncResult async_result)
		{
			return this.deflateStream.EndRead(async_result);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007CFC File Offset: 0x00005EFC
		public override void EndWrite(IAsyncResult async_result)
		{
			this.deflateStream.EndWrite(async_result);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00007D0C File Offset: 0x00005F0C
		public override bool CanRead
		{
			get
			{
				return this.deflateStream.CanRead;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00007D1C File Offset: 0x00005F1C
		public override bool CanSeek
		{
			get
			{
				return this.deflateStream.CanSeek;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00007D2C File Offset: 0x00005F2C
		public override bool CanWrite
		{
			get
			{
				return this.deflateStream.CanWrite;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00007D3C File Offset: 0x00005F3C
		public override long Length
		{
			get
			{
				return this.deflateStream.Length;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00007D4C File Offset: 0x00005F4C
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00007D5C File Offset: 0x00005F5C
		public override long Position
		{
			get
			{
				return this.deflateStream.Position;
			}
			set
			{
				this.deflateStream.Position = value;
			}
		}

		// Token: 0x040000A9 RID: 169
		private DeflateStream deflateStream;
	}
}
