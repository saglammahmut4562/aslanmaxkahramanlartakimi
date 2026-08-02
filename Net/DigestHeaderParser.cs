using System;

namespace System.Net
{
	// Token: 0x02000074 RID: 116
	internal class DigestHeaderParser
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00009E80 File Offset: 0x00008080
		public DigestHeaderParser(string header)
		{
			this.header = header.Trim();
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00009EE0 File Offset: 0x000080E0
		public string Realm
		{
			get
			{
				return this.values[0];
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00009EEC File Offset: 0x000080EC
		public string Opaque
		{
			get
			{
				return this.values[1];
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00009EF8 File Offset: 0x000080F8
		public string Nonce
		{
			get
			{
				return this.values[2];
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00009F04 File Offset: 0x00008104
		public string Algorithm
		{
			get
			{
				return this.values[3];
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00009F10 File Offset: 0x00008110
		public string QOP
		{
			get
			{
				return this.values[4];
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009F1C File Offset: 0x0000811C
		public bool Parse()
		{
			if (!this.header.ToLower().StartsWith("digest "))
			{
				return false;
			}
			this.pos = 6;
			this.length = this.header.Length;
			while (this.pos < this.length)
			{
				string text;
				string text2;
				if (!this.GetKeywordAndValue(out text, out text2))
				{
					return false;
				}
				this.SkipWhitespace();
				if (this.pos < this.length && this.header[this.pos] == ',')
				{
					this.pos++;
				}
				int num = Array.IndexOf<string>(DigestHeaderParser.keywords, text);
				if (num != -1)
				{
					if (this.values[num] != null)
					{
						return false;
					}
					this.values[num] = text2;
				}
			}
			return this.Realm != null && this.Nonce != null;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A00C File Offset: 0x0000820C
		private void SkipWhitespace()
		{
			char c = ' ';
			while (this.pos < this.length && (c == ' ' || c == '\t' || c == '\r' || c == '\n'))
			{
				c = this.header[this.pos++];
			}
			this.pos--;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A080 File Offset: 0x00008280
		private string GetKey()
		{
			this.SkipWhitespace();
			int num = this.pos;
			while (this.pos < this.length && this.header[this.pos] != '=')
			{
				this.pos++;
			}
			return this.header.Substring(num, this.pos - num).Trim().ToLower();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A0F8 File Offset: 0x000082F8
		private bool GetKeywordAndValue(out string key, out string value)
		{
			key = null;
			value = null;
			key = this.GetKey();
			if (this.pos >= this.length)
			{
				return false;
			}
			this.SkipWhitespace();
			if (this.pos + 1 >= this.length || this.header[this.pos++] != '=')
			{
				return false;
			}
			this.SkipWhitespace();
			if (this.pos + 1 >= this.length)
			{
				return false;
			}
			bool flag = false;
			if (this.header[this.pos] == '"')
			{
				this.pos++;
				flag = true;
			}
			int num = this.pos;
			if (flag)
			{
				this.pos = this.header.IndexOf('"', this.pos);
				if (this.pos == -1)
				{
					return false;
				}
			}
			else
			{
				do
				{
					char c = this.header[this.pos];
					if (c == ',' || c == ' ' || c == '\t' || c == '\r' || c == '\n')
					{
						break;
					}
				}
				while (++this.pos < this.length);
				if (this.pos >= this.length && num == this.pos)
				{
					return false;
				}
			}
			value = this.header.Substring(num, this.pos - num);
			this.pos += 2;
			return true;
		}

		// Token: 0x040000E7 RID: 231
		private string header;

		// Token: 0x040000E8 RID: 232
		private int length;

		// Token: 0x040000E9 RID: 233
		private int pos;

		// Token: 0x040000EA RID: 234
		private static string[] keywords = new string[] { "realm", "opaque", "nonce", "algorithm", "qop" };

		// Token: 0x040000EB RID: 235
		private string[] values = new string[DigestHeaderParser.keywords.Length];
	}
}
