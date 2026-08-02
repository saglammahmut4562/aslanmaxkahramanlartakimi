using System;
using System.Runtime.InteropServices;

namespace Mono.Security.X509
{
	// Token: 0x02000008 RID: 8
	internal class OSX509Certificates
	{
		// Token: 0x06000004 RID: 4
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecCertificateCreateWithData(IntPtr allocator, IntPtr nsdataRef);

		// Token: 0x06000005 RID: 5
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern int SecTrustCreateWithCertificates(IntPtr certOrCertArray, IntPtr policies, out IntPtr sectrustref);

		// Token: 0x06000006 RID: 6
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecPolicyCreateSSL(int server, IntPtr cfStringHostname);

		// Token: 0x06000007 RID: 7
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern int SecTrustEvaluate(IntPtr secTrustRef, out OSX509Certificates.SecTrustResult secTrustResultTime);

		// Token: 0x06000008 RID: 8
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private unsafe static extern IntPtr CFDataCreate(IntPtr allocator, byte* bytes, IntPtr length);

		// Token: 0x06000009 RID: 9
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRelease(IntPtr handle);

		// Token: 0x0600000A RID: 10
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFArrayCreate(IntPtr allocator, IntPtr values, IntPtr numValues, IntPtr callbacks);

		// Token: 0x0600000B RID: 11 RVA: 0x00002074 File Offset: 0x00000274
		private unsafe static IntPtr MakeCFData(byte[] data)
		{
			int num = 0;
			return OSX509Certificates.CFDataCreate(IntPtr.Zero, &data[num], (IntPtr)data.Length);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000209C File Offset: 0x0000029C
		private unsafe static IntPtr FromIntPtrs(IntPtr[] values)
		{
			fixed (IntPtr* ptr = (ref values != null && values.Length != 0 ? ref values[0] : ref *null))
			{
				return OSX509Certificates.CFArrayCreate(IntPtr.Zero, (IntPtr)((void*)ptr), (IntPtr)values.Length, IntPtr.Zero);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020E4 File Offset: 0x000002E4
		public static OSX509Certificates.SecTrustResult TrustEvaluateSsl(X509CertificateCollection certificates)
		{
			OSX509Certificates.SecTrustResult secTrustResult;
			try
			{
				secTrustResult = OSX509Certificates._TrustEvaluateSsl(certificates);
			}
			catch
			{
				secTrustResult = OSX509Certificates.SecTrustResult.Deny;
			}
			return secTrustResult;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002120 File Offset: 0x00000320
		private static OSX509Certificates.SecTrustResult _TrustEvaluateSsl(X509CertificateCollection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			int count = certificates.Count;
			IntPtr[] array = new IntPtr[count];
			IntPtr[] array2 = new IntPtr[count];
			IntPtr intPtr = IntPtr.Zero;
			OSX509Certificates.SecTrustResult secTrustResult2;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array[i] = OSX509Certificates.MakeCFData(certificates[i].RawData);
				}
				for (int j = 0; j < count; j++)
				{
					array2[j] = OSX509Certificates.SecCertificateCreateWithData(IntPtr.Zero, array[j]);
					if (array2[j] == IntPtr.Zero)
					{
						return OSX509Certificates.SecTrustResult.Deny;
					}
				}
				intPtr = OSX509Certificates.FromIntPtrs(array2);
				IntPtr intPtr2;
				if (OSX509Certificates.SecTrustCreateWithCertificates(intPtr, OSX509Certificates.sslsecpolicy, out intPtr2) == 0)
				{
					OSX509Certificates.SecTrustResult secTrustResult;
					int num = OSX509Certificates.SecTrustEvaluate(intPtr2, out secTrustResult);
					if (num != 0)
					{
						secTrustResult2 = OSX509Certificates.SecTrustResult.Deny;
					}
					else
					{
						OSX509Certificates.CFRelease(intPtr2);
						secTrustResult2 = secTrustResult;
					}
				}
				else
				{
					secTrustResult2 = OSX509Certificates.SecTrustResult.Deny;
				}
			}
			finally
			{
				for (int k = 0; k < count; k++)
				{
					if (array[k] != IntPtr.Zero)
					{
						OSX509Certificates.CFRelease(array[k]);
					}
				}
				if (intPtr != IntPtr.Zero)
				{
					OSX509Certificates.CFRelease(intPtr);
				}
				else
				{
					for (int l = 0; l < count; l++)
					{
						if (array2[l] != IntPtr.Zero)
						{
							OSX509Certificates.CFRelease(array2[l]);
						}
					}
				}
			}
			return secTrustResult2;
		}

		// Token: 0x04000006 RID: 6
		public const string SecurityLibrary = "/System/Library/Frameworks/Security.framework/Security";

		// Token: 0x04000007 RID: 7
		public const string CoreFoundationLibrary = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		// Token: 0x04000008 RID: 8
		private static IntPtr sslsecpolicy = OSX509Certificates.SecPolicyCreateSSL(0, IntPtr.Zero);

		// Token: 0x02000009 RID: 9
		public enum SecTrustResult
		{
			// Token: 0x0400000A RID: 10
			Invalid,
			// Token: 0x0400000B RID: 11
			Proceed,
			// Token: 0x0400000C RID: 12
			Confirm,
			// Token: 0x0400000D RID: 13
			Deny,
			// Token: 0x0400000E RID: 14
			Unspecified,
			// Token: 0x0400000F RID: 15
			RecoverableTrustFailure,
			// Token: 0x04000010 RID: 16
			FatalTrustFailure,
			// Token: 0x04000011 RID: 17
			ResultOtherError
		}
	}
}
