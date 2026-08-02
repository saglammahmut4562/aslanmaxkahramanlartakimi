using System;

namespace System.Net
{
	// Token: 0x02000066 RID: 102
	internal class BasicClient : IAuthenticationModule
	{
		// Token: 0x0600022F RID: 559 RVA: 0x00008024 File Offset: 0x00006224
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			string text = challenge.Trim();
			if (text.ToLower().IndexOf("basic") == -1)
			{
				return null;
			}
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008068 File Offset: 0x00006268
		private static byte[] GetBytes(string str)
		{
			int i = str.Length;
			byte[] array = new byte[i];
			for (i--; i >= 0; i--)
			{
				array[i] = (byte)str[i];
			}
			return array;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000080A4 File Offset: 0x000062A4
		private static Authorization InternalAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null || credentials == null)
			{
				return null;
			}
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.AuthUri, "basic");
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
			string domain = credential.Domain;
			byte[] array;
			if (domain == null || domain == string.Empty || domain.Trim() == string.Empty)
			{
				array = BasicClient.GetBytes(userName + ":" + password);
			}
			else
			{
				array = BasicClient.GetBytes(string.Concat(new string[] { domain, "\\", userName, ":", password }));
			}
			string text = "Basic " + Convert.ToBase64String(array);
			return new Authorization(text);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000081A0 File Offset: 0x000063A0
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000081AC File Offset: 0x000063AC
		public string AuthenticationType
		{
			get
			{
				return "Basic";
			}
		}
	}
}
