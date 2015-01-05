using System;
using System.Collections.Generic;
using System.Net;

namespace Gaia.CommonLib.Net.Http.RequestModifier
{
    public abstract class AbstractHttpRequestModifier : IHttpRequestModifier
    {
        public virtual IList<IHttpHeaderModifier> HeaderModifiers
        {
            get
            {
                return new List<IHttpHeaderModifier>();
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

        private void ApplyHeaderModifier(HttpWebRequest request, IHttpHeaderModifier modifier)
        {
            if(modifier == null) return;
            string key = modifier.HeaderKey;
            if (!String.IsNullOrWhiteSpace(key))
            {
                string value = request.Headers.Get(key);
                value =  modifier.ModifyHeaderValue(value);
                switch (key.ToUpper())
                {
                    case "CONTENT-TYPE": request.ContentType = value;
                        break;
                    case "CONTENT-LENGTH": request.ContentLength = Convert.ToInt64(value);
                        break;
                    default:
                        request.Headers.Set(key, value);
                        break;
                }
            }
        }

    }
}

