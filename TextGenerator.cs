using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000100 RID: 256
	[StructLayout(0)]
	public sealed class TextGenerator : IDisposable
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x00016810 File Offset: 0x00014A10
		public TextGenerator()
			: this(50)
		{
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001681C File Offset: 0x00014A1C
		public TextGenerator(int initialCapacity)
		{
			this.m_Verts = new TextGenerator.InternalArrayCache<UIVertex>((initialCapacity + 1) * 4);
			this.m_Characters = new TextGenerator.InternalArrayCache<UICharInfo>(initialCapacity + 1);
			this.m_Lines = new TextGenerator.InternalArrayCache<UILineInfo>(20);
			this.Init();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00016858 File Offset: 0x00014A58
		private TextGenerationSettings ValidatedSettings(TextGenerationSettings settings)
		{
			if (settings.font != null && settings.font.dynamic)
			{
				return settings;
			}
			if (settings.size != 0 || settings.style != FontStyle.Normal)
			{
				Debug.LogWarning("Font size and style overrides are only supported for dynamic fonts.");
				settings.size = 0;
				settings.style = FontStyle.Normal;
			}
			if (settings.wrapMode == TextWrapMode.GrowText || settings.wrapMode == TextWrapMode.ShrinkText || settings.wrapMode == TextWrapMode.BestFit)
			{
				Debug.LogWarning("Grow, Shrink, and BestFit wrap modes are only suppoerted for dynamic fonts.");
				settings.wrapMode = TextWrapMode.Wrap;
			}
			return settings;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000168F8 File Offset: 0x00014AF8
		public void Invalidate()
		{
			this.m_HasGenerated = false;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00016904 File Offset: 0x00014B04
		public bool Populate(string str, TextGenerationSettings settings)
		{
			return (!this.m_HasGenerated || !(str == this.m_LastString) || !settings.Equals(this.m_LastSettings)) && this.PopulateAlways(str, settings);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00016940 File Offset: 0x00014B40
		private bool PopulateAlways(string str, TextGenerationSettings settings)
		{
			this.m_LastString = str;
			this.m_HasGenerated = true;
			this.m_CachedVerts = false;
			this.m_CachedCharacters = false;
			this.m_CachedLines = false;
			this.m_LastSettings = settings;
			TextGenerationSettings textGenerationSettings = this.ValidatedSettings(settings);
			return this.Populate_Internal(str, textGenerationSettings.font, textGenerationSettings.color, textGenerationSettings.size, textGenerationSettings.style, textGenerationSettings.richText, textGenerationSettings.wrapMode, textGenerationSettings.anchor, textGenerationSettings.extents, textGenerationSettings.pivot);
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x000169C8 File Offset: 0x00014BC8
		public IList<UIVertex> verts
		{
			get
			{
				if (!this.m_CachedVerts)
				{
					this.m_Verts.ResizeNoCopy(this.vertexCount);
					this.GetVerts(this.m_Verts.buffer);
					this.m_CachedVerts = true;
				}
				return this.m_Verts;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00016A08 File Offset: 0x00014C08
		public IList<UICharInfo> characters
		{
			get
			{
				if (!this.m_CachedCharacters)
				{
					this.m_Characters.ResizeNoCopy(this.characterCount);
					this.GetCharacters(this.m_Characters.buffer);
					this.m_CachedCharacters = true;
				}
				return this.m_Characters;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00016A48 File Offset: 0x00014C48
		public IList<UILineInfo> lines
		{
			get
			{
				if (!this.m_CachedLines)
				{
					this.m_Lines.ResizeNoCopy(this.lineCount);
					this.GetLines(this.m_Lines.buffer);
					this.m_CachedLines = true;
				}
				return this.m_Lines;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00016A88 File Offset: 0x00014C88
		public Vector2 extents
		{
			get
			{
				Rect rectExtents = this.rectExtents;
				Vector2 vector;
				vector.x = rectExtents.width;
				vector.y = rectExtents.height;
				return vector;
			}
		}

		// Token: 0x06000893 RID: 2195
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x06000894 RID: 2196
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Dispose();

		// Token: 0x06000895 RID: 2197 RVA: 0x00016ABC File Offset: 0x00014CBC
		~TextGenerator()
		{
			this.Dispose();
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00016AEC File Offset: 0x00014CEC
		internal bool Populate_Internal(string str, Font font, Color color, int size, FontStyle style, bool richText, TextWrapMode wrapMode, TextAnchor anchor, Vector2 extents, Vector2 pivot)
		{
			return TextGenerator.INTERNAL_CALL_Populate_Internal(this, str, font, ref color, size, style, richText, wrapMode, anchor, ref extents, ref pivot);
		}

		// Token: 0x06000897 RID: 2199
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_Populate_Internal(TextGenerator self, string str, Font font, ref Color color, int size, FontStyle style, bool richText, TextWrapMode wrapMode, TextAnchor anchor, ref Vector2 extents, ref Vector2 pivot);

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000898 RID: 2200
		public extern Rect rectExtents
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000899 RID: 2201
		public extern int vertexCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600089A RID: 2202
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int GetVerts(UIVertex[] verts);

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600089B RID: 2203
		public extern int characterCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600089C RID: 2204
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int GetCharacters(UICharInfo[] characters);

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600089D RID: 2205
		public extern int lineCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600089E RID: 2206
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern int GetLines(UILineInfo[] lines);

		// Token: 0x04000447 RID: 1095
		internal IntPtr m_Ptr;

		// Token: 0x04000448 RID: 1096
		private string m_LastString;

		// Token: 0x04000449 RID: 1097
		private TextGenerationSettings m_LastSettings;

		// Token: 0x0400044A RID: 1098
		private bool m_HasGenerated;

		// Token: 0x0400044B RID: 1099
		private readonly TextGenerator.InternalArrayCache<UIVertex> m_Verts;

		// Token: 0x0400044C RID: 1100
		private readonly TextGenerator.InternalArrayCache<UICharInfo> m_Characters;

		// Token: 0x0400044D RID: 1101
		private readonly TextGenerator.InternalArrayCache<UILineInfo> m_Lines;

		// Token: 0x0400044E RID: 1102
		private bool m_CachedVerts;

		// Token: 0x0400044F RID: 1103
		private bool m_CachedCharacters;

		// Token: 0x04000450 RID: 1104
		private bool m_CachedLines;

		// Token: 0x02000101 RID: 257
		private class InternalArrayCache<T> : ICollection<T>, IList<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600089F RID: 2207 RVA: 0x00016B14 File Offset: 0x00014D14
			public InternalArrayCache(int initialCapacity)
			{
				this.m_Buffer = new T[initialCapacity];
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x00016B28 File Offset: 0x00014D28
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00016B30 File Offset: 0x00014D30
			public T[] buffer
			{
				get
				{
					return this.m_Buffer;
				}
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x00016B38 File Offset: 0x00014D38
			private void IntegrityCheck()
			{
				if (this.m_Buffer.Length < this.m_Size)
				{
					throw new Exception("Internal array cache is invalid. Size of internal array is LESS then cached size");
				}
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x00016B58 File Offset: 0x00014D58
			private void ResizeInternalArray(int newSize, bool copyOld)
			{
				if (newSize == this.m_Buffer.Length)
				{
					return;
				}
				T[] array = new T[newSize];
				if (copyOld)
				{
					Array.Copy(this.m_Buffer, array, Math.Min(this.m_Buffer.Length, newSize));
				}
				this.m_Buffer = array;
				this.m_Size = Math.Min(this.m_Buffer.Length, this.m_Size);
				this.IntegrityCheck();
			}

			// Token: 0x060008A4 RID: 2212 RVA: 0x00016BC4 File Offset: 0x00014DC4
			private void Resize(int newSize, bool copyExisting)
			{
				this.Grow(newSize, copyExisting);
				this.m_Size = newSize;
			}

			// Token: 0x060008A5 RID: 2213 RVA: 0x00016BD8 File Offset: 0x00014DD8
			public void ResizeNoCopy(int newSize)
			{
				this.Resize(newSize, false);
			}

			// Token: 0x060008A6 RID: 2214 RVA: 0x00016BE4 File Offset: 0x00014DE4
			private void Grow(int minSize, bool copyExisting)
			{
				if (minSize < this.m_Buffer.Length)
				{
					return;
				}
				int i;
				for (i = this.m_Buffer.Length; i < minSize; i *= 2)
				{
				}
				this.ResizeInternalArray(i, copyExisting);
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00016C24 File Offset: 0x00014E24
			public int Count
			{
				get
				{
					return this.m_Size;
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00016C2C File Offset: 0x00014E2C
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060008A9 RID: 2217 RVA: 0x00016C30 File Offset: 0x00014E30
			public IEnumerator<T> GetEnumerator()
			{
				for (int i = 0; i < this.m_Size; i++)
				{
					yield return this.m_Buffer[i];
				}
				yield break;
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x00016C4C File Offset: 0x00014E4C
			public bool Contains(T item)
			{
				return this.IndexOf(item) != -1;
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x00016C5C File Offset: 0x00014E5C
			public int IndexOf(T item)
			{
				int num = Array.IndexOf<T>(this.m_Buffer, item);
				return (num != -1 && num <= this.m_Size) ? num : (-1);
			}

			// Token: 0x170001DD RID: 477
			public T this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new IndexOutOfRangeException(UnityString.Format("Index {0} is out of bounds", new object[] { index }));
					}
					return this.m_Buffer[index];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new IndexOutOfRangeException(UnityString.Format("Index {0} is out of bounds", new object[] { index }));
					}
					this.m_Buffer[index] = value;
				}
			}

			// Token: 0x060008AE RID: 2222 RVA: 0x00016D1C File Offset: 0x00014F1C
			public void CopyTo(T[] array, int arrayIndex)
			{
				for (int i = arrayIndex; i < this.Count; i++)
				{
					array[i] = this.buffer[i];
				}
			}

			// Token: 0x060008AF RID: 2223 RVA: 0x00016D54 File Offset: 0x00014F54
			public void Insert(int index, T item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060008B0 RID: 2224 RVA: 0x00016D5C File Offset: 0x00014F5C
			public bool Remove(T item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x00016D64 File Offset: 0x00014F64
			public void RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060008B2 RID: 2226 RVA: 0x00016D6C File Offset: 0x00014F6C
			public void Add(T item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060008B3 RID: 2227 RVA: 0x00016D74 File Offset: 0x00014F74
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x04000451 RID: 1105
			private T[] m_Buffer;

			// Token: 0x04000452 RID: 1106
			private int m_Size;
		}
	}
}
