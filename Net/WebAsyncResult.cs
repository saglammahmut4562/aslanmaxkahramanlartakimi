using System;
using System.IO;
using System.Threading;

namespace System.Net
{
	// Token: 0x020000A5 RID: 165
	internal class WebAsyncResult : IAsyncResult
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x000112E8 File Offset: 0x0000F4E8
		public WebAsyncResult(AsyncCallback cb, object state)
		{
			this.cb = cb;
			this.state = state;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001130C File Offset: 0x0000F50C
		public WebAsyncResult(HttpWebRequest request, AsyncCallback cb, object state)
		{
			this.cb = cb;
			this.state = state;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00011330 File Offset: 0x0000F530
		public WebAsyncResult(AsyncCallback cb, object state, byte[] buffer, int offset, int size)
		{
			this.cb = cb;
			this.state = state;
			this.buffer = buffer;
			this.offset = offset;
			this.size = size;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00011368 File Offset: 0x0000F568
		internal void SetCompleted(bool synch, Exception e)
		{
			this.synch = synch;
			this.exc = e;
			object obj = this.locker;
			lock (obj)
			{
				this.isCompleted = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000113CC File Offset: 0x0000F5CC
		internal void Reset()
		{
			this.callbackDone = false;
			this.exc = null;
			this.response = null;
			this.writeStream = null;
			this.exc = null;
			object obj = this.locker;
			lock (obj)
			{
				this.isCompleted = false;
				if (this.handle != null)
				{
					this.handle.Reset();
				}
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00011444 File Offset: 0x0000F644
		internal void SetCompleted(bool synch, int nbytes)
		{
			this.synch = synch;
			this.nbytes = nbytes;
			this.exc = null;
			object obj = this.locker;
			lock (obj)
			{
				this.isCompleted = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000114B0 File Offset: 0x0000F6B0
		internal void SetCompleted(bool synch, Stream writeStream)
		{
			this.synch = synch;
			this.writeStream = writeStream;
			this.exc = null;
			object obj = this.locker;
			lock (obj)
			{
				this.isCompleted = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001151C File Offset: 0x0000F71C
		internal void SetCompleted(bool synch, HttpWebResponse response)
		{
			this.synch = synch;
			this.response = response;
			this.exc = null;
			object obj = this.locker;
			lock (obj)
			{
				this.isCompleted = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00011588 File Offset: 0x0000F788
		internal void DoCallback()
		{
			if (!this.callbackDone && this.cb != null)
			{
				this.callbackDone = true;
				this.cb(this);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000115B4 File Offset: 0x0000F7B4
		internal void WaitUntilComplete()
		{
			if (this.IsCompleted)
			{
				return;
			}
			this.AsyncWaitHandle.WaitOne();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000115D0 File Offset: 0x0000F7D0
		internal bool WaitUntilComplete(int timeout, bool exitContext)
		{
			return this.IsCompleted || this.AsyncWaitHandle.WaitOne(timeout, exitContext);
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x000115EC File Offset: 0x0000F7EC
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x000115F4 File Offset: 0x0000F7F4
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.isCompleted);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0001164C File Offset: 0x0000F84C
		public bool IsCompleted
		{
			get
			{
				object obj = this.locker;
				bool flag;
				lock (obj)
				{
					flag = this.isCompleted;
				}
				return flag;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00011690 File Offset: 0x0000F890
		internal bool GotException
		{
			get
			{
				return this.exc != null;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x000116A0 File Offset: 0x0000F8A0
		internal Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000116A8 File Offset: 0x0000F8A8
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x000116B0 File Offset: 0x0000F8B0
		internal int NBytes
		{
			get
			{
				return this.nbytes;
			}
			set
			{
				this.nbytes = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x000116BC File Offset: 0x0000F8BC
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x000116C4 File Offset: 0x0000F8C4
		internal IAsyncResult InnerAsyncResult
		{
			get
			{
				return this.innerAsyncResult;
			}
			set
			{
				this.innerAsyncResult = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x000116D0 File Offset: 0x0000F8D0
		internal HttpWebResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x000116D8 File Offset: 0x0000F8D8
		internal byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000116E0 File Offset: 0x0000F8E0
		internal int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x000116E8 File Offset: 0x0000F8E8
		internal int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x040002B8 RID: 696
		private ManualResetEvent handle;

		// Token: 0x040002B9 RID: 697
		private bool synch;

		// Token: 0x040002BA RID: 698
		private bool isCompleted;

		// Token: 0x040002BB RID: 699
		private AsyncCallback cb;

		// Token: 0x040002BC RID: 700
		private object state;

		// Token: 0x040002BD RID: 701
		private int nbytes;

		// Token: 0x040002BE RID: 702
		private IAsyncResult innerAsyncResult;

		// Token: 0x040002BF RID: 703
		private bool callbackDone;

		// Token: 0x040002C0 RID: 704
		private Exception exc;

		// Token: 0x040002C1 RID: 705
		private HttpWebResponse response;

		// Token: 0x040002C2 RID: 706
		private Stream writeStream;

		// Token: 0x040002C3 RID: 707
		private byte[] buffer;

		// Token: 0x040002C4 RID: 708
		private int offset;

		// Token: 0x040002C5 RID: 709
		private int size;

		// Token: 0x040002C6 RID: 710
		private object locker = new object();

		// Token: 0x040002C7 RID: 711
		public bool EndCalled;

		// Token: 0x040002C8 RID: 712
		public bool AsyncWriteAll;
	}
}
