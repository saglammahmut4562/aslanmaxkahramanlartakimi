using System;
using System.Runtime.Serialization;

namespace System.Net
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	public class ProtocolViolationException : InvalidOperationException, ISerializable
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0000E354 File Offset: 0x0000C554
		public ProtocolViolationException()
		{
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000E35C File Offset: 0x0000C55C
		public ProtocolViolationException(string message)
			: base(message)
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000E368 File Offset: 0x0000C568
		protected ProtocolViolationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000E374 File Offset: 0x0000C574
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000E380 File Offset: 0x0000C580
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
