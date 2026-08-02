using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200005B RID: 91
	public sealed class GL
	{
		// Token: 0x060003F6 RID: 1014
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void Vertex3(float x, float y, float z);

		// Token: 0x060003F7 RID: 1015
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void TexCoord2(float x, float y);

		// Token: 0x060003F8 RID: 1016
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void MultiTexCoord2(int unit, float x, float y);

		// Token: 0x060003F9 RID: 1017
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void Begin(int mode);

		// Token: 0x060003FA RID: 1018
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void End();

		// Token: 0x060003FB RID: 1019
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void LoadOrtho();

		// Token: 0x060003FC RID: 1020 RVA: 0x00009378 File Offset: 0x00007578
		public static void LoadProjectionMatrix(Matrix4x4 mat)
		{
			GL.INTERNAL_CALL_LoadProjectionMatrix(ref mat);
		}

		// Token: 0x060003FD RID: 1021
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_LoadProjectionMatrix(ref Matrix4x4 mat);

		// Token: 0x060003FE RID: 1022
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void LoadIdentity();

		// Token: 0x060003FF RID: 1023
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void PushMatrix();

		// Token: 0x06000400 RID: 1024
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void PopMatrix();

		// Token: 0x06000401 RID: 1025 RVA: 0x00009384 File Offset: 0x00007584
		public static Matrix4x4 GetGPUProjectionMatrix(Matrix4x4 proj, bool renderIntoTexture)
		{
			return GL.INTERNAL_CALL_GetGPUProjectionMatrix(ref proj, renderIntoTexture);
		}

		// Token: 0x06000402 RID: 1026
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Matrix4x4 INTERNAL_CALL_GetGPUProjectionMatrix(ref Matrix4x4 proj, bool renderIntoTexture);

		// Token: 0x06000403 RID: 1027 RVA: 0x00009390 File Offset: 0x00007590
		[ExcludeFromDocs]
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor)
		{
			float num = 1f;
			GL.Clear(clearDepth, clearColor, backgroundColor, num);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000093AC File Offset: 0x000075AC
		public static void Clear(bool clearDepth, bool clearColor, Color backgroundColor, [DefaultValue("1.0f")] float depth)
		{
			GL.Internal_Clear(clearDepth, clearColor, backgroundColor, depth);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000093B8 File Offset: 0x000075B8
		private static void Internal_Clear(bool clearDepth, bool clearColor, Color backgroundColor, float depth)
		{
			GL.INTERNAL_CALL_Internal_Clear(clearDepth, clearColor, ref backgroundColor, depth);
		}

		// Token: 0x06000406 RID: 1030
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_Clear(bool clearDepth, bool clearColor, ref Color backgroundColor, float depth);

		// Token: 0x06000407 RID: 1031
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void ClearWithSkybox(bool clearDepth, Camera camera);

		// Token: 0x040000CB RID: 203
		public const int TRIANGLES = 4;

		// Token: 0x040000CC RID: 204
		public const int TRIANGLE_STRIP = 5;

		// Token: 0x040000CD RID: 205
		public const int QUADS = 7;

		// Token: 0x040000CE RID: 206
		public const int LINES = 1;
	}
}
