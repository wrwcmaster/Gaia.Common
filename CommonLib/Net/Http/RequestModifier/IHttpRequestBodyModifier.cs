using System;
using System.IO;

namespace Gaia.Common.Net.Http.RequestModifier
{
	public interface IHttpRequestBodyModifier
	{
		void WriteBodyContent(Stream requestStream);
	}
}

