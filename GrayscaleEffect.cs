using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
[AddComponentMenu("Image Effects/Grayscale")]
[ExecuteInEditMode]
public class GrayscaleEffect : ImageEffectBase
{
	// Token: 0x0600002F RID: 47 RVA: 0x00003350 File Offset: 0x00001550
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_RampOffset", this.rampOffset);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000024 RID: 36
	public Texture textureRamp;

	// Token: 0x04000025 RID: 37
	public float rampOffset;
}
