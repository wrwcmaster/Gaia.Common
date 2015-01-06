using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
    public sealed class HttpRequestUrlEncodedFormModifier : AbstractHttpRequestModifier
    {

        private IEnumerable<KeyValuePair<string, string>> _parameters;
        public Encoding Encoding { get; set; }
        public HttpRequestUrlEncodedFormModifier(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            Encoding = UTF8Encoding.UTF8;
            _parameters = parameters;
        }
        
        public override IList<IHttpHeaderModifier> HeaderModifiers
        {
            get
            {
                return new List<IHttpHeaderModifier>(){
                    new HttpHeaderModifier("Content-Type", "application/x-www-form-urlencoded")
                };
            }
        }

        public override void WriteBodyContent(Stream requestStream)
        {
            if (_parameters != null)
            {
                string text = HttpHelper.KeyValuePairToQuery(_parameters);
                WriteText(requestStream, text);
            }
        }

        private void WriteText(Stream requestStream, string text)
        {
            WriteText(requestStream, text, Encoding);
        }
    }
}
