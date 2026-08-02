using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000112 RID: 274
	public sealed class Transform : Component, IEnumerable
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x000171E4 File Offset: 0x000153E4
		private Transform()
		{
		}

		// Token: 0x060008FD RID: 2301
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_position(out Vector3 value);

		// Token: 0x060008FE RID: 2302
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_position(ref Vector3 value);

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x000171EC File Offset: 0x000153EC
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x00017204 File Offset: 0x00015404
		public Vector3 position
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_position(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_position(ref value);
			}
		}

		// Token: 0x06000901 RID: 2305
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localPosition(out Vector3 value);

		// Token: 0x06000902 RID: 2306
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localPosition(ref Vector3 value);

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00017210 File Offset: 0x00015410
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00017228 File Offset: 0x00015428
		public Vector3 localPosition
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_localPosition(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_localPosition(ref value);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00017234 File Offset: 0x00015434
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x00017250 File Offset: 0x00015450
		public Vector3 eulerAngles
		{
			get
			{
				return this.rotation.eulerAngles;
			}
			set
			{
				this.rotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x06000907 RID: 2311
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localEulerAngles(out Vector3 value);

		// Token: 0x06000908 RID: 2312
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localEulerAngles(ref Vector3 value);

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00017260 File Offset: 0x00015460
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00017278 File Offset: 0x00015478
		public Vector3 localEulerAngles
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_localEulerAngles(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_localEulerAngles(ref value);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00017284 File Offset: 0x00015484
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x00017298 File Offset: 0x00015498
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.right, value);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x000172AC File Offset: 0x000154AC
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x000172C0 File Offset: 0x000154C0
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.up, value);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x000172D4 File Offset: 0x000154D4
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x000172E8 File Offset: 0x000154E8
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
			set
			{
				this.rotation = Quaternion.LookRotation(value);
			}
		}

		// Token: 0x06000911 RID: 2321
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_rotation(out Quaternion value);

		// Token: 0x06000912 RID: 2322
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_rotation(ref Quaternion value);

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x000172F8 File Offset: 0x000154F8
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00017310 File Offset: 0x00015510
		public Quaternion rotation
		{
			get
			{
				Quaternion quaternion;
				this.INTERNAL_get_rotation(out quaternion);
				return quaternion;
			}
			set
			{
				this.INTERNAL_set_rotation(ref value);
			}
		}

		// Token: 0x06000915 RID: 2325
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localRotation(out Quaternion value);

		// Token: 0x06000916 RID: 2326
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localRotation(ref Quaternion value);

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001731C File Offset: 0x0001551C
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00017334 File Offset: 0x00015534
		public Quaternion localRotation
		{
			get
			{
				Quaternion quaternion;
				this.INTERNAL_get_localRotation(out quaternion);
				return quaternion;
			}
			set
			{
				this.INTERNAL_set_localRotation(ref value);
			}
		}

		// Token: 0x06000919 RID: 2329
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localScale(out Vector3 value);

		// Token: 0x0600091A RID: 2330
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_localScale(ref Vector3 value);

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00017340 File Offset: 0x00015540
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00017358 File Offset: 0x00015558
		public Vector3 localScale
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_localScale(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_localScale(ref value);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600091D RID: 2333
		// (set) Token: 0x0600091E RID: 2334
		public extern Transform parent
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600091F RID: 2335
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_worldToLocalMatrix(out Matrix4x4 value);

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00017364 File Offset: 0x00015564
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.INTERNAL_get_worldToLocalMatrix(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x06000921 RID: 2337
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_localToWorldMatrix(out Matrix4x4 value);

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001737C File Offset: 0x0001557C
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.INTERNAL_get_localToWorldMatrix(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00017394 File Offset: 0x00015594
		[ExcludeFromDocs]
		public void Translate(Vector3 translation)
		{
			Space space = Space.Self;
			this.Translate(translation, space);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000173AC File Offset: 0x000155AC
		public void Translate(Vector3 translation, [DefaultValue("Space.Self")] Space relativeTo)
		{
			if (relativeTo == Space.World)
			{
				this.position += translation;
			}
			else
			{
				this.position += this.TransformDirection(translation);
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000173E4 File Offset: 0x000155E4
		[ExcludeFromDocs]
		public void Translate(float x, float y, float z)
		{
			Space space = Space.Self;
			this.Translate(x, y, z, space);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00017400 File Offset: 0x00015600
		public void Translate(float x, float y, float z, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00017414 File Offset: 0x00015614
		public void Translate(Vector3 translation, Transform relativeTo)
		{
			if (relativeTo)
			{
				this.position += relativeTo.TransformDirection(translation);
			}
			else
			{
				this.position += translation;
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00017450 File Offset: 0x00015650
		public void Translate(float x, float y, float z, Transform relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00017464 File Offset: 0x00015664
		[ExcludeFromDocs]
		public void Rotate(Vector3 eulerAngles)
		{
			Space space = Space.Self;
			this.Rotate(eulerAngles, space);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001747C File Offset: 0x0001567C
		public void Rotate(Vector3 eulerAngles, [DefaultValue("Space.Self")] Space relativeTo)
		{
			Quaternion quaternion = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
			if (relativeTo == Space.Self)
			{
				this.localRotation *= quaternion;
			}
			else
			{
				this.rotation *= Quaternion.Inverse(this.rotation) * quaternion * this.rotation;
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000174F0 File Offset: 0x000156F0
		[ExcludeFromDocs]
		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			Space space = Space.Self;
			this.Rotate(xAngle, yAngle, zAngle, space);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001750C File Offset: 0x0001570C
		public void Rotate(float xAngle, float yAngle, float zAngle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), relativeTo);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00017520 File Offset: 0x00015720
		internal void RotateAroundInternal(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAroundInternal(this, ref axis, angle);
		}

		// Token: 0x0600092E RID: 2350
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_RotateAroundInternal(Transform self, ref Vector3 axis, float angle);

		// Token: 0x0600092F RID: 2351 RVA: 0x0001752C File Offset: 0x0001572C
		[ExcludeFromDocs]
		public void Rotate(Vector3 axis, float angle)
		{
			Space space = Space.Self;
			this.Rotate(axis, angle, space);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00017544 File Offset: 0x00015744
		public void Rotate(Vector3 axis, float angle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			if (relativeTo == Space.Self)
			{
				this.RotateAroundInternal(base.transform.TransformDirection(axis), angle * 0.017453292f);
			}
			else
			{
				this.RotateAroundInternal(axis, angle * 0.017453292f);
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001757C File Offset: 0x0001577C
		public void RotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Vector3 vector = this.position;
			Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
			Vector3 vector2 = vector - point;
			vector2 = quaternion * vector2;
			vector = point + vector2;
			this.position = vector;
			this.RotateAroundInternal(axis, angle * 0.017453292f);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000175C8 File Offset: 0x000157C8
		[ExcludeFromDocs]
		public void LookAt(Transform target)
		{
			Vector3 up = Vector3.up;
			this.LookAt(target, up);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000175E4 File Offset: 0x000157E4
		public void LookAt(Transform target, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			if (target)
			{
				this.LookAt(target.position, worldUp);
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00017600 File Offset: 0x00015800
		public void LookAt(Vector3 worldPosition, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			Transform.INTERNAL_CALL_LookAt(this, ref worldPosition, ref worldUp);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001760C File Offset: 0x0001580C
		[ExcludeFromDocs]
		public void LookAt(Vector3 worldPosition)
		{
			Vector3 up = Vector3.up;
			Transform.INTERNAL_CALL_LookAt(this, ref worldPosition, ref up);
		}

		// Token: 0x06000936 RID: 2358
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_LookAt(Transform self, ref Vector3 worldPosition, ref Vector3 worldUp);

		// Token: 0x06000937 RID: 2359 RVA: 0x0001762C File Offset: 0x0001582C
		public Vector3 TransformDirection(Vector3 direction)
		{
			return Transform.INTERNAL_CALL_TransformDirection(this, ref direction);
		}

		// Token: 0x06000938 RID: 2360
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_TransformDirection(Transform self, ref Vector3 direction);

		// Token: 0x06000939 RID: 2361 RVA: 0x00017638 File Offset: 0x00015838
		public Vector3 TransformDirection(float x, float y, float z)
		{
			return this.TransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00017648 File Offset: 0x00015848
		public Vector3 InverseTransformDirection(Vector3 direction)
		{
			return Transform.INTERNAL_CALL_InverseTransformDirection(this, ref direction);
		}

		// Token: 0x0600093B RID: 2363
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_InverseTransformDirection(Transform self, ref Vector3 direction);

		// Token: 0x0600093C RID: 2364 RVA: 0x00017654 File Offset: 0x00015854
		public Vector3 InverseTransformDirection(float x, float y, float z)
		{
			return this.InverseTransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00017664 File Offset: 0x00015864
		public Vector3 TransformPoint(Vector3 position)
		{
			return Transform.INTERNAL_CALL_TransformPoint(this, ref position);
		}

		// Token: 0x0600093E RID: 2366
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_TransformPoint(Transform self, ref Vector3 position);

		// Token: 0x0600093F RID: 2367 RVA: 0x00017670 File Offset: 0x00015870
		public Vector3 TransformPoint(float x, float y, float z)
		{
			return this.TransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00017680 File Offset: 0x00015880
		public Vector3 InverseTransformPoint(Vector3 position)
		{
			return Transform.INTERNAL_CALL_InverseTransformPoint(this, ref position);
		}

		// Token: 0x06000941 RID: 2369
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Vector3 INTERNAL_CALL_InverseTransformPoint(Transform self, ref Vector3 position);

		// Token: 0x06000942 RID: 2370 RVA: 0x0001768C File Offset: 0x0001588C
		public Vector3 InverseTransformPoint(float x, float y, float z)
		{
			return this.InverseTransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000943 RID: 2371
		public extern Transform root
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000944 RID: 2372
		public extern int childCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000945 RID: 2373
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void DetachChildren();

		// Token: 0x06000946 RID: 2374
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetAsFirstSibling();

		// Token: 0x06000947 RID: 2375
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetAsLastSibling();

		// Token: 0x06000948 RID: 2376
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetSiblingIndex(int index);

		// Token: 0x06000949 RID: 2377
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int GetSiblingIndex();

		// Token: 0x0600094A RID: 2378
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Transform Find(string name);

		// Token: 0x0600094B RID: 2379
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_lossyScale(out Vector3 value);

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0001769C File Offset: 0x0001589C
		public Vector3 lossyScale
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_lossyScale(out vector);
				return vector;
			}
		}

		// Token: 0x0600094D RID: 2381
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool IsChildOf(Transform parent);

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600094E RID: 2382
		// (set) Token: 0x0600094F RID: 2383
		public extern bool hasChanged
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000176B4 File Offset: 0x000158B4
		public Transform FindChild(string name)
		{
			return this.Find(name);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x000176C0 File Offset: 0x000158C0
		public IEnumerator GetEnumerator()
		{
			return new Transform.Enumerator(this);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000176C8 File Offset: 0x000158C8
		[Obsolete("use Transform.Rotate instead.")]
		public void RotateAround(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAround(this, ref axis, angle);
		}

		// Token: 0x06000953 RID: 2387
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_RotateAround(Transform self, ref Vector3 axis, float angle);

		// Token: 0x06000954 RID: 2388 RVA: 0x000176D4 File Offset: 0x000158D4
		[Obsolete("use Transform.Rotate instead.")]
		public void RotateAroundLocal(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAroundLocal(this, ref axis, angle);
		}

		// Token: 0x06000955 RID: 2389
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_RotateAroundLocal(Transform self, ref Vector3 axis, float angle);

		// Token: 0x06000956 RID: 2390
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Transform GetChild(int index);

		// Token: 0x06000957 RID: 2391
		[WrapperlessIcall]
		[Obsolete("use Transform.childCount instead.")]
		[MethodImpl(4096)]
		public extern int GetChildCount();

		// Token: 0x02000113 RID: 275
		private sealed class Enumerator : IEnumerator
		{
			// Token: 0x06000958 RID: 2392 RVA: 0x000176E0 File Offset: 0x000158E0
			internal Enumerator(Transform outer)
			{
				this.outer = outer;
			}

			// Token: 0x17000205 RID: 517
			// (get) Token: 0x06000959 RID: 2393 RVA: 0x000176F8 File Offset: 0x000158F8
			public object Current
			{
				get
				{
					return this.outer.GetChild(this.currentIndex);
				}
			}

			// Token: 0x0600095A RID: 2394 RVA: 0x0001770C File Offset: 0x0001590C
			public bool MoveNext()
			{
				int childCount = this.outer.childCount;
				return ++this.currentIndex < childCount;
			}

			// Token: 0x0600095B RID: 2395 RVA: 0x0001773C File Offset: 0x0001593C
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x040004AE RID: 1198
			private Transform outer;

			// Token: 0x040004AF RID: 1199
			private int currentIndex = -1;
		}
	}
}
