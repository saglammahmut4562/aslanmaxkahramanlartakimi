using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
[AddComponentMenu("")]
[RequireComponent(typeof(Camera))]
public class ImageEffectBase : MonoBehaviour
{
	// Token: 0x06000031 RID: 49 RVA: 0x000033A0 File Offset: 0x000015A0
	protected virtual void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.shader || !this.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000032 RID: 50 RVA: 0x000033E8 File Offset: 0x000015E8
	protected Material material
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = new Material(this.shader);
				this.m_Material.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_Material;
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00003420 File Offset: 0x00001620
	protected virtual void OnDisable()
	{
		if (this.m_Material)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_Material);
		}
	}

	// Token: 0x04000026 RID: 38
	public Shader shader;

	// Token: 0x04000027 RID: 39
	private Material m_Material;
}
