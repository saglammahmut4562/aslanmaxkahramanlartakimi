using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x02000070 RID: 112
	[ExecuteInEditMode]
	[Serializable]
	public sealed class GUISkin : ScriptableObject
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		public GUISkin()
		{
			this.m_CustomStyles = new GUIStyle[1];
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		internal void OnEnable()
		{
			this.Apply();
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000EB44 File Offset: 0x0000CD44
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
				if (GUISkin.current == this)
				{
					GUIStyle.SetDefaultFont(this.m_Font);
				}
				this.Apply();
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0000EB78 File Offset: 0x0000CD78
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0000EB80 File Offset: 0x0000CD80
		public GUIStyle box
		{
			get
			{
				return this.m_box;
			}
			set
			{
				this.m_box = value;
				this.Apply();
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0000EB90 File Offset: 0x0000CD90
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0000EB98 File Offset: 0x0000CD98
		public GUIStyle label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
				this.Apply();
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000EBA8 File Offset: 0x0000CDA8
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		public GUIStyle textField
		{
			get
			{
				return this.m_textField;
			}
			set
			{
				this.m_textField = value;
				this.Apply();
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
		public GUIStyle textArea
		{
			get
			{
				return this.m_textArea;
			}
			set
			{
				this.m_textArea = value;
				this.Apply();
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		public GUIStyle button
		{
			get
			{
				return this.m_button;
			}
			set
			{
				this.m_button = value;
				this.Apply();
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		public GUIStyle toggle
		{
			get
			{
				return this.m_toggle;
			}
			set
			{
				this.m_toggle = value;
				this.Apply();
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000EC08 File Offset: 0x0000CE08
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000EC10 File Offset: 0x0000CE10
		public GUIStyle window
		{
			get
			{
				return this.m_window;
			}
			set
			{
				this.m_window = value;
				this.Apply();
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000EC20 File Offset: 0x0000CE20
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000EC28 File Offset: 0x0000CE28
		public GUIStyle horizontalSlider
		{
			get
			{
				return this.m_horizontalSlider;
			}
			set
			{
				this.m_horizontalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000EC38 File Offset: 0x0000CE38
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0000EC40 File Offset: 0x0000CE40
		public GUIStyle horizontalSliderThumb
		{
			get
			{
				return this.m_horizontalSliderThumb;
			}
			set
			{
				this.m_horizontalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000EC50 File Offset: 0x0000CE50
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0000EC58 File Offset: 0x0000CE58
		public GUIStyle verticalSlider
		{
			get
			{
				return this.m_verticalSlider;
			}
			set
			{
				this.m_verticalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000EC68 File Offset: 0x0000CE68
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0000EC70 File Offset: 0x0000CE70
		public GUIStyle verticalSliderThumb
		{
			get
			{
				return this.m_verticalSliderThumb;
			}
			set
			{
				this.m_verticalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000EC80 File Offset: 0x0000CE80
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0000EC88 File Offset: 0x0000CE88
		public GUIStyle horizontalScrollbar
		{
			get
			{
				return this.m_horizontalScrollbar;
			}
			set
			{
				this.m_horizontalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000EC98 File Offset: 0x0000CE98
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		public GUIStyle horizontalScrollbarThumb
		{
			get
			{
				return this.m_horizontalScrollbarThumb;
			}
			set
			{
				this.m_horizontalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		public GUIStyle horizontalScrollbarLeftButton
		{
			get
			{
				return this.m_horizontalScrollbarLeftButton;
			}
			set
			{
				this.m_horizontalScrollbarLeftButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public GUIStyle horizontalScrollbarRightButton
		{
			get
			{
				return this.m_horizontalScrollbarRightButton;
			}
			set
			{
				this.m_horizontalScrollbarRightButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		public GUIStyle verticalScrollbar
		{
			get
			{
				return this.m_verticalScrollbar;
			}
			set
			{
				this.m_verticalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000ECF8 File Offset: 0x0000CEF8
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x0000ED00 File Offset: 0x0000CF00
		public GUIStyle verticalScrollbarThumb
		{
			get
			{
				return this.m_verticalScrollbarThumb;
			}
			set
			{
				this.m_verticalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000ED10 File Offset: 0x0000CF10
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x0000ED18 File Offset: 0x0000CF18
		public GUIStyle verticalScrollbarUpButton
		{
			get
			{
				return this.m_verticalScrollbarUpButton;
			}
			set
			{
				this.m_verticalScrollbarUpButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000ED28 File Offset: 0x0000CF28
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x0000ED30 File Offset: 0x0000CF30
		public GUIStyle verticalScrollbarDownButton
		{
			get
			{
				return this.m_verticalScrollbarDownButton;
			}
			set
			{
				this.m_verticalScrollbarDownButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000ED40 File Offset: 0x0000CF40
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0000ED48 File Offset: 0x0000CF48
		public GUIStyle scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
			set
			{
				this.m_ScrollView = value;
				this.Apply();
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000ED58 File Offset: 0x0000CF58
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0000ED60 File Offset: 0x0000CF60
		public GUIStyle[] customStyles
		{
			get
			{
				return this.m_CustomStyles;
			}
			set
			{
				this.m_CustomStyles = value;
				this.Apply();
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000ED70 File Offset: 0x0000CF70
		public GUISettings settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000ED78 File Offset: 0x0000CF78
		internal static GUIStyle error
		{
			get
			{
				if (GUISkin.ms_Error == null)
				{
					GUISkin.ms_Error = new GUIStyle();
				}
				return GUISkin.ms_Error;
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000ED94 File Offset: 0x0000CF94
		internal void Apply()
		{
			if (this.m_CustomStyles == null)
			{
				Debug.Log("custom styles is null");
			}
			this.BuildStyleCache();
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		private void BuildStyleCache()
		{
			if (this.m_box == null)
			{
				this.m_box = new GUIStyle();
			}
			if (this.m_button == null)
			{
				this.m_button = new GUIStyle();
			}
			if (this.m_toggle == null)
			{
				this.m_toggle = new GUIStyle();
			}
			if (this.m_label == null)
			{
				this.m_label = new GUIStyle();
			}
			if (this.m_window == null)
			{
				this.m_window = new GUIStyle();
			}
			if (this.m_textField == null)
			{
				this.m_textField = new GUIStyle();
			}
			if (this.m_textArea == null)
			{
				this.m_textArea = new GUIStyle();
			}
			if (this.m_horizontalSlider == null)
			{
				this.m_horizontalSlider = new GUIStyle();
			}
			if (this.m_horizontalSliderThumb == null)
			{
				this.m_horizontalSliderThumb = new GUIStyle();
			}
			if (this.m_verticalSlider == null)
			{
				this.m_verticalSlider = new GUIStyle();
			}
			if (this.m_verticalSliderThumb == null)
			{
				this.m_verticalSliderThumb = new GUIStyle();
			}
			if (this.m_horizontalScrollbar == null)
			{
				this.m_horizontalScrollbar = new GUIStyle();
			}
			if (this.m_horizontalScrollbarThumb == null)
			{
				this.m_horizontalScrollbarThumb = new GUIStyle();
			}
			if (this.m_horizontalScrollbarLeftButton == null)
			{
				this.m_horizontalScrollbarLeftButton = new GUIStyle();
			}
			if (this.m_horizontalScrollbarRightButton == null)
			{
				this.m_horizontalScrollbarRightButton = new GUIStyle();
			}
			if (this.m_verticalScrollbar == null)
			{
				this.m_verticalScrollbar = new GUIStyle();
			}
			if (this.m_verticalScrollbarThumb == null)
			{
				this.m_verticalScrollbarThumb = new GUIStyle();
			}
			if (this.m_verticalScrollbarUpButton == null)
			{
				this.m_verticalScrollbarUpButton = new GUIStyle();
			}
			if (this.m_verticalScrollbarDownButton == null)
			{
				this.m_verticalScrollbarDownButton = new GUIStyle();
			}
			if (this.m_ScrollView == null)
			{
				this.m_ScrollView = new GUIStyle();
			}
			this.styles = new Dictionary<string, GUIStyle>(StringComparer.OrdinalIgnoreCase);
			this.styles["box"] = this.m_box;
			this.m_box.name = "box";
			this.styles["button"] = this.m_button;
			this.m_button.name = "button";
			this.styles["toggle"] = this.m_toggle;
			this.m_toggle.name = "toggle";
			this.styles["label"] = this.m_label;
			this.m_label.name = "label";
			this.styles["window"] = this.m_window;
			this.m_window.name = "window";
			this.styles["textfield"] = this.m_textField;
			this.m_textField.name = "textfield";
			this.styles["textarea"] = this.m_textArea;
			this.m_textArea.name = "textarea";
			this.styles["horizontalslider"] = this.m_horizontalSlider;
			this.m_horizontalSlider.name = "horizontalslider";
			this.styles["horizontalsliderthumb"] = this.m_horizontalSliderThumb;
			this.m_horizontalSliderThumb.name = "horizontalsliderthumb";
			this.styles["verticalslider"] = this.m_verticalSlider;
			this.m_verticalSlider.name = "verticalslider";
			this.styles["verticalsliderthumb"] = this.m_verticalSliderThumb;
			this.m_verticalSliderThumb.name = "verticalsliderthumb";
			this.styles["horizontalscrollbar"] = this.m_horizontalScrollbar;
			this.m_horizontalScrollbar.name = "horizontalscrollbar";
			this.styles["horizontalscrollbarthumb"] = this.m_horizontalScrollbarThumb;
			this.m_horizontalScrollbarThumb.name = "horizontalscrollbarthumb";
			this.styles["horizontalscrollbarleftbutton"] = this.m_horizontalScrollbarLeftButton;
			this.m_horizontalScrollbarLeftButton.name = "horizontalscrollbarleftbutton";
			this.styles["horizontalscrollbarrightbutton"] = this.m_horizontalScrollbarRightButton;
			this.m_horizontalScrollbarRightButton.name = "horizontalscrollbarrightbutton";
			this.styles["verticalscrollbar"] = this.m_verticalScrollbar;
			this.m_verticalScrollbar.name = "verticalscrollbar";
			this.styles["verticalscrollbarthumb"] = this.m_verticalScrollbarThumb;
			this.m_verticalScrollbarThumb.name = "verticalscrollbarthumb";
			this.styles["verticalscrollbarupbutton"] = this.m_verticalScrollbarUpButton;
			this.m_verticalScrollbarUpButton.name = "verticalscrollbarupbutton";
			this.styles["verticalscrollbardownbutton"] = this.m_verticalScrollbarDownButton;
			this.m_verticalScrollbarDownButton.name = "verticalscrollbardownbutton";
			this.styles["scrollview"] = this.m_ScrollView;
			this.m_ScrollView.name = "scrollview";
			if (this.m_CustomStyles != null)
			{
				for (int i = 0; i < this.m_CustomStyles.Length; i++)
				{
					if (this.m_CustomStyles[i] != null)
					{
						this.styles[this.m_CustomStyles[i].name] = this.m_CustomStyles[i];
					}
				}
			}
			GUISkin.error.stretchHeight = true;
			GUISkin.error.normal.textColor = Color.red;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		public GUIStyle GetStyle(string styleName)
		{
			GUIStyle guistyle = this.FindStyle(styleName);
			if (guistyle != null)
			{
				return guistyle;
			}
			Debug.LogWarning(string.Concat(new object[]
			{
				"Unable to find style '",
				styleName,
				"' in skin '",
				base.name,
				"' ",
				Event.current.type
			}));
			return GUISkin.error;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000F364 File Offset: 0x0000D564
		public GUIStyle FindStyle(string styleName)
		{
			if (this == null)
			{
				Debug.LogError("GUISkin is NULL");
				return null;
			}
			if (this.styles == null)
			{
				this.BuildStyleCache();
			}
			GUIStyle guistyle;
			if (this.styles.TryGetValue(styleName, out guistyle))
			{
				return guistyle;
			}
			return null;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		internal void MakeCurrent()
		{
			GUISkin.current = this;
			GUIStyle.SetDefaultFont(this.font);
			if (GUISkin.m_SkinChanged != null)
			{
				GUISkin.m_SkinChanged();
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
		public IEnumerator GetEnumerator()
		{
			if (this.styles == null)
			{
				this.BuildStyleCache();
			}
			return this.styles.Values.GetEnumerator();
		}

		// Token: 0x04000135 RID: 309
		[SerializeField]
		private Font m_Font;

		// Token: 0x04000136 RID: 310
		[SerializeField]
		private GUIStyle m_box;

		// Token: 0x04000137 RID: 311
		[SerializeField]
		private GUIStyle m_button;

		// Token: 0x04000138 RID: 312
		[SerializeField]
		private GUIStyle m_toggle;

		// Token: 0x04000139 RID: 313
		[SerializeField]
		private GUIStyle m_label;

		// Token: 0x0400013A RID: 314
		[SerializeField]
		private GUIStyle m_textField;

		// Token: 0x0400013B RID: 315
		[SerializeField]
		private GUIStyle m_textArea;

		// Token: 0x0400013C RID: 316
		[SerializeField]
		private GUIStyle m_window;

		// Token: 0x0400013D RID: 317
		[SerializeField]
		private GUIStyle m_horizontalSlider;

		// Token: 0x0400013E RID: 318
		[SerializeField]
		private GUIStyle m_horizontalSliderThumb;

		// Token: 0x0400013F RID: 319
		[SerializeField]
		private GUIStyle m_verticalSlider;

		// Token: 0x04000140 RID: 320
		[SerializeField]
		private GUIStyle m_verticalSliderThumb;

		// Token: 0x04000141 RID: 321
		[SerializeField]
		private GUIStyle m_horizontalScrollbar;

		// Token: 0x04000142 RID: 322
		[SerializeField]
		private GUIStyle m_horizontalScrollbarThumb;

		// Token: 0x04000143 RID: 323
		[SerializeField]
		private GUIStyle m_horizontalScrollbarLeftButton;

		// Token: 0x04000144 RID: 324
		[SerializeField]
		private GUIStyle m_horizontalScrollbarRightButton;

		// Token: 0x04000145 RID: 325
		[SerializeField]
		private GUIStyle m_verticalScrollbar;

		// Token: 0x04000146 RID: 326
		[SerializeField]
		private GUIStyle m_verticalScrollbarThumb;

		// Token: 0x04000147 RID: 327
		[SerializeField]
		private GUIStyle m_verticalScrollbarUpButton;

		// Token: 0x04000148 RID: 328
		[SerializeField]
		private GUIStyle m_verticalScrollbarDownButton;

		// Token: 0x04000149 RID: 329
		[SerializeField]
		private GUIStyle m_ScrollView;

		// Token: 0x0400014A RID: 330
		[SerializeField]
		internal GUIStyle[] m_CustomStyles;

		// Token: 0x0400014B RID: 331
		[SerializeField]
		private GUISettings m_Settings = new GUISettings();

		// Token: 0x0400014C RID: 332
		internal static GUIStyle ms_Error;

		// Token: 0x0400014D RID: 333
		private Dictionary<string, GUIStyle> styles;

		// Token: 0x0400014E RID: 334
		internal static GUISkin.SkinChangedDelegate m_SkinChanged;

		// Token: 0x0400014F RID: 335
		internal static GUISkin current;

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x06000584 RID: 1412
		internal delegate void SkinChangedDelegate();
	}
}
