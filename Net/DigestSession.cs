using System;
using System.Security.Cryptography;
using System.Text;

namespace System.Net
{
	// Token: 0x02000075 RID: 117
	internal class DigestSession
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x0000A284 File Offset: 0x00008484
		public DigestSession()
		{
			this._nc = 1;
			this.lastUse = DateTime.Now;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000A2AC File Offset: 0x000084AC
		public string Algorithm
		{
			get
			{
				return this.parser.Algorithm;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000A2BC File Offset: 0x000084BC
		public string Realm
		{
			get
			{
				return this.parser.Realm;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000A2CC File Offset: 0x000084CC
		public string Nonce
		{
			get
			{
				return this.parser.Nonce;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000A2DC File Offset: 0x000084DC
		public string Opaque
		{
			get
			{
				return this.parser.Opaque;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000A2EC File Offset: 0x000084EC
		public string QOP
		{
			get
			{
				return this.parser.QOP;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000A2FC File Offset: 0x000084FC
		public string CNonce
		{
			get
			{
				if (this._cnonce == null)
				{
					byte[] array = new byte[15];
					DigestSession.rng.GetBytes(array);
					this._cnonce = Convert.ToBase64String(array);
					Array.Clear(array, 0, array.Length);
				}
				return this._cnonce;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A344 File Offset: 0x00008544
		public bool Parse(string challenge)
		{
			this.parser = new DigestHeaderParser(challenge);
			if (!this.parser.Parse())
			{
				return false;
			}
			if (this.parser.Algorithm == null || this.parser.Algorithm.ToUpper().StartsWith("MD5"))
			{
				this.hash = HashAlgorithm.Create("MD5");
			}
			return true;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A3B0 File Offset: 0x000085B0
		private string HashToHexString(string toBeHashed)
		{
			if (this.hash == null)
			{
				return null;
			}
			this.hash.Initialize();
			byte[] array = this.hash.ComputeHash(Encoding.ASCII.GetBytes(toBeHashed));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A42C File Offset: 0x0000862C
		private string HA1(string username, string password)
		{
			string text = string.Format("{0}:{1}:{2}", username, this.Realm, password);
			if (this.Algorithm != null && this.Algorithm.ToLower() == "md5-sess")
			{
				text = string.Format("{0}:{1}:{2}", this.HashToHexString(text), this.Nonce, this.CNonce);
			}
			return this.HashToHexString(text);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A498 File Offset: 0x00008698
		private string HA2(HttpWebRequest webRequest)
		{
			string text = string.Format("{0}:{1}", webRequest.Method, webRequest.RequestUri.PathAndQuery);
			if (this.QOP == "auth-int")
			{
			}
			return this.HashToHexString(text);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A4E0 File Offset: 0x000086E0
		private string Response(string username, string password, HttpWebRequest webRequest)
		{
			string text = string.Format("{0}:{1}:", this.HA1(username, password), this.Nonce);
			if (this.QOP != null)
			{
				text += string.Format("{0}:{1}:{2}:", this._nc.ToString("X8"), this.CNonce, this.QOP);
			}
			text += this.HA2(webRequest);
			return this.HashToHexString(text);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A554 File Offset: 0x00008754
		public Authorization Authenticate(WebRequest webRequest, ICredentials credentials)
		{
			if (this.parser == null)
			{
				throw new InvalidOperationException();
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			this.lastUse = DateTime.Now;
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.RequestUri, "digest");
			if (credential == null)
			{
				return null;
			}
			string userName = credential.UserName;
			if (userName == null || userName == string.Empty)
			{
				return null;
			}
			string password = credential.Password;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Digest username=\"{0}\", ", userName);
			stringBuilder.AppendFormat("realm=\"{0}\", ", this.Realm);
			stringBuilder.AppendFormat("nonce=\"{0}\", ", this.Nonce);
			stringBuilder.AppendFormat("uri=\"{0}\", ", httpWebRequest.Address.PathAndQuery);
			if (this.Algorithm != null)
			{
				stringBuilder.AppendFormat("algorithm=\"{0}\", ", this.Algorithm);
			}
			stringBuilder.AppendFormat("response=\"{0}\", ", this.Response(userName, password, httpWebRequest));
			if (this.QOP != null)
			{
				stringBuilder.AppendFormat("qop=\"{0}\", ", this.QOP);
			}
			lock (this)
			{
				if (this.QOP != null)
				{
					stringBuilder.AppendFormat("nc={0:X8}, ", this._nc);
					this._nc++;
				}
			}
			if (this.CNonce != null)
			{
				stringBuilder.AppendFormat("cnonce=\"{0}\", ", this.CNonce);
			}
			if (this.Opaque != null)
			{
				stringBuilder.AppendFormat("opaque=\"{0}\", ", this.Opaque);
			}
			stringBuilder.Length -= 2;
			return new Authorization(stringBuilder.ToString());
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000A720 File Offset: 0x00008920
		public DateTime LastUse
		{
			get
			{
				return this.lastUse;
			}
		}

		// Token: 0x040000EC RID: 236
		private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

		// Token: 0x040000ED RID: 237
		private DateTime lastUse;

		// Token: 0x040000EE RID: 238
		private int _nc;

		// Token: 0x040000EF RID: 239
		private HashAlgorithm hash;

		// Token: 0x040000F0 RID: 240
		private DigestHeaderParser parser;

		// Token: 0x040000F1 RID: 241
		private string _cnonce;
	}
}
