using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200005F RID: 95
	public sealed class Graphics
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x00009404 File Offset: 0x00007604
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix)
		{
			Graphics.Internal_DrawMeshNow2(mesh, matrix, -1);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00009410 File Offset: 0x00007610
		private static void Internal_DrawMeshNow2(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			Graphics.INTERNAL_CALL_Internal_DrawMeshNow2(mesh, ref matrix, materialIndex);
		}

		// Token: 0x06000414 RID: 1044
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_DrawMeshNow2(Mesh mesh, ref Matrix4x4 matrix, int materialIndex);

		// Token: 0x06000415 RID: 1045
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void DrawProceduralIndirect(MeshTopology topology, ComputeBuffer bufferWithArgs, [DefaultValue("0")] int argsOffset);

		// Token: 0x06000416 RID: 1046
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void DrawTexture(ref InternalDrawTextureArguments arguments);

		// Token: 0x06000417 RID: 1047
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void Blit(Texture source, RenderTexture dest);

		// Token: 0x06000418 RID: 1048 RVA: 0x0000941C File Offset: 0x0000761C
		[ExcludeFromDocs]
		public static void Blit(Texture source, RenderTexture dest, Material mat)
		{
			int num = -1;
			Graphics.Blit(source, dest, mat, num);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00009434 File Offset: 0x00007634
		public static void Blit(Texture source, RenderTexture dest, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial(source, dest, mat, pass, true);
		}

		// Token: 0x0600041A RID: 1050
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_BlitMaterial(Texture source, RenderTexture dest, Material mat, int pass, bool setRT);

		// Token: 0x0600041B RID: 1051 RVA: 0x00009440 File Offset: 0x00007640
		public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, params Vector2[] offsets)
		{
			Graphics.Internal_BlitMultiTap(source, dest, mat, offsets);
		}

		// Token: 0x0600041C RID: 1052
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_BlitMultiTap(Texture source, RenderTexture dest, Material mat, Vector2[] offsets);

		// Token: 0x0600041D RID: 1053 RVA: 0x0000944C File Offset: 0x0000764C
		public static void SetRenderTarget(RenderTexture rt)
		{
			Graphics.Internal_SetRT(rt, 0, -1);
		}

		// Token: 0x0600041E RID: 1054
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetRT(RenderTexture rt, int mipLevel, int face);

		// Token: 0x0600041F RID: 1055 RVA: 0x00009458 File Offset: 0x00007658
		public static void SetRandomWriteTarget(int index, ComputeBuffer uav)
		{
			Graphics.Internal_SetRandomWriteTargetBuffer(index, uav);
		}

		// Token: 0x06000420 RID: 1056
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void ClearRandomWriteTargets();

		// Token: 0x06000421 RID: 1057
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetRandomWriteTargetBuffer(int index, ComputeBuffer uav);
	}
}
