using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
[AddComponentMenu("Image Effects/Color Correction (Ramp)")]
[ExecuteInEditMode]
public class ColorCorrectionEffect : ImageEffectBase
{
	// Token: 0x06000019 RID: 25 RVA: 0x00002AD0 File Offset: 0x00000CD0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400000C RID: 12
	public Texture textureRamp;
}
