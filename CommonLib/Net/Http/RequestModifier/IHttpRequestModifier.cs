using System;
using System.Collections.Generic;
using System.Net;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
	public interface IHttpRequestModifier : IHttpRequestUriModifier, IHttpRequestBodyModifier
	{
		HttpWebRequest PreProcessRequest(HttpWebRequest request);
		HttpWebRequest PostProcessRequest(HttpWebRequest request);
	}
}

