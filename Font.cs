using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000052 RID: 82
	public sealed class Font : Object
	{
		// Token: 0x06000383 RID: 899 RVA: 0x00008E8C File Offset: 0x0000708C
		public Font()
		{
			Font.Internal_CreateFont(this, null);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00008E9C File Offset: 0x0000709C
		public Font(string name)
		{
			Font.Internal_CreateFont(this, name);
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000385 RID: 901 RVA: 0x00008EAC File Offset: 0x000070AC
		// (remove) Token: 0x06000386 RID: 902 RVA: 0x00008EC8 File Offset: 0x000070C8
		private event Font.FontTextureRebuildCallback m_FontTextureRebuildCallback;

		// Token: 0x06000387 RID: 903
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateFont([Writable] Font _font, string name);

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000388 RID: 904
		// (set) Token: 0x06000389 RID: 905
		public extern Material material
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600038A RID: 906
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool HasCharacter(char c);

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600038B RID: 907
		// (set) Token: 0x0600038C RID: 908
		public extern string[] fontNames
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600038D RID: 909
		// (set) Token: 0x0600038E RID: 910
		public extern CharacterInfo[] characterInfo
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600038F RID: 911
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void RequestCharactersInTexture(string characters, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x06000390 RID: 912 RVA: 0x00008EE4 File Offset: 0x000070E4
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters, int size)
		{
			FontStyle fontStyle = FontStyle.Normal;
			this.RequestCharactersInTexture(characters, size, fontStyle);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00008EFC File Offset: 0x000070FC
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters)
		{
			FontStyle fontStyle = FontStyle.Normal;
			int num = 0;
			this.RequestCharactersInTexture(characters, num, fontStyle);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00008F18 File Offset: 0x00007118
		private void InvokeFontTextureRebuildCallback_Internal()
		{
			if (this.m_FontTextureRebuildCallback != null)
			{
				this.m_FontTextureRebuildCallback();
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00008F30 File Offset: 0x00007130
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00008F38 File Offset: 0x00007138
		public Font.FontTextureRebuildCallback textureRebuildCallback
		{
			get
			{
				return this.m_FontTextureRebuildCallback;
			}
			set
			{
				this.m_FontTextureRebuildCallback = value;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00008F44 File Offset: 0x00007144
		public static int GetMaxVertsForString(string str)
		{
			return str.Length * 4 + 4;
		}

		// Token: 0x06000396 RID: 918
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool GetCharacterInfo(char ch, out CharacterInfo info, [DefaultValue("0")] int size, [DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x06000397 RID: 919 RVA: 0x00008F50 File Offset: 0x00007150
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info, int size)
		{
			FontStyle fontStyle = FontStyle.Normal;
			return this.GetCharacterInfo(ch, out info, size, fontStyle);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00008F6C File Offset: 0x0000716C
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info)
		{
			FontStyle fontStyle = FontStyle.Normal;
			int num = 0;
			return this.GetCharacterInfo(ch, out info, num, fontStyle);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000399 RID: 921
		public extern bool dynamic
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x0600039B RID: 923
		public delegate void FontTextureRebuildCallback();
	}
}
