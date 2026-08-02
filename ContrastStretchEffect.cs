using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
[AddComponentMenu("Image Effects/Contrast Stretch")]
[ExecuteInEditMode]
public class ContrastStretchEffect : MonoBehaviour
{
	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600001B RID: 27 RVA: 0x00002B38 File Offset: 0x00000D38
	protected Material materialLum
	{
		get
		{
			if (this.m_materialLum == null)
			{
				this.m_materialLum = new Material(this.shaderLum);
				this.m_materialLum.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialLum;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600001C RID: 28 RVA: 0x00002B70 File Offset: 0x00000D70
	protected Material materialReduce
	{
		get
		{
			if (this.m_materialReduce == null)
			{
				this.m_materialReduce = new Material(this.shaderReduce);
				this.m_materialReduce.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialReduce;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600001D RID: 29 RVA: 0x00002BA8 File Offset: 0x00000DA8
	protected Material materialAdapt
	{
		get
		{
			if (this.m_materialAdapt == null)
			{
				this.m_materialAdapt = new Material(this.shaderAdapt);
				this.m_materialAdapt.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialAdapt;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x0600001E RID: 30 RVA: 0x00002BE0 File Offset: 0x00000DE0
	protected Material materialApply
	{
		get
		{
			if (this.m_materialApply == null)
			{
				this.m_materialApply = new Material(this.shaderApply);
				this.m_materialApply.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_materialApply;
		}
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002C18 File Offset: 0x00000E18
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.shaderAdapt.isSupported || !this.shaderApply.isSupported || !this.shaderLum.isSupported || !this.shaderReduce.isSupported)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002C80 File Offset: 0x00000E80
	private void OnEnable()
	{
		for (int i = 0; i < 2; i++)
		{
			if (!this.adaptRenderTex[i])
			{
				this.adaptRenderTex[i] = new RenderTexture(1, 1, 32);
				this.adaptRenderTex[i].hideFlags = HideFlags.HideAndDontSave;
			}
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002CD4 File Offset: 0x00000ED4
	private void OnDisable()
	{
		for (int i = 0; i < 2; i++)
		{
			global::UnityEngine.Object.DestroyImmediate(this.adaptRenderTex[i]);
			this.adaptRenderTex[i] = null;
		}
		if (this.m_materialLum)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_materialLum);
		}
		if (this.m_materialReduce)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_materialReduce);
		}
		if (this.m_materialAdapt)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_materialAdapt);
		}
		if (this.m_materialApply)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_materialApply);
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002D78 File Offset: 0x00000F78
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture renderTexture = RenderTexture.GetTemporary(source.width / 1, source.height / 1);
		Graphics.Blit(source, renderTexture, this.materialLum);
		while (renderTexture.width > 1 || renderTexture.height > 1)
		{
			int num = renderTexture.width / 2;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = renderTexture.height / 2;
			if (num2 < 1)
			{
				num2 = 1;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2);
			Graphics.Blit(renderTexture, temporary, this.materialReduce);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		this.CalculateAdaptation(renderTexture);
		this.materialApply.SetTexture("_AdaptTex", this.adaptRenderTex[this.curAdaptIndex]);
		Graphics.Blit(source, destination, this.materialApply);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002E48 File Offset: 0x00001048
	private void CalculateAdaptation(Texture curTexture)
	{
		int num = this.curAdaptIndex;
		this.curAdaptIndex = (this.curAdaptIndex + 1) % 2;
		float num2 = 1f - Mathf.Pow(1f - this.adaptationSpeed, 30f * Time.deltaTime);
		num2 = Mathf.Clamp(num2, 0.01f, 1f);
		this.materialAdapt.SetTexture("_CurTex", curTexture);
		this.materialAdapt.SetVector("_AdaptParams", new Vector4(num2, this.limitMinimum, this.limitMaximum, 0f));
		Graphics.Blit(this.adaptRenderTex[num], this.adaptRenderTex[this.curAdaptIndex], this.materialAdapt);
	}

	// Token: 0x0400000D RID: 13
	public float adaptationSpeed = 0.02f;

	// Token: 0x0400000E RID: 14
	public float limitMinimum = 0.2f;

	// Token: 0x0400000F RID: 15
	public float limitMaximum = 0.6f;

	// Token: 0x04000010 RID: 16
	private RenderTexture[] adaptRenderTex = new RenderTexture[2];

	// Token: 0x04000011 RID: 17
	private int curAdaptIndex;

	// Token: 0x04000012 RID: 18
	public Shader shaderLum;

	// Token: 0x04000013 RID: 19
	private Material m_materialLum;

	// Token: 0x04000014 RID: 20
	public Shader shaderReduce;

	// Token: 0x04000015 RID: 21
	private Material m_materialReduce;

	// Token: 0x04000016 RID: 22
	public Shader shaderAdapt;

	// Token: 0x04000017 RID: 23
	private Material m_materialAdapt;

	// Token: 0x04000018 RID: 24
	public Shader shaderApply;

	// Token: 0x04000019 RID: 25
	private Material m_materialApply;
}
