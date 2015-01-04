using System;
using System.Collections.Generic;
using System.Net;

namespace CommonLib.Net.Http.RequestModifier
{
    public class AbstractHttpRequestModifier : IHttpRequestModifier
    {
        public IList<IHttpRequestHeaderModifier> HeaderModifiers { get; }

        #region IHttpRequestModifier implementation

        public HttpWebRequest PreProcessRequest(HttpWebRequest request)
        {
            if (request != null && HeaderModifiers != null)
            {
                foreach (var modifier in HeaderModifiers)
                {
                    ApplyHeaderModifier(request, modifier);
                }
            }
            return request;
        }

        public abstract HttpWebRequest PostProcessRequest(HttpWebRequest request);

        #endregion

        #region IHttpRequestBodyModifier implementation

        public abstract void WriteBodyContent(System.IO.Stream requestStream);

        #endregion

        #region IHttpRequestUriModifier implementation

        public abstract Uri ModifyUri(Uri originalUri);

        #endregion

        public AbstractHttpRequestModifier()
        {
        }

        private void ApplyHeaderModifier(HttpWebRequest request, IHttpRequestHeaderModifier modifier)
        {
            if(modifier == null) return request;
            string key = modifier.HeaderKey;
            if (!String.IsNullOrWhiteSpace(key))
            {

            }
        }

    }
}

