using System;
using System.Net;
using System.Linq;
using Gaia.Common.Execute.Control;
using Gaia.Common.Net.Http.RequestModifier;
using System.Collections.Generic;
using System.IO;
using Gaia.Common.Net.Http.ResponseParser;
using System.Web;
using Gaia.Common.Collections;

namespace Gaia.Common.Net.Http
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

            if (method != HttpMethod.GET)
            {
                Stream stream = request.GetRequestStream();
                foreach (var modifier in modifiers)
                {
                    modifier.WriteBodyContent(stream);
                }
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

        public static KeyValuePairList<string, string> QueryToKeyValuePair(string query)
        {
            KeyValuePairList<string, string> rtn = new KeyValuePairList<string, string>();
            query = query ?? "";
            string[] pairList = query.Split('&');
            foreach (string pair in pairList)
            {
                int index = pair.IndexOf("=");
                if (index >= 0 && index < pair.Length)
                {
                    string key = HttpUtility.UrlDecode(pair.Substring(0, index));
                    string value = HttpUtility.UrlDecode(pair.Substring(index + 1));//TODO: check range, decode
                    rtn.Add(key, value);
                }
            }
            return rtn;
        }

        public static string KeyValuePairToQuery(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            StringWriter sw = new StringWriter();
            bool firstFlag = true;
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                if (firstFlag)
                {
                    firstFlag = false;
                }
                else
                {
                    sw.Write("&");
                }
                sw.Write(HttpUtility.UrlEncode(parameter.Key)); //TODO: query parameter encoding
                sw.Write("=");
                sw.Write(HttpUtility.UrlEncode(parameter.Value));
            }
            return sw.ToString();
        }
	}
}

