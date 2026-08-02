using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x0200003E RID: 62
	public class Component : Object
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00007A88 File Offset: 0x00005C88
		public Transform transform
		{
			get
			{
				return this.InternalGetTransform();
			}
		}

		// Token: 0x060002F8 RID: 760
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern Transform InternalGetTransform();

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002F9 RID: 761
		public extern Rigidbody rigidbody
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002FA RID: 762
		public extern Rigidbody2D rigidbody2D
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002FB RID: 763
		public extern Camera camera
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002FC RID: 764
		public extern Light light
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002FD RID: 765
		public extern Animation animation
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002FE RID: 766
		public extern Renderer renderer
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002FF RID: 767
		public extern AudioSource audio
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000300 RID: 768
		public extern GUITexture guiTexture
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000301 RID: 769
		public extern Collider collider
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000302 RID: 770
		public extern ParticleSystem particleSystem
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00007A90 File Offset: 0x00005C90
		public GameObject gameObject
		{
			get
			{
				return this.InternalGetGameObject();
			}
		}

		// Token: 0x06000304 RID: 772
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern GameObject InternalGetGameObject();

		// Token: 0x06000305 RID: 773
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(4096)]
		public extern Component GetComponent(Type type);

		// Token: 0x06000306 RID: 774 RVA: 0x00007A98 File Offset: 0x00005C98
		public T GetComponent<T>() where T : Component
		{
			return this.GetComponent(typeof(T)) as T;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00007AB4 File Offset: 0x00005CB4
		public T[] GetComponentsInChildren<T>(bool includeInactive) where T : Component
		{
			return this.gameObject.GetComponentsInChildren<T>(includeInactive);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00007AC4 File Offset: 0x00005CC4
		public T[] GetComponentsInChildren<T>() where T : Component
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x06000309 RID: 777
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Component[] GetComponentsWithCorrectReturnType(Type type);

		// Token: 0x0600030A RID: 778 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public T[] GetComponents<T>() where T : Component
		{
			return (T[])this.GetComponentsWithCorrectReturnType(typeof(T));
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600030B RID: 779
		// (set) Token: 0x0600030C RID: 780
		public extern string tag
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600030D RID: 781
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x0600030E RID: 782 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x0600030F RID: 783
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06000310 RID: 784 RVA: 0x00007AF4 File Offset: 0x00005CF4
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.BroadcastMessage(methodName, obj, sendMessageOptions);
		}
	}
}
