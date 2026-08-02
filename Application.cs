using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	public sealed class Application
	{
		// Token: 0x060001DF RID: 479
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void Quit();

		// Token: 0x060001E0 RID: 480
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void CancelQuit();

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001E1 RID: 481
		public static extern int loadedLevel
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001E2 RID: 482
		public static extern string loadedLevelName
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006A44 File Offset: 0x00004C44
		public static void LoadLevel(int index)
		{
			Application.LoadLevelAsync(null, index, false, true);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006A50 File Offset: 0x00004C50
		public static void LoadLevel(string name)
		{
			Application.LoadLevelAsync(name, -1, false, true);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006A5C File Offset: 0x00004C5C
		public static AsyncOperation LoadLevelAsync(int index)
		{
			return Application.LoadLevelAsync(null, index, false, false);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006A68 File Offset: 0x00004C68
		public static AsyncOperation LoadLevelAsync(string levelName)
		{
			return Application.LoadLevelAsync(levelName, -1, false, false);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006A74 File Offset: 0x00004C74
		public static AsyncOperation LoadLevelAdditiveAsync(int index)
		{
			return Application.LoadLevelAsync(null, index, true, false);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006A80 File Offset: 0x00004C80
		public static AsyncOperation LoadLevelAdditiveAsync(string levelName)
		{
			return Application.LoadLevelAsync(levelName, -1, true, false);
		}

		// Token: 0x060001E9 RID: 489
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern AsyncOperation LoadLevelAsync(string monoLevelName, int index, bool additive, bool mustCompleteNextFrame);

		// Token: 0x060001EA RID: 490 RVA: 0x00006A8C File Offset: 0x00004C8C
		public static void LoadLevelAdditive(int index)
		{
			Application.LoadLevelAsync(null, index, true, true);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006A98 File Offset: 0x00004C98
		public static void LoadLevelAdditive(string name)
		{
			Application.LoadLevelAsync(name, -1, true, true);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001EC RID: 492
		public static extern bool isLoadingLevel
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001ED RID: 493
		public static extern int levelCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060001EE RID: 494
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern float GetStreamProgressForLevelByName(string levelName);

		// Token: 0x060001EF RID: 495
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float GetStreamProgressForLevel(int levelIndex);

		// Token: 0x060001F0 RID: 496 RVA: 0x00006AA4 File Offset: 0x00004CA4
		public static float GetStreamProgressForLevel(string levelName)
		{
			return Application.GetStreamProgressForLevelByName(levelName);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001F1 RID: 497
		public static extern int streamedBytes
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060001F2 RID: 498
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool CanStreamedLevelBeLoadedByName(string levelName);

		// Token: 0x060001F3 RID: 499
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool CanStreamedLevelBeLoaded(int levelIndex);

		// Token: 0x060001F4 RID: 500 RVA: 0x00006AAC File Offset: 0x00004CAC
		public static bool CanStreamedLevelBeLoaded(string levelName)
		{
			return Application.CanStreamedLevelBeLoadedByName(levelName);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001F5 RID: 501
		public static extern bool isPlaying
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001F6 RID: 502
		public static extern bool isEditor
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001F7 RID: 503
		public static extern bool isWebPlayer
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001F8 RID: 504
		public static extern RuntimePlatform platform
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060001F9 RID: 505
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void CaptureScreenshot(string filename, [DefaultValue("0")] int superSize);

		// Token: 0x060001FA RID: 506 RVA: 0x00006AB4 File Offset: 0x00004CB4
		[ExcludeFromDocs]
		public static void CaptureScreenshot(string filename)
		{
			int num = 0;
			Application.CaptureScreenshot(filename, num);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001FB RID: 507
		// (set) Token: 0x060001FC RID: 508
		public static extern bool runInBackground
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00006ACC File Offset: 0x00004CCC
		[Obsolete("use Application.isEditor instead")]
		public static bool isPlayer
		{
			get
			{
				return !Application.isEditor;
			}
		}

		// Token: 0x060001FE RID: 510
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool HasProLicense();

		// Token: 0x060001FF RID: 511
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern bool HasAdvancedLicense();

		// Token: 0x06000200 RID: 512
		[Obsolete("Use Object.DontDestroyOnLoad instead")]
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void DontDestroyOnLoad(Object mono);

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000201 RID: 513
		public static extern string dataPath
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000202 RID: 514
		public static extern string streamingAssetsPath
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000203 RID: 515
		[SecurityCritical]
		public static extern string persistentDataPath
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000204 RID: 516
		public static extern string temporaryCachePath
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000205 RID: 517
		public static extern string srcValue
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000206 RID: 518
		public static extern string absoluteURL
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00006AD8 File Offset: 0x00004CD8
		[Obsolete("Please use absoluteURL instead")]
		public static string absoluteUrl
		{
			get
			{
				return Application.absoluteURL;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006AE0 File Offset: 0x00004CE0
		private static string ObjectToJSString(object o)
		{
			if (o == null)
			{
				return "null";
			}
			if (o is string)
			{
				string text = o.ToString().Replace("\\", "\\\\");
				text = text.Replace("\"", "\\\"");
				text = text.Replace("\n", "\\n");
				text = text.Replace("\r", "\\r");
				text = text.Replace("\0", string.Empty);
				text = text.Replace("\u2028", string.Empty);
				text = text.Replace("\u2029", string.Empty);
				return '"' + text + '"';
			}
			if (o is int || o is short || o is uint || o is ushort || o is byte)
			{
				return o.ToString();
			}
			if (o is float)
			{
				NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
				return ((float)o).ToString(numberFormat);
			}
			if (o is double)
			{
				NumberFormatInfo numberFormat2 = CultureInfo.InvariantCulture.NumberFormat;
				return ((double)o).ToString(numberFormat2);
			}
			if (o is char)
			{
				if ((char)o == '"')
				{
					return "\"\\\"\"";
				}
				return '"' + o.ToString() + '"';
			}
			else
			{
				if (o is IList)
				{
					IList list = (IList)o;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("new Array(");
					int count = list.Count;
					for (int i = 0; i < count; i++)
					{
						if (i != 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(Application.ObjectToJSString(list[i]));
					}
					stringBuilder.Append(")");
					return stringBuilder.ToString();
				}
				return Application.ObjectToJSString(o.ToString());
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006CE8 File Offset: 0x00004EE8
		public static void ExternalCall(string functionName, params object[] args)
		{
			Application.Internal_ExternalCall(Application.BuildInvocationForArguments(functionName, args));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006CF8 File Offset: 0x00004EF8
		private static string BuildInvocationForArguments(string functionName, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionName);
			stringBuilder.Append('(');
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(Application.ObjectToJSString(args[i]));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(';');
			return stringBuilder.ToString();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006D6C File Offset: 0x00004F6C
		public static void ExternalEval(string script)
		{
			if (script.Length > 0 && script[script.Length - 1] != ';')
			{
				script += ';';
			}
			Application.Internal_ExternalCall(script);
		}

		// Token: 0x0600020C RID: 524
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_ExternalCall(string script);

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600020D RID: 525
		public static extern string unityVersion
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600020E RID: 526
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern int GetBuildUnityVersion();

		// Token: 0x0600020F RID: 527
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern int GetNumericUnityVersion(string version);

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000210 RID: 528
		public static extern bool webSecurityEnabled
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000211 RID: 529
		public static extern string webSecurityHostUrl
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000212 RID: 530
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void OpenURL(string url);

		// Token: 0x06000213 RID: 531
		[WrapperlessIcall]
		[Obsolete("For internal use only")]
		[MethodImpl(4096)]
		public static extern void CommitSuicide(int mode);

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000214 RID: 532
		// (set) Token: 0x06000215 RID: 533
		public static extern int targetFrameRate
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000216 RID: 534
		public static extern SystemLanguage systemLanguage
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006DA4 File Offset: 0x00004FA4
		public static void RegisterLogCallback(Application.LogCallback handler)
		{
			Application.s_LogCallback = handler;
			Application.SetLogCallbackDefined(handler != null, false);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006DBC File Offset: 0x00004FBC
		public static void RegisterLogCallbackThreaded(Application.LogCallback handler)
		{
			Application.s_LogCallback = handler;
			Application.SetLogCallbackDefined(handler != null, true);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006DD4 File Offset: 0x00004FD4
		private static void CallLogCallback(string logString, string stackTrace, LogType type)
		{
			if (Application.s_LogCallback != null)
			{
				Application.s_LogCallback(logString, stackTrace, type);
			}
		}

		// Token: 0x0600021A RID: 538
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void SetLogCallbackDefined(bool defined, bool threaded);

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600021B RID: 539
		// (set) Token: 0x0600021C RID: 540
		public static extern ThreadPriority backgroundLoadingPriority
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600021D RID: 541
		public static extern NetworkReachability internetReachability
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600021E RID: 542
		public static extern bool genuine
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600021F RID: 543
		public static extern bool genuineCheckAvailable
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000220 RID: 544
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern AsyncOperation RequestUserAuthorization(UserAuthorization mode);

		// Token: 0x06000221 RID: 545
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool HasUserAuthorization(UserAuthorization mode);

		// Token: 0x06000222 RID: 546
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal static extern void ReplyToUserAuthorizationRequest(bool reply, [DefaultValue("false")] bool remember);

		// Token: 0x06000223 RID: 547 RVA: 0x00006DF4 File Offset: 0x00004FF4
		[ExcludeFromDocs]
		internal static void ReplyToUserAuthorizationRequest(bool reply)
		{
			bool flag = false;
			Application.ReplyToUserAuthorizationRequest(reply, flag);
		}

		// Token: 0x06000224 RID: 548
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern int GetUserAuthorizationRequestMode_Internal();

		// Token: 0x06000225 RID: 549 RVA: 0x00006E0C File Offset: 0x0000500C
		internal static UserAuthorization GetUserAuthorizationRequestMode()
		{
			return (UserAuthorization)Application.GetUserAuthorizationRequestMode_Internal();
		}

		// Token: 0x04000029 RID: 41
		private static volatile Application.LogCallback s_LogCallback;

		// Token: 0x0200001B RID: 27
		// (Invoke) Token: 0x06000227 RID: 551
		public delegate void LogCallback(string condition, string stackTrace, LogType type);
	}
}
