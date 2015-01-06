using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
    public class HttpRequestSimpleHeaderModifier : AbstractHttpRequestModifier
    {
        public string HeaderKey { get; set; }
        public string HeaderValue { get; set; }
        public HttpRequestSimpleHeaderModifier(string headerKey, string headerValue)
        {
            HeaderKey = headerKey;
            HeaderValue = headerValue;
        }
        public override IList<IHttpHeaderModifier> HeaderModifiers
        {
            get
            {
                return new List<IHttpHeaderModifier>()
                {
                    new HttpHeaderModifier(HeaderKey, HeaderValue)
                };
            }
        }
        public override void WriteBodyContent(System.IO.Stream requestStream)
        {

        }

    }
}
