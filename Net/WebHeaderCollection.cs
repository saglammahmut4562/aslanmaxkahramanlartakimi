using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Net
{
	// Token: 0x020000AD RID: 173
	[DefaultMember("Item")]
	[ComVisible(true)]
	[Serializable]
	public class WebHeaderCollection : global::System.Collections.Specialized.NameValueCollection, ISerializable
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x00015164 File Offset: 0x00013364
		public WebHeaderCollection()
		{
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001516C File Offset: 0x0001336C
		protected WebHeaderCollection(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			try
			{
				int num = serializationInfo.GetInt32("Count");
				for (int i = 0; i < num; i++)
				{
					this.Add(serializationInfo.GetString(i.ToString()), serializationInfo.GetString((num + i).ToString()));
				}
			}
			catch (SerializationException)
			{
				int num = serializationInfo.GetInt32("count");
				for (int j = 0; j < num; j++)
				{
					this.Add(serializationInfo.GetString("k" + j), serializationInfo.GetString("v" + j));
				}
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001522C File Offset: 0x0001342C
		internal WebHeaderCollection(bool internallyCreated)
		{
			this.internallyCreated = internallyCreated;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001523C File Offset: 0x0001343C
		static WebHeaderCollection()
		{
			WebHeaderCollection.restricted.Add("accept", true);
			WebHeaderCollection.restricted.Add("connection", true);
			WebHeaderCollection.restricted.Add("content-length", true);
			WebHeaderCollection.restricted.Add("content-type", true);
			WebHeaderCollection.restricted.Add("date", true);
			WebHeaderCollection.restricted.Add("expect", true);
			WebHeaderCollection.restricted.Add("host", true);
			WebHeaderCollection.restricted.Add("if-modified-since", true);
			WebHeaderCollection.restricted.Add("range", true);
			WebHeaderCollection.restricted.Add("referer", true);
			WebHeaderCollection.restricted.Add("transfer-encoding", true);
			WebHeaderCollection.restricted.Add("user-agent", true);
			WebHeaderCollection.restricted.Add("proxy-connection", true);
			WebHeaderCollection.restricted_response = new Dictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);
			WebHeaderCollection.restricted_response.Add("Content-Length", true);
			WebHeaderCollection.restricted_response.Add("Transfer-Encoding", true);
			WebHeaderCollection.restricted_response.Add("WWW-Authenticate", true);
			WebHeaderCollection.multiValue = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
			WebHeaderCollection.multiValue.Add("accept", true);
			WebHeaderCollection.multiValue.Add("accept-charset", true);
			WebHeaderCollection.multiValue.Add("accept-encoding", true);
			WebHeaderCollection.multiValue.Add("accept-language", true);
			WebHeaderCollection.multiValue.Add("accept-ranges", true);
			WebHeaderCollection.multiValue.Add("allow", true);
			WebHeaderCollection.multiValue.Add("authorization", true);
			WebHeaderCollection.multiValue.Add("cache-control", true);
			WebHeaderCollection.multiValue.Add("connection", true);
			WebHeaderCollection.multiValue.Add("content-encoding", true);
			WebHeaderCollection.multiValue.Add("content-language", true);
			WebHeaderCollection.multiValue.Add("expect", true);
			WebHeaderCollection.multiValue.Add("if-match", true);
			WebHeaderCollection.multiValue.Add("if-none-match", true);
			WebHeaderCollection.multiValue.Add("proxy-authenticate", true);
			WebHeaderCollection.multiValue.Add("public", true);
			WebHeaderCollection.multiValue.Add("range", true);
			WebHeaderCollection.multiValue.Add("transfer-encoding", true);
			WebHeaderCollection.multiValue.Add("upgrade", true);
			WebHeaderCollection.multiValue.Add("vary", true);
			WebHeaderCollection.multiValue.Add("via", true);
			WebHeaderCollection.multiValue.Add("warning", true);
			WebHeaderCollection.multiValue.Add("www-authenticate", true);
			WebHeaderCollection.multiValue.Add("set-cookie", true);
			WebHeaderCollection.multiValue.Add("set-cookie2", true);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000155E8 File Offset: 0x000137E8
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000155F4 File Offset: 0x000137F4
		public void Add(string header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			int num = header.IndexOf(':');
			if (num == -1)
			{
				throw new ArgumentException("no colon found", "header");
			}
			this.Add(header.Substring(0, num), header.Substring(num + 1));
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001564C File Offset: 0x0001384C
		public override void Add(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.internallyCreated && WebHeaderCollection.IsRestricted(name))
			{
				throw new ArgumentException("This header must be modified with the appropiate property.");
			}
			this.AddWithoutValidate(name, value);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00015688 File Offset: 0x00013888
		protected void AddWithoutValidate(string headerName, string headerValue)
		{
			if (!WebHeaderCollection.IsHeaderName(headerName))
			{
				throw new ArgumentException("invalid header name: " + headerName, "headerName");
			}
			if (headerValue == null)
			{
				headerValue = string.Empty;
			}
			else
			{
				headerValue = headerValue.Trim();
			}
			if (!WebHeaderCollection.IsHeaderValue(headerValue))
			{
				throw new ArgumentException("invalid header value: " + headerValue, "headerValue");
			}
			base.Add(headerName, headerValue);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000156FC File Offset: 0x000138FC
		public override string[] GetValues(string header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			string[] values = base.GetValues(header);
			if (values == null || values.Length == 0)
			{
				return null;
			}
			return values;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00015734 File Offset: 0x00013934
		public static bool IsRestricted(string headerName)
		{
			if (headerName == null)
			{
				throw new ArgumentNullException("headerName");
			}
			if (headerName == string.Empty)
			{
				throw new ArgumentException("empty string", "headerName");
			}
			if (!WebHeaderCollection.IsHeaderName(headerName))
			{
				throw new ArgumentException("Invalid character in header");
			}
			return WebHeaderCollection.restricted.ContainsKey(headerName);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00015794 File Offset: 0x00013994
		public override void OnDeserialization(object sender)
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00015798 File Offset: 0x00013998
		public override void Remove(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.internallyCreated && WebHeaderCollection.IsRestricted(name))
			{
				throw new ArgumentException("restricted header");
			}
			base.Remove(name);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000157D4 File Offset: 0x000139D4
		public override void Set(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.internallyCreated && WebHeaderCollection.IsRestricted(name))
			{
				throw new ArgumentException("restricted header");
			}
			if (!WebHeaderCollection.IsHeaderName(name))
			{
				throw new ArgumentException("invalid header name");
			}
			if (value == null)
			{
				value = string.Empty;
			}
			else
			{
				value = value.Trim();
			}
			if (!WebHeaderCollection.IsHeaderValue(value))
			{
				throw new ArgumentException("invalid header value");
			}
			base.Set(name, value);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00015864 File Offset: 0x00013A64
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = base.Count;
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append(this.GetKey(i)).Append(": ").Append(this.Get(i))
					.Append("\r\n");
			}
			return stringBuilder.Append("\r\n").ToString();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000158D0 File Offset: 0x00013AD0
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			int count = base.Count;
			serializationInfo.AddValue("Count", count);
			for (int i = 0; i < count; i++)
			{
				serializationInfo.AddValue(i.ToString(), this.GetKey(i));
				serializationInfo.AddValue((count + i).ToString(), this.Get(i));
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00015930 File Offset: 0x00013B30
		public override int Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00015938 File Offset: 0x00013B38
		public override global::System.Collections.Specialized.NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return base.Keys;
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00015940 File Offset: 0x00013B40
		public override string Get(int index)
		{
			return base.Get(index);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001594C File Offset: 0x00013B4C
		public override string Get(string name)
		{
			return base.Get(name);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00015958 File Offset: 0x00013B58
		public override string GetKey(int index)
		{
			return base.GetKey(index);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00015964 File Offset: 0x00013B64
		public override IEnumerator GetEnumerator()
		{
			return base.GetEnumerator();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001596C File Offset: 0x00013B6C
		internal void SetInternal(string header)
		{
			int num = header.IndexOf(':');
			if (num == -1)
			{
				throw new ArgumentException("no colon found", "header");
			}
			this.SetInternal(header.Substring(0, num), header.Substring(num + 1));
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000159B0 File Offset: 0x00013BB0
		internal void SetInternal(string name, string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			else
			{
				value = value.Trim();
			}
			if (!WebHeaderCollection.IsHeaderValue(value))
			{
				throw new ArgumentException("invalid header value");
			}
			if (WebHeaderCollection.IsMultiValue(name))
			{
				base.Add(name, value);
			}
			else
			{
				base.Remove(name);
				base.Set(name, value);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00015A14 File Offset: 0x00013C14
		internal void RemoveAndAdd(string name, string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			else
			{
				value = value.Trim();
			}
			base.Remove(name);
			base.Set(name, value);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00015A40 File Offset: 0x00013C40
		internal void RemoveInternal(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			base.Remove(name);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00015A5C File Offset: 0x00013C5C
		internal static bool IsMultiValue(string headerName)
		{
			return headerName != null && !(headerName == string.Empty) && WebHeaderCollection.multiValue.ContainsKey(headerName);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00015A84 File Offset: 0x00013C84
		internal static bool IsHeaderValue(string value)
		{
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if (c == '\u007f')
				{
					return false;
				}
				if (c < ' ' && c != '\r' && c != '\n' && c != '\t')
				{
					return false;
				}
				if (c == '\n' && ++i < length)
				{
					c = value[i];
					if (c != ' ' && c != '\t')
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00015B0C File Offset: 0x00013D0C
		internal static bool IsHeaderName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return false;
			}
			int length = name.Length;
			for (int i = 0; i < length; i++)
			{
				char c = name[i];
				if (c > '~' || !WebHeaderCollection.allowed_chars[(int)c])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000329 RID: 809
		private static readonly Hashtable restricted = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);

		// Token: 0x0400032A RID: 810
		private static readonly Hashtable multiValue;

		// Token: 0x0400032B RID: 811
		private static readonly Dictionary<string, bool> restricted_response;

		// Token: 0x0400032C RID: 812
		private bool internallyCreated;

		// Token: 0x0400032D RID: 813
		private static bool[] allowed_chars = new bool[]
		{
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, true, false, true, true, true, true, false,
			false, false, true, true, false, true, true, false, true, true,
			true, true, true, true, true, true, true, true, false, false,
			false, false, false, false, false, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, false, false, false, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, false, true, false
		};
	}
}
