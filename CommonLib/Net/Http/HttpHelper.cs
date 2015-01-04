using System;
using System.Net;
using System.Linq;
using CommonLib.Execute.Control;
using CommonLib.Net.Http.RequestModifier;
using System.Collections.Generic;
using System.IO;

namespace CommonLib.Net.Http
{
	public static class HttpHelper
	{
		public static void SendRequest(Uri uri, HttpMethod method, IList<IHttpRequestModifier> modifiers,IExecutionControl control)
		{
			if (control != null && control.isCancelled ()) return;
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
		}

		public static Uri ApplyUriModifiers(Uri uri, IList<IHttpRequestModifier> modifiers)
		{
			foreach (var modifier in modifiers)
			{
				uri = modifier.ModifyUri(uri);
			}
			return uri;
		}
	}
}

