using System;

namespace System.Net
{
	// Token: 0x02000079 RID: 121
	public enum HttpStatusCode
	{
		// Token: 0x040000F3 RID: 243
		Continue = 100,
		// Token: 0x040000F4 RID: 244
		SwitchingProtocols,
		// Token: 0x040000F5 RID: 245
		OK = 200,
		// Token: 0x040000F6 RID: 246
		Created,
		// Token: 0x040000F7 RID: 247
		Accepted,
		// Token: 0x040000F8 RID: 248
		NonAuthoritativeInformation,
		// Token: 0x040000F9 RID: 249
		NoContent,
		// Token: 0x040000FA RID: 250
		ResetContent,
		// Token: 0x040000FB RID: 251
		PartialContent,
		// Token: 0x040000FC RID: 252
		MultipleChoices = 300,
		// Token: 0x040000FD RID: 253
		Ambiguous = 300,
		// Token: 0x040000FE RID: 254
		MovedPermanently,
		// Token: 0x040000FF RID: 255
		Moved = 301,
		// Token: 0x04000100 RID: 256
		Found,
		// Token: 0x04000101 RID: 257
		Redirect = 302,
		// Token: 0x04000102 RID: 258
		SeeOther,
		// Token: 0x04000103 RID: 259
		RedirectMethod = 303,
		// Token: 0x04000104 RID: 260
		NotModified,
		// Token: 0x04000105 RID: 261
		UseProxy,
		// Token: 0x04000106 RID: 262
		Unused,
		// Token: 0x04000107 RID: 263
		TemporaryRedirect,
		// Token: 0x04000108 RID: 264
		RedirectKeepVerb = 307,
		// Token: 0x04000109 RID: 265
		BadRequest = 400,
		// Token: 0x0400010A RID: 266
		Unauthorized,
		// Token: 0x0400010B RID: 267
		PaymentRequired,
		// Token: 0x0400010C RID: 268
		Forbidden,
		// Token: 0x0400010D RID: 269
		NotFound,
		// Token: 0x0400010E RID: 270
		MethodNotAllowed,
		// Token: 0x0400010F RID: 271
		NotAcceptable,
		// Token: 0x04000110 RID: 272
		ProxyAuthenticationRequired,
		// Token: 0x04000111 RID: 273
		RequestTimeout,
		// Token: 0x04000112 RID: 274
		Conflict,
		// Token: 0x04000113 RID: 275
		Gone,
		// Token: 0x04000114 RID: 276
		LengthRequired,
		// Token: 0x04000115 RID: 277
		PreconditionFailed,
		// Token: 0x04000116 RID: 278
		RequestEntityTooLarge,
		// Token: 0x04000117 RID: 279
		RequestUriTooLong,
		// Token: 0x04000118 RID: 280
		UnsupportedMediaType,
		// Token: 0x04000119 RID: 281
		RequestedRangeNotSatisfiable,
		// Token: 0x0400011A RID: 282
		ExpectationFailed,
		// Token: 0x0400011B RID: 283
		InternalServerError = 500,
		// Token: 0x0400011C RID: 284
		NotImplemented,
		// Token: 0x0400011D RID: 285
		BadGateway,
		// Token: 0x0400011E RID: 286
		ServiceUnavailable,
		// Token: 0x0400011F RID: 287
		GatewayTimeout,
		// Token: 0x04000120 RID: 288
		HttpVersionNotSupported
	}
}
