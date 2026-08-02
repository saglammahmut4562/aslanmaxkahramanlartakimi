using System;
using System.IO;

namespace System.Net
{
	// Token: 0x020000A8 RID: 168
	internal class WebConnectionData
	{
		// Token: 0x040002EA RID: 746
		public HttpWebRequest request;

		// Token: 0x040002EB RID: 747
		public int StatusCode;

		// Token: 0x040002EC RID: 748
		public string StatusDescription;

		// Token: 0x040002ED RID: 749
		public WebHeaderCollection Headers;

		// Token: 0x040002EE RID: 750
		public Version Version;

		// Token: 0x040002EF RID: 751
		public Stream stream;

		// Token: 0x040002F0 RID: 752
		public string Challenge;
	}
}
