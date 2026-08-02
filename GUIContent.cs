using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	[StructLayout(0)]
	public sealed class GUIContent
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		public GUIContent()
		{
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		public GUIContent(string text)
		{
			this.m_Text = text;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
		public GUIContent(Texture image)
		{
			this.m_Image = image;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000BF10 File Offset: 0x0000A110
		public GUIContent(GUIContent src)
		{
			this.m_Text = src.m_Text;
			this.m_Image = src.m_Image;
			this.m_Tooltip = src.m_Tooltip;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000BF90 File Offset: 0x0000A190
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0000BF98 File Offset: 0x0000A198
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.m_Text = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		public string tooltip
		{
			get
			{
				return this.m_Tooltip;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		internal int hash
		{
			get
			{
				int num = 0;
				if (this.m_Text != null && this.m_Text != string.Empty)
				{
					num = this.m_Text.GetHashCode() * 37;
				}
				return num;
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000BFF4 File Offset: 0x0000A1F4
		internal static GUIContent Temp(string t)
		{
			GUIContent.s_Text.m_Text = t;
			return GUIContent.s_Text;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000C008 File Offset: 0x0000A208
		internal static GUIContent Temp(Texture i)
		{
			GUIContent.s_Image.m_Image = i;
			return GUIContent.s_Image;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000C01C File Offset: 0x0000A21C
		internal static GUIContent Temp(string t, Texture i)
		{
			GUIContent.s_TextImage.m_Text = t;
			GUIContent.s_TextImage.m_Image = i;
			return GUIContent.s_TextImage;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000C03C File Offset: 0x0000A23C
		internal static void ClearStaticCache()
		{
			GUIContent.s_Text.m_Text = null;
			GUIContent.s_Image.m_Image = null;
			GUIContent.s_TextImage.m_Text = null;
			GUIContent.s_TextImage.m_Image = null;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000C06C File Offset: 0x0000A26C
		internal static GUIContent[] Temp(string[] texts)
		{
			GUIContent[] array = new GUIContent[texts.Length];
			for (int i = 0; i < texts.Length; i++)
			{
				array[i] = new GUIContent(texts[i]);
			}
			return array;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		internal static GUIContent[] Temp(Texture[] images)
		{
			GUIContent[] array = new GUIContent[images.Length];
			for (int i = 0; i < images.Length; i++)
			{
				array[i] = new GUIContent(images[i]);
			}
			return array;
		}

		// Token: 0x040000E9 RID: 233
		[SerializeField]
		private string m_Text = string.Empty;

		// Token: 0x040000EA RID: 234
		[SerializeField]
		private Texture m_Image;

		// Token: 0x040000EB RID: 235
		[SerializeField]
		private string m_Tooltip = string.Empty;

		// Token: 0x040000EC RID: 236
		public static GUIContent none = new GUIContent(string.Empty);

		// Token: 0x040000ED RID: 237
		private static GUIContent s_Text = new GUIContent();

		// Token: 0x040000EE RID: 238
		private static GUIContent s_Image = new GUIContent();

		// Token: 0x040000EF RID: 239
		private static GUIContent s_TextImage = new GUIContent();
	}
}
