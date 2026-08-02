using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace UnityEngine
{
	// Token: 0x020000F5 RID: 245
	public class StackTraceUtility
	{
		// Token: 0x06000825 RID: 2085 RVA: 0x00013690 File Offset: 0x00011890
		internal static void SetProjectFolder(string folder)
		{
			StackTraceUtility.projectFolder = folder;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00013698 File Offset: 0x00011898
		public static string ExtractStackTrace()
		{
			StackTrace stackTrace = new StackTrace(1, true);
			return StackTraceUtility.ExtractFormattedStackTrace(stackTrace).ToString();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000136BC File Offset: 0x000118BC
		private static bool IsSystemStacktraceType(object name)
		{
			string text = (string)name;
			return text.StartsWith("UnityEditor.") || text.StartsWith("UnityEngine.") || text.StartsWith("System.") || text.StartsWith("UnityScript.Lang.") || text.StartsWith("Boo.Lang.") || text.StartsWith("UnityEngine.SetupCoroutine");
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00013730 File Offset: 0x00011930
		public static string ExtractStringFromException(object exception)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			StackTraceUtility.ExtractStringFromExceptionInternal(exception, out empty, out empty2);
			return empty + "\n" + empty2;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00013760 File Offset: 0x00011960
		internal static void ExtractStringFromExceptionInternal(object exceptiono, out string message, out string stackTrace)
		{
			if (exceptiono == null)
			{
				throw new ArgumentException("ExtractStringFromExceptionInternal called with null exception");
			}
			Exception ex = exceptiono as Exception;
			if (ex == null)
			{
				throw new ArgumentException("ExtractStringFromExceptionInternal called with an exceptoin that was not of type System.Exception");
			}
			StringBuilder stringBuilder = new StringBuilder((ex.StackTrace != null) ? (ex.StackTrace.Length * 2) : 512);
			message = string.Empty;
			string text = string.Empty;
			while (ex != null)
			{
				if (text.Length == 0)
				{
					text = ex.StackTrace;
				}
				else
				{
					text = ex.StackTrace + "\n" + text;
				}
				string text2 = ex.GetType().Name;
				string text3 = string.Empty;
				if (ex.Message != null)
				{
					text3 = ex.Message;
				}
				if (text3.Trim().Length != 0)
				{
					text2 += ": ";
					text2 += text3;
				}
				message = text2;
				if (ex.InnerException != null)
				{
					text = "Rethrow as " + text2 + "\n" + text;
				}
				ex = ex.InnerException;
			}
			stringBuilder.Append(text + "\n");
			StackTrace stackTrace2 = new StackTrace(1, true);
			stringBuilder.Append(StackTraceUtility.ExtractFormattedStackTrace(stackTrace2));
			stackTrace = stringBuilder.ToString();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000138A4 File Offset: 0x00011AA4
		internal static string PostprocessStacktrace(string oldString, bool stripEngineInternalInformation)
		{
			if (oldString == null)
			{
				return string.Empty;
			}
			string[] array = oldString.Split(new char[] { '\n' });
			StringBuilder stringBuilder = new StringBuilder(oldString.Length);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
			}
			for (int j = 0; j < array.Length; j++)
			{
				string text = array[j];
				if (text.Length != 0 && text[0] != '\n')
				{
					if (!text.StartsWith("in (unmanaged)"))
					{
						if (stripEngineInternalInformation && text.StartsWith("UnityEditor.EditorGUIUtility:RenderGameViewCameras"))
						{
							break;
						}
						if (stripEngineInternalInformation && j < array.Length - 1 && StackTraceUtility.IsSystemStacktraceType(text))
						{
							if (StackTraceUtility.IsSystemStacktraceType(array[j + 1]))
							{
								goto IL_0261;
							}
							int num = text.IndexOf(" (at");
							if (num != -1)
							{
								text = text.Substring(0, num);
							}
						}
						if (text.IndexOf("(wrapper managed-to-native)") == -1)
						{
							if (text.IndexOf("(wrapper delegate-invoke)") == -1)
							{
								if (text.IndexOf("at <0x00000> <unknown method>") == -1)
								{
									if (!stripEngineInternalInformation || !text.StartsWith("[") || !text.EndsWith("]"))
									{
										if (text.StartsWith("at "))
										{
											text = text.Remove(0, 3);
										}
										int num2 = text.IndexOf("[0x");
										int num3 = -1;
										if (num2 != -1)
										{
											num3 = text.IndexOf("]", num2);
										}
										if (num2 != -1 && num3 > num2)
										{
											text = text.Remove(num2, num3 - num2 + 1);
										}
										text = text.Replace("  in <filename unknown>:0", string.Empty);
										text = text.Replace(StackTraceUtility.projectFolder, string.Empty);
										text = text.Replace('\\', '/');
										int num4 = text.LastIndexOf("  in ");
										if (num4 != -1)
										{
											text = text.Remove(num4, 5);
											text = text.Insert(num4, " (at ");
											text = text.Insert(text.Length, ")");
										}
										stringBuilder.Append(text + "\n");
									}
								}
							}
						}
					}
				}
				IL_0261:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00013B28 File Offset: 0x00011D28
		internal static string ExtractFormattedStackTrace(StackTrace stackTrace)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			for (int i = 0; i < stackTrace.FrameCount; i++)
			{
				StackFrame frame = stackTrace.GetFrame(i);
				MethodBase method = frame.GetMethod();
				if (method != null)
				{
					Type declaringType = method.DeclaringType;
					if (declaringType != null)
					{
						string @namespace = declaringType.Namespace;
						if (@namespace != null && @namespace.Length != 0)
						{
							stringBuilder.Append(@namespace);
							stringBuilder.Append(".");
						}
						stringBuilder.Append(declaringType.Name);
						stringBuilder.Append(":");
						stringBuilder.Append(method.Name);
						stringBuilder.Append("(");
						int j = 0;
						ParameterInfo[] parameters = method.GetParameters();
						bool flag = true;
						while (j < parameters.Length)
						{
							if (!flag)
							{
								stringBuilder.Append(", ");
							}
							else
							{
								flag = false;
							}
							stringBuilder.Append(parameters[j].ParameterType.Name);
							j++;
						}
						stringBuilder.Append(")");
						string text = frame.GetFileName();
						if (text != null && (!(declaringType.Name == "Debug") || !(declaringType.Namespace == "UnityEngine")))
						{
							stringBuilder.Append(" (at ");
							if (text.StartsWith(StackTraceUtility.projectFolder))
							{
								text = text.Substring(StackTraceUtility.projectFolder.Length, text.Length - StackTraceUtility.projectFolder.Length);
							}
							stringBuilder.Append(text);
							stringBuilder.Append(":");
							stringBuilder.Append(frame.GetFileLineNumber().ToString());
							stringBuilder.Append(")");
						}
						stringBuilder.Append("\n");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040003B6 RID: 950
		private static string projectFolder = string.Empty;
	}
}
