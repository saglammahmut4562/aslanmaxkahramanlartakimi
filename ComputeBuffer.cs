using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace UnityEngine
{
	// Token: 0x0200003F RID: 63
	public sealed class ComputeBuffer : IDisposable
	{
		// Token: 0x06000311 RID: 785 RVA: 0x00007B10 File Offset: 0x00005D10
		public ComputeBuffer(int count, int stride, ComputeBufferType type)
		{
			this.m_Ptr = IntPtr.Zero;
			ComputeBuffer.InitBuffer(this, count, stride, type);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00007B2C File Offset: 0x00005D2C
		~ComputeBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00007B5C File Offset: 0x00005D5C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00007B6C File Offset: 0x00005D6C
		private void Dispose(bool disposing)
		{
			ComputeBuffer.DestroyBuffer(this);
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06000315 RID: 789
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void InitBuffer(ComputeBuffer buf, int count, int stride, ComputeBufferType type);

		// Token: 0x06000316 RID: 790
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void DestroyBuffer(ComputeBuffer buf);

		// Token: 0x06000317 RID: 791 RVA: 0x00007B80 File Offset: 0x00005D80
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00007B88 File Offset: 0x00005D88
		[SecuritySafeCritical]
		public void SetData(Array data)
		{
			this.InternalSetData(data, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000319 RID: 793
		[WrapperlessIcall]
		[SecurityCritical]
		[MethodImpl(4096)]
		private extern void InternalSetData(Array data, int elemSize);

		// Token: 0x0600031A RID: 794
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void CopyCount(ComputeBuffer src, ComputeBuffer dst, int dstOffset);

		// Token: 0x04000065 RID: 101
		internal IntPtr m_Ptr;
	}
}
