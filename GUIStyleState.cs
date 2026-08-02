using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	[StructLayout(0)]
	public sealed class GUIStyleState
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x0000F9AC File Offset: 0x0000DBAC
		public GUIStyleState()
		{
			this.Init();
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000F9BC File Offset: 0x0000DBBC
		internal GUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000F9D4 File Offset: 0x0000DBD4
		~GUIStyleState()
		{
			if (this.m_SourceStyle == null)
			{
				this.Cleanup();
			}
		}

		// Token: 0x060005CE RID: 1486
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x060005CF RID: 1487
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x060005D0 RID: 1488
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_textColor(ref Color value);

		// Token: 0x17000122 RID: 290
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x0000FA10 File Offset: 0x0000DC10
		public Color textColor
		{
			set
			{
				this.INTERNAL_set_textColor(ref value);
			}
		}

		// Token: 0x04000161 RID: 353
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000162 RID: 354
		private GUIStyle m_SourceStyle;
	}
}
