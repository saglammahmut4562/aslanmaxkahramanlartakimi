using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000096 RID: 150
	public class Material : Object
	{
		// Token: 0x0600062F RID: 1583 RVA: 0x0001008C File Offset: 0x0000E28C
		public Material(Shader shader)
		{
			Material.Internal_CreateWithShader(this, shader);
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000630 RID: 1584
		// (set) Token: 0x06000631 RID: 1585
		public extern Shader shader
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001009C File Offset: 0x0000E29C
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x000100AC File Offset: 0x0000E2AC
		public Color color
		{
			get
			{
				return this.GetColor("_Color");
			}
			set
			{
				this.SetColor("_Color", value);
			}
		}

		// Token: 0x1700013A RID: 314
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x000100BC File Offset: 0x0000E2BC
		public Texture mainTexture
		{
			set
			{
				this.SetTexture("_MainTex", value);
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000100CC File Offset: 0x0000E2CC
		public void SetColor(string propertyName, Color color)
		{
			this.SetColor(Shader.PropertyToID(propertyName), color);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000100DC File Offset: 0x0000E2DC
		public void SetColor(int nameID, Color color)
		{
			Material.INTERNAL_CALL_SetColor(this, nameID, ref color);
		}

		// Token: 0x06000637 RID: 1591
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetColor(Material self, int nameID, ref Color color);

		// Token: 0x06000638 RID: 1592 RVA: 0x000100E8 File Offset: 0x0000E2E8
		public Color GetColor(string propertyName)
		{
			return this.GetColor(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000639 RID: 1593
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Color GetColor(int nameID);

		// Token: 0x0600063A RID: 1594 RVA: 0x000100F8 File Offset: 0x0000E2F8
		public void SetVector(string propertyName, Vector4 vector)
		{
			this.SetColor(propertyName, new Color(vector.x, vector.y, vector.z, vector.w));
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00010124 File Offset: 0x0000E324
		public void SetTexture(string propertyName, Texture texture)
		{
			this.SetTexture(Shader.PropertyToID(propertyName), texture);
		}

		// Token: 0x0600063C RID: 1596
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetTexture(int nameID, Texture texture);

		// Token: 0x0600063D RID: 1597 RVA: 0x00010134 File Offset: 0x0000E334
		public void SetMatrix(string propertyName, Matrix4x4 matrix)
		{
			this.SetMatrix(Shader.PropertyToID(propertyName), matrix);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00010144 File Offset: 0x0000E344
		public void SetMatrix(int nameID, Matrix4x4 matrix)
		{
			Material.INTERNAL_CALL_SetMatrix(this, nameID, ref matrix);
		}

		// Token: 0x0600063F RID: 1599
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetMatrix(Material self, int nameID, ref Matrix4x4 matrix);

		// Token: 0x06000640 RID: 1600 RVA: 0x00010150 File Offset: 0x0000E350
		public void SetFloat(string propertyName, float value)
		{
			this.SetFloat(Shader.PropertyToID(propertyName), value);
		}

		// Token: 0x06000641 RID: 1601
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetFloat(int nameID, float value);

		// Token: 0x06000642 RID: 1602
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void SetBuffer(string propertyName, ComputeBuffer buffer);

		// Token: 0x06000643 RID: 1603 RVA: 0x00010160 File Offset: 0x0000E360
		public bool HasProperty(string propertyName)
		{
			return this.HasProperty(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000644 RID: 1604
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool HasProperty(int nameID);

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000645 RID: 1605
		public extern int passCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000646 RID: 1606
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern bool SetPass(int pass);

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000647 RID: 1607
		public extern int renderQueue
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000648 RID: 1608
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_CreateWithShader([Writable] Material mono, Shader shader);
	}
}
