using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	public sealed class Camera : Behaviour
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002A4 RID: 676
		// (set) Token: 0x060002A5 RID: 677
		public extern float fieldOfView
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002A6 RID: 678
		public extern float nearClipPlane
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002A7 RID: 679
		public extern float farClipPlane
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002A8 RID: 680
		public extern bool hdr
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002A9 RID: 681
		public extern float orthographicSize
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002AA RID: 682
		// (set) Token: 0x060002AB RID: 683
		public extern bool orthographic
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000071 RID: 113
		// (set) Token: 0x060002AC RID: 684
		public extern TransparencySortMode transparencySortMode
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00007580 File Offset: 0x00005780
		public bool isOrthoGraphic
		{
			get
			{
				return this.orthographic;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002AE RID: 686
		public extern float depth
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002AF RID: 687
		public extern float aspect
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002B0 RID: 688
		// (set) Token: 0x060002B1 RID: 689
		public extern int cullingMask
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002B2 RID: 690
		public extern int eventMask
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060002B3 RID: 691
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_rect(out Rect value);

		// Token: 0x060002B4 RID: 692
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_rect(ref Rect value);

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00007588 File Offset: 0x00005788
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x000075A0 File Offset: 0x000057A0
		public Rect rect
		{
			get
			{
				Rect rect;
				this.INTERNAL_get_rect(out rect);
				return rect;
			}
			set
			{
				this.INTERNAL_set_rect(ref value);
			}
		}

		// Token: 0x060002B7 RID: 695
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_pixelRect(out Rect value);

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000075AC File Offset: 0x000057AC
		public Rect pixelRect
		{
			get
			{
				Rect rect;
				this.INTERNAL_get_pixelRect(out rect);
				return rect;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002B9 RID: 697
		// (set) Token: 0x060002BA RID: 698
		public extern RenderTexture targetTexture
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002BB RID: 699
		public extern float pixelWidth
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002BC RID: 700
		public extern float pixelHeight
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060002BD RID: 701
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_worldToCameraMatrix(out Matrix4x4 value);

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000075C4 File Offset: 0x000057C4
		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.INTERNAL_get_worldToCameraMatrix(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x060002BF RID: 703
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_projectionMatrix(out Matrix4x4 value);

		// Token: 0x060002C0 RID: 704
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_projectionMatrix(ref Matrix4x4 value);

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x000075DC File Offset: 0x000057DC
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x000075F4 File Offset: 0x000057F4
		public Matrix4x4 projectionMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.INTERNAL_get_projectionMatrix(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.INTERNAL_set_projectionMatrix(ref value);
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00007600 File Offset: 0x00005800
		public void ResetProjectionMatrix()
		{
			Camera.INTERNAL_CALL_ResetProjectionMatrix(this);
		}

		// Token: 0x060002C4 RID: 708
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_ResetProjectionMatrix(Camera self);

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002C5 RID: 709
		// (set) Token: 0x060002C6 RID: 710
		public extern CameraClearFlags clearFlags
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00007608 File Offset: 0x00005808
		public Vector3 WorldToViewportPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_WorldToViewportPoint(this, ref position);
		}

		// Token: 0x060002C8 RID: 712
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_WorldToViewportPoint(Camera self, ref Vector3 position);

		// Token: 0x060002C9 RID: 713 RVA: 0x00007614 File Offset: 0x00005814
		public Vector3 ScreenToWorldPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ScreenToWorldPoint(this, ref position);
		}

		// Token: 0x060002CA RID: 714
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_ScreenToWorldPoint(Camera self, ref Vector3 position);

		// Token: 0x060002CB RID: 715 RVA: 0x00007620 File Offset: 0x00005820
		public Ray ScreenPointToRay(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ScreenPointToRay(this, ref position);
		}

		// Token: 0x060002CC RID: 716
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Ray INTERNAL_CALL_ScreenPointToRay(Camera self, ref Vector3 position);

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002CD RID: 717
		public static extern Camera main
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002CE RID: 718
		public static extern int allCamerasCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060002CF RID: 719
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetAllCameras(Camera[] cameras);

		// Token: 0x060002D0 RID: 720
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Render();

		// Token: 0x060002D1 RID: 721
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void RenderWithShader(Shader shader, string replacementTag);

		// Token: 0x060002D2 RID: 722
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void CopyFrom(Camera other);

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002D3 RID: 723
		// (set) Token: 0x060002D4 RID: 724
		public extern DepthTextureMode depthTextureMode
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}
	}
}
