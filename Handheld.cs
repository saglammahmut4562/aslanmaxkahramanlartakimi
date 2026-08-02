using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200007A RID: 122
	public sealed class Handheld
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		public static bool PlayFullScreenMovie(string path, [DefaultValue("Color.black")] Color bgColor, [DefaultValue("FullScreenMovieControlMode.Full")] FullScreenMovieControlMode controlMode, [DefaultValue("FullScreenMovieScalingMode.AspectFit")] FullScreenMovieScalingMode scalingMode)
		{
			return Handheld.INTERNAL_CALL_PlayFullScreenMovie(path, ref bgColor, controlMode, scalingMode);
		}

		// Token: 0x06000604 RID: 1540
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_PlayFullScreenMovie(string path, ref Color bgColor, FullScreenMovieControlMode controlMode, FullScreenMovieScalingMode scalingMode);
	}
}
