using System;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	public class AndroidJavaClass : AndroidJavaObject
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000033DC File Offset: 0x000015DC
		internal AndroidJavaClass(IntPtr jclass)
		{
			if (jclass == IntPtr.Zero)
			{
				throw new Exception("JNI: Init'd AndroidJavaClass with null ptr!");
			}
			this.m_jclass = AndroidJNI.NewGlobalRef(jclass);
			this.m_jobject = IntPtr.Zero;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003418 File Offset: 0x00001618
		public AndroidJavaClass(string className)
		{
			this._AndroidJavaClass(className);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003428 File Offset: 0x00001628
		private void _AndroidJavaClass(string className)
		{
			base.DebugPrint("Creating AndroidJavaClass from " + className);
			using (AndroidJavaObject androidJavaObject = AndroidJavaObject.FindClass(className))
			{
				this.m_jclass = AndroidJNI.NewGlobalRef(androidJavaObject.GetRawObject());
				this.m_jobject = IntPtr.Zero;
			}
		}
	}
}
