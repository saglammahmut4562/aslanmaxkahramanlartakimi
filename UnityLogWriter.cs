using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine
{
	// Token: 0x02000119 RID: 281
	internal sealed class UnityLogWriter : TextWriter
	{
		// Token: 0x06000961 RID: 2401
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void WriteStringToUnityLog(string s);

		// Token: 0x06000962 RID: 2402 RVA: 0x000177A0 File Offset: 0x000159A0
		public static void Init()
		{
			Console.SetOut(new UnityLogWriter());
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x000177AC File Offset: 0x000159AC
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000177B4 File Offset: 0x000159B4
		public override void Write(char value)
		{
			UnityLogWriter.WriteStringToUnityLog(value.ToString());
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000177C4 File Offset: 0x000159C4
		public override void Write(string s)
		{
			UnityLogWriter.WriteStringToUnityLog(s);
		}
	}
}
