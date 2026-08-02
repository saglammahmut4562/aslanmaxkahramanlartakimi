using System;

namespace System.Net
{
	// Token: 0x02000070 RID: 112
	internal class CookieParser
	{
		// Token: 0x0600028F RID: 655 RVA: 0x000098E0 File Offset: 0x00007AE0
		public CookieParser(string header)
			: this(header, 0)
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000098EC File Offset: 0x00007AEC
		public CookieParser(string header, int position)
		{
			this.header = header;
			this.pos = position;
			this.length = header.Length;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009910 File Offset: 0x00007B10
		public bool GetNextNameValue(out string name, out string val)
		{
			name = null;
			val = null;
			if (this.pos >= this.length)
			{
				return false;
			}
			name = this.GetCookieName();
			if (this.pos < this.header.Length && this.header[this.pos] == '=')
			{
				this.pos++;
				val = this.GetCookieValue();
			}
			if (this.pos < this.length && this.header[this.pos] == ';')
			{
				this.pos++;
			}
			return true;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000099BC File Offset: 0x00007BBC
		private string GetCookieName()
		{
			int num = this.pos;
			while (num < this.length && char.IsWhiteSpace(this.header[num]))
			{
				num++;
			}
			int num2 = num;
			while (num < this.length && this.header[num] != ';' && this.header[num] != '=')
			{
				num++;
			}
			this.pos = num;
			return this.header.Substring(num2, num - num2).Trim();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009A54 File Offset: 0x00007C54
		private string GetCookieValue()
		{
			if (this.pos >= this.length)
			{
				return null;
			}
			int num = this.pos;
			while (num < this.length && char.IsWhiteSpace(this.header[num]))
			{
				num++;
			}
			int num2;
			if (this.header[num] == '"')
			{
				num = (num2 = num + 1);
				while (num < this.length && this.header[num] != '"')
				{
					num++;
				}
				int num3 = num;
				while (num3 < this.length && this.header[num3] != ';')
				{
					num3++;
				}
				this.pos = num3;
			}
			else
			{
				num2 = num;
				while (num < this.length && this.header[num] != ';')
				{
					num++;
				}
				this.pos = num;
			}
			return this.header.Substring(num2, num - num2).Trim();
		}

		// Token: 0x040000DF RID: 223
		private string header;

		// Token: 0x040000E0 RID: 224
		private int pos;

		// Token: 0x040000E1 RID: 225
		private int length;
	}
}
