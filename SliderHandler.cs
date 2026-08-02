using System;

namespace UnityEngine
{
	// Token: 0x020000DC RID: 220
	internal struct SliderHandler
	{
		// Token: 0x060007D2 RID: 2002 RVA: 0x00012820 File Offset: 0x00010A20
		public SliderHandler(Rect position, float currentValue, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id)
		{
			this.position = position;
			this.currentValue = currentValue;
			this.size = size;
			this.start = start;
			this.end = end;
			this.slider = slider;
			this.thumb = thumb;
			this.horiz = horiz;
			this.id = id;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00012874 File Offset: 0x00010A74
		public float Handle()
		{
			if (this.slider == null || this.thumb == null)
			{
				return this.currentValue;
			}
			switch (this.CurrentEventType())
			{
			case EventType.MouseDown:
				return this.OnMouseDown();
			case EventType.MouseUp:
				return this.OnMouseUp();
			case EventType.MouseDrag:
				return this.OnMouseDrag();
			case EventType.Repaint:
				return this.OnRepaint();
			}
			return this.currentValue;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x000128F4 File Offset: 0x00010AF4
		private float OnMouseDown()
		{
			if (!this.position.Contains(this.CurrentEvent().mousePosition) || this.IsEmptySlider())
			{
				return this.currentValue;
			}
			GUI.scrollTroughSide = 0;
			GUIUtility.hotControl = this.id;
			this.CurrentEvent().Use();
			if (this.ThumbSelectionRect().Contains(this.CurrentEvent().mousePosition))
			{
				this.StartDraggingWithValue(this.ClampedCurrentValue());
				return this.currentValue;
			}
			GUI.changed = true;
			if (this.SupportsPageMovements())
			{
				this.SliderState().isDragging = false;
				GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(250.0);
				GUI.scrollTroughSide = this.CurrentScrollTroughSide();
				return this.PageMovementValue();
			}
			float num = this.ValueForCurrentMousePosition();
			this.StartDraggingWithValue(num);
			return this.Clamp(num);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000129E0 File Offset: 0x00010BE0
		private float OnMouseDrag()
		{
			if (GUIUtility.hotControl != this.id)
			{
				return this.currentValue;
			}
			SliderState sliderState = this.SliderState();
			if (!sliderState.isDragging)
			{
				return this.currentValue;
			}
			GUI.changed = true;
			this.CurrentEvent().Use();
			float num = this.MousePosition() - sliderState.dragStartPos;
			float num2 = sliderState.dragStartValue + num / this.ValuesPerPixel();
			return this.Clamp(num2);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00012A54 File Offset: 0x00010C54
		private float OnMouseUp()
		{
			if (GUIUtility.hotControl == this.id)
			{
				this.CurrentEvent().Use();
				GUIUtility.hotControl = 0;
			}
			return this.currentValue;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00012A80 File Offset: 0x00010C80
		private float OnRepaint()
		{
			this.slider.Draw(this.position, GUIContent.none, this.id);
			this.thumb.Draw(this.ThumbRect(), GUIContent.none, this.id);
			if (GUIUtility.hotControl != this.id || !this.position.Contains(this.CurrentEvent().mousePosition) || this.IsEmptySlider())
			{
				return this.currentValue;
			}
			if (this.ThumbRect().Contains(this.CurrentEvent().mousePosition))
			{
				if (GUI.scrollTroughSide != 0)
				{
					GUIUtility.hotControl = 0;
				}
				return this.currentValue;
			}
			GUI.InternalRepaintEditorWindow();
			if (SystemClock.now < GUI.nextScrollStepTime)
			{
				return this.currentValue;
			}
			if (this.CurrentScrollTroughSide() != GUI.scrollTroughSide)
			{
				return this.currentValue;
			}
			GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(30.0);
			if (this.SupportsPageMovements())
			{
				this.SliderState().isDragging = false;
				GUI.changed = true;
				return this.PageMovementValue();
			}
			return this.ClampedCurrentValue();
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00012BB8 File Offset: 0x00010DB8
		private EventType CurrentEventType()
		{
			return this.CurrentEvent().GetTypeForControl(this.id);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00012BCC File Offset: 0x00010DCC
		private int CurrentScrollTroughSide()
		{
			float num = ((!this.horiz) ? this.CurrentEvent().mousePosition.y : this.CurrentEvent().mousePosition.x);
			float num2 = ((!this.horiz) ? this.ThumbRect().y : this.ThumbRect().x);
			return (num <= num2) ? (-1) : 1;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00012C50 File Offset: 0x00010E50
		private bool IsEmptySlider()
		{
			return this.start == this.end;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00012C60 File Offset: 0x00010E60
		private bool SupportsPageMovements()
		{
			return this.size != 0f && GUI.usePageScrollbars;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00012C7C File Offset: 0x00010E7C
		private float PageMovementValue()
		{
			float num = this.currentValue;
			int num2 = ((this.start <= this.end) ? 1 : (-1));
			if (this.MousePosition() > this.PageUpMovementBound())
			{
				num += this.size * (float)num2 * 0.9f;
			}
			else
			{
				num -= this.size * (float)num2 * 0.9f;
			}
			return this.Clamp(num);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00012CEC File Offset: 0x00010EEC
		private float PageUpMovementBound()
		{
			if (this.horiz)
			{
				return this.ThumbRect().xMax - this.position.x;
			}
			return this.ThumbRect().yMax - this.position.y;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00012D40 File Offset: 0x00010F40
		private Event CurrentEvent()
		{
			return Event.current;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00012D48 File Offset: 0x00010F48
		private float ValueForCurrentMousePosition()
		{
			if (this.horiz)
			{
				return (this.MousePosition() - this.ThumbRect().width * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			return (this.MousePosition() - this.ThumbRect().height * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00012DD0 File Offset: 0x00010FD0
		private float Clamp(float value)
		{
			return Mathf.Clamp(value, this.MinValue(), this.MaxValue());
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00012DE4 File Offset: 0x00010FE4
		private Rect ThumbSelectionRect()
		{
			return this.ThumbRect();
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00012DFC File Offset: 0x00010FFC
		private void StartDraggingWithValue(float dragStartValue)
		{
			SliderState sliderState = this.SliderState();
			sliderState.dragStartPos = this.MousePosition();
			sliderState.dragStartValue = dragStartValue;
			sliderState.isDragging = true;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00012E2C File Offset: 0x0001102C
		private SliderState SliderState()
		{
			return (SliderState)GUIUtility.GetStateObject(typeof(SliderState), this.id);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00012E48 File Offset: 0x00011048
		private Rect ThumbRect()
		{
			return (!this.horiz) ? this.VerticalThumbRect() : this.HorizontalThumbRect();
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00012E68 File Offset: 0x00011068
		private Rect VerticalThumbRect()
		{
			float num = this.ValuesPerPixel();
			if (this.start < this.end)
			{
				return new Rect(this.position.x + (float)this.slider.padding.left, (this.ClampedCurrentValue() - this.start) * num + this.position.y + (float)this.slider.padding.top, this.position.width - (float)this.slider.padding.horizontal, this.size * num + this.ThumbSize());
			}
			return new Rect(this.position.x + (float)this.slider.padding.left, (this.ClampedCurrentValue() + this.size - this.start) * num + this.position.y + (float)this.slider.padding.top, this.position.width - (float)this.slider.padding.horizontal, this.size * -num + this.ThumbSize());
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00012FA4 File Offset: 0x000111A4
		private Rect HorizontalThumbRect()
		{
			float num = this.ValuesPerPixel();
			if (this.start < this.end)
			{
				return new Rect((this.ClampedCurrentValue() - this.start) * num + this.position.x + (float)this.slider.padding.left, this.position.y + (float)this.slider.padding.top, this.size * num + this.ThumbSize(), this.position.height - (float)this.slider.padding.vertical);
			}
			return new Rect((this.ClampedCurrentValue() + this.size - this.start) * num + this.position.x + (float)this.slider.padding.left, this.position.y, this.size * -num + this.ThumbSize(), this.position.height);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000130BC File Offset: 0x000112BC
		private float ClampedCurrentValue()
		{
			return this.Clamp(this.currentValue);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000130CC File Offset: 0x000112CC
		private float MousePosition()
		{
			if (this.horiz)
			{
				return this.CurrentEvent().mousePosition.x - this.position.x;
			}
			return this.CurrentEvent().mousePosition.y - this.position.y;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001312C File Offset: 0x0001132C
		private float ValuesPerPixel()
		{
			if (this.horiz)
			{
				return (this.position.width - (float)this.slider.padding.horizontal - this.ThumbSize()) / (this.end - this.start);
			}
			return (this.position.height - (float)this.slider.padding.vertical - this.ThumbSize()) / (this.end - this.start);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000131B0 File Offset: 0x000113B0
		private float ThumbSize()
		{
			if (this.horiz)
			{
				return (this.thumb.fixedWidth == 0f) ? ((float)this.thumb.padding.horizontal) : this.thumb.fixedWidth;
			}
			return (this.thumb.fixedHeight == 0f) ? ((float)this.thumb.padding.vertical) : this.thumb.fixedHeight;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00013238 File Offset: 0x00011438
		private float MaxValue()
		{
			return Mathf.Max(this.start, this.end) - this.size;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00013254 File Offset: 0x00011454
		private float MinValue()
		{
			return Mathf.Min(this.start, this.end);
		}

		// Token: 0x0400036C RID: 876
		private readonly Rect position;

		// Token: 0x0400036D RID: 877
		private readonly float currentValue;

		// Token: 0x0400036E RID: 878
		private readonly float size;

		// Token: 0x0400036F RID: 879
		private readonly float start;

		// Token: 0x04000370 RID: 880
		private readonly float end;

		// Token: 0x04000371 RID: 881
		private readonly GUIStyle slider;

		// Token: 0x04000372 RID: 882
		private readonly GUIStyle thumb;

		// Token: 0x04000373 RID: 883
		private readonly bool horiz;

		// Token: 0x04000374 RID: 884
		private readonly int id;
	}
}
