using Gaia.Common.Execute.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.Net.Http.ResponseParser
{
    public interface IHttpResponseParser<T>
    {
        T ParseHttpResponse(HttpWebResponse response, IExecutionControl control);
    }
}
