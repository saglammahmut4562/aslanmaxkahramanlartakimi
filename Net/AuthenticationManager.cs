using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000064 RID: 100
	public class AuthenticationManager
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00007D98 File Offset: 0x00005F98
		private static void EnsureModules()
		{
			object obj = AuthenticationManager.locker;
			lock (obj)
			{
				if (AuthenticationManager.modules == null)
				{
					AuthenticationManager.modules = new ArrayList();
					AuthenticationManager.modules.Add(new BasicClient());
					AuthenticationManager.modules.Add(new DigestClient());
				}
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007E08 File Offset: 0x00006008
		public static Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (credentials == null)
			{
				throw new ArgumentNullException("credentials");
			}
			if (challenge == null)
			{
				throw new ArgumentNullException("challenge");
			}
			return AuthenticationManager.DoAuthenticate(challenge, request, credentials);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007E48 File Offset: 0x00006048
		private static Authorization DoAuthenticate(string challenge, WebRequest request, ICredentials credentials)
		{
			AuthenticationManager.EnsureModules();
			ArrayList arrayList = AuthenticationManager.modules;
			lock (arrayList)
			{
				foreach (object obj in AuthenticationManager.modules)
				{
					IAuthenticationModule authenticationModule = (IAuthenticationModule)obj;
					Authorization authorization = authenticationModule.Authenticate(challenge, request, credentials);
					if (authorization != null)
					{
						authorization.Module = authenticationModule;
						return authorization;
					}
				}
			}
			return null;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007EF8 File Offset: 0x000060F8
		public static Authorization PreAuthenticate(WebRequest request, ICredentials credentials)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (credentials == null)
			{
				return null;
			}
			AuthenticationManager.EnsureModules();
			ArrayList arrayList = AuthenticationManager.modules;
			lock (arrayList)
			{
				foreach (object obj in AuthenticationManager.modules)
				{
					IAuthenticationModule authenticationModule = (IAuthenticationModule)obj;
					Authorization authorization = authenticationModule.PreAuthenticate(request, credentials);
					if (authorization != null)
					{
						authorization.Module = authenticationModule;
						return authorization;
					}
				}
			}
			return null;
		}

		// Token: 0x040000AB RID: 171
		private static ArrayList modules;

		// Token: 0x040000AC RID: 172
		private static object locker = new object();

		// Token: 0x040000AD RID: 173
		private static ICredentialPolicy credential_policy = null;
	}
}
