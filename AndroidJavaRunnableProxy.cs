using System;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	internal class AndroidJavaRunnableProxy : AndroidJavaProxy
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00004C6C File Offset: 0x00002E6C
		public AndroidJavaRunnableProxy(AndroidJavaRunnable runnable)
			: base("java/lang/Runnable")
		{
			this.mRunnable = runnable;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004C80 File Offset: 0x00002E80
		public void run()
		{
			this.mRunnable();
		}

		// Token: 0x04000009 RID: 9
		private AndroidJavaRunnable mRunnable;
	}
}
