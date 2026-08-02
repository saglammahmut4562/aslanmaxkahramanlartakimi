using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000045 RID: 69
	public sealed class Debug
	{
		// Token: 0x0600031D RID: 797 RVA: 0x00007BD4 File Offset: 0x00005DD4
		public static void DrawLine(Vector3 start, Vector3 end, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.INTERNAL_CALL_DrawLine(ref start, ref end, ref color, duration, depthTest);
		}

		// Token: 0x0600031E RID: 798
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_DrawLine(ref Vector3 start, ref Vector3 end, ref Color color, float duration, bool depthTest);

		// Token: 0x0600031F RID: 799 RVA: 0x00007BE4 File Offset: 0x00005DE4
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color)
		{
			bool flag = true;
			float num = 0f;
			Debug.DrawRay(start, dir, color, num, flag);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00007C04 File Offset: 0x00005E04
		public static void DrawRay(Vector3 start, Vector3 dir, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine(start, start + dir, color, duration, depthTest);
		}

		// Token: 0x06000321 RID: 801
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_Log(int level, string msg, [Writable] Object obj);

		// Token: 0x06000322 RID: 802
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_LogException(Exception exception, [Writable] Object obj);

		// Token: 0x06000323 RID: 803 RVA: 0x00007C18 File Offset: 0x00005E18
		public static void Log(object message)
		{
			Debug.Internal_Log(0, (message == null) ? "Null" : message.ToString(), null);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00007C38 File Offset: 0x00005E38
		public static void LogError(object message)
		{
			Debug.Internal_Log(2, (message == null) ? "Null" : message.ToString(), null);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00007C58 File Offset: 0x00005E58
		public static void LogException(Exception exception)
		{
			Debug.Internal_LogException(exception, null);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00007C64 File Offset: 0x00005E64
		public static void LogWarning(object message)
		{
			Debug.Internal_Log(1, message.ToString(), null);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00007C74 File Offset: 0x00005E74
		public static void LogWarning(object message, Object context)
		{
			Debug.Internal_Log(1, message.ToString(), context);
		}
	}
}
