using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000B0 RID: 176
	public sealed class PlayerPrefs
	{
		// Token: 0x06000705 RID: 1797
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool TrySetInt(string key, int value);

		// Token: 0x06000706 RID: 1798
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool TrySetSetString(string key, string value);

		// Token: 0x06000707 RID: 1799 RVA: 0x0001157C File Offset: 0x0000F77C
		public static void SetInt(string key, int value)
		{
			if (!PlayerPrefs.TrySetInt(key, value))
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06000708 RID: 1800
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int GetInt(string key, [DefaultValue("0")] int defaultValue);

		// Token: 0x06000709 RID: 1801 RVA: 0x00011598 File Offset: 0x0000F798
		public static void SetString(string key, string value)
		{
			if (!PlayerPrefs.TrySetSetString(key, value))
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x0600070A RID: 1802
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern string GetString(string key, [DefaultValue("\"\"")] string defaultValue);

		// Token: 0x0600070B RID: 1803
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void Save();
	}
}
