using FS_Game.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FS_Game.Common.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Joins collection of strings and separators them by <paramref name="separator"/>.
        /// Result is same as using <see cref="string.Join(string, IEnumerable{string})"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinToString(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }
        /// <summary>
        /// Joins collection of <typeparamref name="T"/> objects and separators them by <paramref name="separator"/>.
        /// Result is same as using <see cref="string.Join(string, IEnumerable{string})"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinToString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source.Select(x => x?.ToString()));
        }
        //TODO: Add documentation
        public static string ToLowerCaseFirstLetter(this string str) =>
            ToUpperCaseFirstLetter(str, CultureInfo.InvariantCulture);
        //TODO: Add documentation
        public static string ToLowerCaseFirstLetter(this string str, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            var letters = str.ToCharArray();
            letters[0] = char.ToLower(letters[0], cultureInfo);
            return new string(letters);
        }
        //TODO: Add documentation
        public static string ToUpperCaseFirstLetter(this string str) =>
            ToUpperCaseFirstLetter(str, CultureInfo.InvariantCulture);
        //TODO: Add documentation
        public static string ToUpperCaseFirstLetter(this string str, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            var letters = str.ToCharArray();
            letters[0] = char.ToUpper(letters[0], cultureInfo);
            return new string(letters);
        }
        /// <summary>
        /// Convert string to alphanumeric representation according with POSIX standard
        /// </summary>
        /// <remarks>
        /// From input string remove all non digit [0-9] and non [A-Z] letter.
        /// Lower letter [a-z] converting to upper form [A-Z].
        /// For null input string return null.
        /// </remarks>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToAlphaNumeric(this string str)
        {
            if (str is null)
                return null;
            string alphaNumericString = "";
            foreach (char c in str)
            {
                // [0-9]
                if (c >= 0x30 && c <= 0x39)
                {
                    alphaNumericString += c;
                }
                // [A-Z]
                else if (c >= 0x41 && c <= 0x5A)
                {
                    alphaNumericString += c;
                }
                // [a-z] => [A-Z]
                else if (c >= 0x61 && c <= 0x7A)
                {
                    alphaNumericString += c - 0x20;
                }
                else
                {
                    // Skip others characters
                }
            }
            return alphaNumericString;
        }
        //TODO: Add documentation
        public static string ToKebabCase(this string source) => ToKebabCase(source, CultureInfo.InvariantCulture);
        //TODO: Add documentation
        public static string ToKebabCase(this string source, CultureInfo info)
        {
            if (source is null)
                return null;
            if (source.Length == 0)
                return string.Empty;
            return source.ToLower().SplitBy(x => char.IsWhiteSpace(x), StringSplitOptions.RemoveEmptyEntries).JoinToString("-");
        }
        //TODO: Add documentation
        public static string ToSnakeCase(this string source) => ToSnakeCase(source, CultureInfo.InvariantCulture);
        //TODO: Add documentation
        public static string ToSnakeCase(this string source, CultureInfo info)
        {
            if (source is null)
                return null;
            if (source.Length == 0)
                return string.Empty;
            return source.ToLower().SplitBy(x => char.IsWhiteSpace(x), StringSplitOptions.RemoveEmptyEntries).JoinToString("_");
        }
        //TODO: Add documentation
        public static string ToMacroCase(this string source) => ToMacroCase(source, CultureInfo.InvariantCulture);
        //TODO: Add documentation
        public static string ToMacroCase(this string source, CultureInfo info)
        {
            if (source is null)
                return null;
            if (source.Length == 0)
                return string.Empty;
            return source.ToUpper().SplitBy(x => char.IsWhiteSpace(x), StringSplitOptions.RemoveEmptyEntries).JoinToString("_");
        }
        //TODO: Add documentation
        public static string ToCamelCase(this string source) => ToCamelCase(source, CultureInfo.InvariantCulture);
        //TODO: Add documentation
        public static string ToCamelCase(this string source, CultureInfo info)
        {
            return source.ToPascalCase(info).ToLowerCaseFirstLetter(info);
        }
        //TODO: Add documentation
        public static string ToPascalCase(this string source) => ToPascalCase(source, CultureInfo.InvariantCulture);
        //TODO: Add documentation
        public static string ToPascalCase(this string source, CultureInfo info)
        {
            return source
                .SplitBy(c => !char.IsLetterOrDigit(c), StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower(info).ToUpperCaseFirstLetter(info))
                .JoinToString("");
        }
        //TODO: Add documentation
        public static string[] SplitBy(this string source, Predicate<char> spliterPredicate)
            => SplitBy(source, spliterPredicate, StringSplitOptions.None);
        //TODO: Add documentation
        public static string[] SplitBy(this string source, Predicate<char> spliterPredicate, StringSplitOptions stringSplitOptions)
        {
            Guard.IsNotNull(source, nameof(source));
            Guard.IsNotNull(spliterPredicate, nameof(spliterPredicate));
            List<string> result = new List<string>();
            StringBuilder bld = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                if (spliterPredicate(source[i]))
                {
                    string word = bld.ToString();
                    if (!string.IsNullOrEmpty(word) || stringSplitOptions != StringSplitOptions.RemoveEmptyEntries)
                    {
                        result.Add(word);
                    }
                    bld.Clear();
                }
                else
                {
                    bld.Append(source[i]);
                }
            }
            string rest = bld.ToString();
            if (!string.IsNullOrEmpty(rest))
            {
                result.Add(rest);
            }
            return result.ToArray();
        }
        //TODO: Add documentation
        public static string Align(this string source, int charactersPerLine)
        {
            Guard.IsNotNull(source, nameof(source));
            StringBuilder bld = new StringBuilder();
            bool firstParagraph = true;
            foreach (var paragraph in source.Split(Environment.NewLine))
            {
                int charsOnLine = 0;
                bool firstChar = true;
                firstParagraph.ExceptFirst(() => bld.Append(Environment.NewLine));
                foreach (var word in paragraph.SplitBy(x => char.IsWhiteSpace(x)))
                {
                    if (charsOnLine + word.Length + 1 <= charactersPerLine)
                    {
                        firstChar.ExceptFirst(() => bld.Append(' '));
                        bld.Append(word);
                        charsOnLine = charsOnLine + word.Length + 1;
                    }
                    else
                    {
                        bld.Append(Environment.NewLine);
                        bld.Append(word);
                        charsOnLine = word.Length;
                    }
                }
            }
            return bld.ToString();
        }
        public static string Indent(this string source, string indentation = "\t", bool includeFirstLine = true)
        {
            string prefix = (includeFirstLine) ? indentation : "";
            return prefix + source.Replace(Environment.NewLine, Environment.NewLine + indentation);
        }
    }
}