using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Net
{
	// Token: 0x02000068 RID: 104
	internal class ChunkStream
	{
		// Token: 0x06000238 RID: 568 RVA: 0x000081B4 File Offset: 0x000063B4
		public ChunkStream(byte[] buffer, int offset, int size, WebHeaderCollection headers)
			: this(headers)
		{
			this.Write(buffer, offset, size);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000081C8 File Offset: 0x000063C8
		public ChunkStream(WebHeaderCollection headers)
		{
			this.headers = headers;
			this.saved = new StringBuilder();
			this.chunks = new ArrayList();
			this.chunkSize = -1;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000081F4 File Offset: 0x000063F4
		public void ResetBuffer()
		{
			this.chunkSize = -1;
			this.chunkRead = 0;
			this.chunks.Clear();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00008210 File Offset: 0x00006410
		public void WriteAndReadBack(byte[] buffer, int offset, int size, ref int read)
		{
			if (offset + read > 0)
			{
				this.Write(buffer, offset, offset + read);
			}
			read = this.Read(buffer, offset, size);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00008238 File Offset: 0x00006438
		public int Read(byte[] buffer, int offset, int size)
		{
			return this.ReadFromChunks(buffer, offset, size);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008244 File Offset: 0x00006444
		private int ReadFromChunks(byte[] buffer, int offset, int size)
		{
			int count = this.chunks.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				ChunkStream.Chunk chunk = (ChunkStream.Chunk)this.chunks[i];
				if (chunk != null)
				{
					if (chunk.Offset == chunk.Bytes.Length)
					{
						this.chunks[i] = null;
					}
					else
					{
						num += chunk.Read(buffer, offset + num, size - num);
						if (num == size)
						{
							break;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000082D0 File Offset: 0x000064D0
		public void Write(byte[] buffer, int offset, int size)
		{
			this.InternalWrite(buffer, ref offset, size);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000082DC File Offset: 0x000064DC
		private void InternalWrite(byte[] buffer, ref int offset, int size)
		{
			if (this.state == ChunkStream.State.None)
			{
				this.state = this.GetChunkSize(buffer, ref offset, size);
				if (this.state == ChunkStream.State.None)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (this.state == ChunkStream.State.Body && offset < size)
			{
				this.state = this.ReadBody(buffer, ref offset, size);
				if (this.state == ChunkStream.State.Body)
				{
					return;
				}
			}
			if (this.state == ChunkStream.State.BodyFinished && offset < size)
			{
				this.state = this.ReadCRLF(buffer, ref offset, size);
				if (this.state == ChunkStream.State.BodyFinished)
				{
					return;
				}
				this.sawCR = false;
			}
			if (this.state == ChunkStream.State.Trailer && offset < size)
			{
				this.state = this.ReadTrailer(buffer, ref offset, size);
				if (this.state == ChunkStream.State.Trailer)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (offset < size)
			{
				this.InternalWrite(buffer, ref offset, size);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000083EC File Offset: 0x000065EC
		public bool WantMore
		{
			get
			{
				return this.chunkRead != this.chunkSize || this.chunkSize != 0 || this.state != ChunkStream.State.None;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000841C File Offset: 0x0000661C
		public int ChunkLeft
		{
			get
			{
				return this.chunkSize - this.chunkRead;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000842C File Offset: 0x0000662C
		private ChunkStream.State ReadBody(byte[] buffer, ref int offset, int size)
		{
			if (this.chunkSize == 0)
			{
				return ChunkStream.State.BodyFinished;
			}
			int num = size - offset;
			if (num + this.chunkRead > this.chunkSize)
			{
				num = this.chunkSize - this.chunkRead;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(buffer, offset, array, 0, num);
			this.chunks.Add(new ChunkStream.Chunk(array));
			offset += num;
			this.chunkRead += num;
			return (this.chunkRead != this.chunkSize) ? ChunkStream.State.Body : ChunkStream.State.BodyFinished;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000084BC File Offset: 0x000066BC
		private ChunkStream.State GetChunkSize(byte[] buffer, ref int offset, int size)
		{
			char c = '\0';
			while (offset < size)
			{
				c = (char)buffer[offset++];
				if (c == '\r')
				{
					if (this.sawCR)
					{
						ChunkStream.ThrowProtocolViolation("2 CR found");
					}
					this.sawCR = true;
				}
				else
				{
					if (this.sawCR && c == '\n')
					{
						break;
					}
					if (c == ' ')
					{
						this.gotit = true;
					}
					if (!this.gotit)
					{
						this.saved.Append(c);
					}
					if (this.saved.Length > 20)
					{
						ChunkStream.ThrowProtocolViolation("chunk size too long.");
					}
				}
			}
			if (!this.sawCR || c != '\n')
			{
				if (offset < size)
				{
					ChunkStream.ThrowProtocolViolation("Missing \\n");
				}
				try
				{
					if (this.saved.Length > 0)
					{
						this.chunkSize = int.Parse(ChunkStream.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
					}
				}
				catch (Exception)
				{
					ChunkStream.ThrowProtocolViolation("Cannot parse chunk size.");
				}
				return ChunkStream.State.None;
			}
			this.chunkRead = 0;
			try
			{
				this.chunkSize = int.Parse(ChunkStream.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
			}
			catch (Exception)
			{
				ChunkStream.ThrowProtocolViolation("Cannot parse chunk size.");
			}
			if (this.chunkSize == 0)
			{
				this.trailerState = 2;
				return ChunkStream.State.Trailer;
			}
			return ChunkStream.State.Body;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00008644 File Offset: 0x00006844
		private static string RemoveChunkExtension(string input)
		{
			int num = input.IndexOf(';');
			if (num == -1)
			{
				return input;
			}
			return input.Substring(0, num);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000866C File Offset: 0x0000686C
		private ChunkStream.State ReadCRLF(byte[] buffer, ref int offset, int size)
		{
			if (!this.sawCR)
			{
				if ((ushort)buffer[offset++] != 13)
				{
					ChunkStream.ThrowProtocolViolation("Expecting \\r");
				}
				this.sawCR = true;
				if (offset == size)
				{
					return ChunkStream.State.BodyFinished;
				}
			}
			if (this.sawCR && (ushort)buffer[offset++] != 10)
			{
				ChunkStream.ThrowProtocolViolation("Expecting \\n");
			}
			return ChunkStream.State.None;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000086DC File Offset: 0x000068DC
		private ChunkStream.State ReadTrailer(byte[] buffer, ref int offset, int size)
		{
			if (this.trailerState == 2 && (ushort)buffer[offset] == 13 && this.saved.Length == 0)
			{
				offset++;
				if (offset < size && (ushort)buffer[offset] == 10)
				{
					offset++;
					return ChunkStream.State.None;
				}
				offset--;
			}
			int num = this.trailerState;
			string text = "\r\n\r";
			while (offset < size && num < 4)
			{
				char c = (char)buffer[offset++];
				if ((num == 0 || num == 2) && c == '\r')
				{
					num++;
				}
				else if ((num == 1 || num == 3) && c == '\n')
				{
					num++;
				}
				else if (num > 0)
				{
					this.saved.Append(text.Substring(0, (this.saved.Length != 0) ? num : (num - 2)));
					num = 0;
					if (this.saved.Length > 4196)
					{
						ChunkStream.ThrowProtocolViolation("Error reading trailer (too long).");
					}
				}
			}
			if (num < 4)
			{
				this.trailerState = num;
				if (offset < size)
				{
					ChunkStream.ThrowProtocolViolation("Error reading trailer.");
				}
				return ChunkStream.State.Trailer;
			}
			StringReader stringReader = new StringReader(this.saved.ToString());
			string text2;
			while ((text2 = stringReader.ReadLine()) != null && text2 != string.Empty)
			{
				this.headers.Add(text2);
			}
			return ChunkStream.State.None;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008864 File Offset: 0x00006A64
		private static void ThrowProtocolViolation(string message)
		{
			WebException ex = new WebException(message, null, WebExceptionStatus.ServerProtocolViolation, null);
			throw ex;
		}

		// Token: 0x040000B3 RID: 179
		internal WebHeaderCollection headers;

		// Token: 0x040000B4 RID: 180
		private int chunkSize;

		// Token: 0x040000B5 RID: 181
		private int chunkRead;

		// Token: 0x040000B6 RID: 182
		private ChunkStream.State state;

		// Token: 0x040000B7 RID: 183
		private StringBuilder saved;

		// Token: 0x040000B8 RID: 184
		private bool sawCR;

		// Token: 0x040000B9 RID: 185
		private bool gotit;

		// Token: 0x040000BA RID: 186
		private int trailerState;

		// Token: 0x040000BB RID: 187
		private ArrayList chunks;

		// Token: 0x02000069 RID: 105
		private class Chunk
		{
			// Token: 0x06000248 RID: 584 RVA: 0x00008880 File Offset: 0x00006A80
			public Chunk(byte[] chunk)
			{
				this.Bytes = chunk;
			}

			// Token: 0x06000249 RID: 585 RVA: 0x00008890 File Offset: 0x00006A90
			public int Read(byte[] buffer, int offset, int size)
			{
				int num = ((size <= this.Bytes.Length - this.Offset) ? size : (this.Bytes.Length - this.Offset));
				Buffer.BlockCopy(this.Bytes, this.Offset, buffer, offset, num);
				this.Offset += num;
				return num;
			}

			// Token: 0x040000BC RID: 188
			public byte[] Bytes;

			// Token: 0x040000BD RID: 189
			public int Offset;
		}

		// Token: 0x0200006A RID: 106
		private enum State
		{
			// Token: 0x040000BF RID: 191
			None,
			// Token: 0x040000C0 RID: 192
			Body,
			// Token: 0x040000C1 RID: 193
			BodyFinished,
			// Token: 0x040000C2 RID: 194
			Trailer
		}
	}
}
