using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Screen Space Ambient Occlusion")]
[ExecuteInEditMode]
public class SSAOEffect : MonoBehaviour
{
	// Token: 0x06000043 RID: 67 RVA: 0x00003B04 File Offset: 0x00001D04
	private static Material CreateMaterial(Shader shader)
	{
		if (!shader)
		{
			return null;
		}
		return new Material(shader)
		{
			hideFlags = HideFlags.HideAndDontSave
		};
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003B30 File Offset: 0x00001D30
	private static void DestroyMaterial(Material mat)
	{
		if (mat)
		{
			global::UnityEngine.Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003B48 File Offset: 0x00001D48
	private void OnDisable()
	{
		SSAOEffect.DestroyMaterial(this.m_SSAOMaterial);
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00003B58 File Offset: 0x00001D58
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects || !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			this.m_Supported = false;
			base.enabled = false;
			return;
		}
		this.CreateMaterials();
		if (!this.m_SSAOMaterial || this.m_SSAOMaterial.passCount != 5)
		{
			this.m_Supported = false;
			base.enabled = false;
			return;
		}
		this.m_Supported = true;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003BC8 File Offset: 0x00001DC8
	private void OnEnable()
	{
		base.camera.depthTextureMode |= DepthTextureMode.DepthNormals;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003BE0 File Offset: 0x00001DE0
	private void CreateMaterials()
	{
		if (!this.m_SSAOMaterial && this.m_SSAOShader.isSupported)
		{
			this.m_SSAOMaterial = SSAOEffect.CreateMaterial(this.m_SSAOShader);
			this.m_SSAOMaterial.SetTexture("_RandomTexture", this.m_RandomTexture);
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003C34 File Offset: 0x00001E34
	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.m_Supported || !this.m_SSAOShader.isSupported)
		{
			base.enabled = false;
			return;
		}
		this.CreateMaterials();
		this.m_Downsampling = Mathf.Clamp(this.m_Downsampling, 1, 6);
		this.m_Radius = Mathf.Clamp(this.m_Radius, 0.05f, 1f);
		this.m_MinZ = Mathf.Clamp(this.m_MinZ, 1E-05f, 0.5f);
		this.m_OcclusionIntensity = Mathf.Clamp(this.m_OcclusionIntensity, 0.5f, 4f);
		this.m_OcclusionAttenuation = Mathf.Clamp(this.m_OcclusionAttenuation, 0.2f, 2f);
		this.m_Blur = Mathf.Clamp(this.m_Blur, 0, 4);
		RenderTexture renderTexture = RenderTexture.GetTemporary(source.width / this.m_Downsampling, source.height / this.m_Downsampling, 0);
		float fieldOfView = base.camera.fieldOfView;
		float farClipPlane = base.camera.farClipPlane;
		float num = Mathf.Tan(fieldOfView * 0.017453292f * 0.5f) * farClipPlane;
		float num2 = num * base.camera.aspect;
		this.m_SSAOMaterial.SetVector("_FarCorner", new Vector3(num2, num, farClipPlane));
		int num3;
		int num4;
		if (this.m_RandomTexture)
		{
			num3 = this.m_RandomTexture.width;
			num4 = this.m_RandomTexture.height;
		}
		else
		{
			num3 = 1;
			num4 = 1;
		}
		this.m_SSAOMaterial.SetVector("_NoiseScale", new Vector3((float)renderTexture.width / (float)num3, (float)renderTexture.height / (float)num4, 0f));
		this.m_SSAOMaterial.SetVector("_Params", new Vector4(this.m_Radius, this.m_MinZ, 1f / this.m_OcclusionAttenuation, this.m_OcclusionIntensity));
		bool flag = this.m_Blur > 0;
		Graphics.Blit((!flag) ? source : null, renderTexture, this.m_SSAOMaterial, (int)this.m_SampleCount);
		if (flag)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
			this.m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4((float)this.m_Blur / (float)source.width, 0f, 0f, 0f));
			this.m_SSAOMaterial.SetTexture("_SSAO", renderTexture);
			Graphics.Blit(null, temporary, this.m_SSAOMaterial, 3);
			RenderTexture.ReleaseTemporary(renderTexture);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0);
			this.m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4(0f, (float)this.m_Blur / (float)source.height, 0f, 0f));
			this.m_SSAOMaterial.SetTexture("_SSAO", temporary);
			Graphics.Blit(source, temporary2, this.m_SSAOMaterial, 3);
			RenderTexture.ReleaseTemporary(temporary);
			renderTexture = temporary2;
		}
		this.m_SSAOMaterial.SetTexture("_SSAO", renderTexture);
		Graphics.Blit(source, destination, this.m_SSAOMaterial, 4);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	// Token: 0x0400003D RID: 61
	public float m_Radius = 0.4f;

	// Token: 0x0400003E RID: 62
	public SSAOEffect.SSAOSamples m_SampleCount = SSAOEffect.SSAOSamples.Medium;

	// Token: 0x0400003F RID: 63
	public float m_OcclusionIntensity = 1.5f;

	// Token: 0x04000040 RID: 64
	public int m_Blur = 2;

	// Token: 0x04000041 RID: 65
	public int m_Downsampling = 2;

	// Token: 0x04000042 RID: 66
	public float m_OcclusionAttenuation = 1f;

	// Token: 0x04000043 RID: 67
	public float m_MinZ = 0.01f;

	// Token: 0x04000044 RID: 68
	public Shader m_SSAOShader;

	// Token: 0x04000045 RID: 69
	private Material m_SSAOMaterial;

	// Token: 0x04000046 RID: 70
	public Texture2D m_RandomTexture;

	// Token: 0x04000047 RID: 71
	private bool m_Supported;

	// Token: 0x0200000E RID: 14
	public enum SSAOSamples
	{
		// Token: 0x04000049 RID: 73
		Low,
		// Token: 0x0400004A RID: 74
		Medium,
		// Token: 0x0400004B RID: 75
		High
	}
}
