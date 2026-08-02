using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
[AddComponentMenu("Image Effects/Vortex")]
[ExecuteInEditMode]
public class VortexEffect : ImageEffectBase
{
	// Token: 0x0600004F RID: 79 RVA: 0x00004010 File Offset: 0x00002210
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, this.angle, this.center, this.radius);
	}

	// Token: 0x0400004F RID: 79
	public Vector2 radius = new Vector2(0.4f, 0.4f);

	// Token: 0x04000050 RID: 80
	public float angle = 50f;

	// Token: 0x04000051 RID: 81
	public Vector2 center = new Vector2(0.5f, 0.5f);
}
