using Gaia.Common.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.Net.Http.ResponseParser
{
    public class HttpResponseJSONObjectParser<T> : IHttpResponseParser<T>
    {
        
        public HttpResponseJSONObjectParser()
        {
            
        }

        public T ParseHttpResponse(System.Net.HttpWebResponse response, Execute.Control.IExecutionControl control)
        {
            /*using (Stream s = response.GetResponseStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(s);
            }*/
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                string responseStr = sr.ReadToEnd();
                string json = responseStr;
                try
                {
                    return (T)new DataContractJsonSerializer(typeof(T)).Deserialize(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to parse json response");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(json);
                    throw;
                }

            }
        }
    }
}
