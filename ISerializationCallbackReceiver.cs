using System;

namespace UnityEngine
{
	// Token: 0x0200008C RID: 140
	public interface ISerializationCallbackReceiver
	{
		// Token: 0x06000624 RID: 1572
		void OnBeforeSerialize();

		// Token: 0x06000625 RID: 1573
		void OnAfterDeserialize();
	}
}
