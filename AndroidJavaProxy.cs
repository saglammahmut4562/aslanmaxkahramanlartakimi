using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	public class AndroidJavaProxy
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00004A88 File Offset: 0x00002C88
		public AndroidJavaProxy(string javaInterface)
			: this(new AndroidJavaClass(javaInterface))
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004A98 File Offset: 0x00002C98
		public AndroidJavaProxy(AndroidJavaClass javaInterface)
		{
			this.javaInterface = javaInterface;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public virtual AndroidJavaObject Invoke(string methodName, object[] args)
		{
			Exception ex = null;
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			Type[] array = new Type[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				array[i] = ((args[i] != null) ? args[i].GetType() : typeof(AndroidJavaObject));
			}
			try
			{
				MethodInfo method = base.GetType().GetMethod(methodName, bindingFlags, null, array, null);
				if (method != null)
				{
					return _AndroidJNIHelper.Box(method.Invoke(this, args));
				}
			}
			catch (TargetInvocationException ex2)
			{
				ex = ex2.InnerException;
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			string[] array2 = new string[args.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = array[j].ToString();
			}
			if (ex != null)
			{
				throw new TargetInvocationException(string.Concat(new object[]
				{
					base.GetType(),
					".",
					methodName,
					"(",
					string.Join(",", array2),
					")"
				}), ex);
			}
			throw new Exception(string.Concat(new object[]
			{
				"No such proxy method: ",
				base.GetType(),
				".",
				methodName,
				"(",
				string.Join(",", array2),
				")"
			}));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004C2C File Offset: 0x00002E2C
		public virtual AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
		{
			object[] array = new object[javaArgs.Length];
			for (int i = 0; i < javaArgs.Length; i++)
			{
				array[i] = _AndroidJNIHelper.Unbox(javaArgs[i]);
			}
			return this.Invoke(methodName, array);
		}

		// Token: 0x04000008 RID: 8
		public readonly AndroidJavaClass javaInterface;
	}
}
