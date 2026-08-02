using System;
using System.Text;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	public class AndroidJavaObject : IDisposable
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00003498 File Offset: 0x00001698
		internal AndroidJavaObject(IntPtr jobject)
			: this()
		{
			if (jobject == IntPtr.Zero)
			{
				throw new Exception("JNI: Init'd AndroidJavaObject with null ptr!");
			}
			IntPtr objectClass = AndroidJNISafe.GetObjectClass(jobject);
			this.m_jobject = AndroidJNI.NewGlobalRef(jobject);
			this.m_jclass = AndroidJNI.NewGlobalRef(objectClass);
			AndroidJNISafe.DeleteLocalRef(objectClass);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000034EC File Offset: 0x000016EC
		internal AndroidJavaObject()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000034F4 File Offset: 0x000016F4
		public AndroidJavaObject(string className, params object[] args)
			: this()
		{
			this._AndroidJavaObject(className, args);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003508 File Offset: 0x00001708
		protected void DebugPrint(string msg)
		{
			if (!AndroidJavaObject.enableDebugPrints)
			{
				return;
			}
			Debug.Log(msg);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000351C File Offset: 0x0000171C
		protected void DebugPrint(string call, string methodName, string signature, object[] args)
		{
			if (!AndroidJavaObject.enableDebugPrints)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in args)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append((obj != null) ? obj.GetType().ToString() : "<null>");
			}
			Debug.Log(string.Concat(new string[]
			{
				call,
				"(\"",
				methodName,
				"\"",
				stringBuilder.ToString(),
				") = ",
				signature
			}));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000035C0 File Offset: 0x000017C0
		private void _AndroidJavaObject(string className, params object[] args)
		{
			this.DebugPrint("Creating AndroidJavaObject from " + className);
			if (args == null)
			{
				args = new object[1];
			}
			using (AndroidJavaObject androidJavaObject = AndroidJavaObject.FindClass(className))
			{
				this.m_jclass = AndroidJNI.NewGlobalRef(androidJavaObject.GetRawObject());
				jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
				try
				{
					IntPtr constructorID = AndroidJNIHelper.GetConstructorID(this.m_jclass, args);
					IntPtr intPtr = AndroidJNISafe.NewObject(this.m_jclass, constructorID, array);
					this.m_jobject = AndroidJNI.NewGlobalRef(intPtr);
					AndroidJNISafe.DeleteLocalRef(intPtr);
				}
				finally
				{
					AndroidJNIHelper.DeleteJNIArgArray(args, array);
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003674 File Offset: 0x00001874
		~AndroidJavaObject()
		{
			this.Dispose(true);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000036A4 File Offset: 0x000018A4
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_disposed = true;
			AndroidJNISafe.DeleteGlobalRef(this.m_jobject);
			AndroidJNISafe.DeleteGlobalRef(this.m_jclass);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000036D0 File Offset: 0x000018D0
		protected void _Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000036E0 File Offset: 0x000018E0
		protected void _Call(string methodName, params object[] args)
		{
			if (args == null)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID(this.m_jclass, methodName, args, false);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			try
			{
				AndroidJNISafe.CallVoidMethod(this.m_jobject, methodID, array);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000373C File Offset: 0x0000193C
		protected ReturnType _Call<ReturnType>(string methodName, params object[] args)
		{
			if (args == null)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, false);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			ReturnType returnType;
			try
			{
				if (typeof(ReturnType).IsPrimitive)
				{
					if (typeof(ReturnType) == typeof(int))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallIntMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(bool))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallBooleanMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(byte))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallByteMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(short))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallShortMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(long))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallLongMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(float))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallFloatMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(double))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallDoubleMethod(this.m_jobject, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(char))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallCharMethod(this.m_jobject, methodID, array));
					}
					else
					{
						returnType = default(ReturnType);
					}
				}
				else if (typeof(ReturnType) == typeof(string))
				{
					returnType = (ReturnType)((object)AndroidJNISafe.CallStringMethod(this.m_jobject, methodID, array));
				}
				else if (typeof(ReturnType) == typeof(AndroidJavaClass))
				{
					IntPtr intPtr = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
					returnType = (ReturnType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(intPtr));
				}
				else if (typeof(ReturnType) == typeof(AndroidJavaObject))
				{
					IntPtr intPtr2 = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
					returnType = (ReturnType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(intPtr2));
				}
				else
				{
					if (!typeof(Array).IsAssignableFrom(typeof(ReturnType)))
					{
						throw new Exception("JNI: Unknown return type '" + typeof(ReturnType) + "'");
					}
					IntPtr intPtr3 = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
					returnType = (ReturnType)((object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(intPtr3));
				}
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
			return returnType;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003A84 File Offset: 0x00001C84
		protected FieldType _Get<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, false);
			if (typeof(FieldType).IsPrimitive)
			{
				if (typeof(FieldType) == typeof(int))
				{
					return (FieldType)((object)AndroidJNISafe.GetIntField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(bool))
				{
					return (FieldType)((object)AndroidJNISafe.GetBooleanField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(byte))
				{
					return (FieldType)((object)AndroidJNISafe.GetByteField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(short))
				{
					return (FieldType)((object)AndroidJNISafe.GetShortField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(long))
				{
					return (FieldType)((object)AndroidJNISafe.GetLongField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(float))
				{
					return (FieldType)((object)AndroidJNISafe.GetFloatField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(double))
				{
					return (FieldType)((object)AndroidJNISafe.GetDoubleField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(char))
				{
					return (FieldType)((object)AndroidJNISafe.GetCharField(this.m_jobject, fieldID));
				}
				return default(FieldType);
			}
			else
			{
				if (typeof(FieldType) == typeof(string))
				{
					return (FieldType)((object)AndroidJNISafe.GetStringField(this.m_jobject, fieldID));
				}
				if (typeof(FieldType) == typeof(AndroidJavaClass))
				{
					IntPtr objectField = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
					return (FieldType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(objectField));
				}
				if (typeof(FieldType) == typeof(AndroidJavaObject))
				{
					IntPtr objectField2 = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
					return (FieldType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(objectField2));
				}
				if (typeof(Array).IsAssignableFrom(typeof(FieldType)))
				{
					IntPtr objectField3 = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
					return (FieldType)((object)AndroidJNIHelper.ConvertFromJNIArray<FieldType>(objectField3));
				}
				throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003D2C File Offset: 0x00001F2C
		protected void _Set<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, false);
			if (typeof(FieldType).IsPrimitive)
			{
				if (typeof(FieldType) == typeof(int))
				{
					AndroidJNISafe.SetIntField(this.m_jobject, fieldID, (int)((object)val));
				}
				else if (typeof(FieldType) == typeof(bool))
				{
					AndroidJNISafe.SetBooleanField(this.m_jobject, fieldID, (bool)((object)val));
				}
				else if (typeof(FieldType) == typeof(byte))
				{
					AndroidJNISafe.SetByteField(this.m_jobject, fieldID, (byte)((object)val));
				}
				else if (typeof(FieldType) == typeof(short))
				{
					AndroidJNISafe.SetShortField(this.m_jobject, fieldID, (short)((object)val));
				}
				else if (typeof(FieldType) == typeof(long))
				{
					AndroidJNISafe.SetLongField(this.m_jobject, fieldID, (long)((object)val));
				}
				else if (typeof(FieldType) == typeof(float))
				{
					AndroidJNISafe.SetFloatField(this.m_jobject, fieldID, (float)((object)val));
				}
				else if (typeof(FieldType) == typeof(double))
				{
					AndroidJNISafe.SetDoubleField(this.m_jobject, fieldID, (double)((object)val));
				}
				else if (typeof(FieldType) == typeof(char))
				{
					AndroidJNISafe.SetCharField(this.m_jobject, fieldID, (char)((object)val));
				}
			}
			else if (typeof(FieldType) == typeof(string))
			{
				AndroidJNISafe.SetStringField(this.m_jobject, fieldID, (string)((object)val));
			}
			else if (typeof(FieldType) == typeof(AndroidJavaClass))
			{
				AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, ((AndroidJavaClass)((object)val)).m_jclass);
			}
			else if (typeof(FieldType) == typeof(AndroidJavaObject))
			{
				AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, ((AndroidJavaObject)((object)val)).m_jobject);
			}
			else
			{
				if (!typeof(Array).IsAssignableFrom(typeof(FieldType)))
				{
					throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
				}
				IntPtr intPtr = AndroidJNIHelper.ConvertToJNIArray((Array)((object)val));
				AndroidJNISafe.SetObjectField(this.m_jclass, fieldID, intPtr);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000400C File Offset: 0x0000220C
		protected void _CallStatic(string methodName, params object[] args)
		{
			if (args == null)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID(this.m_jclass, methodName, args, true);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			try
			{
				AndroidJNISafe.CallStaticVoidMethod(this.m_jclass, methodID, array);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00004068 File Offset: 0x00002268
		protected ReturnType _CallStatic<ReturnType>(string methodName, params object[] args)
		{
			if (args == null)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, true);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			ReturnType returnType;
			try
			{
				if (typeof(ReturnType).IsPrimitive)
				{
					if (typeof(ReturnType) == typeof(int))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticIntMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(bool))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticBooleanMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(byte))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticByteMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(short))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticShortMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(long))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticLongMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(float))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticFloatMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(double))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticDoubleMethod(this.m_jclass, methodID, array));
					}
					else if (typeof(ReturnType) == typeof(char))
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticCharMethod(this.m_jclass, methodID, array));
					}
					else
					{
						returnType = default(ReturnType);
					}
				}
				else if (typeof(ReturnType) == typeof(string))
				{
					returnType = (ReturnType)((object)AndroidJNISafe.CallStaticStringMethod(this.m_jclass, methodID, array));
				}
				else if (typeof(ReturnType) == typeof(AndroidJavaClass))
				{
					IntPtr intPtr = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
					returnType = (ReturnType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(intPtr));
				}
				else if (typeof(ReturnType) == typeof(AndroidJavaObject))
				{
					IntPtr intPtr2 = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
					returnType = (ReturnType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(intPtr2));
				}
				else
				{
					if (!typeof(Array).IsAssignableFrom(typeof(ReturnType)))
					{
						throw new Exception("JNI: Unknown return type '" + typeof(ReturnType) + "'");
					}
					IntPtr intPtr3 = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
					returnType = (ReturnType)((object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(intPtr3));
				}
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
			return returnType;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000043B0 File Offset: 0x000025B0
		protected FieldType _GetStatic<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, true);
			if (typeof(FieldType).IsPrimitive)
			{
				if (typeof(FieldType) == typeof(int))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticIntField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(bool))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticBooleanField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(byte))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticByteField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(short))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticShortField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(long))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticLongField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(float))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticFloatField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(double))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticDoubleField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(char))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticCharField(this.m_jclass, fieldID));
				}
				return default(FieldType);
			}
			else
			{
				if (typeof(FieldType) == typeof(string))
				{
					return (FieldType)((object)AndroidJNISafe.GetStaticStringField(this.m_jclass, fieldID));
				}
				if (typeof(FieldType) == typeof(AndroidJavaClass))
				{
					IntPtr staticObjectField = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
					return (FieldType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(staticObjectField));
				}
				if (typeof(FieldType) == typeof(AndroidJavaObject))
				{
					IntPtr staticObjectField2 = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
					return (FieldType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(staticObjectField2));
				}
				if (typeof(Array).IsAssignableFrom(typeof(FieldType)))
				{
					IntPtr staticObjectField3 = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
					return (FieldType)((object)AndroidJNIHelper.ConvertFromJNIArray<FieldType>(staticObjectField3));
				}
				throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00004658 File Offset: 0x00002858
		protected void _SetStatic<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, true);
			if (typeof(FieldType).IsPrimitive)
			{
				if (typeof(FieldType) == typeof(int))
				{
					AndroidJNISafe.SetStaticIntField(this.m_jclass, fieldID, (int)((object)val));
				}
				else if (typeof(FieldType) == typeof(bool))
				{
					AndroidJNISafe.SetStaticBooleanField(this.m_jclass, fieldID, (bool)((object)val));
				}
				else if (typeof(FieldType) == typeof(byte))
				{
					AndroidJNISafe.SetStaticByteField(this.m_jclass, fieldID, (byte)((object)val));
				}
				else if (typeof(FieldType) == typeof(short))
				{
					AndroidJNISafe.SetStaticShortField(this.m_jclass, fieldID, (short)((object)val));
				}
				else if (typeof(FieldType) == typeof(long))
				{
					AndroidJNISafe.SetStaticLongField(this.m_jclass, fieldID, (long)((object)val));
				}
				else if (typeof(FieldType) == typeof(float))
				{
					AndroidJNISafe.SetStaticFloatField(this.m_jclass, fieldID, (float)((object)val));
				}
				else if (typeof(FieldType) == typeof(double))
				{
					AndroidJNISafe.SetStaticDoubleField(this.m_jclass, fieldID, (double)((object)val));
				}
				else if (typeof(FieldType) == typeof(char))
				{
					AndroidJNISafe.SetStaticCharField(this.m_jclass, fieldID, (char)((object)val));
				}
			}
			else if (typeof(FieldType) == typeof(string))
			{
				AndroidJNISafe.SetStaticStringField(this.m_jclass, fieldID, (string)((object)val));
			}
			else if (typeof(FieldType) == typeof(AndroidJavaClass))
			{
				AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, ((AndroidJavaClass)((object)val)).m_jclass);
			}
			else if (typeof(FieldType) == typeof(AndroidJavaObject))
			{
				AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, ((AndroidJavaObject)((object)val)).m_jobject);
			}
			else
			{
				if (!typeof(Array).IsAssignableFrom(typeof(FieldType)))
				{
					throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
				}
				IntPtr intPtr = AndroidJNIHelper.ConvertToJNIArray((Array)((object)val));
				AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, intPtr);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004938 File Offset: 0x00002B38
		internal static AndroidJavaObject AndroidJavaObjectDeleteLocalRef(IntPtr jobject)
		{
			AndroidJavaObject androidJavaObject;
			try
			{
				androidJavaObject = new AndroidJavaObject(jobject);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jobject);
			}
			return androidJavaObject;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004970 File Offset: 0x00002B70
		internal static AndroidJavaClass AndroidJavaClassDeleteLocalRef(IntPtr jclass)
		{
			AndroidJavaClass androidJavaClass;
			try
			{
				androidJavaClass = new AndroidJavaClass(jclass);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jclass);
			}
			return androidJavaClass;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000049A8 File Offset: 0x00002BA8
		protected IntPtr _GetRawObject()
		{
			return this.m_jobject;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000049B0 File Offset: 0x00002BB0
		protected IntPtr _GetRawClass()
		{
			return this.m_jclass;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000049B8 File Offset: 0x00002BB8
		protected static AndroidJavaObject FindClass(string name)
		{
			return AndroidJavaObject.JavaLangClass.CallStatic<AndroidJavaObject>("forName", new object[] { name.Replace('/', '.') });
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000049E8 File Offset: 0x00002BE8
		protected static AndroidJavaClass JavaLangClass
		{
			get
			{
				if (AndroidJavaObject.s_JavaLangClass == null)
				{
					AndroidJavaObject.s_JavaLangClass = new AndroidJavaClass(AndroidJNISafe.FindClass("java/lang/Class"));
				}
				return AndroidJavaObject.s_JavaLangClass;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004A10 File Offset: 0x00002C10
		public void Dispose()
		{
			this._Dispose();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004A18 File Offset: 0x00002C18
		public void Call(string methodName, params object[] args)
		{
			this._Call(methodName, args);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004A24 File Offset: 0x00002C24
		public void CallStatic(string methodName, params object[] args)
		{
			this._CallStatic(methodName, args);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004A30 File Offset: 0x00002C30
		public FieldType Get<FieldType>(string fieldName)
		{
			return this._Get<FieldType>(fieldName);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004A3C File Offset: 0x00002C3C
		public void Set<FieldType>(string fieldName, FieldType val)
		{
			this._Set<FieldType>(fieldName, val);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004A48 File Offset: 0x00002C48
		public FieldType GetStatic<FieldType>(string fieldName)
		{
			return this._GetStatic<FieldType>(fieldName);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004A54 File Offset: 0x00002C54
		public void SetStatic<FieldType>(string fieldName, FieldType val)
		{
			this._SetStatic<FieldType>(fieldName, val);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004A60 File Offset: 0x00002C60
		public IntPtr GetRawObject()
		{
			return this._GetRawObject();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004A68 File Offset: 0x00002C68
		public IntPtr GetRawClass()
		{
			return this._GetRawClass();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004A70 File Offset: 0x00002C70
		public ReturnType Call<ReturnType>(string methodName, params object[] args)
		{
			return this._Call<ReturnType>(methodName, args);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004A7C File Offset: 0x00002C7C
		public ReturnType CallStatic<ReturnType>(string methodName, params object[] args)
		{
			return this._CallStatic<ReturnType>(methodName, args);
		}

		// Token: 0x04000003 RID: 3
		private static bool enableDebugPrints;

		// Token: 0x04000004 RID: 4
		private bool m_disposed;

		// Token: 0x04000005 RID: 5
		protected IntPtr m_jobject;

		// Token: 0x04000006 RID: 6
		protected IntPtr m_jclass;

		// Token: 0x04000007 RID: 7
		private static AndroidJavaClass s_JavaLangClass;
	}
}
