using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[StructLayout(0)]
	public sealed class AnimationCurve
	{
		// Token: 0x060001AD RID: 429 RVA: 0x000067C4 File Offset: 0x000049C4
		public AnimationCurve(params Keyframe[] keys)
		{
			this.Init(keys);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000067D4 File Offset: 0x000049D4
		public AnimationCurve()
		{
			this.Init(null);
		}

		// Token: 0x060001AF RID: 431
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x060001B0 RID: 432 RVA: 0x000067E4 File Offset: 0x000049E4
		~AnimationCurve()
		{
			this.Cleanup();
		}

		// Token: 0x060001B1 RID: 433
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern float Evaluate(float time);

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006814 File Offset: 0x00004A14
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000681C File Offset: 0x00004A1C
		public Keyframe[] keys
		{
			get
			{
				return this.GetKeys();
			}
			set
			{
				this.SetKeys(value);
			}
		}

		// Token: 0x060001B4 RID: 436
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int AddKey(float time, float value);

		// Token: 0x060001B5 RID: 437 RVA: 0x00006828 File Offset: 0x00004A28
		public int AddKey(Keyframe key)
		{
			return this.AddKey_Internal(key);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006834 File Offset: 0x00004A34
		private int AddKey_Internal(Keyframe key)
		{
			return AnimationCurve.INTERNAL_CALL_AddKey_Internal(this, ref key);
		}

		// Token: 0x060001B7 RID: 439
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int INTERNAL_CALL_AddKey_Internal(AnimationCurve self, ref Keyframe key);

		// Token: 0x060001B8 RID: 440 RVA: 0x00006840 File Offset: 0x00004A40
		public int MoveKey(int index, Keyframe key)
		{
			return AnimationCurve.INTERNAL_CALL_MoveKey(this, index, ref key);
		}

		// Token: 0x060001B9 RID: 441
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int INTERNAL_CALL_MoveKey(AnimationCurve self, int index, ref Keyframe key);

		// Token: 0x060001BA RID: 442
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void RemoveKey(int index);

		// Token: 0x17000014 RID: 20
		public Keyframe this[int index]
		{
			get
			{
				return this.GetKey_Internal(index);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001BC RID: 444
		public extern int length
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060001BD RID: 445
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void SetKeys(Keyframe[] keys);

		// Token: 0x060001BE RID: 446
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Keyframe GetKey_Internal(int index);

		// Token: 0x060001BF RID: 447
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Keyframe[] GetKeys();

		// Token: 0x060001C0 RID: 448
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SmoothTangents(int index, float weight);

		// Token: 0x060001C1 RID: 449 RVA: 0x00006858 File Offset: 0x00004A58
		public static AnimationCurve Linear(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			float num = (valueEnd - valueStart) / (timeEnd - timeStart);
			Keyframe[] array = new Keyframe[]
			{
				new Keyframe(timeStart, valueStart, 0f, num),
				new Keyframe(timeEnd, valueEnd, num, 0f)
			};
			return new AnimationCurve(array);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000068AC File Offset: 0x00004AAC
		public static AnimationCurve EaseInOut(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			Keyframe[] array = new Keyframe[]
			{
				new Keyframe(timeStart, valueStart, 0f, 0f),
				new Keyframe(timeEnd, valueEnd, 0f, 0f)
			};
			return new AnimationCurve(array);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001C3 RID: 451
		// (set) Token: 0x060001C4 RID: 452
		public extern WrapMode preWrapMode
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001C5 RID: 453
		// (set) Token: 0x060001C6 RID: 454
		public extern WrapMode postWrapMode
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060001C7 RID: 455
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init(Keyframe[] keys);

		// Token: 0x04000017 RID: 23
		internal IntPtr m_Ptr;
	}
}
