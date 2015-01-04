using System;
using System.Net;
using System.Linq;
using Gaia.CommonLib.Execute.Control;
using Gaia.CommonLib.Net.Http.RequestModifier;
using System.Collections.Generic;
using System.IO;
using Gaia.CommonLib.Net.Http.ResponseParser;

namespace Gaia.CommonLib.Net.Http
{
	public static class HttpHelper
	{
        public static HttpWebRequest SendRequest(Uri uri, HttpMethod method, IList<IHttpRequestModifier> modifiers, IExecutionControl control)
		{
			if (control != null && control.isCancelled ()) return null;
			if(method == null) throw new ArgumentNullException("method");
			uri = ApplyUriModifiers(uri, modifiers);
			if(uri == null) throw new ArgumentNullException("uri");

			HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
			request.Method = method.ToString();
			foreach (var modifier in modifiers)
			{
				request = modifier.PreProcessRequest(request);
			}

			Stream stream = request.GetRequestStream();
			foreach (var modifier in modifiers)
			{
				modifier.WriteBodyContent(stream);
			}

			foreach (var modifier in modifiers)
			{
				request = modifier.PostProcessRequest(request);
			}
            return request;
		}

		public static Uri ApplyUriModifiers(Uri uri, IList<IHttpRequestModifier> modifiers)
		{
			foreach (var modifier in modifiers)
			{
				uri = modifier.ModifyUri(uri);
			}
			return uri;
		}

        public static T ParseResponse<T>(HttpWebRequest request, IHttpResponseParser<T> parser, IExecutionControl control)
        {
            if (request == null || parser == null) return default(T);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return parser.ParseHttpResponse(response, control);
        }

        public static T SendRequest<T>(Uri uri, HttpMethod method, IList<IHttpRequestModifier> modifiers, IHttpResponseParser<T> parser, IExecutionControl control)
        {
            //TODO: calc progress
            //TODO: Exception Handling
            HttpWebRequest request = SendRequest(uri, method, modifiers, control);
            return ParseResponse(request, parser, control);
        }
	}
}

