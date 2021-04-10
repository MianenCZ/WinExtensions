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

        public static IEnumerable<T> ExceptLast<T>(this IEnumerable<T> queries, int cut = 1)
        {
            T[] enumerable = queries as T[] ?? queries.ToArray();
            return enumerable.Take(enumerable.Length - cut);
        }

        public static IEnumerable<(int index, T value)> Indexed<T>(this IEnumerable<T> input, int begin = 0)
        {
            int counter = begin;
            foreach (var value in input)
            {
                yield return (begin++, value);
            }
        }
    }
}
