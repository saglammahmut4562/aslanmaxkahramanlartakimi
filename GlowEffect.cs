using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Glow")]
public class GlowEffect : MonoBehaviour
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000025 RID: 37 RVA: 0x00002F48 File Offset: 0x00001148
	protected Material compositeMaterial
	{
		get
		{
			if (this.m_CompositeMaterial == null)
			{
				this.m_CompositeMaterial = new Material(this.compositeShader);
				this.m_CompositeMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_CompositeMaterial;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000026 RID: 38 RVA: 0x00002F80 File Offset: 0x00001180
	protected Material blurMaterial
	{
		get
		{
			if (this.m_BlurMaterial == null)
			{
				this.m_BlurMaterial = new Material(this.blurShader);
				this.m_BlurMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_BlurMaterial;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000027 RID: 39 RVA: 0x00002FB8 File Offset: 0x000011B8
	protected Material downsampleMaterial
	{
		get
		{
			if (this.m_DownsampleMaterial == null)
			{
				this.m_DownsampleMaterial = new Material(this.downsampleShader);
				this.m_DownsampleMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_DownsampleMaterial;
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002FF0 File Offset: 0x000011F0
	protected void OnDisable()
	{
		if (this.m_CompositeMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_CompositeMaterial);
		}
		if (this.m_BlurMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_BlurMaterial);
		}
		if (this.m_DownsampleMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_DownsampleMaterial);
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003050 File Offset: 0x00001250
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (this.downsampleShader == null)
		{
			Debug.Log("No downsample shader assigned! Disabling glow.");
			base.enabled = false;
		}
		else
		{
			if (!this.blurMaterial.shader.isSupported)
			{
				base.enabled = false;
			}
			if (!this.compositeMaterial.shader.isSupported)
			{
				base.enabled = false;
			}
			if (!this.downsampleMaterial.shader.isSupported)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000030EC File Offset: 0x000012EC
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.blurMaterial, new Vector2[]
		{
			new Vector2(num, num),
			new Vector2(-num, num),
			new Vector2(num, -num),
			new Vector2(-num, -num)
		});
	}

	// Token: 0x0600002B RID: 43 RVA: 0x0000316C File Offset: 0x0000136C
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		this.downsampleMaterial.color = new Color(this.glowTint.r, this.glowTint.g, this.glowTint.b, this.glowTint.a / 4f);
		Graphics.Blit(source, dest, this.downsampleMaterial);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000031C8 File Offset: 0x000013C8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.glowIntensity = Mathf.Clamp(this.glowIntensity, 0f, 10f);
		this.blurIterations = Mathf.Clamp(this.blurIterations, 0, 30);
		this.blurSpread = Mathf.Clamp(this.blurSpread, 0.5f, 1f);
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		this.DownSample4x(source, temporary);
		float num = Mathf.Clamp01((this.glowIntensity - 1f) / 4f);
		this.blurMaterial.color = new Color(1f, 1f, 1f, 0.25f + num);
		bool flag = true;
		for (int i = 0; i < this.blurIterations; i++)
		{
			if (flag)
			{
				this.FourTapCone(temporary, temporary2, i);
			}
			else
			{
				this.FourTapCone(temporary2, temporary, i);
			}
			flag = !flag;
		}
		Graphics.Blit(source, destination);
		if (flag)
		{
			this.BlitGlow(temporary, destination);
		}
		else
		{
			this.BlitGlow(temporary2, destination);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003304 File Offset: 0x00001504
	public void BlitGlow(RenderTexture source, RenderTexture dest)
	{
		this.compositeMaterial.color = new Color(1f, 1f, 1f, Mathf.Clamp01(this.glowIntensity));
		Graphics.Blit(source, dest, this.compositeMaterial);
	}

	// Token: 0x0400001A RID: 26
	public float glowIntensity = 1.5f;

	// Token: 0x0400001B RID: 27
	public int blurIterations = 3;

	// Token: 0x0400001C RID: 28
	public float blurSpread = 0.7f;

	// Token: 0x0400001D RID: 29
	public Color glowTint = new Color(1f, 1f, 1f, 0f);

	// Token: 0x0400001E RID: 30
	public Shader compositeShader;

	// Token: 0x0400001F RID: 31
	private Material m_CompositeMaterial;

	// Token: 0x04000020 RID: 32
	public Shader blurShader;

	// Token: 0x04000021 RID: 33
	private Material m_BlurMaterial;

	// Token: 0x04000022 RID: 34
	public Shader downsampleShader;

	// Token: 0x04000023 RID: 35
	private Material m_DownsampleMaterial;
}
