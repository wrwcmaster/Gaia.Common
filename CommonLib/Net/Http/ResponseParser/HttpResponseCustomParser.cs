using Gaia.Common.Execute.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.Net.Http.ResponseParser
{
    public class HttpResponseCustomParser<T> : IHttpResponseParser<T>
    {
        private Func<HttpWebResponse, IExecutionControl, T> _parserFunc;
        public HttpResponseCustomParser(Func<HttpWebResponse, IExecutionControl, T> parserFunc)
        {
            _parserFunc = parserFunc;
        }

        public T ParseHttpResponse(HttpWebResponse response, IExecutionControl control)
        {
            return _parserFunc(response, control);
        }
    }
}
