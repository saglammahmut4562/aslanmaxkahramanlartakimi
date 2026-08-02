using System;

namespace UnityEngine
{
	// Token: 0x02000107 RID: 263
	public enum TextureFormat
	{
		// Token: 0x04000458 RID: 1112
		Alpha8 = 1,
		// Token: 0x04000459 RID: 1113
		ARGB4444,
		// Token: 0x0400045A RID: 1114
		RGB24,
		// Token: 0x0400045B RID: 1115
		RGBA32,
		// Token: 0x0400045C RID: 1116
		ARGB32,
		// Token: 0x0400045D RID: 1117
		RGB565 = 7,
		// Token: 0x0400045E RID: 1118
		DXT1 = 10,
		// Token: 0x0400045F RID: 1119
		DXT5 = 12,
		// Token: 0x04000460 RID: 1120
		RGBA4444,
		// Token: 0x04000461 RID: 1121
		BGRA32,
		// Token: 0x04000462 RID: 1122
		PVRTC_RGB2 = 30,
		// Token: 0x04000463 RID: 1123
		PVRTC_RGBA2,
		// Token: 0x04000464 RID: 1124
		PVRTC_RGB4,
		// Token: 0x04000465 RID: 1125
		PVRTC_RGBA4,
		// Token: 0x04000466 RID: 1126
		ETC_RGB4,
		// Token: 0x04000467 RID: 1127
		ATC_RGB4,
		// Token: 0x04000468 RID: 1128
		ATC_RGBA8,
		// Token: 0x04000469 RID: 1129
		ATF_RGB_DXT1 = 38,
		// Token: 0x0400046A RID: 1130
		ATF_RGBA_JPG,
		// Token: 0x0400046B RID: 1131
		ATF_RGB_JPG,
		// Token: 0x0400046C RID: 1132
		EAC_R,
		// Token: 0x0400046D RID: 1133
		EAC_R_SIGNED,
		// Token: 0x0400046E RID: 1134
		EAC_RG,
		// Token: 0x0400046F RID: 1135
		EAC_RG_SIGNED,
		// Token: 0x04000470 RID: 1136
		ETC2_RGB,
		// Token: 0x04000471 RID: 1137
		ETC2_RGBA1,
		// Token: 0x04000472 RID: 1138
		ETC2_RGBA8,
		// Token: 0x04000473 RID: 1139
		ASTC_RGB_4x4,
		// Token: 0x04000474 RID: 1140
		ASTC_RGB_5x5,
		// Token: 0x04000475 RID: 1141
		ASTC_RGB_6x6,
		// Token: 0x04000476 RID: 1142
		ASTC_RGB_8x8,
		// Token: 0x04000477 RID: 1143
		ASTC_RGB_10x10,
		// Token: 0x04000478 RID: 1144
		ASTC_RGB_12x12,
		// Token: 0x04000479 RID: 1145
		ASTC_RGBA_4x4,
		// Token: 0x0400047A RID: 1146
		ASTC_RGBA_5x5,
		// Token: 0x0400047B RID: 1147
		ASTC_RGBA_6x6,
		// Token: 0x0400047C RID: 1148
		ASTC_RGBA_8x8,
		// Token: 0x0400047D RID: 1149
		ASTC_RGBA_10x10,
		// Token: 0x0400047E RID: 1150
		ASTC_RGBA_12x12,
		// Token: 0x0400047F RID: 1151
		[Obsolete("Use PVRTC_RGB2")]
		PVRTC_2BPP_RGB = 30,
		// Token: 0x04000480 RID: 1152
		[Obsolete("Use PVRTC_RGBA2")]
		PVRTC_2BPP_RGBA,
		// Token: 0x04000481 RID: 1153
		[Obsolete("Use PVRTC_RGB4")]
		PVRTC_4BPP_RGB,
		// Token: 0x04000482 RID: 1154
		[Obsolete("Use PVRTC_RGBA4")]
		PVRTC_4BPP_RGBA
	}
}
