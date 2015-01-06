using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
    public class HttpRequestSimpleUriModifier : AbstractHttpRequestModifier
    {
        public string QueryKey { get; set; }
        public string QueryValue { get; set; }
        public HttpRequestSimpleUriModifier(string queryKey, string queryValue)
        {
            QueryKey = queryKey;
            QueryValue = queryValue;
        }

        public override Uri ModifyUri(Uri originalUri)
        {
            UriBuilder builder = new UriBuilder(originalUri);
            var parameters = HttpHelper.QueryToKeyValuePair(builder.Query);
            int insertIndex = 0;
            for (; insertIndex < parameters.Count; insertIndex++)
            {
                KeyValuePair<string, string> pair = parameters[insertIndex];
                if (String.Equals(pair.Key, QueryKey, StringComparison.OrdinalIgnoreCase))
                {
                    parameters.RemoveAt(insertIndex);
                    break;
                }
            }

            KeyValuePair<string, string> newPair = new KeyValuePair<string, string>(QueryKey, QueryValue);
            if (insertIndex < parameters.Count)
            {
                parameters.Insert(insertIndex, newPair);
            }
            else
            {
                parameters.Add(newPair);
            }
            builder.Query = HttpHelper.KeyValuePairToQuery(parameters);
            return builder.Uri;
        }
    }
}
