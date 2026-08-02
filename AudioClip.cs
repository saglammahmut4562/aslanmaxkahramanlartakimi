using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	public sealed class AudioClip : Object
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000235 RID: 565 RVA: 0x00007034 File Offset: 0x00005234
		// (remove) Token: 0x06000236 RID: 566 RVA: 0x00007050 File Offset: 0x00005250
		private event AudioClip.PCMReaderCallback m_PCMReaderCallback;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000237 RID: 567 RVA: 0x0000706C File Offset: 0x0000526C
		// (remove) Token: 0x06000238 RID: 568 RVA: 0x00007088 File Offset: 0x00005288
		private event AudioClip.PCMSetPositionCallback m_PCMSetPositionCallback;

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000239 RID: 569
		public extern float length
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600023A RID: 570
		public extern int samples
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600023B RID: 571
		public extern int channels
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600023C RID: 572
		public extern int frequency
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600023D RID: 573
		public extern bool isReadyToPlay
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600023E RID: 574
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void GetData(float[] data, int offsetSamples);

		// Token: 0x0600023F RID: 575
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetData(float[] data, int offsetSamples);

		// Token: 0x06000240 RID: 576 RVA: 0x000070A4 File Offset: 0x000052A4
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, _3D, stream, null, null);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000070C4 File Offset: 0x000052C4
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, _3D, stream, pcmreadercallback, null);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000070E4 File Offset: 0x000052E4
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			if (name == null)
			{
				throw new NullReferenceException();
			}
			if (lengthSamples <= 0)
			{
				throw new ArgumentException("Length of created clip must be larger than 0");
			}
			if (channels <= 0)
			{
				throw new ArgumentException("Number of channels in created clip must be greater than 0");
			}
			if (frequency <= 0)
			{
				throw new ArgumentException("Frequency in created clip must be greater than 0");
			}
			AudioClip audioClip = AudioClip.Construct_Internal();
			if (pcmreadercallback != null)
			{
				AudioClip audioClip2 = audioClip;
				audioClip2.m_PCMReaderCallback = (AudioClip.PCMReaderCallback)Delegate.Combine(audioClip2.m_PCMReaderCallback, pcmreadercallback);
			}
			if (pcmsetpositioncallback != null)
			{
				AudioClip audioClip3 = audioClip;
				audioClip3.m_PCMSetPositionCallback = (AudioClip.PCMSetPositionCallback)Delegate.Combine(audioClip3.m_PCMSetPositionCallback, pcmsetpositioncallback);
			}
			audioClip.Init_Internal(name, lengthSamples, channels, frequency, _3D, stream);
			return audioClip;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007188 File Offset: 0x00005388
		private void InvokePCMReaderCallback_Internal(float[] data)
		{
			if (this.m_PCMReaderCallback != null)
			{
				this.m_PCMReaderCallback(data);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000071A4 File Offset: 0x000053A4
		private void InvokePCMSetPositionCallback_Internal(int position)
		{
			if (this.m_PCMSetPositionCallback != null)
			{
				this.m_PCMSetPositionCallback(position);
			}
		}

		// Token: 0x06000245 RID: 581
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern AudioClip Construct_Internal();

		// Token: 0x06000246 RID: 582
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init_Internal(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream);

		// Token: 0x02000022 RID: 34
		// (Invoke) Token: 0x06000248 RID: 584
		public delegate void PCMReaderCallback(float[] data);

		// Token: 0x02000023 RID: 35
		// (Invoke) Token: 0x0600024C RID: 588
		public delegate void PCMSetPositionCallback(int position);
	}
}
