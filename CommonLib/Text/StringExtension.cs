using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.CommonLib.Text
{
    public static class StringExtension
    {
        /// <summary>
        /// Extract string content between beginMatch and endMatch (for first match)<br/>
        /// Usage: "var name = 'Hello world!';".Extract("name = '", "';") => Hello world!
        /// </summary>
        public static string Extract(this string content, string beginMatch, string endMatch)
        {
            var beginIndex = content.IndexOf(beginMatch) + beginMatch.Length;
            if (beginIndex == -1) return null;
            var endIndex = content.IndexOf(endMatch, beginIndex);
            if (endIndex == -1) return null;
            int length = endIndex - beginIndex;
            return content.Substring(beginIndex, length);
        }
    }
}
