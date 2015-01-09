using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.Net.Http.ResponseParser
{
    public class HttpResponseStringParser : IHttpResponseParser<string>
    {
        Encoding Encoding { get; set; }
        public HttpResponseStringParser(Encoding encoding)
        {
            Encoding = encoding;
        }

        public HttpResponseStringParser()
            : this(UTF8Encoding.UTF8)
        {
            
        }
        public string ParseHttpResponse(System.Net.HttpWebResponse response, Execute.Control.IExecutionControl control)
        {
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s, Encoding))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
