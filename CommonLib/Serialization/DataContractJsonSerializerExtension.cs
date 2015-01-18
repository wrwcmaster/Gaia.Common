using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.Serialization
{
    public static class DataContractJsonSerializerExtension
    {
        public static object Deserialize(this DataContractJsonSerializer serializer, string jsonString)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    sw.Write(jsonString);
                    sw.Flush();
                    ms.Position = 0;
                    return serializer.ReadObject(ms);
                }
            }
        }

        public static string Serialize(this DataContractJsonSerializer serializer, object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
