using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200002B RID: 43
	internal static class Extensions
	{
		// Token: 0x0600012B RID: 299 RVA: 0x000060C0 File Offset: 0x000042C0
		public static bool IsGenericInstanceOf(this Type self, Type type)
		{
			return self.IsGenericType && self.GetGenericTypeDefinition() == type;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000060D8 File Offset: 0x000042D8
		public static bool IsNullable(this Type self)
		{
			return self.IsValueType && self.IsGenericInstanceOf(typeof(Nullable<>));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000060F8 File Offset: 0x000042F8
		public static bool IsExpression(this Type self)
		{
			return self == typeof(Expression) || self.IsSubclassOf(typeof(Expression));
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006120 File Offset: 0x00004320
		public static bool IsAssignableTo(this Type self, Type type)
		{
			return type.IsAssignableFrom(self) || Extensions.ArrayTypeIsAssignableTo(self, type);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006138 File Offset: 0x00004338
		public static Type GetFirstGenericArgument(this Type self)
		{
			return self.GetGenericArguments()[0];
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006144 File Offset: 0x00004344
		public static Type GetNotNullableType(this Type self)
		{
			return (!self.IsNullable()) ? self : self.GetFirstGenericArgument();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006160 File Offset: 0x00004360
		public static MethodInfo GetInvokeMethod(this Type self)
		{
			return self.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006170 File Offset: 0x00004370
		private static bool ArrayTypeIsAssignableTo(Type type, Type candidate)
		{
			return type.IsArray && candidate.IsArray && type.GetArrayRank() == candidate.GetArrayRank() && type.GetElementType().IsAssignableTo(candidate.GetElementType());
		}
	}
}
