using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
[AddComponentMenu("Image Effects/Sepia Tone")]
[ExecuteInEditMode]
public class SepiaToneEffect : ImageEffectBase
{
	// Token: 0x0600004B RID: 75 RVA: 0x00003F54 File Offset: 0x00002154
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, base.material);
	}
}
