using System;

namespace UnityEngine
{
	// Token: 0x0200008F RID: 143
	public enum KeyCode
	{
		// Token: 0x040001AC RID: 428
		None,
		// Token: 0x040001AD RID: 429
		Backspace = 8,
		// Token: 0x040001AE RID: 430
		Delete = 127,
		// Token: 0x040001AF RID: 431
		Tab = 9,
		// Token: 0x040001B0 RID: 432
		Clear = 12,
		// Token: 0x040001B1 RID: 433
		Return,
		// Token: 0x040001B2 RID: 434
		Pause = 19,
		// Token: 0x040001B3 RID: 435
		Escape = 27,
		// Token: 0x040001B4 RID: 436
		Space = 32,
		// Token: 0x040001B5 RID: 437
		Keypad0 = 256,
		// Token: 0x040001B6 RID: 438
		Keypad1,
		// Token: 0x040001B7 RID: 439
		Keypad2,
		// Token: 0x040001B8 RID: 440
		Keypad3,
		// Token: 0x040001B9 RID: 441
		Keypad4,
		// Token: 0x040001BA RID: 442
		Keypad5,
		// Token: 0x040001BB RID: 443
		Keypad6,
		// Token: 0x040001BC RID: 444
		Keypad7,
		// Token: 0x040001BD RID: 445
		Keypad8,
		// Token: 0x040001BE RID: 446
		Keypad9,
		// Token: 0x040001BF RID: 447
		KeypadPeriod,
		// Token: 0x040001C0 RID: 448
		KeypadDivide,
		// Token: 0x040001C1 RID: 449
		KeypadMultiply,
		// Token: 0x040001C2 RID: 450
		KeypadMinus,
		// Token: 0x040001C3 RID: 451
		KeypadPlus,
		// Token: 0x040001C4 RID: 452
		KeypadEnter,
		// Token: 0x040001C5 RID: 453
		KeypadEquals,
		// Token: 0x040001C6 RID: 454
		UpArrow,
		// Token: 0x040001C7 RID: 455
		DownArrow,
		// Token: 0x040001C8 RID: 456
		RightArrow,
		// Token: 0x040001C9 RID: 457
		LeftArrow,
		// Token: 0x040001CA RID: 458
		Insert,
		// Token: 0x040001CB RID: 459
		Home,
		// Token: 0x040001CC RID: 460
		End,
		// Token: 0x040001CD RID: 461
		PageUp,
		// Token: 0x040001CE RID: 462
		PageDown,
		// Token: 0x040001CF RID: 463
		F1,
		// Token: 0x040001D0 RID: 464
		F2,
		// Token: 0x040001D1 RID: 465
		F3,
		// Token: 0x040001D2 RID: 466
		F4,
		// Token: 0x040001D3 RID: 467
		F5,
		// Token: 0x040001D4 RID: 468
		F6,
		// Token: 0x040001D5 RID: 469
		F7,
		// Token: 0x040001D6 RID: 470
		F8,
		// Token: 0x040001D7 RID: 471
		F9,
		// Token: 0x040001D8 RID: 472
		F10,
		// Token: 0x040001D9 RID: 473
		F11,
		// Token: 0x040001DA RID: 474
		F12,
		// Token: 0x040001DB RID: 475
		F13,
		// Token: 0x040001DC RID: 476
		F14,
		// Token: 0x040001DD RID: 477
		F15,
		// Token: 0x040001DE RID: 478
		Alpha0 = 48,
		// Token: 0x040001DF RID: 479
		Alpha1,
		// Token: 0x040001E0 RID: 480
		Alpha2,
		// Token: 0x040001E1 RID: 481
		Alpha3,
		// Token: 0x040001E2 RID: 482
		Alpha4,
		// Token: 0x040001E3 RID: 483
		Alpha5,
		// Token: 0x040001E4 RID: 484
		Alpha6,
		// Token: 0x040001E5 RID: 485
		Alpha7,
		// Token: 0x040001E6 RID: 486
		Alpha8,
		// Token: 0x040001E7 RID: 487
		Alpha9,
		// Token: 0x040001E8 RID: 488
		Exclaim = 33,
		// Token: 0x040001E9 RID: 489
		DoubleQuote,
		// Token: 0x040001EA RID: 490
		Hash,
		// Token: 0x040001EB RID: 491
		Dollar,
		// Token: 0x040001EC RID: 492
		Ampersand = 38,
		// Token: 0x040001ED RID: 493
		Quote,
		// Token: 0x040001EE RID: 494
		LeftParen,
		// Token: 0x040001EF RID: 495
		RightParen,
		// Token: 0x040001F0 RID: 496
		Asterisk,
		// Token: 0x040001F1 RID: 497
		Plus,
		// Token: 0x040001F2 RID: 498
		Comma,
		// Token: 0x040001F3 RID: 499
		Minus,
		// Token: 0x040001F4 RID: 500
		Period,
		// Token: 0x040001F5 RID: 501
		Slash,
		// Token: 0x040001F6 RID: 502
		Colon = 58,
		// Token: 0x040001F7 RID: 503
		Semicolon,
		// Token: 0x040001F8 RID: 504
		Less,
		// Token: 0x040001F9 RID: 505
		Equals,
		// Token: 0x040001FA RID: 506
		Greater,
		// Token: 0x040001FB RID: 507
		Question,
		// Token: 0x040001FC RID: 508
		At,
		// Token: 0x040001FD RID: 509
		LeftBracket = 91,
		// Token: 0x040001FE RID: 510
		Backslash,
		// Token: 0x040001FF RID: 511
		RightBracket,
		// Token: 0x04000200 RID: 512
		Caret,
		// Token: 0x04000201 RID: 513
		Underscore,
		// Token: 0x04000202 RID: 514
		BackQuote,
		// Token: 0x04000203 RID: 515
		A,
		// Token: 0x04000204 RID: 516
		B,
		// Token: 0x04000205 RID: 517
		C,
		// Token: 0x04000206 RID: 518
		D,
		// Token: 0x04000207 RID: 519
		E,
		// Token: 0x04000208 RID: 520
		F,
		// Token: 0x04000209 RID: 521
		G,
		// Token: 0x0400020A RID: 522
		H,
		// Token: 0x0400020B RID: 523
		I,
		// Token: 0x0400020C RID: 524
		J,
		// Token: 0x0400020D RID: 525
		K,
		// Token: 0x0400020E RID: 526
		L,
		// Token: 0x0400020F RID: 527
		M,
		// Token: 0x04000210 RID: 528
		N,
		// Token: 0x04000211 RID: 529
		O,
		// Token: 0x04000212 RID: 530
		P,
		// Token: 0x04000213 RID: 531
		Q,
		// Token: 0x04000214 RID: 532
		R,
		// Token: 0x04000215 RID: 533
		S,
		// Token: 0x04000216 RID: 534
		T,
		// Token: 0x04000217 RID: 535
		U,
		// Token: 0x04000218 RID: 536
		V,
		// Token: 0x04000219 RID: 537
		W,
		// Token: 0x0400021A RID: 538
		X,
		// Token: 0x0400021B RID: 539
		Y,
		// Token: 0x0400021C RID: 540
		Z,
		// Token: 0x0400021D RID: 541
		Numlock = 300,
		// Token: 0x0400021E RID: 542
		CapsLock,
		// Token: 0x0400021F RID: 543
		ScrollLock,
		// Token: 0x04000220 RID: 544
		RightShift,
		// Token: 0x04000221 RID: 545
		LeftShift,
		// Token: 0x04000222 RID: 546
		RightControl,
		// Token: 0x04000223 RID: 547
		LeftControl,
		// Token: 0x04000224 RID: 548
		RightAlt,
		// Token: 0x04000225 RID: 549
		LeftAlt,
		// Token: 0x04000226 RID: 550
		LeftCommand = 310,
		// Token: 0x04000227 RID: 551
		LeftApple = 310,
		// Token: 0x04000228 RID: 552
		LeftWindows,
		// Token: 0x04000229 RID: 553
		RightCommand = 309,
		// Token: 0x0400022A RID: 554
		RightApple = 309,
		// Token: 0x0400022B RID: 555
		RightWindows = 312,
		// Token: 0x0400022C RID: 556
		AltGr,
		// Token: 0x0400022D RID: 557
		Help = 315,
		// Token: 0x0400022E RID: 558
		Print,
		// Token: 0x0400022F RID: 559
		SysReq,
		// Token: 0x04000230 RID: 560
		Break,
		// Token: 0x04000231 RID: 561
		Menu,
		// Token: 0x04000232 RID: 562
		Mouse0 = 323,
		// Token: 0x04000233 RID: 563
		Mouse1,
		// Token: 0x04000234 RID: 564
		Mouse2,
		// Token: 0x04000235 RID: 565
		Mouse3,
		// Token: 0x04000236 RID: 566
		Mouse4,
		// Token: 0x04000237 RID: 567
		Mouse5,
		// Token: 0x04000238 RID: 568
		Mouse6,
		// Token: 0x04000239 RID: 569
		JoystickButton0,
		// Token: 0x0400023A RID: 570
		JoystickButton1,
		// Token: 0x0400023B RID: 571
		JoystickButton2,
		// Token: 0x0400023C RID: 572
		JoystickButton3,
		// Token: 0x0400023D RID: 573
		JoystickButton4,
		// Token: 0x0400023E RID: 574
		JoystickButton5,
		// Token: 0x0400023F RID: 575
		JoystickButton6,
		// Token: 0x04000240 RID: 576
		JoystickButton7,
		// Token: 0x04000241 RID: 577
		JoystickButton8,
		// Token: 0x04000242 RID: 578
		JoystickButton9,
		// Token: 0x04000243 RID: 579
		JoystickButton10,
		// Token: 0x04000244 RID: 580
		JoystickButton11,
		// Token: 0x04000245 RID: 581
		JoystickButton12,
		// Token: 0x04000246 RID: 582
		JoystickButton13,
		// Token: 0x04000247 RID: 583
		JoystickButton14,
		// Token: 0x04000248 RID: 584
		JoystickButton15,
		// Token: 0x04000249 RID: 585
		JoystickButton16,
		// Token: 0x0400024A RID: 586
		JoystickButton17,
		// Token: 0x0400024B RID: 587
		JoystickButton18,
		// Token: 0x0400024C RID: 588
		JoystickButton19,
		// Token: 0x0400024D RID: 589
		Joystick1Button0,
		// Token: 0x0400024E RID: 590
		Joystick1Button1,
		// Token: 0x0400024F RID: 591
		Joystick1Button2,
		// Token: 0x04000250 RID: 592
		Joystick1Button3,
		// Token: 0x04000251 RID: 593
		Joystick1Button4,
		// Token: 0x04000252 RID: 594
		Joystick1Button5,
		// Token: 0x04000253 RID: 595
		Joystick1Button6,
		// Token: 0x04000254 RID: 596
		Joystick1Button7,
		// Token: 0x04000255 RID: 597
		Joystick1Button8,
		// Token: 0x04000256 RID: 598
		Joystick1Button9,
		// Token: 0x04000257 RID: 599
		Joystick1Button10,
		// Token: 0x04000258 RID: 600
		Joystick1Button11,
		// Token: 0x04000259 RID: 601
		Joystick1Button12,
		// Token: 0x0400025A RID: 602
		Joystick1Button13,
		// Token: 0x0400025B RID: 603
		Joystick1Button14,
		// Token: 0x0400025C RID: 604
		Joystick1Button15,
		// Token: 0x0400025D RID: 605
		Joystick1Button16,
		// Token: 0x0400025E RID: 606
		Joystick1Button17,
		// Token: 0x0400025F RID: 607
		Joystick1Button18,
		// Token: 0x04000260 RID: 608
		Joystick1Button19,
		// Token: 0x04000261 RID: 609
		Joystick2Button0,
		// Token: 0x04000262 RID: 610
		Joystick2Button1,
		// Token: 0x04000263 RID: 611
		Joystick2Button2,
		// Token: 0x04000264 RID: 612
		Joystick2Button3,
		// Token: 0x04000265 RID: 613
		Joystick2Button4,
		// Token: 0x04000266 RID: 614
		Joystick2Button5,
		// Token: 0x04000267 RID: 615
		Joystick2Button6,
		// Token: 0x04000268 RID: 616
		Joystick2Button7,
		// Token: 0x04000269 RID: 617
		Joystick2Button8,
		// Token: 0x0400026A RID: 618
		Joystick2Button9,
		// Token: 0x0400026B RID: 619
		Joystick2Button10,
		// Token: 0x0400026C RID: 620
		Joystick2Button11,
		// Token: 0x0400026D RID: 621
		Joystick2Button12,
		// Token: 0x0400026E RID: 622
		Joystick2Button13,
		// Token: 0x0400026F RID: 623
		Joystick2Button14,
		// Token: 0x04000270 RID: 624
		Joystick2Button15,
		// Token: 0x04000271 RID: 625
		Joystick2Button16,
		// Token: 0x04000272 RID: 626
		Joystick2Button17,
		// Token: 0x04000273 RID: 627
		Joystick2Button18,
		// Token: 0x04000274 RID: 628
		Joystick2Button19,
		// Token: 0x04000275 RID: 629
		Joystick3Button0,
		// Token: 0x04000276 RID: 630
		Joystick3Button1,
		// Token: 0x04000277 RID: 631
		Joystick3Button2,
		// Token: 0x04000278 RID: 632
		Joystick3Button3,
		// Token: 0x04000279 RID: 633
		Joystick3Button4,
		// Token: 0x0400027A RID: 634
		Joystick3Button5,
		// Token: 0x0400027B RID: 635
		Joystick3Button6,
		// Token: 0x0400027C RID: 636
		Joystick3Button7,
		// Token: 0x0400027D RID: 637
		Joystick3Button8,
		// Token: 0x0400027E RID: 638
		Joystick3Button9,
		// Token: 0x0400027F RID: 639
		Joystick3Button10,
		// Token: 0x04000280 RID: 640
		Joystick3Button11,
		// Token: 0x04000281 RID: 641
		Joystick3Button12,
		// Token: 0x04000282 RID: 642
		Joystick3Button13,
		// Token: 0x04000283 RID: 643
		Joystick3Button14,
		// Token: 0x04000284 RID: 644
		Joystick3Button15,
		// Token: 0x04000285 RID: 645
		Joystick3Button16,
		// Token: 0x04000286 RID: 646
		Joystick3Button17,
		// Token: 0x04000287 RID: 647
		Joystick3Button18,
		// Token: 0x04000288 RID: 648
		Joystick3Button19,
		// Token: 0x04000289 RID: 649
		Joystick4Button0,
		// Token: 0x0400028A RID: 650
		Joystick4Button1,
		// Token: 0x0400028B RID: 651
		Joystick4Button2,
		// Token: 0x0400028C RID: 652
		Joystick4Button3,
		// Token: 0x0400028D RID: 653
		Joystick4Button4,
		// Token: 0x0400028E RID: 654
		Joystick4Button5,
		// Token: 0x0400028F RID: 655
		Joystick4Button6,
		// Token: 0x04000290 RID: 656
		Joystick4Button7,
		// Token: 0x04000291 RID: 657
		Joystick4Button8,
		// Token: 0x04000292 RID: 658
		Joystick4Button9,
		// Token: 0x04000293 RID: 659
		Joystick4Button10,
		// Token: 0x04000294 RID: 660
		Joystick4Button11,
		// Token: 0x04000295 RID: 661
		Joystick4Button12,
		// Token: 0x04000296 RID: 662
		Joystick4Button13,
		// Token: 0x04000297 RID: 663
		Joystick4Button14,
		// Token: 0x04000298 RID: 664
		Joystick4Button15,
		// Token: 0x04000299 RID: 665
		Joystick4Button16,
		// Token: 0x0400029A RID: 666
		Joystick4Button17,
		// Token: 0x0400029B RID: 667
		Joystick4Button18,
		// Token: 0x0400029C RID: 668
		Joystick4Button19
	}
}
