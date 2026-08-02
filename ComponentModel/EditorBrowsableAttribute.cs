using System;

namespace System.ComponentModel
{
	// Token: 0x02000030 RID: 48
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
	public sealed class EditorBrowsableAttribute : Attribute
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00005344 File Offset: 0x00003544
		public EditorBrowsableAttribute(EditorBrowsableState state)
		{
			this.state = state;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005354 File Offset: 0x00003554
		public EditorBrowsableState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000535C File Offset: 0x0000355C
		public override bool Equals(object obj)
		{
			return obj is EditorBrowsableAttribute && (obj == this || ((EditorBrowsableAttribute)obj).State == this.state);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005388 File Offset: 0x00003588
		public override int GetHashCode()
		{
			return this.state.GetHashCode();
		}

		// Token: 0x04000066 RID: 102
		private EditorBrowsableState state;
	}
}
