using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x020000FB RID: 251
	public class TextEditor
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00013D50 File Offset: 0x00011F50
		private void ClearCursorPos()
		{
			this.hasHorizontalCursorPos = false;
			this.m_iAltCursorPos = -1;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00013D60 File Offset: 0x00011F60
		public void OnFocus()
		{
			if (this.multiline)
			{
				this.pos = (this.selectPos = 0);
			}
			else
			{
				this.SelectAll();
			}
			this.m_HasFocus = true;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00013D9C File Offset: 0x00011F9C
		public void OnLostFocus()
		{
			this.m_HasFocus = false;
			this.scrollOffset = Vector2.zero;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00013DB0 File Offset: 0x00011FB0
		private void GrabGraphicalCursorPos()
		{
			if (!this.hasHorizontalCursorPos)
			{
				this.graphicalCursorPos = this.style.GetCursorPixelPosition(this.position, this.content, this.pos);
				this.graphicalSelectCursorPos = this.style.GetCursorPixelPosition(this.position, this.content, this.selectPos);
				this.hasHorizontalCursorPos = false;
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00013E18 File Offset: 0x00012018
		public bool HandleKeyEvent(Event e)
		{
			this.InitKeyActions();
			EventModifiers modifiers = e.modifiers;
			e.modifiers &= ~EventModifiers.CapsLock;
			if (TextEditor.s_Keyactions.ContainsKey(e))
			{
				TextEditor.TextEditOp textEditOp = TextEditor.s_Keyactions[e];
				this.PerformOperation(textEditOp);
				e.modifiers = modifiers;
				this.UpdateScrollOffset();
				return true;
			}
			e.modifiers = modifiers;
			return false;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00013E7C File Offset: 0x0001207C
		public bool DeleteLineBack()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.pos;
			int num2 = num;
			while (num2-- != 0)
			{
				if (this.content.text[num2] == '\n')
				{
					num = num2 + 1;
					break;
				}
			}
			if (num2 == -1)
			{
				num = 0;
			}
			if (this.pos != num)
			{
				this.content.text = this.content.text.Remove(num, this.pos - num);
				this.selectPos = (this.pos = num);
				return true;
			}
			return false;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00013F24 File Offset: 0x00012124
		public bool DeleteWordBack()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.FindEndOfPreviousWord(this.pos);
			if (this.pos != num)
			{
				this.content.text = this.content.text.Remove(num, this.pos - num);
				this.selectPos = (this.pos = num);
				return true;
			}
			return false;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00013F98 File Offset: 0x00012198
		public bool DeleteWordForward()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.FindStartOfNextWord(this.pos);
			if (this.pos < this.content.text.Length)
			{
				this.content.text = this.content.text.Remove(this.pos, num - this.pos);
				return true;
			}
			return false;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00014010 File Offset: 0x00012210
		public bool Delete()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			if (this.pos < this.content.text.Length)
			{
				this.content.text = this.content.text.Remove(this.pos, 1);
				return true;
			}
			return false;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00014074 File Offset: 0x00012274
		public bool CanPaste()
		{
			return GUIUtility.systemCopyBuffer.Length != 0;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00014088 File Offset: 0x00012288
		public bool Backspace()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			if (this.pos > 0)
			{
				this.content.text = this.content.text.Remove(this.pos - 1, 1);
				this.selectPos = --this.pos;
				this.ClearCursorPos();
				return true;
			}
			return false;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000140FC File Offset: 0x000122FC
		public void SelectAll()
		{
			this.pos = 0;
			this.selectPos = this.content.text.Length;
			this.ClearCursorPos();
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00014124 File Offset: 0x00012324
		public void SelectNone()
		{
			this.selectPos = this.pos;
			this.ClearCursorPos();
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00014138 File Offset: 0x00012338
		public bool hasSelection
		{
			get
			{
				return this.pos != this.selectPos;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001414C File Offset: 0x0001234C
		public string SelectedText
		{
			get
			{
				int length = this.content.text.Length;
				if (this.pos > length)
				{
					this.pos = length;
				}
				if (this.selectPos > length)
				{
					this.selectPos = length;
				}
				if (this.pos == this.selectPos)
				{
					return string.Empty;
				}
				if (this.pos < this.selectPos)
				{
					return this.content.text.Substring(this.pos, this.selectPos - this.pos);
				}
				return this.content.text.Substring(this.selectPos, this.pos - this.selectPos);
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00014200 File Offset: 0x00012400
		public bool DeleteSelection()
		{
			int length = this.content.text.Length;
			if (this.pos > length)
			{
				this.pos = length;
			}
			if (this.selectPos > length)
			{
				this.selectPos = length;
			}
			if (this.pos == this.selectPos)
			{
				return false;
			}
			if (this.pos < this.selectPos)
			{
				this.content.text = this.content.text.Substring(0, this.pos) + this.content.text.Substring(this.selectPos, this.content.text.Length - this.selectPos);
				this.selectPos = this.pos;
			}
			else
			{
				this.content.text = this.content.text.Substring(0, this.selectPos) + this.content.text.Substring(this.pos, this.content.text.Length - this.pos);
				this.pos = this.selectPos;
			}
			this.ClearCursorPos();
			return true;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00014334 File Offset: 0x00012534
		public void ReplaceSelection(string replace)
		{
			this.DeleteSelection();
			this.content.text = this.content.text.Insert(this.pos, replace);
			this.selectPos = (this.pos += replace.Length);
			this.ClearCursorPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00014394 File Offset: 0x00012594
		public void Insert(char c)
		{
			this.ReplaceSelection(c.ToString());
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000143A4 File Offset: 0x000125A4
		public void MoveSelectionToAltCursor()
		{
			if (this.m_iAltCursorPos == -1)
			{
				return;
			}
			int iAltCursorPos = this.m_iAltCursorPos;
			string selectedText = this.SelectedText;
			this.content.text = this.content.text.Insert(iAltCursorPos, selectedText);
			if (iAltCursorPos < this.pos)
			{
				this.pos += selectedText.Length;
				this.selectPos += selectedText.Length;
			}
			this.DeleteSelection();
			this.selectPos = (this.pos = iAltCursorPos);
			this.ClearCursorPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00014440 File Offset: 0x00012640
		public void MoveRight()
		{
			this.ClearCursorPos();
			if (this.selectPos == this.pos)
			{
				this.pos++;
				this.ClampPos();
				this.selectPos = this.pos;
			}
			else if (this.selectPos > this.pos)
			{
				this.pos = this.selectPos;
			}
			else
			{
				this.selectPos = this.pos;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000144C0 File Offset: 0x000126C0
		public void MoveLeft()
		{
			if (this.selectPos == this.pos)
			{
				this.pos--;
				if (this.pos < 0)
				{
					this.pos = 0;
				}
				this.selectPos = this.pos;
			}
			else if (this.selectPos > this.pos)
			{
				this.selectPos = this.pos;
			}
			else
			{
				this.pos = this.selectPos;
			}
			this.ClearCursorPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001454C File Offset: 0x0001274C
		public void MoveUp()
		{
			if (this.selectPos < this.pos)
			{
				this.selectPos = this.pos;
			}
			else
			{
				this.pos = this.selectPos;
			}
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y - 1f;
			this.pos = (this.selectPos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos));
			if (this.pos <= 0)
			{
				this.ClearCursorPos();
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000145E8 File Offset: 0x000127E8
		public void MoveDown()
		{
			if (this.selectPos > this.pos)
			{
				this.selectPos = this.pos;
			}
			else
			{
				this.pos = this.selectPos;
			}
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y + (this.style.lineHeight + 5f);
			this.pos = (this.selectPos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos));
			if (this.pos == this.content.text.Length)
			{
				this.ClearCursorPos();
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000146A0 File Offset: 0x000128A0
		public void MoveLineStart()
		{
			int num = ((this.selectPos >= this.pos) ? this.pos : this.selectPos);
			int num2 = num;
			while (num2-- != 0)
			{
				if (this.content.text[num2] == '\n')
				{
					this.selectPos = (this.pos = num2 + 1);
					return;
				}
			}
			this.selectPos = (this.pos = 0);
			this.UpdateScrollOffset();
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00014724 File Offset: 0x00012924
		public void MoveLineEnd()
		{
			int num = ((this.selectPos <= this.pos) ? this.pos : this.selectPos);
			int i = num;
			int length = this.content.text.Length;
			while (i < length)
			{
				if (this.content.text[i] == '\n')
				{
					this.selectPos = (this.pos = i);
					return;
				}
				i++;
			}
			this.selectPos = (this.pos = length);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000147B8 File Offset: 0x000129B8
		public void MoveGraphicalLineStart()
		{
			this.pos = (this.selectPos = this.GetGraphicalLineStart((this.pos >= this.selectPos) ? this.selectPos : this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00014804 File Offset: 0x00012A04
		public void MoveGraphicalLineEnd()
		{
			this.pos = (this.selectPos = this.GetGraphicalLineEnd((this.pos <= this.selectPos) ? this.selectPos : this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00014850 File Offset: 0x00012A50
		public void MoveTextStart()
		{
			this.selectPos = (this.pos = 0);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00014874 File Offset: 0x00012A74
		public void MoveTextEnd()
		{
			this.selectPos = (this.pos = this.content.text.Length);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000148A8 File Offset: 0x00012AA8
		public void MoveParagraphForward()
		{
			this.pos = ((this.pos <= this.selectPos) ? this.selectPos : this.pos);
			if (this.pos < this.content.text.Length)
			{
				this.selectPos = (this.pos = this.content.text.IndexOf('\n', this.pos + 1));
				if (this.pos == -1)
				{
					this.selectPos = (this.pos = this.content.text.Length);
				}
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00014954 File Offset: 0x00012B54
		public void MoveParagraphBackward()
		{
			this.pos = ((this.pos >= this.selectPos) ? this.selectPos : this.pos);
			if (this.pos > 1)
			{
				this.selectPos = (this.pos = this.content.text.LastIndexOf('\n', this.pos - 2) + 1);
			}
			else
			{
				this.selectPos = (this.pos = 0);
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x000149DC File Offset: 0x00012BDC
		public void MoveCursorToPosition(Vector2 cursorPosition)
		{
			this.selectPos = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			if (!Event.current.shift)
			{
				this.pos = this.selectPos;
			}
			this.ClampPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00014A3C File Offset: 0x00012C3C
		public void MoveAltCursorToPosition(Vector2 cursorPosition)
		{
			this.m_iAltCursorPos = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			this.ClampPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00014A74 File Offset: 0x00012C74
		public bool IsOverSelection(Vector2 cursorPosition)
		{
			int cursorStringIndex = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			return cursorStringIndex < Mathf.Max(this.pos, this.selectPos) && cursorStringIndex > Mathf.Min(this.pos, this.selectPos);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00014AD4 File Offset: 0x00012CD4
		public void SelectToPosition(Vector2 cursorPosition)
		{
			if (!this.m_MouseDragSelectsWholeWords)
			{
				this.pos = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
			}
			else
			{
				int num = this.style.GetCursorStringIndex(this.position, this.content, cursorPosition + this.scrollOffset);
				if (this.m_DblClickSnap == TextEditor.DblClickSnapping.WORDS)
				{
					if (num < this.m_DblClickInitPos)
					{
						this.pos = this.FindEndOfClassification(num, -1);
						this.selectPos = this.FindEndOfClassification(this.m_DblClickInitPos, 1);
					}
					else
					{
						if (num >= this.content.text.Length)
						{
							num = this.content.text.Length - 1;
						}
						this.pos = this.FindEndOfClassification(num, 1);
						this.selectPos = this.FindEndOfClassification(this.m_DblClickInitPos - 1, -1);
					}
				}
				else if (num < this.m_DblClickInitPos)
				{
					if (num > 0)
					{
						this.pos = this.content.text.LastIndexOf('\n', num - 2) + 1;
					}
					else
					{
						this.pos = 0;
					}
					this.selectPos = this.content.text.LastIndexOf('\n', this.m_DblClickInitPos);
				}
				else
				{
					if (num < this.content.text.Length)
					{
						this.pos = this.content.text.IndexOf('\n', num + 1) + 1;
						if (this.pos <= 0)
						{
							this.pos = this.content.text.Length;
						}
					}
					else
					{
						this.pos = this.content.text.Length;
					}
					this.selectPos = this.content.text.LastIndexOf('\n', this.m_DblClickInitPos - 2) + 1;
				}
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00014CC4 File Offset: 0x00012EC4
		public void SelectLeft()
		{
			if (this.m_bJustSelected && this.pos > this.selectPos)
			{
				int num = this.pos;
				this.pos = this.selectPos;
				this.selectPos = num;
			}
			this.m_bJustSelected = false;
			this.pos--;
			if (this.pos < 0)
			{
				this.pos = 0;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00014D38 File Offset: 0x00012F38
		public void SelectRight()
		{
			if (this.m_bJustSelected && this.pos < this.selectPos)
			{
				int num = this.pos;
				this.pos = this.selectPos;
				this.selectPos = num;
			}
			this.m_bJustSelected = false;
			this.pos++;
			int length = this.content.text.Length;
			if (this.pos > length)
			{
				this.pos = length;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00014DBC File Offset: 0x00012FBC
		public void SelectUp()
		{
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y - 1f;
			this.pos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos);
			this.UpdateScrollOffset();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00014E10 File Offset: 0x00013010
		public void SelectDown()
		{
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y + (this.style.lineHeight + 5f);
			this.pos = this.style.GetCursorStringIndex(this.position, this.content, this.graphicalCursorPos);
			this.UpdateScrollOffset();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00014E70 File Offset: 0x00013070
		public void SelectTextEnd()
		{
			this.pos = this.content.text.Length;
			this.UpdateScrollOffset();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00014E90 File Offset: 0x00013090
		public void SelectTextStart()
		{
			this.pos = 0;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00014EA0 File Offset: 0x000130A0
		public void MouseDragSelectsWholeWords(bool on)
		{
			this.m_MouseDragSelectsWholeWords = on;
			this.m_DblClickInitPos = this.pos;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00014EB8 File Offset: 0x000130B8
		public void DblClickSnap(TextEditor.DblClickSnapping snapping)
		{
			this.m_DblClickSnap = snapping;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00014EC4 File Offset: 0x000130C4
		private int GetGraphicalLineStart(int p)
		{
			Vector2 cursorPixelPosition = this.style.GetCursorPixelPosition(this.position, this.content, p);
			cursorPixelPosition.x = 0f;
			return this.style.GetCursorStringIndex(this.position, this.content, cursorPixelPosition);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00014F10 File Offset: 0x00013110
		private int GetGraphicalLineEnd(int p)
		{
			Vector2 cursorPixelPosition = this.style.GetCursorPixelPosition(this.position, this.content, p);
			cursorPixelPosition.x += 5000f;
			return this.style.GetCursorStringIndex(this.position, this.content, cursorPixelPosition);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00014F64 File Offset: 0x00013164
		private int FindNextSeperator(int startPos)
		{
			int length = this.content.text.Length;
			while (startPos < length && !TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos++;
			}
			while (startPos < length && TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos++;
			}
			return startPos;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00014FDC File Offset: 0x000131DC
		private static bool isLetterLikeChar(char c)
		{
			return char.IsLetterOrDigit(c) || c == '\'';
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00014FF4 File Offset: 0x000131F4
		private int FindPrevSeperator(int startPos)
		{
			startPos--;
			while (startPos > 0 && !TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos--;
			}
			while (startPos >= 0 && TextEditor.isLetterLikeChar(this.content.text[startPos]))
			{
				startPos--;
			}
			return startPos + 1;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00015064 File Offset: 0x00013264
		public void MoveWordRight()
		{
			this.pos = ((this.pos <= this.selectPos) ? this.selectPos : this.pos);
			this.pos = (this.selectPos = this.FindNextSeperator(this.pos));
			this.ClearCursorPos();
			this.UpdateScrollOffset();
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000150C0 File Offset: 0x000132C0
		public void MoveToStartOfNextWord()
		{
			this.ClearCursorPos();
			if (this.pos != this.selectPos)
			{
				this.MoveRight();
				return;
			}
			this.pos = (this.selectPos = this.FindStartOfNextWord(this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001510C File Offset: 0x0001330C
		public void MoveToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			if (this.pos != this.selectPos)
			{
				this.MoveLeft();
				return;
			}
			this.pos = (this.selectPos = this.FindEndOfPreviousWord(this.pos));
			this.UpdateScrollOffset();
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00015158 File Offset: 0x00013358
		public void SelectToStartOfNextWord()
		{
			this.ClearCursorPos();
			this.pos = this.FindStartOfNextWord(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00015178 File Offset: 0x00013378
		public void SelectToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			this.pos = this.FindEndOfPreviousWord(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00015198 File Offset: 0x00013398
		private TextEditor.CharacterType ClassifyChar(char c)
		{
			if (char.IsWhiteSpace(c))
			{
				return TextEditor.CharacterType.WhiteSpace;
			}
			if (char.IsLetterOrDigit(c) || c == '\'')
			{
				return TextEditor.CharacterType.LetterLike;
			}
			return TextEditor.CharacterType.Symbol;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000151C0 File Offset: 0x000133C0
		public int FindStartOfNextWord(int p)
		{
			int length = this.content.text.Length;
			if (p == length)
			{
				return p;
			}
			char c = this.content.text[p];
			TextEditor.CharacterType characterType = this.ClassifyChar(c);
			if (characterType != TextEditor.CharacterType.WhiteSpace)
			{
				p++;
				while (p < length && this.ClassifyChar(this.content.text[p]) == characterType)
				{
					p++;
				}
			}
			else if (c == '\t' || c == '\n')
			{
				return p + 1;
			}
			if (p == length)
			{
				return p;
			}
			c = this.content.text[p];
			if (c == ' ')
			{
				while (p < length && char.IsWhiteSpace(this.content.text[p]))
				{
					p++;
				}
			}
			else if (c == '\t' || c == '\n')
			{
				return p;
			}
			return p;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000152BC File Offset: 0x000134BC
		private int FindEndOfPreviousWord(int p)
		{
			if (p == 0)
			{
				return p;
			}
			p--;
			while (p > 0 && this.content.text[p] == ' ')
			{
				p--;
			}
			TextEditor.CharacterType characterType = this.ClassifyChar(this.content.text[p]);
			if (characterType != TextEditor.CharacterType.WhiteSpace)
			{
				while (p > 0 && this.ClassifyChar(this.content.text[p - 1]) == characterType)
				{
					p--;
				}
			}
			return p;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00015350 File Offset: 0x00013550
		public void MoveWordLeft()
		{
			this.pos = ((this.pos >= this.selectPos) ? this.selectPos : this.pos);
			this.pos = this.FindPrevSeperator(this.pos);
			this.selectPos = this.pos;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000153AC File Offset: 0x000135AC
		public void SelectWordRight()
		{
			this.ClearCursorPos();
			int num = this.selectPos;
			if (this.pos < this.selectPos)
			{
				this.selectPos = this.pos;
				this.MoveWordRight();
				this.selectPos = num;
				this.pos = ((this.pos >= this.selectPos) ? this.selectPos : this.pos);
				return;
			}
			this.selectPos = this.pos;
			this.MoveWordRight();
			this.selectPos = num;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00015438 File Offset: 0x00013638
		public void SelectWordLeft()
		{
			this.ClearCursorPos();
			int num = this.selectPos;
			if (this.pos > this.selectPos)
			{
				this.selectPos = this.pos;
				this.MoveWordLeft();
				this.selectPos = num;
				this.pos = ((this.pos <= this.selectPos) ? this.selectPos : this.pos);
				return;
			}
			this.selectPos = this.pos;
			this.MoveWordLeft();
			this.selectPos = num;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000154C4 File Offset: 0x000136C4
		public void ExpandSelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			if (this.pos < this.selectPos)
			{
				this.pos = this.GetGraphicalLineStart(this.pos);
			}
			else
			{
				int num = this.pos;
				this.pos = this.GetGraphicalLineStart(this.selectPos);
				this.selectPos = num;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00015528 File Offset: 0x00013728
		public void ExpandSelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			if (this.pos > this.selectPos)
			{
				this.pos = this.GetGraphicalLineEnd(this.pos);
			}
			else
			{
				int num = this.pos;
				this.pos = this.GetGraphicalLineEnd(this.selectPos);
				this.selectPos = num;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001558C File Offset: 0x0001378C
		public void SelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			this.pos = this.GetGraphicalLineStart(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000155AC File Offset: 0x000137AC
		public void SelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			this.pos = this.GetGraphicalLineEnd(this.pos);
			this.UpdateScrollOffset();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000155CC File Offset: 0x000137CC
		public void SelectParagraphForward()
		{
			this.ClearCursorPos();
			bool flag = this.pos < this.selectPos;
			if (this.pos < this.content.text.Length)
			{
				this.pos = this.content.text.IndexOf('\n', this.pos + 1);
				if (this.pos == -1)
				{
					this.pos = this.content.text.Length;
				}
				if (flag && this.pos > this.selectPos)
				{
					this.pos = this.selectPos;
				}
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00015674 File Offset: 0x00013874
		public void SelectParagraphBackward()
		{
			this.ClearCursorPos();
			bool flag = this.pos > this.selectPos;
			if (this.pos > 1)
			{
				this.pos = this.content.text.LastIndexOf('\n', this.pos - 2) + 1;
				if (flag && this.pos < this.selectPos)
				{
					this.pos = this.selectPos;
				}
			}
			else
			{
				this.selectPos = (this.pos = 0);
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00015704 File Offset: 0x00013904
		public void SelectCurrentWord()
		{
			this.ClearCursorPos();
			int length = this.content.text.Length;
			this.selectPos = this.pos;
			if (length == 0)
			{
				return;
			}
			if (this.pos >= length)
			{
				this.pos = length - 1;
			}
			if (this.selectPos >= length)
			{
				this.selectPos--;
			}
			if (this.pos < this.selectPos)
			{
				this.pos = this.FindEndOfClassification(this.pos, -1);
				this.selectPos = this.FindEndOfClassification(this.selectPos, 1);
			}
			else
			{
				this.pos = this.FindEndOfClassification(this.pos, 1);
				this.selectPos = this.FindEndOfClassification(this.selectPos, -1);
			}
			this.m_bJustSelected = true;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000157DC File Offset: 0x000139DC
		private int FindEndOfClassification(int p, int dir)
		{
			int length = this.content.text.Length;
			if (p >= length || p < 0)
			{
				return p;
			}
			TextEditor.CharacterType characterType = this.ClassifyChar(this.content.text[p]);
			for (;;)
			{
				p += dir;
				if (p < 0)
				{
					break;
				}
				if (p >= length)
				{
					return length;
				}
				if (this.ClassifyChar(this.content.text[p]) != characterType)
				{
					goto Block_4;
				}
			}
			return 0;
			Block_4:
			if (dir == 1)
			{
				return p;
			}
			return p + 1;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00015864 File Offset: 0x00013A64
		public void SelectCurrentParagraph()
		{
			this.ClearCursorPos();
			int length = this.content.text.Length;
			if (this.pos < length)
			{
				this.pos = this.content.text.IndexOf('\n', this.pos);
				if (this.pos == -1)
				{
					this.pos = this.content.text.Length;
				}
				else
				{
					this.pos++;
				}
			}
			if (this.selectPos != 0)
			{
				this.selectPos = this.content.text.LastIndexOf('\n', this.selectPos - 1) + 1;
			}
			this.UpdateScrollOffset();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001591C File Offset: 0x00013B1C
		private void UpdateScrollOffset()
		{
			int num = this.pos;
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.content, num);
			Rect rect = this.style.padding.Remove(this.position);
			Vector2 vector = new Vector2(this.style.CalcSize(this.content).x, this.style.CalcHeight(this.content, this.position.width));
			if (vector.x < this.position.width)
			{
				this.scrollOffset.x = 0f;
			}
			else
			{
				if (this.graphicalCursorPos.x + 1f > this.scrollOffset.x + rect.width)
				{
					this.scrollOffset.x = this.graphicalCursorPos.x - rect.width;
				}
				if (this.graphicalCursorPos.x < this.scrollOffset.x + (float)this.style.padding.left)
				{
					this.scrollOffset.x = this.graphicalCursorPos.x - (float)this.style.padding.left;
				}
			}
			if (vector.y < rect.height)
			{
				this.scrollOffset.y = 0f;
			}
			else
			{
				if (this.graphicalCursorPos.y + this.style.lineHeight > this.scrollOffset.y + rect.height + (float)this.style.padding.top)
				{
					this.scrollOffset.y = this.graphicalCursorPos.y - rect.height - (float)this.style.padding.top + this.style.lineHeight;
				}
				if (this.graphicalCursorPos.y < this.scrollOffset.y + (float)this.style.padding.top)
				{
					this.scrollOffset.y = this.graphicalCursorPos.y - (float)this.style.padding.top;
				}
			}
			if (this.scrollOffset.y > 0f && vector.y - this.scrollOffset.y < rect.height)
			{
				this.scrollOffset.y = vector.y - rect.height - (float)this.style.padding.top - (float)this.style.padding.bottom;
			}
			this.scrollOffset.y = ((this.scrollOffset.y >= 0f) ? this.scrollOffset.y : 0f);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00015C2C File Offset: 0x00013E2C
		public void DrawCursor(string text)
		{
			string text2 = this.content.text;
			int num = this.pos;
			if (Input.compositionString.Length > 0)
			{
				this.content.text = text.Substring(0, this.pos) + Input.compositionString + text.Substring(this.selectPos);
				num += Input.compositionString.Length;
			}
			else
			{
				this.content.text = text;
			}
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.content, num);
			Vector2 contentOffset = this.style.contentOffset;
			this.style.contentOffset -= this.scrollOffset;
			this.style.Internal_clipOffset = this.scrollOffset;
			Input.compositionCursorPos = this.graphicalCursorPos + new Vector2(this.position.x, this.position.y + this.style.lineHeight) - this.scrollOffset;
			if (Input.compositionString.Length > 0)
			{
				this.style.DrawWithTextSelection(this.position, this.content, this.controlID, this.pos, this.pos + Input.compositionString.Length, true);
			}
			else
			{
				this.style.DrawWithTextSelection(this.position, this.content, this.controlID, this.pos, this.selectPos);
			}
			if (this.m_iAltCursorPos != -1)
			{
				this.style.DrawCursor(this.position, this.content, this.controlID, this.m_iAltCursorPos);
			}
			this.style.contentOffset = contentOffset;
			this.style.Internal_clipOffset = Vector2.zero;
			this.content.text = text2;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00015E2C File Offset: 0x0001402C
		private bool PerformOperation(TextEditor.TextEditOp operation)
		{
			switch (operation)
			{
			case TextEditor.TextEditOp.MoveLeft:
				this.MoveLeft();
				return false;
			case TextEditor.TextEditOp.MoveRight:
				this.MoveRight();
				return false;
			case TextEditor.TextEditOp.MoveUp:
				this.MoveUp();
				return false;
			case TextEditor.TextEditOp.MoveDown:
				this.MoveDown();
				return false;
			case TextEditor.TextEditOp.MoveLineStart:
				this.MoveLineStart();
				return false;
			case TextEditor.TextEditOp.MoveLineEnd:
				this.MoveLineEnd();
				return false;
			case TextEditor.TextEditOp.MoveTextStart:
				this.MoveTextStart();
				return false;
			case TextEditor.TextEditOp.MoveTextEnd:
				this.MoveTextEnd();
				return false;
			case TextEditor.TextEditOp.MoveGraphicalLineStart:
				this.MoveGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.MoveGraphicalLineEnd:
				this.MoveGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.MoveWordLeft:
				this.MoveWordLeft();
				return false;
			case TextEditor.TextEditOp.MoveWordRight:
				this.MoveWordRight();
				return false;
			case TextEditor.TextEditOp.MoveParagraphForward:
				this.MoveParagraphForward();
				return false;
			case TextEditor.TextEditOp.MoveParagraphBackward:
				this.MoveParagraphBackward();
				return false;
			case TextEditor.TextEditOp.MoveToStartOfNextWord:
				this.MoveToStartOfNextWord();
				return false;
			case TextEditor.TextEditOp.MoveToEndOfPreviousWord:
				this.MoveToEndOfPreviousWord();
				return false;
			case TextEditor.TextEditOp.SelectLeft:
				this.SelectLeft();
				return false;
			case TextEditor.TextEditOp.SelectRight:
				this.SelectRight();
				return false;
			case TextEditor.TextEditOp.SelectUp:
				this.SelectUp();
				return false;
			case TextEditor.TextEditOp.SelectDown:
				this.SelectDown();
				return false;
			case TextEditor.TextEditOp.SelectTextStart:
				this.SelectTextStart();
				return false;
			case TextEditor.TextEditOp.SelectTextEnd:
				this.SelectTextEnd();
				return false;
			case TextEditor.TextEditOp.ExpandSelectGraphicalLineStart:
				this.ExpandSelectGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd:
				this.ExpandSelectGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.SelectGraphicalLineStart:
				this.SelectGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.SelectGraphicalLineEnd:
				this.SelectGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.SelectWordLeft:
				this.SelectWordLeft();
				return false;
			case TextEditor.TextEditOp.SelectWordRight:
				this.SelectWordRight();
				return false;
			case TextEditor.TextEditOp.SelectToEndOfPreviousWord:
				this.SelectToEndOfPreviousWord();
				return false;
			case TextEditor.TextEditOp.SelectToStartOfNextWord:
				this.SelectToStartOfNextWord();
				return false;
			case TextEditor.TextEditOp.SelectParagraphBackward:
				this.SelectParagraphBackward();
				return false;
			case TextEditor.TextEditOp.SelectParagraphForward:
				this.SelectParagraphForward();
				return false;
			case TextEditor.TextEditOp.Delete:
				return this.Delete();
			case TextEditor.TextEditOp.Backspace:
				return this.Backspace();
			case TextEditor.TextEditOp.DeleteWordBack:
				return this.DeleteWordBack();
			case TextEditor.TextEditOp.DeleteWordForward:
				return this.DeleteWordForward();
			case TextEditor.TextEditOp.DeleteLineBack:
				return this.DeleteLineBack();
			case TextEditor.TextEditOp.Cut:
				return this.Cut();
			case TextEditor.TextEditOp.Copy:
				this.Copy();
				return false;
			case TextEditor.TextEditOp.Paste:
				return this.Paste();
			case TextEditor.TextEditOp.SelectAll:
				this.SelectAll();
				return false;
			case TextEditor.TextEditOp.SelectNone:
				this.SelectNone();
				return false;
			}
			Debug.Log("Unimplemented: " + operation);
			return false;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x000160CC File Offset: 0x000142CC
		public void SaveBackup()
		{
			this.oldText = this.content.text;
			this.oldPos = this.pos;
			this.oldSelectPos = this.selectPos;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000160F8 File Offset: 0x000142F8
		public void Undo()
		{
			this.content.text = this.oldText;
			this.pos = this.oldPos;
			this.selectPos = this.oldSelectPos;
			this.UpdateScrollOffset();
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001612C File Offset: 0x0001432C
		public bool Cut()
		{
			if (this.isPasswordField)
			{
				return false;
			}
			this.Copy();
			return this.DeleteSelection();
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00016148 File Offset: 0x00014348
		public void Copy()
		{
			if (this.selectPos == this.pos)
			{
				return;
			}
			if (this.isPasswordField)
			{
				return;
			}
			string text;
			if (this.pos < this.selectPos)
			{
				text = this.content.text.Substring(this.pos, this.selectPos - this.pos);
			}
			else
			{
				text = this.content.text.Substring(this.selectPos, this.pos - this.selectPos);
			}
			GUIUtility.systemCopyBuffer = text;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000161D8 File Offset: 0x000143D8
		public bool Paste()
		{
			string systemCopyBuffer = GUIUtility.systemCopyBuffer;
			if (systemCopyBuffer != string.Empty)
			{
				this.ReplaceSelection(systemCopyBuffer);
				return true;
			}
			return false;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00016208 File Offset: 0x00014408
		private static void MapKey(string key, TextEditor.TextEditOp action)
		{
			TextEditor.s_Keyactions[Event.KeyboardEvent(key)] = action;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001621C File Offset: 0x0001441C
		private void InitKeyActions()
		{
			if (TextEditor.s_Keyactions != null)
			{
				return;
			}
			TextEditor.s_Keyactions = new Dictionary<Event, TextEditor.TextEditOp>();
			TextEditor.MapKey("left", TextEditor.TextEditOp.MoveLeft);
			TextEditor.MapKey("right", TextEditor.TextEditOp.MoveRight);
			TextEditor.MapKey("up", TextEditor.TextEditOp.MoveUp);
			TextEditor.MapKey("down", TextEditor.TextEditOp.MoveDown);
			TextEditor.MapKey("#left", TextEditor.TextEditOp.SelectLeft);
			TextEditor.MapKey("#right", TextEditor.TextEditOp.SelectRight);
			TextEditor.MapKey("#up", TextEditor.TextEditOp.SelectUp);
			TextEditor.MapKey("#down", TextEditor.TextEditOp.SelectDown);
			TextEditor.MapKey("delete", TextEditor.TextEditOp.Delete);
			TextEditor.MapKey("backspace", TextEditor.TextEditOp.Backspace);
			TextEditor.MapKey("#backspace", TextEditor.TextEditOp.Backspace);
			if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.OSXDashboardPlayer || Application.platform == RuntimePlatform.OSXEditor)
			{
				TextEditor.MapKey("^left", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("^right", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("&left", TextEditor.TextEditOp.MoveWordLeft);
				TextEditor.MapKey("&right", TextEditor.TextEditOp.MoveWordRight);
				TextEditor.MapKey("&up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("&down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("%left", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("%right", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("%up", TextEditor.TextEditOp.MoveTextStart);
				TextEditor.MapKey("%down", TextEditor.TextEditOp.MoveTextEnd);
				TextEditor.MapKey("#home", TextEditor.TextEditOp.SelectTextStart);
				TextEditor.MapKey("#end", TextEditor.TextEditOp.SelectTextEnd);
				TextEditor.MapKey("#^left", TextEditor.TextEditOp.ExpandSelectGraphicalLineStart);
				TextEditor.MapKey("#^right", TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd);
				TextEditor.MapKey("#^up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#^down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#&left", TextEditor.TextEditOp.SelectWordLeft);
				TextEditor.MapKey("#&right", TextEditor.TextEditOp.SelectWordRight);
				TextEditor.MapKey("#&up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#&down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#%left", TextEditor.TextEditOp.ExpandSelectGraphicalLineStart);
				TextEditor.MapKey("#%right", TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd);
				TextEditor.MapKey("#%up", TextEditor.TextEditOp.SelectTextStart);
				TextEditor.MapKey("#%down", TextEditor.TextEditOp.SelectTextEnd);
				TextEditor.MapKey("%a", TextEditor.TextEditOp.SelectAll);
				TextEditor.MapKey("%x", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("%c", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("%v", TextEditor.TextEditOp.Paste);
				TextEditor.MapKey("^d", TextEditor.TextEditOp.Delete);
				TextEditor.MapKey("^h", TextEditor.TextEditOp.Backspace);
				TextEditor.MapKey("^b", TextEditor.TextEditOp.MoveLeft);
				TextEditor.MapKey("^f", TextEditor.TextEditOp.MoveRight);
				TextEditor.MapKey("^a", TextEditor.TextEditOp.MoveLineStart);
				TextEditor.MapKey("^e", TextEditor.TextEditOp.MoveLineEnd);
				TextEditor.MapKey("&delete", TextEditor.TextEditOp.DeleteWordForward);
				TextEditor.MapKey("&backspace", TextEditor.TextEditOp.DeleteWordBack);
				TextEditor.MapKey("%backspace", TextEditor.TextEditOp.DeleteLineBack);
			}
			else
			{
				TextEditor.MapKey("home", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("end", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("%left", TextEditor.TextEditOp.MoveWordLeft);
				TextEditor.MapKey("%right", TextEditor.TextEditOp.MoveWordRight);
				TextEditor.MapKey("%up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("%down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("^left", TextEditor.TextEditOp.MoveToEndOfPreviousWord);
				TextEditor.MapKey("^right", TextEditor.TextEditOp.MoveToStartOfNextWord);
				TextEditor.MapKey("^up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("^down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("#^left", TextEditor.TextEditOp.SelectToEndOfPreviousWord);
				TextEditor.MapKey("#^right", TextEditor.TextEditOp.SelectToStartOfNextWord);
				TextEditor.MapKey("#^up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#^down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#home", TextEditor.TextEditOp.SelectGraphicalLineStart);
				TextEditor.MapKey("#end", TextEditor.TextEditOp.SelectGraphicalLineEnd);
				TextEditor.MapKey("^delete", TextEditor.TextEditOp.DeleteWordForward);
				TextEditor.MapKey("^backspace", TextEditor.TextEditOp.DeleteWordBack);
				TextEditor.MapKey("%backspace", TextEditor.TextEditOp.DeleteLineBack);
				TextEditor.MapKey("^a", TextEditor.TextEditOp.SelectAll);
				TextEditor.MapKey("^x", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("^c", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("^v", TextEditor.TextEditOp.Paste);
				TextEditor.MapKey("#delete", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("^insert", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("#insert", TextEditor.TextEditOp.Paste);
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000165DC File Offset: 0x000147DC
		public void ClampPos()
		{
			if (this.m_HasFocus && this.controlID != GUIUtility.keyboardControl)
			{
				this.OnLostFocus();
			}
			if (!this.m_HasFocus && this.controlID == GUIUtility.keyboardControl)
			{
				this.OnFocus();
			}
			if (this.pos < 0)
			{
				this.pos = 0;
			}
			else if (this.pos > this.content.text.Length)
			{
				this.pos = this.content.text.Length;
			}
			if (this.selectPos < 0)
			{
				this.selectPos = 0;
			}
			else if (this.selectPos > this.content.text.Length)
			{
				this.selectPos = this.content.text.Length;
			}
			if (this.m_iAltCursorPos > this.content.text.Length)
			{
				this.m_iAltCursorPos = this.content.text.Length;
			}
		}

		// Token: 0x040003EC RID: 1004
		public TouchScreenKeyboard keyboardOnScreen;

		// Token: 0x040003ED RID: 1005
		public int pos;

		// Token: 0x040003EE RID: 1006
		public int selectPos;

		// Token: 0x040003EF RID: 1007
		public int controlID;

		// Token: 0x040003F0 RID: 1008
		public GUIContent content = new GUIContent();

		// Token: 0x040003F1 RID: 1009
		public GUIStyle style = GUIStyle.none;

		// Token: 0x040003F2 RID: 1010
		public Rect position;

		// Token: 0x040003F3 RID: 1011
		public bool multiline;

		// Token: 0x040003F4 RID: 1012
		public bool hasHorizontalCursorPos;

		// Token: 0x040003F5 RID: 1013
		public bool isPasswordField;

		// Token: 0x040003F6 RID: 1014
		internal bool m_HasFocus;

		// Token: 0x040003F7 RID: 1015
		public Vector2 scrollOffset = Vector2.zero;

		// Token: 0x040003F8 RID: 1016
		public Vector2 graphicalCursorPos;

		// Token: 0x040003F9 RID: 1017
		public Vector2 graphicalSelectCursorPos;

		// Token: 0x040003FA RID: 1018
		private bool m_MouseDragSelectsWholeWords;

		// Token: 0x040003FB RID: 1019
		private int m_DblClickInitPos;

		// Token: 0x040003FC RID: 1020
		private TextEditor.DblClickSnapping m_DblClickSnap;

		// Token: 0x040003FD RID: 1021
		private bool m_bJustSelected;

		// Token: 0x040003FE RID: 1022
		private int m_iAltCursorPos = -1;

		// Token: 0x040003FF RID: 1023
		private string oldText;

		// Token: 0x04000400 RID: 1024
		private int oldPos;

		// Token: 0x04000401 RID: 1025
		private int oldSelectPos;

		// Token: 0x04000402 RID: 1026
		private static Dictionary<Event, TextEditor.TextEditOp> s_Keyactions;

		// Token: 0x020000FC RID: 252
		private enum CharacterType
		{
			// Token: 0x04000404 RID: 1028
			LetterLike,
			// Token: 0x04000405 RID: 1029
			Symbol,
			// Token: 0x04000406 RID: 1030
			Symbol2,
			// Token: 0x04000407 RID: 1031
			WhiteSpace
		}

		// Token: 0x020000FD RID: 253
		public enum DblClickSnapping : byte
		{
			// Token: 0x04000409 RID: 1033
			WORDS,
			// Token: 0x0400040A RID: 1034
			PARAGRAPHS
		}

		// Token: 0x020000FE RID: 254
		private enum TextEditOp
		{
			// Token: 0x0400040C RID: 1036
			MoveLeft,
			// Token: 0x0400040D RID: 1037
			MoveRight,
			// Token: 0x0400040E RID: 1038
			MoveUp,
			// Token: 0x0400040F RID: 1039
			MoveDown,
			// Token: 0x04000410 RID: 1040
			MoveLineStart,
			// Token: 0x04000411 RID: 1041
			MoveLineEnd,
			// Token: 0x04000412 RID: 1042
			MoveTextStart,
			// Token: 0x04000413 RID: 1043
			MoveTextEnd,
			// Token: 0x04000414 RID: 1044
			MovePageUp,
			// Token: 0x04000415 RID: 1045
			MovePageDown,
			// Token: 0x04000416 RID: 1046
			MoveGraphicalLineStart,
			// Token: 0x04000417 RID: 1047
			MoveGraphicalLineEnd,
			// Token: 0x04000418 RID: 1048
			MoveWordLeft,
			// Token: 0x04000419 RID: 1049
			MoveWordRight,
			// Token: 0x0400041A RID: 1050
			MoveParagraphForward,
			// Token: 0x0400041B RID: 1051
			MoveParagraphBackward,
			// Token: 0x0400041C RID: 1052
			MoveToStartOfNextWord,
			// Token: 0x0400041D RID: 1053
			MoveToEndOfPreviousWord,
			// Token: 0x0400041E RID: 1054
			SelectLeft,
			// Token: 0x0400041F RID: 1055
			SelectRight,
			// Token: 0x04000420 RID: 1056
			SelectUp,
			// Token: 0x04000421 RID: 1057
			SelectDown,
			// Token: 0x04000422 RID: 1058
			SelectTextStart,
			// Token: 0x04000423 RID: 1059
			SelectTextEnd,
			// Token: 0x04000424 RID: 1060
			SelectPageUp,
			// Token: 0x04000425 RID: 1061
			SelectPageDown,
			// Token: 0x04000426 RID: 1062
			ExpandSelectGraphicalLineStart,
			// Token: 0x04000427 RID: 1063
			ExpandSelectGraphicalLineEnd,
			// Token: 0x04000428 RID: 1064
			SelectGraphicalLineStart,
			// Token: 0x04000429 RID: 1065
			SelectGraphicalLineEnd,
			// Token: 0x0400042A RID: 1066
			SelectWordLeft,
			// Token: 0x0400042B RID: 1067
			SelectWordRight,
			// Token: 0x0400042C RID: 1068
			SelectToEndOfPreviousWord,
			// Token: 0x0400042D RID: 1069
			SelectToStartOfNextWord,
			// Token: 0x0400042E RID: 1070
			SelectParagraphBackward,
			// Token: 0x0400042F RID: 1071
			SelectParagraphForward,
			// Token: 0x04000430 RID: 1072
			Delete,
			// Token: 0x04000431 RID: 1073
			Backspace,
			// Token: 0x04000432 RID: 1074
			DeleteWordBack,
			// Token: 0x04000433 RID: 1075
			DeleteWordForward,
			// Token: 0x04000434 RID: 1076
			DeleteLineBack,
			// Token: 0x04000435 RID: 1077
			Cut,
			// Token: 0x04000436 RID: 1078
			Copy,
			// Token: 0x04000437 RID: 1079
			Paste,
			// Token: 0x04000438 RID: 1080
			SelectAll,
			// Token: 0x04000439 RID: 1081
			SelectNone,
			// Token: 0x0400043A RID: 1082
			ScrollStart,
			// Token: 0x0400043B RID: 1083
			ScrollEnd,
			// Token: 0x0400043C RID: 1084
			ScrollPageUp,
			// Token: 0x0400043D RID: 1085
			ScrollPageDown
		}
	}
}
