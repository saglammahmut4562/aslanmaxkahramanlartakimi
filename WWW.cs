using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000125 RID: 293
	public sealed class WWW : IDisposable
	{
		// Token: 0x060009BA RID: 2490
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern WWW(string url, int version, uint crc);

		// Token: 0x060009BB RID: 2491 RVA: 0x0001851C File Offset: 0x0001671C
		internal static Dictionary<string, string> ParseHTTPHeaderString(string input)
		{
			if (input == null)
			{
				throw new ArgumentException("input was null to ParseHTTPHeaderString");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			StringReader stringReader = new StringReader(input);
			int num = 0;
			for (;;)
			{
				string text = stringReader.ReadLine();
				if (text == null)
				{
					break;
				}
				if (num++ == 0 && text.StartsWith("HTTP"))
				{
					dictionary["STATUS"] = text;
				}
				else
				{
					int num2 = text.IndexOf(": ");
					if (num2 != -1)
					{
						string text2 = text.Substring(0, num2).ToUpper();
						string text3 = text.Substring(num2 + 2);
						dictionary[text2] = text3;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000185CC File Offset: 0x000167CC
		public void Dispose()
		{
			this.DestroyWWW(true);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000185D8 File Offset: 0x000167D8
		~WWW()
		{
			this.DestroyWWW(false);
		}

		// Token: 0x060009BE RID: 2494
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void DestroyWWW(bool cancel);

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00018608 File Offset: 0x00016808
		public Dictionary<string, string> responseHeaders
		{
			get
			{
				if (!this.isDone)
				{
					throw new UnityException("WWW is not finished downloading yet");
				}
				return WWW.ParseHTTPHeaderString(this.responseHeadersString);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009C0 RID: 2496
		private extern string responseHeadersString
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0001862C File Offset: 0x0001682C
		public string text
		{
			get
			{
				if (!this.isDone)
				{
					throw new UnityException("WWW is not ready downloading yet");
				}
				return this.GetTextEncoder().GetString(this.bytes, 0, this.bytes.Length);
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00018660 File Offset: 0x00016860
		private Encoding GetTextEncoder()
		{
			string text = null;
			if (this.responseHeaders.TryGetValue("CONTENT-TYPE", out text))
			{
				int num = text.IndexOf("charset", StringComparison.OrdinalIgnoreCase);
				if (num > -1)
				{
					int num2 = text.IndexOf('=', num);
					if (num2 > -1)
					{
						string text2 = text.Substring(num2 + 1).Trim().Trim(new char[] { '\'', '"' })
							.Trim();
						int num3 = text2.IndexOf(';');
						if (num3 > -1)
						{
							text2 = text2.Substring(0, num3);
						}
						try
						{
							return Encoding.GetEncoding(text2);
						}
						catch (Exception)
						{
							Debug.Log("Unsupported encoding: '" + text2 + "'");
						}
					}
				}
			}
			return Encoding.UTF8;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060009C3 RID: 2499
		public extern byte[] bytes
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060009C4 RID: 2500
		public extern string error
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060009C5 RID: 2501
		public extern bool isDone
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060009C6 RID: 2502
		public extern float progress
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00018738 File Offset: 0x00016938
		[ExcludeFromDocs]
		public static WWW LoadFromCacheOrDownload(string url, int version)
		{
			uint num = 0U;
			return WWW.LoadFromCacheOrDownload(url, version, num);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00018750 File Offset: 0x00016950
		public static WWW LoadFromCacheOrDownload(string url, int version, [DefaultValue("0")] uint crc)
		{
			return new WWW(url, version, crc);
		}

		// Token: 0x040004D4 RID: 1236
		internal IntPtr m_Ptr;
	}
}
