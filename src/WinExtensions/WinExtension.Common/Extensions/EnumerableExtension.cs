using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinExtension.Common.Extensions
{
    public static class EnumerableExtension
    {
        public static string JoinToString<T>(this IEnumerable<T> e, char separator)
        {
            return string.Join(separator, e);
        }

        public static string JoinToString<T>(this IEnumerable<T> e, string separator)
        {
            return string.Join(separator, e);
        }

        public static IEnumerable<T> ExceptLast<T>(this IEnumerable<T> queries, int cut = 1)
        {
            T[] enumerable = queries as T[] ?? queries.ToArray();
            return enumerable.Take(enumerable.Length - cut);
        }
    }
}
