using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.Collections
{
    public static class IEnumerableExtension
    {
        public static IEnumerable GetSafeEnumerable(this IEnumerable obj)
        {
            if (obj == null) return new object[0];
            else return obj;
        }

        public static IEnumerable<T> GetSafeEnumerable<T>(this IEnumerable<T> obj)
        {
            if (obj == null) return new T[0];
            else return obj;
        }
    }
}
