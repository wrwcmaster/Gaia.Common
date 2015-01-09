using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.Net.Http.RequestModifier
{
    public class HttpHeaderModifier : IHttpHeaderModifier
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

        public HttpHeaderModifier(string headerKey, string headerValue)
        {
            HeaderKey = headerKey;
            HeaderValue = headerValue;
        }
    }
}
