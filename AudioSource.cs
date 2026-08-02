using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	public sealed class AudioSource : Behaviour
	{
		// Token: 0x17000046 RID: 70
		// (set) Token: 0x06000252 RID: 594
		public extern float volume
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000253 RID: 595
		// (set) Token: 0x06000254 RID: 596
		public extern float pitch
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000255 RID: 597
		// (set) Token: 0x06000256 RID: 598
		public extern float time
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000257 RID: 599
		// (set) Token: 0x06000258 RID: 600
		public extern AudioClip clip
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000259 RID: 601
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Play([DefaultValue("0")] ulong delay);

		// Token: 0x0600025A RID: 602 RVA: 0x000071C0 File Offset: 0x000053C0
		[ExcludeFromDocs]
		public void Play()
		{
			ulong num = 0UL;
			this.Play(num);
		}

		// Token: 0x0600025B RID: 603
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void PlayDelayed(float delay);

		// Token: 0x0600025C RID: 604
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void PlayScheduled(double time);

		// Token: 0x0600025D RID: 605
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetScheduledStartTime(double time);

		// Token: 0x0600025E RID: 606
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Stop();

		// Token: 0x0600025F RID: 607 RVA: 0x000071D8 File Offset: 0x000053D8
		public void Pause()
		{
			AudioSource.INTERNAL_CALL_Pause(this);
		}

		// Token: 0x06000260 RID: 608
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Pause(AudioSource self);

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000261 RID: 609
		public extern bool isPlaying
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000262 RID: 610
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void PlayOneShot(AudioClip clip, [DefaultValue("1.0F")] float volumeScale);

		// Token: 0x06000263 RID: 611 RVA: 0x000071E0 File Offset: 0x000053E0
		[ExcludeFromDocs]
		public void PlayOneShot(AudioClip clip)
		{
			float num = 1f;
			this.PlayOneShot(clip, num);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000264 RID: 612
		// (set) Token: 0x06000265 RID: 613
		public extern bool loop
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000266 RID: 614
		// (set) Token: 0x06000267 RID: 615
		public extern bool ignoreListenerVolume
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004D RID: 77
		// (set) Token: 0x06000268 RID: 616
		public extern bool playOnAwake
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000269 RID: 617
		// (set) Token: 0x0600026A RID: 618
		public extern bool ignoreListenerPause
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600026B RID: 619
		// (set) Token: 0x0600026C RID: 620
		public extern AudioVelocityUpdateMode velocityUpdateMode
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600026D RID: 621
		// (set) Token: 0x0600026E RID: 622
		public extern float panLevel
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600026F RID: 623
		// (set) Token: 0x06000270 RID: 624
		public extern bool bypassEffects
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000271 RID: 625
		// (set) Token: 0x06000272 RID: 626
		public extern float dopplerLevel
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000273 RID: 627
		// (set) Token: 0x06000274 RID: 628
		public extern float spread
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000275 RID: 629
		// (set) Token: 0x06000276 RID: 630
		public extern int priority
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000277 RID: 631
		// (set) Token: 0x06000278 RID: 632
		public extern float minDistance
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000279 RID: 633
		// (set) Token: 0x0600027A RID: 634
		public extern float maxDistance
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600027B RID: 635
		// (set) Token: 0x0600027C RID: 636
		public extern float pan
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600027D RID: 637
		// (set) Token: 0x0600027E RID: 638
		public extern AudioRolloffMode rolloffMode
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
