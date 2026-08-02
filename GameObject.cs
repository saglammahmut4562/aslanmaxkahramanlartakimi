using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000059 RID: 89
	public sealed class GameObject : Object
	{
		// Token: 0x0600039E RID: 926 RVA: 0x00008F88 File Offset: 0x00007188
		public GameObject(string name)
		{
			GameObject.Internal_CreateGameObject(this, name);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00008F98 File Offset: 0x00007198
		public GameObject()
		{
			GameObject.Internal_CreateGameObject(this, null);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00008FA8 File Offset: 0x000071A8
		public GameObject(string name, params Type[] components)
		{
			GameObject.Internal_CreateGameObject(this, name);
			foreach (Type type in components)
			{
				this.AddComponent(type);
			}
		}

		// Token: 0x060003A1 RID: 929
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SampleAnimation(AnimationClip animation, float time);

		// Token: 0x060003A2 RID: 930
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern GameObject CreatePrimitive(PrimitiveType type);

		// Token: 0x060003A3 RID: 931
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Component GetComponent(Type type);

		// Token: 0x060003A4 RID: 932 RVA: 0x00008FE4 File Offset: 0x000071E4
		public T GetComponent<T>() where T : Component
		{
			return this.GetComponent(typeof(T)) as T;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00009000 File Offset: 0x00007200
		public Component GetComponent(string type)
		{
			return this.GetComponentByName(type);
		}

		// Token: 0x060003A6 RID: 934
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Component GetComponentByName(string type);

		// Token: 0x060003A7 RID: 935 RVA: 0x0000900C File Offset: 0x0000720C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type type)
		{
			if (this.activeInHierarchy)
			{
				Component component = this.GetComponent(type);
				if (component != null)
				{
					return component;
				}
			}
			Transform transform = this.transform;
			if (transform != null)
			{
				foreach (object obj in transform)
				{
					Transform transform2 = (Transform)obj;
					Component componentInChildren = transform2.gameObject.GetComponentInChildren(type);
					if (componentInChildren != null)
					{
						return componentInChildren;
					}
				}
			}
			return null;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000090C4 File Offset: 0x000072C4
		public T GetComponentInChildren<T>() where T : Component
		{
			return this.GetComponentInChildren(typeof(T)) as T;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000090E0 File Offset: 0x000072E0
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type type)
		{
			if (this.activeInHierarchy)
			{
				Component component = this.GetComponent(type);
				if (component != null)
				{
					return component;
				}
			}
			Transform transform = this.transform.parent;
			if (transform != null)
			{
				while (transform != null)
				{
					if (transform.gameObject.activeInHierarchy)
					{
						Component component2 = transform.gameObject.GetComponent(type);
						if (component2 != null)
						{
							return component2;
						}
					}
					transform = transform.parent;
				}
			}
			return null;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000916C File Offset: 0x0000736C
		public T GetComponentInParent<T>() where T : Component
		{
			return this.GetComponentInParent(typeof(T)) as T;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003AB RID: 939
		// (set) Token: 0x060003AC RID: 940
		public extern bool isStatic
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003AD RID: 941
		internal extern bool isStaticBatchable
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00009188 File Offset: 0x00007388
		[CanConvertToFlash]
		public Component[] GetComponents(Type type)
		{
			return this.GetComponentsInternal(type, false, false, true, false);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00009198 File Offset: 0x00007398
		public T[] GetComponents<T>() where T : Component
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, false, true, false);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000091B4 File Offset: 0x000073B4
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type type)
		{
			bool flag = false;
			return this.GetComponentsInChildren(type, flag);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000091CC File Offset: 0x000073CC
		public Component[] GetComponentsInChildren(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return this.GetComponentsInternal(type, false, true, includeInactive, false);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000091DC File Offset: 0x000073DC
		public T[] GetComponentsInChildren<T>(bool includeInactive) where T : Component
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, false);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000091F8 File Offset: 0x000073F8
		public T[] GetComponentsInChildren<T>() where T : Component
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00009204 File Offset: 0x00007404
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type type)
		{
			bool flag = false;
			return this.GetComponentsInParent(type, flag);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000921C File Offset: 0x0000741C
		public Component[] GetComponentsInParent(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return this.GetComponentsInternal(type, false, true, includeInactive, true);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000922C File Offset: 0x0000742C
		public T[] GetComponentsInParent<T>(bool includeInactive) where T : Component
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, true);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00009248 File Offset: 0x00007448
		public T[] GetComponentsInParent<T>() where T : Component
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x060003B8 RID: 952
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Component[] GetComponentsInternal(Type type, bool isGenericTypeArray, bool recursive, bool includeInactive, bool reverse);

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003B9 RID: 953
		public extern Transform transform
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003BA RID: 954
		public extern Rigidbody rigidbody
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003BB RID: 955
		public extern Rigidbody2D rigidbody2D
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003BC RID: 956
		public extern Camera camera
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003BD RID: 957
		public extern Light light
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003BE RID: 958
		public extern Animation animation
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003BF RID: 959
		public extern ConstantForce constantForce
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003C0 RID: 960
		public extern Renderer renderer
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003C1 RID: 961
		public extern AudioSource audio
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003C2 RID: 962
		public extern GUIText guiText
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003C3 RID: 963
		public extern NetworkView networkView
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003C4 RID: 964
		[Obsolete("Please use guiTexture instead")]
		public extern GUIElement guiElement
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003C5 RID: 965
		public extern GUITexture guiTexture
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003C6 RID: 966
		public extern Collider collider
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003C7 RID: 967
		public extern Collider2D collider2D
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003C8 RID: 968
		public extern HingeJoint hingeJoint
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003C9 RID: 969
		public extern ParticleEmitter particleEmitter
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003CA RID: 970
		public extern ParticleSystem particleSystem
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003CB RID: 971
		// (set) Token: 0x060003CC RID: 972
		public extern int layer
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003CD RID: 973
		// (set) Token: 0x060003CE RID: 974
		[Obsolete("GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.")]
		public extern bool active
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060003CF RID: 975
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetActive(bool value);

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003D0 RID: 976
		public extern bool activeSelf
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003D1 RID: 977
		public extern bool activeInHierarchy
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003D2 RID: 978
		[WrapperlessIcall]
		[Obsolete("gameObject.SetActiveRecursively() is obsolete. Use GameObject.SetActive(), which is now inherited by children.")]
		[MethodImpl(4096)]
		public extern void SetActiveRecursively(bool state);

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003D3 RID: 979
		// (set) Token: 0x060003D4 RID: 980
		public extern string tag
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060003D5 RID: 981
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool CompareTag(string tag);

		// Token: 0x060003D6 RID: 982
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern GameObject FindGameObjectWithTag(string tag);

		// Token: 0x060003D7 RID: 983 RVA: 0x00009254 File Offset: 0x00007454
		public static GameObject FindWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		// Token: 0x060003D8 RID: 984
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern GameObject[] FindGameObjectsWithTag(string tag);

		// Token: 0x060003D9 RID: 985
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060003DA RID: 986 RVA: 0x0000925C File Offset: 0x0000745C
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessageUpwards(methodName, value, sendMessageOptions);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00009274 File Offset: 0x00007474
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessageUpwards(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00009290 File Offset: 0x00007490
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x060003DD RID: 989
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060003DE RID: 990 RVA: 0x0000929C File Offset: 0x0000749C
		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessage(methodName, value, sendMessageOptions);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000092B4 File Offset: 0x000074B4
		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000092D0 File Offset: 0x000074D0
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x060003E1 RID: 993
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060003E2 RID: 994 RVA: 0x000092DC File Offset: 0x000074DC
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.BroadcastMessage(methodName, parameter, sendMessageOptions);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000092F4 File Offset: 0x000074F4
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.BroadcastMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00009310 File Offset: 0x00007510
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}

		// Token: 0x060003E5 RID: 997
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Component AddComponent(string className);

		// Token: 0x060003E6 RID: 998 RVA: 0x0000931C File Offset: 0x0000751C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component AddComponent(Type componentType)
		{
			return this.Internal_AddComponentWithType(componentType);
		}

		// Token: 0x060003E7 RID: 999
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Component Internal_AddComponentWithType(Type componentType);

		// Token: 0x060003E8 RID: 1000 RVA: 0x00009328 File Offset: 0x00007528
		public T AddComponent<T>() where T : Component
		{
			return this.AddComponent(typeof(T)) as T;
		}

		// Token: 0x060003E9 RID: 1001
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateGameObject([Writable] GameObject mono, string name);

		// Token: 0x060003EA RID: 1002
		[Obsolete("gameObject.PlayAnimation is not supported anymore. Use animation.Play")]
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void PlayAnimation(AnimationClip animation);

		// Token: 0x060003EB RID: 1003
		[WrapperlessIcall]
		[Obsolete("gameObject.StopAnimation is not supported anymore. Use animation.Stop")]
		[MethodImpl(4096)]
		public extern void StopAnimation();

		// Token: 0x060003EC RID: 1004
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern GameObject Find(string name);

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00009344 File Offset: 0x00007544
		public GameObject gameObject
		{
			get
			{
				return this;
			}
		}
	}
}
