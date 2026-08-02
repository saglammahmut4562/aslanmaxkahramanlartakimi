using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
[AddComponentMenu("Image Effects/Twirl")]
[ExecuteInEditMode]
public class TwirlEffect : ImageEffectBase
{
	// Token: 0x0600004D RID: 77 RVA: 0x00003FA4 File Offset: 0x000021A4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, this.angle, this.center, this.radius);
	}

	// Token: 0x0400004C RID: 76
	public Vector2 radius = new Vector2(0.3f, 0.3f);

	// Token: 0x0400004D RID: 77
	public float angle = 50f;

	// Token: 0x0400004E RID: 78
	public Vector2 center = new Vector2(0.5f, 0.5f);
}
