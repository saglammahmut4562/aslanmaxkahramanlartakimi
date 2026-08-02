using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	public sealed class AnimationClip : Motion
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00006780 File Offset: 0x00004980
		public AnimationClip()
		{
			AnimationClip.Internal_CreateAnimationClip(this);
		}

		// Token: 0x0600019B RID: 411
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateAnimationClip([Writable] AnimationClip self);

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600019C RID: 412
		public extern float length
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600019D RID: 413
		internal extern float startTime
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600019E RID: 414
		internal extern float stopTime
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600019F RID: 415
		// (set) Token: 0x060001A0 RID: 416
		public extern float frameRate
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060001A1 RID: 417
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetCurve(string relativePath, Type type, string propertyName, AnimationCurve curve);

		// Token: 0x060001A2 RID: 418 RVA: 0x00006790 File Offset: 0x00004990
		public void EnsureQuaternionContinuity()
		{
			AnimationClip.INTERNAL_CALL_EnsureQuaternionContinuity(this);
		}

		// Token: 0x060001A3 RID: 419
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_EnsureQuaternionContinuity(AnimationClip self);

		// Token: 0x060001A4 RID: 420 RVA: 0x00006798 File Offset: 0x00004998
		public void ClearCurves()
		{
			AnimationClip.INTERNAL_CALL_ClearCurves(this);
		}

		// Token: 0x060001A5 RID: 421
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_ClearCurves(AnimationClip self);

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001A6 RID: 422
		// (set) Token: 0x060001A7 RID: 423
		public extern WrapMode wrapMode
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060001A8 RID: 424
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void AddEvent(AnimationEvent evt);

		// Token: 0x060001A9 RID: 425
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localBounds(out Bounds value);

		// Token: 0x060001AA RID: 426
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localBounds(ref Bounds value);

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000067A0 File Offset: 0x000049A0
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000067B8 File Offset: 0x000049B8
		public Bounds localBounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_localBounds(out bounds);
				return bounds;
			}
			set
			{
				this.INTERNAL_set_localBounds(ref value);
			}
		}
	}
}
