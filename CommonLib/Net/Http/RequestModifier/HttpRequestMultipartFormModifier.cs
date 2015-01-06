using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
    public sealed class HttpRequestMultipartFormModifier : AbstractHttpRequestModifier
    {
        public class FileInfo
        {
            public string FileName { get; set; }
            public Func<Stream> FileStreamProvider { get; set; }

            public FileInfo(string filePath)
            {
                if (filePath == null) throw new ArgumentNullException("filePath");
                FileName = Path.GetFileName(filePath);
                FileStreamProvider = () => new FileStream(filePath, FileMode.Open);
            }

            public FileInfo(FileStream fileStream)
            {
                if (fileStream == null) throw new ArgumentNullException("fileStream");
                FileName = Path.GetFileName(fileStream.Name);
                FileStreamProvider = () => fileStream;
            }
        }

        private IEnumerable<KeyValuePair<string, string>> _parameters;
        private IEnumerable<KeyValuePair<string, FileInfo>> _fileFields;
        private String _boundary;
        private String BoundaryMultiPart { get { return "--" + _boundary; } }
        private String BoundaryEnd { get { return "--" + _boundary + "--"; } }
        public Encoding Encoding { get; set; }
        public HttpRequestMultipartFormModifier(IEnumerable<KeyValuePair<string, string>> parameters, IEnumerable<KeyValuePair<string, FileInfo>> fileFields)
        {
            Encoding = UTF8Encoding.UTF8;
            _boundary = System.Guid.NewGuid().ToString();
            _parameters = parameters;
            _fileFields = fileFields;
        }
        
        public override IList<IHttpHeaderModifier> HeaderModifiers
        {
            get
            {
                return new List<IHttpHeaderModifier>(){
                    new HttpHeaderModifier("Content-Type", "multipart/form-data; boundary=" + _boundary)
                };
            }
        }

        public override void WriteBodyContent(Stream requestStream)
        {
            WriteParamBodyParts(requestStream);
            WriteFileBodyParts(requestStream);
            WriteText(requestStream, BoundaryEnd);
        }

        private void WriteText(Stream requestStream, string text)
        {
            WriteText(requestStream, text, Encoding);
        }

        private void WriteParamBodyParts(Stream requestStream)
        {
            if (_parameters == null) return;
            foreach(var parameter in _parameters){
                WriteText(requestStream, String.Format("{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", BoundaryMultiPart, parameter.Key ?? "", parameter.Value ?? ""));
            }
        }

        private void WriteFileBodyParts(Stream requestStream)
        {
            if (_fileFields == null) return;
            foreach (var fileField in _fileFields)
            {
                if(fileField.Value != null){
                    string fieldName = fileField.Key;
                    string fileName = fileField.Value.FileName;
                    WriteText(requestStream, String.Format("{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: application/octet-stream; charset={3}\r\n\r\n", BoundaryMultiPart, fileField.Key ?? "", fileName ?? "TempFile", Encoding.WebName));
                    using(Stream fileStream = fileField.Value.FileStreamProvider.Invoke()){
                        fileStream.CopyTo(requestStream); //TODO: support IExecutionControl
                    }
                    WriteText(requestStream, "\r\n");
                }            
            }
        }
    }
}
