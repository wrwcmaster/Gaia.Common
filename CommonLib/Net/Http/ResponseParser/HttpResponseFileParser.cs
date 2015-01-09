using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaia.Common.Text;

namespace Gaia.Common.Net.Http.ResponseParser
{
    public class HttpResponseFileParser : IHttpResponseParser<HttpResponseFileParser.TempFileInfo>
    {
        public HttpResponseFileParser()
        {

        }

        public HttpResponseFileParser.TempFileInfo ParseHttpResponse(System.Net.HttpWebResponse response, Execute.Control.IExecutionControl control)
        {
            string tempFile = Path.GetTempFileName();
            string fileName = response.Headers.Get("Content-Disposition").Extract("filename=\"", "\"");
            using (FileStream fs = new FileStream(tempFile, FileMode.Create))
            {
                using (Stream s = response.GetResponseStream())
                {
                    s.CopyTo(fs); //TODO: support execution control
                }
            }
            return new HttpResponseFileParser.TempFileInfo { TempFilePath = tempFile, PreferredName = fileName };
        }

        public class TempFileInfo
        {
            public string TempFilePath { get; set; }
            public string PreferredName { get; set; }
        }
    }
}
