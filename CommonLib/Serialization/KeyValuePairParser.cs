using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.Serialization
{
    public class KeyValuePairParser
    {
        /// <summary>
        /// Convert key-value map to string, like "k1=v1;k2=v2;k3=v3" 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">The dictionary to be converted</param>
        /// <param name="format">The format for each key-value pair, like "%s=%s"</param>
        /// <param name="separator">The divider between key-value pair, like ";"</param>
        /// <returns>Formatted dictionary content as string</returns>
        public static string Compose<TKey, TValue>(Dictionary<TKey, TValue> dictionary, string format, string separator)
        {
            if (dictionary == null) return null;
            if (string.IsNullOrEmpty(format)) format = "%s=%s";
            if (string.IsNullOrEmpty(separator)) separator = ";";
            bool isFirstItem = true;

            StringBuilder sb = new StringBuilder();
            foreach (TKey key in dictionary.Keys)
            {
                if (isFirstItem)
                {
                    isFirstItem = false;
                }
                else
                {
                    sb.Append(separator);
                }

                string item = string.Format(format, key, dictionary[key]);
                sb.Append(item);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Parse key-value pairs and update given dictionary
        /// </summary>
        /// <param name="keyValuePairs">Key-value pair String like "k1=v1;k2=v2;k3=v3"</param>
        /// <param name="dictionary">The dictionary for key-value pairs to update</param>
        /// <param name="separator">Separator like ';'</param>
        /// <returns>Filled dictionary</returns>
        public static Dictionary<string, string> Parse(string keyValuePairs, Dictionary<string, string> dictionary, char separator)
        {
            if (dictionary == null) dictionary = new Dictionary<string, string>();
            string[] keyValuePairList = keyValuePairs.Split(separator); //TODO: support string separator
            foreach (string keyValuePair in keyValuePairList)
            {
                if (!string.IsNullOrEmpty(keyValuePair))
                {
                    int index = keyValuePair.IndexOf("=");
                    if (index > -1)
                    {
                        string key = keyValuePair.Substring(0, index);
                        string value = keyValuePair.Substring(index + 1);
                        dictionary[key] = value;
                    }
                }
            }
            return dictionary;
        }
    }
}
