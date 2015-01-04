using System;
using System.Collections.Generic;
using System.Net;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
    public abstract class AbstractHttpRequestModifier : IHttpRequestModifier
    {
        public virtual IList<IHttpRequestHeaderModifier> HeaderModifiers
        {
            get
            {
                return new List<IHttpRequestHeaderModifier>();
            }
        }

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

        public virtual HttpWebRequest PostProcessRequest(HttpWebRequest request)
        {
            return request;
        }

        #endregion

        #region IHttpRequestBodyModifier implementation

        public abstract void WriteBodyContent(System.IO.Stream requestStream);

        #endregion

        #region IHttpRequestUriModifier implementation

        public Uri ModifyUri(Uri originalUri)
        {
            return originalUri;
        }

        #endregion

        public AbstractHttpRequestModifier()
        {
        }

        private void ApplyHeaderModifier(HttpWebRequest request, IHttpRequestHeaderModifier modifier)
        {
            if(modifier == null) return;
            string key = modifier.HeaderKey;
            if (!String.IsNullOrWhiteSpace(key))
            {
                string value = request.Headers.Get(key);
                request.Headers.Set(key, modifier.ModifyHeaderValue(value));
            }
        }

    }
}

