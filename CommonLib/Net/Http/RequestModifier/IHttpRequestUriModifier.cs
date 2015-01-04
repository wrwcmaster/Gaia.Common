using System;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
	public interface IHttpRequestUriModifier
	{
		Uri ModifyUri(Uri originalUri);
	}
}

