using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
    public class HttpRequestHeaderModifier : IHttpRequestHeaderModifier
    {

        public string HeaderKey
        {
            get;
            set;
        }

        public string HeaderValue
        {
            get;
            set;
        }

        public string ModifyHeaderValue(string originalValue)
        {
            return HeaderValue;
        }

        public HttpRequestHeaderModifier(string headerKey, string headerValue)
        {
            HeaderKey = headerKey;
            HeaderValue = headerValue;
        }
    }
}
