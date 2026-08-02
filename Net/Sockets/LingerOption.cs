using System;

namespace System.Net.Sockets
{
	// Token: 0x02000094 RID: 148
	public class LingerOption
	{
		// Token: 0x06000386 RID: 902 RVA: 0x0000F67C File Offset: 0x0000D87C
		public LingerOption(bool enable, int secs)
		{
			this.enabled = enable;
			this.seconds = secs;
		}

		// Token: 0x040001D5 RID: 469
		private bool enabled;

		// Token: 0x040001D6 RID: 470
		private int seconds;
	}
}
