using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000063 RID: 99
	internal sealed class GUIClip
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x0000BE5C File Offset: 0x0000A05C
		internal static void Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.INTERNAL_CALL_Push(ref screenRect, ref scrollOffset, ref renderOffset, resetOffset);
		}

		// Token: 0x060004DF RID: 1247
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Push(ref Rect screenRect, ref Vector2 scrollOffset, ref Vector2 renderOffset, bool resetOffset);

		// Token: 0x060004E0 RID: 1248
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void Pop();

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000BE6C File Offset: 0x0000A06C
		public static Vector2 Unclip(Vector2 pos)
		{
			GUIClip.Unclip_Vector2(ref pos);
			return pos;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000BE78 File Offset: 0x0000A078
		private static void Unclip_Vector2(ref Vector2 pos)
		{
			GUIClip.INTERNAL_CALL_Unclip_Vector2(ref pos);
		}

		// Token: 0x060004E3 RID: 1251
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Unclip_Vector2(ref Vector2 pos);

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000BE80 File Offset: 0x0000A080
		public static Vector2 Clip(Vector2 absolutePos)
		{
			GUIClip.Clip_Vector2(ref absolutePos);
			return absolutePos;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000BE8C File Offset: 0x0000A08C
		private static void Clip_Vector2(ref Vector2 absolutePos)
		{
			GUIClip.INTERNAL_CALL_Clip_Vector2(ref absolutePos);
		}

		// Token: 0x060004E6 RID: 1254
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Clip_Vector2(ref Vector2 absolutePos);

		// Token: 0x060004E7 RID: 1255
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern Matrix4x4 GetMatrix();

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000BE94 File Offset: 0x0000A094
		internal static void SetMatrix(Matrix4x4 m)
		{
			GUIClip.INTERNAL_CALL_SetMatrix(ref m);
		}

		// Token: 0x060004E9 RID: 1257
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetMatrix(ref Matrix4x4 m);
	}
}
