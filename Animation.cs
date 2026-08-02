using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	public sealed class Animation : Behaviour, IEnumerable
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000157 RID: 343
		// (set) Token: 0x06000158 RID: 344
		public extern AnimationClip clip
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000159 RID: 345
		// (set) Token: 0x0600015A RID: 346
		public extern bool playAutomatically
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600015B RID: 347
		// (set) Token: 0x0600015C RID: 348
		public extern WrapMode wrapMode
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000064E4 File Offset: 0x000046E4
		public void Stop()
		{
			Animation.INTERNAL_CALL_Stop(this);
		}

		// Token: 0x0600015E RID: 350
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Stop(Animation self);

		// Token: 0x0600015F RID: 351 RVA: 0x000064EC File Offset: 0x000046EC
		public void Stop(string name)
		{
			this.Internal_StopByName(name);
		}

		// Token: 0x06000160 RID: 352
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_StopByName(string name);

		// Token: 0x06000161 RID: 353 RVA: 0x000064F8 File Offset: 0x000046F8
		public void Rewind(string name)
		{
			this.Internal_RewindByName(name);
		}

		// Token: 0x06000162 RID: 354
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_RewindByName(string name);

		// Token: 0x06000163 RID: 355 RVA: 0x00006504 File Offset: 0x00004704
		public void Rewind()
		{
			Animation.INTERNAL_CALL_Rewind(this);
		}

		// Token: 0x06000164 RID: 356
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Rewind(Animation self);

		// Token: 0x06000165 RID: 357 RVA: 0x0000650C File Offset: 0x0000470C
		public void Sample()
		{
			Animation.INTERNAL_CALL_Sample(this);
		}

		// Token: 0x06000166 RID: 358
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Sample(Animation self);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000167 RID: 359
		public extern bool isPlaying
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000168 RID: 360
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool IsPlaying(string name);

		// Token: 0x17000007 RID: 7
		public AnimationState this[string name]
		{
			get
			{
				return this.GetState(name);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006520 File Offset: 0x00004720
		[ExcludeFromDocs]
		public bool Play()
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			return this.Play(playMode);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006538 File Offset: 0x00004738
		public bool Play([DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			return this.PlayDefaultAnimation(mode);
		}

		// Token: 0x0600016C RID: 364
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool Play(string animation, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x0600016D RID: 365 RVA: 0x00006544 File Offset: 0x00004744
		[ExcludeFromDocs]
		public bool Play(string animation)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			return this.Play(animation, playMode);
		}

		// Token: 0x0600016E RID: 366
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void CrossFade(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x0600016F RID: 367 RVA: 0x0000655C File Offset: 0x0000475C
		[ExcludeFromDocs]
		public void CrossFade(string animation, float fadeLength)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			this.CrossFade(animation, fadeLength, playMode);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006574 File Offset: 0x00004774
		[ExcludeFromDocs]
		public void CrossFade(string animation)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			float num = 0.3f;
			this.CrossFade(animation, num, playMode);
		}

		// Token: 0x06000171 RID: 369
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Blend(string animation, [DefaultValue("1.0F")] float targetWeight, [DefaultValue("0.3F")] float fadeLength);

		// Token: 0x06000172 RID: 370 RVA: 0x00006594 File Offset: 0x00004794
		[ExcludeFromDocs]
		public void Blend(string animation, float targetWeight)
		{
			float num = 0.3f;
			this.Blend(animation, targetWeight, num);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000065B0 File Offset: 0x000047B0
		[ExcludeFromDocs]
		public void Blend(string animation)
		{
			float num = 0.3f;
			float num2 = 1f;
			this.Blend(animation, num2, num);
		}

		// Token: 0x06000174 RID: 372
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern AnimationState CrossFadeQueued(string animation, [DefaultValue("0.3F")] float fadeLength, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06000175 RID: 373 RVA: 0x000065D4 File Offset: 0x000047D4
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength, QueueMode queue)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			return this.CrossFadeQueued(animation, fadeLength, queue, playMode);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000065F0 File Offset: 0x000047F0
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			QueueMode queueMode = QueueMode.CompleteOthers;
			return this.CrossFadeQueued(animation, fadeLength, queueMode, playMode);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000660C File Offset: 0x0000480C
		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			QueueMode queueMode = QueueMode.CompleteOthers;
			float num = 0.3f;
			return this.CrossFadeQueued(animation, num, queueMode, playMode);
		}

		// Token: 0x06000178 RID: 376
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern AnimationState PlayQueued(string animation, [DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		// Token: 0x06000179 RID: 377 RVA: 0x00006630 File Offset: 0x00004830
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation, QueueMode queue)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			return this.PlayQueued(animation, queue, playMode);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006648 File Offset: 0x00004848
		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation)
		{
			PlayMode playMode = PlayMode.StopSameLayer;
			QueueMode queueMode = QueueMode.CompleteOthers;
			return this.PlayQueued(animation, queueMode, playMode);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006664 File Offset: 0x00004864
		public void AddClip(AnimationClip clip, string newName)
		{
			this.AddClip(clip, newName, int.MinValue, int.MaxValue);
		}

		// Token: 0x0600017C RID: 380
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame, [DefaultValue("false")] bool addLoopFrame);

		// Token: 0x0600017D RID: 381 RVA: 0x00006678 File Offset: 0x00004878
		[ExcludeFromDocs]
		public void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame)
		{
			bool flag = false;
			this.AddClip(clip, newName, firstFrame, lastFrame, flag);
		}

		// Token: 0x0600017E RID: 382
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void RemoveClip(AnimationClip clip);

		// Token: 0x0600017F RID: 383 RVA: 0x00006694 File Offset: 0x00004894
		public void RemoveClip(string clipName)
		{
			this.RemoveClip2(clipName);
		}

		// Token: 0x06000180 RID: 384
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int GetClipCount();

		// Token: 0x06000181 RID: 385
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void RemoveClip2(string clipName);

		// Token: 0x06000182 RID: 386
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern bool PlayDefaultAnimation(PlayMode mode);

		// Token: 0x06000183 RID: 387 RVA: 0x000066A0 File Offset: 0x000048A0
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(AnimationPlayMode mode)
		{
			return this.PlayDefaultAnimation((PlayMode)mode);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000066AC File Offset: 0x000048AC
		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(string animation, AnimationPlayMode mode)
		{
			return this.Play(animation, (PlayMode)mode);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000066B8 File Offset: 0x000048B8
		public void SyncLayer(int layer)
		{
			Animation.INTERNAL_CALL_SyncLayer(this, layer);
		}

		// Token: 0x06000186 RID: 390
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SyncLayer(Animation self, int layer);

		// Token: 0x06000187 RID: 391 RVA: 0x000066C4 File Offset: 0x000048C4
		public IEnumerator GetEnumerator()
		{
			return new Animation.Enumerator(this);
		}

		// Token: 0x06000188 RID: 392
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern AnimationState GetState(string name);

		// Token: 0x06000189 RID: 393
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern AnimationState GetStateAtIndex(int index);

		// Token: 0x0600018A RID: 394
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern int GetStateCount();

		// Token: 0x0600018B RID: 395 RVA: 0x000066CC File Offset: 0x000048CC
		public AnimationClip GetClip(string name)
		{
			AnimationState state = this.GetState(name);
			if (state)
			{
				return state.clip;
			}
			return null;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600018C RID: 396
		// (set) Token: 0x0600018D RID: 397
		public extern bool animatePhysics
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600018E RID: 398
		// (set) Token: 0x0600018F RID: 399
		[Obsolete("Use cullingType instead")]
		public extern bool animateOnlyIfVisible
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000190 RID: 400
		// (set) Token: 0x06000191 RID: 401
		public extern AnimationCullingType cullingType
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000192 RID: 402
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localBounds(out Bounds value);

		// Token: 0x06000193 RID: 403
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localBounds(ref Bounds value);

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000066F4 File Offset: 0x000048F4
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000670C File Offset: 0x0000490C
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

		// Token: 0x0200000F RID: 15
		private sealed class Enumerator : IEnumerator
		{
			// Token: 0x06000196 RID: 406 RVA: 0x00006718 File Offset: 0x00004918
			internal Enumerator(Animation outer)
			{
				this.m_Outer = outer;
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000197 RID: 407 RVA: 0x00006730 File Offset: 0x00004930
			public object Current
			{
				get
				{
					return this.m_Outer.GetStateAtIndex(this.m_CurrentIndex);
				}
			}

			// Token: 0x06000198 RID: 408 RVA: 0x00006744 File Offset: 0x00004944
			public bool MoveNext()
			{
				int stateCount = this.m_Outer.GetStateCount();
				this.m_CurrentIndex++;
				return this.m_CurrentIndex < stateCount;
			}

			// Token: 0x06000199 RID: 409 RVA: 0x00006774 File Offset: 0x00004974
			public void Reset()
			{
				this.m_CurrentIndex = -1;
			}

			// Token: 0x04000010 RID: 16
			private Animation m_Outer;

			// Token: 0x04000011 RID: 17
			private int m_CurrentIndex = -1;
		}
	}
}
