using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
    public sealed class HttpRequestJSONModifier<T> : AbstractHttpRequestModifier
    {
        public T Parameter { get; set; }
        public HttpRequestJSONModifier(T parameter)
        {
            Parameter = parameter;
        }
        
        public override IList<IHttpHeaderModifier> HeaderModifiers
        {
            get
            {
                return new List<IHttpHeaderModifier>(){
                    new HttpHeaderModifier("Content-Type", "application/json")
                };
            }
        }

        public override void WriteBodyContent(Stream requestStream)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(requestStream, Parameter);
        }
    }
}
