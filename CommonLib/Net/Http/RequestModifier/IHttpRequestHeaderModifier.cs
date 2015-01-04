using System;

namespace CommonLib.Net.Http.RequestModifier
{
	public interface IHttpRequestHeaderModifier
	{
		string HeaderKey { get; }
		string ModifyHeaderValue (string originalValue);
	}
}

