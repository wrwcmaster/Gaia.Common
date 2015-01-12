using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gaia.Common.Text;
using Gaia.Common.IO;

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
            string fileName = Path.GetFileName(tempFile);
            string contentDisposition = response.Headers.Get("Content-Disposition");
            foreach(string part in contentDisposition.Split(';')){
                string trimedPart = part.Trim();
                if(trimedPart.StartsWith("filename=")){
                    fileName = trimedPart.Substring(9).Trim('"');
                    break;
                }
            }
            using (FileStream fs = new FileStream(tempFile, FileMode.Create))
            {
                long size = response.ContentLength;
                using (Stream s = response.GetResponseStream())
                {
                    s.CopyTo(fs, size, control); 
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
