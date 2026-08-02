using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	public sealed class GUISettings
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		public Color cursorColor
		{
			get
			{
				return this.m_CursorColor;
			}
		}

		// Token: 0x0600054A RID: 1354
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern float Internal_GetCursorFlashSpeed();

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		public float cursorFlashSpeed
		{
			get
			{
				if (this.m_CursorFlashSpeed >= 0f)
				{
					return this.m_CursorFlashSpeed;
				}
				return GUISettings.Internal_GetCursorFlashSpeed();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
		}

		// Token: 0x04000130 RID: 304
		[SerializeField]
		private bool m_DoubleClickSelectsWord = true;

		// Token: 0x04000131 RID: 305
		[SerializeField]
		private bool m_TripleClickSelectsLine = true;

		// Token: 0x04000132 RID: 306
		[SerializeField]
		private Color m_CursorColor = Color.white;

		// Token: 0x04000133 RID: 307
		[SerializeField]
		private float m_CursorFlashSpeed = -1f;

		// Token: 0x04000134 RID: 308
		[SerializeField]
		private Color m_SelectionColor = new Color(0.5f, 0.5f, 1f);
	}
}
