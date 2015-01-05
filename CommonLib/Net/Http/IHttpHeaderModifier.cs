using System;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
	public interface IHttpHeaderModifier
	{
		string HeaderKey { get; }
		string ModifyHeaderValue (string originalValue);
	}
}

