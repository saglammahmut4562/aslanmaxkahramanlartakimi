using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Motion Blur (Color Accumulation)")]
[ExecuteInEditMode]
public class MotionBlur : ImageEffectBase
{
	// Token: 0x06000039 RID: 57 RVA: 0x00003528 File Offset: 0x00001728
	protected override void Start()
	{
		if (!SystemInfo.supportsRenderTextures)
		{
			base.enabled = false;
			return;
		}
		base.Start();
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00003544 File Offset: 0x00001744
	protected override void OnDisable()
	{
		base.OnDisable();
		global::UnityEngine.Object.DestroyImmediate(this.accumTexture);
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00003558 File Offset: 0x00001758
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.accumTexture == null || this.accumTexture.width != source.width || this.accumTexture.height != source.height)
		{
			global::UnityEngine.Object.DestroyImmediate(this.accumTexture);
			this.accumTexture = new RenderTexture(source.width, source.height, 0);
			this.accumTexture.hideFlags = HideFlags.HideAndDontSave;
			Graphics.Blit(source, this.accumTexture);
		}
		if (this.extraBlur)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
			Graphics.Blit(this.accumTexture, temporary);
			Graphics.Blit(temporary, this.accumTexture);
			RenderTexture.ReleaseTemporary(temporary);
		}
		this.blurAmount = Mathf.Clamp(this.blurAmount, 0f, 0.92f);
		base.material.SetTexture("_MainTex", this.accumTexture);
		base.material.SetFloat("_AccumOrig", 1f - this.blurAmount);
		Graphics.Blit(source, this.accumTexture, base.material);
		Graphics.Blit(this.accumTexture, destination);
	}

	// Token: 0x04000028 RID: 40
	public float blurAmount = 0.8f;

	// Token: 0x04000029 RID: 41
	public bool extraBlur;

	// Token: 0x0400002A RID: 42
	private RenderTexture accumTexture;
}
