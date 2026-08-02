using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace System.Net
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	public sealed class Cookie
	{
		// Token: 0x0600024A RID: 586 RVA: 0x000088EC File Offset: 0x00006AEC
		public Cookie()
		{
			this.expires = DateTime.MinValue;
			this.timestamp = DateTime.Now;
			this.domain = string.Empty;
			this.name = string.Empty;
			this.val = string.Empty;
			this.comment = string.Empty;
			this.port = string.Empty;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000894C File Offset: 0x00006B4C
		public Cookie(string name, string value)
			: this()
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000899C File Offset: 0x00006B9C
		// (set) Token: 0x0600024E RID: 590 RVA: 0x000089A4 File Offset: 0x00006BA4
		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = ((value != null) ? value : string.Empty);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000089C0 File Offset: 0x00006BC0
		// (set) Token: 0x06000250 RID: 592 RVA: 0x000089C8 File Offset: 0x00006BC8
		public global::System.Uri CommentUri
		{
			get
			{
				return this.commentUri;
			}
			set
			{
				this.commentUri = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000089D4 File Offset: 0x00006BD4
		// (set) Token: 0x06000252 RID: 594 RVA: 0x000089DC File Offset: 0x00006BDC
		public bool Discard
		{
			get
			{
				return this.discard;
			}
			set
			{
				this.discard = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000089E8 File Offset: 0x00006BE8
		// (set) Token: 0x06000254 RID: 596 RVA: 0x000089F0 File Offset: 0x00006BF0
		public string Domain
		{
			get
			{
				return this.domain;
			}
			set
			{
				if (Cookie.IsNullOrEmpty(value))
				{
					this.domain = string.Empty;
					this.ExactDomain = true;
				}
				else
				{
					this.domain = value;
					this.ExactDomain = value[0] != '.';
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00008A30 File Offset: 0x00006C30
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00008A38 File Offset: 0x00006C38
		internal bool ExactDomain
		{
			get
			{
				return this.exact_domain;
			}
			set
			{
				this.exact_domain = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00008A44 File Offset: 0x00006C44
		public bool Expired
		{
			get
			{
				return this.expires <= DateTime.Now && this.expires != DateTime.MinValue;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00008A70 File Offset: 0x00006C70
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00008A78 File Offset: 0x00006C78
		public DateTime Expires
		{
			get
			{
				return this.expires;
			}
			set
			{
				this.expires = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00008A84 File Offset: 0x00006C84
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00008A8C File Offset: 0x00006C8C
		public bool HttpOnly
		{
			get
			{
				return this.httpOnly;
			}
			set
			{
				this.httpOnly = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00008A98 File Offset: 0x00006C98
		// (set) Token: 0x0600025D RID: 605 RVA: 0x00008AA0 File Offset: 0x00006CA0
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (Cookie.IsNullOrEmpty(value))
				{
					throw new CookieException("Name cannot be empty");
				}
				if (value[0] == '$' || value.IndexOfAny(Cookie.reservedCharsName) != -1)
				{
					this.name = string.Empty;
					throw new CookieException("Name contains invalid characters");
				}
				this.name = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00008B00 File Offset: 0x00006D00
		// (set) Token: 0x0600025F RID: 607 RVA: 0x00008B20 File Offset: 0x00006D20
		public string Path
		{
			get
			{
				return (this.path != null) ? this.path : string.Empty;
			}
			set
			{
				this.path = ((value != null) ? value : string.Empty);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00008B3C File Offset: 0x00006D3C
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00008B44 File Offset: 0x00006D44
		public string Port
		{
			get
			{
				return this.port;
			}
			set
			{
				if (Cookie.IsNullOrEmpty(value))
				{
					this.port = string.Empty;
					return;
				}
				if (value[0] != '"' || value[value.Length - 1] != '"')
				{
					throw new CookieException("The 'Port'='" + value + "' part of the cookie is invalid. Port must be enclosed by double quotes.");
				}
				this.port = value;
				string[] array = this.port.Split(Cookie.portSeparators);
				this.ports = new int[array.Length];
				for (int i = 0; i < this.ports.Length; i++)
				{
					this.ports[i] = int.MinValue;
					if (array[i].Length != 0)
					{
						try
						{
							this.ports[i] = int.Parse(array[i]);
						}
						catch (Exception ex)
						{
							throw new CookieException("The 'Port'='" + value + "' part of the cookie is invalid. Invalid value: " + array[i], ex);
						}
					}
				}
				this.Version = 1;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00008C4C File Offset: 0x00006E4C
		internal int[] Ports
		{
			get
			{
				return this.ports;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00008C54 File Offset: 0x00006E54
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00008C5C File Offset: 0x00006E5C
		public bool Secure
		{
			get
			{
				return this.secure;
			}
			set
			{
				this.secure = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00008C68 File Offset: 0x00006E68
		public DateTime TimeStamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00008C70 File Offset: 0x00006E70
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00008C78 File Offset: 0x00006E78
		public string Value
		{
			get
			{
				return this.val;
			}
			set
			{
				if (value == null)
				{
					this.val = string.Empty;
					return;
				}
				this.val = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00008C94 File Offset: 0x00006E94
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00008C9C File Offset: 0x00006E9C
		public int Version
		{
			get
			{
				return this.version;
			}
			set
			{
				if (value < 0 || value > 10)
				{
					this.version = 0;
				}
				else
				{
					this.version = value;
				}
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008CC0 File Offset: 0x00006EC0
		public override bool Equals(object obj)
		{
			Cookie cookie = obj as Cookie;
			return cookie != null && string.Compare(this.name, cookie.name, true, CultureInfo.InvariantCulture) == 0 && string.Compare(this.val, cookie.val, false, CultureInfo.InvariantCulture) == 0 && string.Compare(this.Path, cookie.Path, false, CultureInfo.InvariantCulture) == 0 && string.Compare(this.domain, cookie.domain, true, CultureInfo.InvariantCulture) == 0 && this.version == cookie.version;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008D5C File Offset: 0x00006F5C
		public override int GetHashCode()
		{
			return Cookie.hash(CaseInsensitiveHashCodeProvider.DefaultInvariant.GetHashCode(this.name), this.val.GetHashCode(), this.Path.GetHashCode(), CaseInsensitiveHashCodeProvider.DefaultInvariant.GetHashCode(this.domain), this.version);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008DAC File Offset: 0x00006FAC
		private static int hash(int i, int j, int k, int l, int m)
		{
			return i ^ ((j << 13) | (j >> 19)) ^ ((k << 26) | (k >> 6)) ^ ((l << 7) | (l >> 25)) ^ ((m << 20) | (m >> 12));
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008DE4 File Offset: 0x00006FE4
		internal string ToString(global::System.Uri uri)
		{
			if (this.name.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			if (this.version > 0)
			{
				stringBuilder.Append("$Version=").Append(this.version).Append("; ");
			}
			stringBuilder.Append(this.name).Append("=").Append(this.val);
			if (this.version == 0)
			{
				return stringBuilder.ToString();
			}
			if (!Cookie.IsNullOrEmpty(this.path))
			{
				stringBuilder.Append("; $Path=").Append(this.path);
			}
			else if (uri != null)
			{
				stringBuilder.Append("; $Path=/").Append(this.path);
			}
			bool flag = uri == null || uri.Host != this.domain;
			if (flag && !Cookie.IsNullOrEmpty(this.domain))
			{
				stringBuilder.Append("; $Domain=").Append(this.domain);
			}
			if (this.port != null && this.port.Length != 0)
			{
				stringBuilder.Append("; $Port=").Append(this.port);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008F48 File Offset: 0x00007148
		private static bool IsNullOrEmpty(string s)
		{
			return s == null || s.Length == 0;
		}

		// Token: 0x040000C3 RID: 195
		private string comment;

		// Token: 0x040000C4 RID: 196
		private global::System.Uri commentUri;

		// Token: 0x040000C5 RID: 197
		private bool discard;

		// Token: 0x040000C6 RID: 198
		private string domain;

		// Token: 0x040000C7 RID: 199
		private DateTime expires;

		// Token: 0x040000C8 RID: 200
		private bool httpOnly;

		// Token: 0x040000C9 RID: 201
		private string name;

		// Token: 0x040000CA RID: 202
		private string path;

		// Token: 0x040000CB RID: 203
		private string port;

		// Token: 0x040000CC RID: 204
		private int[] ports;

		// Token: 0x040000CD RID: 205
		private bool secure;

		// Token: 0x040000CE RID: 206
		private DateTime timestamp;

		// Token: 0x040000CF RID: 207
		private string val;

		// Token: 0x040000D0 RID: 208
		private int version;

		// Token: 0x040000D1 RID: 209
		private static char[] reservedCharsName = new char[] { ' ', '=', ';', ',', '\n', '\r', '\t' };

		// Token: 0x040000D2 RID: 210
		private static char[] portSeparators = new char[] { '"', ',' };

		// Token: 0x040000D3 RID: 211
		private static string tspecials = "()<>@,;:\\\"/[]?={} \t";

		// Token: 0x040000D4 RID: 212
		private bool exact_domain;
	}
}
