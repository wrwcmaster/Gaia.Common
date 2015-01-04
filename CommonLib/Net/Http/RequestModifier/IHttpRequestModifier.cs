using System;
using System.Collections.Generic;
using System.Net;

namespace CommonLib.Net.Http.RequestModifier
{
	public interface IHttpRequestModifier : IHttpRequestUriModifier, IHttpRequestBodyModifier
	{
		HttpWebRequest PreProcessRequest(HttpWebRequest request);
		HttpWebRequest PostProcessRequest(HttpWebRequest request);
	}
}

