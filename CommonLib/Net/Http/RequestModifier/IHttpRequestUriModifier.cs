using System;

namespace Gaia.Common.Net.Http.RequestModifier
{
	public interface IHttpRequestUriModifier
	{
		Uri ModifyUri(Uri originalUri);
	}
}

