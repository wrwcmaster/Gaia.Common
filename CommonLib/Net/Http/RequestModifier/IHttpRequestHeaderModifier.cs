using System;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
	public interface IHttpRequestHeaderModifier
	{
		string HeaderKey { get; }
		string ModifyHeaderValue (string originalValue);
	}
}

