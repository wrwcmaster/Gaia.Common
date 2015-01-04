using System;

namespace CommonLib.Net.Http.RequestModifier
{
	public interface IHttpRequestUriModifier
	{
		Uri ModifyUri(Uri originalUri);
	}
}

