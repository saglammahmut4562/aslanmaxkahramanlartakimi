using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	public sealed class AndroidJNIHelper
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00004C98 File Offset: 0x00002E98
		private AndroidJNIHelper()
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060000D9 RID: 217
		// (set) Token: 0x060000DA RID: 218
		public static extern bool debug
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004CA0 File Offset: 0x00002EA0
		[ExcludeFromDocs]
		public static IntPtr GetConstructorID(IntPtr javaClass)
		{
			string empty = string.Empty;
			return AndroidJNIHelper.GetConstructorID(javaClass, empty);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004CBC File Offset: 0x00002EBC
		public static IntPtr GetConstructorID(IntPtr javaClass, [DefaultValue("\"\"")] string signature)
		{
			return _AndroidJNIHelper.GetConstructorID(javaClass, signature);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004CC8 File Offset: 0x00002EC8
		[ExcludeFromDocs]
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, string signature)
		{
			bool flag = false;
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, flag);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004CE0 File Offset: 0x00002EE0
		[ExcludeFromDocs]
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName)
		{
			bool flag = false;
			string empty = string.Empty;
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, empty, flag);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004D00 File Offset: 0x00002F00
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("\"\"")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, isStatic);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004D0C File Offset: 0x00002F0C
		[ExcludeFromDocs]
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, string signature)
		{
			bool flag = false;
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, flag);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004D24 File Offset: 0x00002F24
		[ExcludeFromDocs]
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName)
		{
			bool flag = false;
			string empty = string.Empty;
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, empty, flag);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004D44 File Offset: 0x00002F44
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("\"\"")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, isStatic);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004D50 File Offset: 0x00002F50
		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return _AndroidJNIHelper.CreateJavaRunnable(jrunnable);
		}

		// Token: 0x060000E4 RID: 228
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern IntPtr CreateJavaProxy(AndroidJavaProxy proxy);

		// Token: 0x060000E5 RID: 229 RVA: 0x00004D58 File Offset: 0x00002F58
		public static IntPtr ConvertToJNIArray(Array array)
		{
			return _AndroidJNIHelper.ConvertToJNIArray(array);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004D60 File Offset: 0x00002F60
		public static jvalue[] CreateJNIArgArray(object[] args)
		{
			return _AndroidJNIHelper.CreateJNIArgArray(args);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004D68 File Offset: 0x00002F68
		public static void DeleteJNIArgArray(object[] args, jvalue[] jniArgs)
		{
			_AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004D74 File Offset: 0x00002F74
		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return _AndroidJNIHelper.GetConstructorID(jclass, args);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004D80 File Offset: 0x00002F80
		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(jclass, methodName, args, isStatic);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004D8C File Offset: 0x00002F8C
		public static string GetSignature(object obj)
		{
			return _AndroidJNIHelper.GetSignature(obj);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004D94 File Offset: 0x00002F94
		public static string GetSignature(object[] args)
		{
			return _AndroidJNIHelper.GetSignature(args);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004D9C File Offset: 0x00002F9C
		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			return _AndroidJNIHelper.ConvertFromJNIArray<ArrayType>(array);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004DA4 File Offset: 0x00002FA4
		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID<ReturnType>(jclass, methodName, args, isStatic);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public static IntPtr GetFieldID<FieldType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID<FieldType>(jclass, fieldName, isStatic);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004DBC File Offset: 0x00002FBC
		public static string GetSignature<ReturnType>(object[] args)
		{
			return _AndroidJNIHelper.GetSignature<ReturnType>(args);
		}
	}
}
