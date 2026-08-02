using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
[AddComponentMenu("Image Effects/Blur")]
[ExecuteInEditMode]
public class BlurEffect : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000012 RID: 18 RVA: 0x00002884 File Offset: 0x00000A84
	protected Material material
	{
		get
		{
			if (BlurEffect.m_Material == null)
			{
				BlurEffect.m_Material = new Material(this.blurShader);
				BlurEffect.m_Material.hideFlags = HideFlags.DontSave;
			}
			return BlurEffect.m_Material;
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000028C4 File Offset: 0x00000AC4
	protected void OnDisable()
	{
		if (BlurEffect.m_Material)
		{
			global::UnityEngine.Object.DestroyImmediate(BlurEffect.m_Material);
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000028E0 File Offset: 0x00000AE0
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.blurShader || !this.material.shader.isSupported)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000292C File Offset: 0x00000B2C
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000029AC File Offset: 0x00000BAC
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002A24 File Offset: 0x00000C24
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
		this.DownSample4x(source, temporary);
		bool flag = true;
		for (int i = 0; i < this.iterations; i++)
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
		if (flag)
		{
			Graphics.Blit(temporary, destination);
		}
		else
		{
			Graphics.Blit(temporary2, destination);
		}
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x04000008 RID: 8
	public int iterations = 3;

	// Token: 0x04000009 RID: 9
	public float blurSpread = 0.6f;

	// Token: 0x0400000A RID: 10
	public Shader blurShader;

	// Token: 0x0400000B RID: 11
	private static Material m_Material;
}
