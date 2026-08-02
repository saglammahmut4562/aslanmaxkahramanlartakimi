using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200010E RID: 270
	public sealed class TouchScreenKeyboard
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x00017050 File Offset: 0x00015250
		public TouchScreenKeyboard(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder)
		{
			TouchScreenKeyboard_InternalConstructorHelperArguments touchScreenKeyboard_InternalConstructorHelperArguments = default(TouchScreenKeyboard_InternalConstructorHelperArguments);
			touchScreenKeyboard_InternalConstructorHelperArguments.keyboardType = Convert.ToUInt32(keyboardType);
			touchScreenKeyboard_InternalConstructorHelperArguments.autocorrection = Convert.ToUInt32(autocorrection);
			touchScreenKeyboard_InternalConstructorHelperArguments.multiline = Convert.ToUInt32(multiline);
			touchScreenKeyboard_InternalConstructorHelperArguments.secure = Convert.ToUInt32(secure);
			touchScreenKeyboard_InternalConstructorHelperArguments.alert = Convert.ToUInt32(alert);
			this.TouchScreenKeyboard_InternalConstructorHelper(ref touchScreenKeyboard_InternalConstructorHelperArguments, text, textPlaceholder);
		}

		// Token: 0x060008EC RID: 2284
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void Destroy();

		// Token: 0x060008ED RID: 2285 RVA: 0x000170C0 File Offset: 0x000152C0
		~TouchScreenKeyboard()
		{
			this.Destroy();
		}

		// Token: 0x060008EE RID: 2286
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void TouchScreenKeyboard_InternalConstructorHelper(ref TouchScreenKeyboard_InternalConstructorHelperArguments arguments, string text, string textPlaceholder);

		// Token: 0x060008EF RID: 2287 RVA: 0x000170F0 File Offset: 0x000152F0
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert)
		{
			string empty = string.Empty;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, empty);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00017114 File Offset: 0x00015314
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure)
		{
			string empty = string.Empty;
			bool flag = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, flag, empty);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00017138 File Offset: 0x00015338
		public static TouchScreenKeyboard Open(string text, [DefaultValue("TouchScreenKeyboardType.Default")] TouchScreenKeyboardType keyboardType, [DefaultValue("true")] bool autocorrection, [DefaultValue("false")] bool multiline, [DefaultValue("false")] bool secure, [DefaultValue("false")] bool alert, [DefaultValue("\"\"")] string textPlaceholder)
		{
			return new TouchScreenKeyboard(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder);
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008F2 RID: 2290
		public extern string text
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001F1 RID: 497
		// (set) Token: 0x060008F3 RID: 2291
		public static extern bool hideInput
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008F4 RID: 2292
		// (set) Token: 0x060008F5 RID: 2293
		public extern bool active
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
			[WrapperlessIcall]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008F6 RID: 2294
		public extern bool done
		{
			[WrapperlessIcall]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0400049E RID: 1182
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}
