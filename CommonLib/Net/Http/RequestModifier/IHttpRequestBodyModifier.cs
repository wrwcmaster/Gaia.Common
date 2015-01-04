using System;
using System.IO;

namespace CommonLib.Net.Http.RequestModifier
{
	public interface IHttpRequestBodyModifier
	{
		void WriteBodyContent(Stream requestStream);
	}
}

