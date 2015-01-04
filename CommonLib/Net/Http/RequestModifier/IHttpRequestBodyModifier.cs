using System;
using System.IO;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
	public interface IHttpRequestBodyModifier
	{
		void WriteBodyContent(Stream requestStream);
	}
}

