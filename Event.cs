using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200004B RID: 75
	[StructLayout(0)]
	public sealed class Event
	{
		// Token: 0x06000342 RID: 834 RVA: 0x00007E5C File Offset: 0x0000605C
		public Event()
		{
			this.Init();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00007E6C File Offset: 0x0000606C
		public Event(Event other)
		{
			if (other == null)
			{
				throw new ArgumentException("Event to copy from is null.");
			}
			this.InitCopy(other);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00007E8C File Offset: 0x0000608C
		private Event(IntPtr ptr)
		{
			this.InitPtr(ptr);
		}

		// Token: 0x06000345 RID: 837
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Init();

		// Token: 0x06000346 RID: 838 RVA: 0x00007E9C File Offset: 0x0000609C
		~Event()
		{
			this.Cleanup();
		}

		// Token: 0x06000347 RID: 839
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Cleanup();

		// Token: 0x06000348 RID: 840
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void InitCopy(Event other);

		// Token: 0x06000349 RID: 841
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void InitPtr(IntPtr ptr);

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600034A RID: 842
		public extern EventType rawType
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600034B RID: 843
		// (set) Token: 0x0600034C RID: 844
		public extern EventType type
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600034D RID: 845
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern EventType GetTypeForControl(int controlID);

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00007ECC File Offset: 0x000060CC
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00007EE4 File Offset: 0x000060E4
		public Vector2 mousePosition
		{
			get
			{
				Vector2 vector;
				this.Internal_GetMousePosition(out vector);
				return vector;
			}
			set
			{
				this.Internal_SetMousePosition(value);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00007EF0 File Offset: 0x000060F0
		private void Internal_SetMousePosition(Vector2 value)
		{
			Event.INTERNAL_CALL_Internal_SetMousePosition(this, ref value);
		}

		// Token: 0x06000351 RID: 849
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_SetMousePosition(Event self, ref Vector2 value);

		// Token: 0x06000352 RID: 850
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_GetMousePosition(out Vector2 value);

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00007EFC File Offset: 0x000060FC
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00007F14 File Offset: 0x00006114
		public Vector2 delta
		{
			get
			{
				Vector2 vector;
				this.Internal_GetMouseDelta(out vector);
				return vector;
			}
			set
			{
				this.Internal_SetMouseDelta(value);
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00007F20 File Offset: 0x00006120
		private void Internal_SetMouseDelta(Vector2 value)
		{
			Event.INTERNAL_CALL_Internal_SetMouseDelta(this, ref value);
		}

		// Token: 0x06000356 RID: 854
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_Internal_SetMouseDelta(Event self, ref Vector2 value);

		// Token: 0x06000357 RID: 855
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Internal_GetMouseDelta(out Vector2 value);

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00007F2C File Offset: 0x0000612C
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00007F40 File Offset: 0x00006140
		[Obsolete("Use HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);", true)]
		public Ray mouseRay
		{
			get
			{
				return new Ray(Vector3.up, Vector3.up);
			}
			set
			{
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600035A RID: 858
		// (set) Token: 0x0600035B RID: 859
		public extern int button
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600035C RID: 860
		// (set) Token: 0x0600035D RID: 861
		public extern EventModifiers modifiers
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600035E RID: 862
		// (set) Token: 0x0600035F RID: 863
		public extern float pressure
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000360 RID: 864
		// (set) Token: 0x06000361 RID: 865
		public extern int clickCount
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000362 RID: 866
		// (set) Token: 0x06000363 RID: 867
		public extern char character
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000364 RID: 868
		// (set) Token: 0x06000365 RID: 869
		public extern string commandName
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000366 RID: 870
		// (set) Token: 0x06000367 RID: 871
		public extern KeyCode keyCode
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00007F44 File Offset: 0x00006144
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00007F54 File Offset: 0x00006154
		public bool shift
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) != (EventModifiers)0;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Shift;
				}
				else
				{
					this.modifiers |= EventModifiers.Shift;
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00007F80 File Offset: 0x00006180
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00007F90 File Offset: 0x00006190
		public bool control
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) != (EventModifiers)0;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Control;
				}
				else
				{
					this.modifiers |= EventModifiers.Control;
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00007FBC File Offset: 0x000061BC
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00007FCC File Offset: 0x000061CC
		public bool alt
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) != (EventModifiers)0;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Alt;
				}
				else
				{
					this.modifiers |= EventModifiers.Alt;
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00007FF8 File Offset: 0x000061F8
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00008008 File Offset: 0x00006208
		public bool command
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) != (EventModifiers)0;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Command;
				}
				else
				{
					this.modifiers |= EventModifiers.Command;
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00008034 File Offset: 0x00006234
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00008048 File Offset: 0x00006248
		public bool capsLock
		{
			get
			{
				return (this.modifiers & EventModifiers.CapsLock) != (EventModifiers)0;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.CapsLock;
				}
				else
				{
					this.modifiers |= EventModifiers.CapsLock;
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00008074 File Offset: 0x00006274
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00008088 File Offset: 0x00006288
		public bool numeric
		{
			get
			{
				return (this.modifiers & EventModifiers.Numeric) != (EventModifiers)0;
			}
			set
			{
				if (!value)
				{
					this.modifiers &= ~EventModifiers.Shift;
				}
				else
				{
					this.modifiers |= EventModifiers.Shift;
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000374 RID: 884 RVA: 0x000080B4 File Offset: 0x000062B4
		public bool functionKey
		{
			get
			{
				return (this.modifiers & EventModifiers.FunctionKey) != (EventModifiers)0;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000080C8 File Offset: 0x000062C8
		// (set) Token: 0x06000376 RID: 886 RVA: 0x000080D0 File Offset: 0x000062D0
		public static Event current
		{
			get
			{
				return Event.s_Current;
			}
			set
			{
				if (value != null)
				{
					Event.s_Current = value;
				}
				else
				{
					Event.s_Current = Event.s_MasterEvent;
				}
				Event.Internal_SetNativeEvent(Event.s_Current.m_Ptr);
			}
		}

		// Token: 0x06000377 RID: 887
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void Internal_SetNativeEvent(IntPtr ptr);

		// Token: 0x06000378 RID: 888 RVA: 0x000080FC File Offset: 0x000062FC
		private static void Internal_MakeMasterEventCurrent()
		{
			if (Event.s_MasterEvent == null)
			{
				Event.s_MasterEvent = new Event();
			}
			Event.s_Current = Event.s_MasterEvent;
			Event.Internal_SetNativeEvent(Event.s_MasterEvent.m_Ptr);
		}

		// Token: 0x06000379 RID: 889
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Use();

		// Token: 0x0600037A RID: 890
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool PopEvent(Event outEvent);

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000812C File Offset: 0x0000632C
		public bool isKey
		{
			get
			{
				EventType type = this.type;
				return type == EventType.KeyDown || type == EventType.KeyUp;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00008150 File Offset: 0x00006350
		public bool isMouse
		{
			get
			{
				EventType type = this.type;
				return type == EventType.MouseMove || type == EventType.MouseDown || type == EventType.MouseUp || type == EventType.MouseDrag;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00008180 File Offset: 0x00006380
		public static Event KeyboardEvent(string key)
		{
			Event @event = new Event();
			@event.type = EventType.KeyDown;
			if (key == null || key == string.Empty)
			{
				return @event;
			}
			int num = 0;
			bool flag;
			do
			{
				flag = true;
				if (num >= key.Length)
				{
					break;
				}
				char c = key[num];
				switch (c)
				{
				case '#':
					@event.modifiers |= EventModifiers.Shift;
					num++;
					break;
				default:
					if (c != '^')
					{
						flag = false;
					}
					else
					{
						@event.modifiers |= EventModifiers.Control;
						num++;
					}
					break;
				case '%':
					@event.modifiers |= EventModifiers.Command;
					num++;
					break;
				case '&':
					@event.modifiers |= EventModifiers.Alt;
					num++;
					break;
				}
			}
			while (flag);
			string text = key.Substring(num, key.Length - num).ToLower();
			string text2 = text;
			switch (text2)
			{
			case "[0]":
				@event.character = '0';
				@event.keyCode = KeyCode.Keypad0;
				return @event;
			case "[1]":
				@event.character = '1';
				@event.keyCode = KeyCode.Keypad1;
				return @event;
			case "[2]":
				@event.character = '2';
				@event.keyCode = KeyCode.Keypad2;
				return @event;
			case "[3]":
				@event.character = '3';
				@event.keyCode = KeyCode.Keypad3;
				return @event;
			case "[4]":
				@event.character = '4';
				@event.keyCode = KeyCode.Keypad4;
				return @event;
			case "[5]":
				@event.character = '5';
				@event.keyCode = KeyCode.Keypad5;
				return @event;
			case "[6]":
				@event.character = '6';
				@event.keyCode = KeyCode.Keypad6;
				return @event;
			case "[7]":
				@event.character = '7';
				@event.keyCode = KeyCode.Keypad7;
				return @event;
			case "[8]":
				@event.character = '8';
				@event.keyCode = KeyCode.Keypad8;
				return @event;
			case "[9]":
				@event.character = '9';
				@event.keyCode = KeyCode.Keypad9;
				return @event;
			case "[.]":
				@event.character = '.';
				@event.keyCode = KeyCode.KeypadPeriod;
				return @event;
			case "[/]":
				@event.character = '/';
				@event.keyCode = KeyCode.KeypadDivide;
				return @event;
			case "[-]":
				@event.character = '-';
				@event.keyCode = KeyCode.KeypadMinus;
				return @event;
			case "[+]":
				@event.character = '+';
				@event.keyCode = KeyCode.KeypadPlus;
				return @event;
			case "[=]":
				@event.character = '=';
				@event.keyCode = KeyCode.KeypadEquals;
				return @event;
			case "[equals]":
				@event.character = '=';
				@event.keyCode = KeyCode.KeypadEquals;
				return @event;
			case "[enter]":
				@event.character = '\n';
				@event.keyCode = KeyCode.KeypadEnter;
				return @event;
			case "up":
				@event.keyCode = KeyCode.UpArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "down":
				@event.keyCode = KeyCode.DownArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "left":
				@event.keyCode = KeyCode.LeftArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "right":
				@event.keyCode = KeyCode.RightArrow;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "insert":
				@event.keyCode = KeyCode.Insert;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "home":
				@event.keyCode = KeyCode.Home;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "end":
				@event.keyCode = KeyCode.End;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "pgup":
				@event.keyCode = KeyCode.PageDown;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "page up":
				@event.keyCode = KeyCode.PageUp;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "pgdown":
				@event.keyCode = KeyCode.PageUp;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "page down":
				@event.keyCode = KeyCode.PageDown;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "backspace":
				@event.keyCode = KeyCode.Backspace;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "delete":
				@event.keyCode = KeyCode.Delete;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "tab":
				@event.keyCode = KeyCode.Tab;
				return @event;
			case "f1":
				@event.keyCode = KeyCode.F1;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f2":
				@event.keyCode = KeyCode.F2;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f3":
				@event.keyCode = KeyCode.F3;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f4":
				@event.keyCode = KeyCode.F4;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f5":
				@event.keyCode = KeyCode.F5;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f6":
				@event.keyCode = KeyCode.F6;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f7":
				@event.keyCode = KeyCode.F7;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f8":
				@event.keyCode = KeyCode.F8;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f9":
				@event.keyCode = KeyCode.F9;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f10":
				@event.keyCode = KeyCode.F10;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f11":
				@event.keyCode = KeyCode.F11;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f12":
				@event.keyCode = KeyCode.F12;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f13":
				@event.keyCode = KeyCode.F13;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f14":
				@event.keyCode = KeyCode.F14;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "f15":
				@event.keyCode = KeyCode.F15;
				@event.modifiers |= EventModifiers.FunctionKey;
				return @event;
			case "[esc]":
				@event.keyCode = KeyCode.Escape;
				return @event;
			case "return":
				@event.character = '\n';
				@event.keyCode = KeyCode.Return;
				@event.modifiers &= ~EventModifiers.FunctionKey;
				return @event;
			case "space":
				@event.keyCode = KeyCode.Space;
				@event.character = ' ';
				@event.modifiers &= ~EventModifiers.FunctionKey;
				return @event;
			}
			if (text.Length != 1)
			{
				try
				{
					@event.keyCode = (KeyCode)((int)Enum.Parse(typeof(KeyCode), text, true));
				}
				catch (ArgumentException)
				{
					Debug.LogError(UnityString.Format("Unable to find key name that matches '{0}'", new object[] { text }));
				}
			}
			else
			{
				@event.character = text.ToLower()[0];
				@event.keyCode = (KeyCode)@event.character;
				if (@event.modifiers != (EventModifiers)0)
				{
					@event.character = '\0';
				}
			}
			return @event;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00008C14 File Offset: 0x00006E14
		public override int GetHashCode()
		{
			int num = 1;
			if (this.isKey)
			{
				num = (int)((ushort)this.keyCode);
			}
			if (this.isMouse)
			{
				num = this.mousePosition.GetHashCode();
			}
			return (num * 37) | (int)this.modifiers;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00008C60 File Offset: 0x00006E60
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			Event @event = (Event)obj;
			if (this.type != @event.type || this.modifiers != @event.modifiers)
			{
				return false;
			}
			if (this.isKey)
			{
				return this.keyCode == @event.keyCode && this.modifiers == @event.modifiers;
			}
			return this.isMouse && this.mousePosition == @event.mousePosition;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00008D10 File Offset: 0x00006F10
		public override string ToString()
		{
			if (this.isKey)
			{
				if (this.character == '\0')
				{
					return UnityString.Format("Event:{0}   Character:\\0   Modifiers:{1}   KeyCode:{2}", new object[] { this.type, this.modifiers, this.keyCode });
				}
				return UnityString.Format(string.Concat(new object[]
				{
					"Event:",
					this.type,
					"   Character:",
					(int)this.character,
					"   Modifiers:",
					this.modifiers,
					"   KeyCode:",
					this.keyCode
				}), new object[0]);
			}
			else
			{
				if (this.isMouse)
				{
					return UnityString.Format("Event: {0}   Position: {1} Modifiers: {2}", new object[] { this.type, this.mousePosition, this.modifiers });
				}
				if (this.type == EventType.ExecuteCommand || this.type == EventType.ValidateCommand)
				{
					return UnityString.Format("Event: {0}  \"{1}\"", new object[] { this.type, this.commandName });
				}
				return string.Empty + this.type;
			}
		}

		// Token: 0x04000080 RID: 128
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000081 RID: 129
		private static Event s_Current;

		// Token: 0x04000082 RID: 130
		private static Event s_MasterEvent;
	}
}
