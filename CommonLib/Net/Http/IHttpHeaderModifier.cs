using System;

namespace Gaia.Common.Net.Http.RequestModifier
{
	public interface IHttpHeaderModifier
	{
		string HeaderKey { get; }
		string ModifyHeaderValue (string originalValue);
	}
}

