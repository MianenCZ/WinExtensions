using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinExtension.GetOpt.Exceptions;

namespace WinExtension.GetOpt.Helpers
{
    internal static class DefaultFormatters
    {
        public static Func<string, char> ToCharFormatter()
        {
            return s =>
            {
                if (char.TryParse(s, out var val))
                    return val;

                throw new ArgumentFormatException($"Value '{s}' can't parsed to {typeof(char)}.");
            };
        }

        public static Func<string, byte> ToByteFormatter()
        {
            return s =>
            {
                if (byte.TryParse(s, out var val))
                    return val;

                throw new ArgumentFormatException($"Value '{s}' can't parsed to {typeof(byte)}.");
            };
        }

        public static Func<string, int> ToIntFormatter()
        {
            return s =>
            {
                if (int.TryParse(s, out var val))
                    return val;

                throw new ArgumentFormatException($"Value '{s}' can't parsed to {typeof(int)}.");
            };
        }

        public static Func<string, float> ToFloatFormatter()
        {
            return s =>
            {
                if (float.TryParse(s, out var val))
                    return val;

                throw new ArgumentFormatException($"Value '{s}' can't parsed to {typeof(float)}.");
            };
        }

        public static Func<string, double> ToDoubleFormatter()
        {
            return s =>
            {
                if (double.TryParse(s, out var val))
                    return val;

                throw new ArgumentFormatException($"Value '{s}' can't parsed to {typeof(double)}.");
            };
        }

        public static Func<string, decimal> ToDecimalFormatter()
        {
            return s =>
            {
                if (decimal.TryParse(s, out var val))
                    return val;

                throw new ArgumentFormatException($"Value '{s}' can't parsed to {typeof(decimal)}.");
            };
        }

        public static Func<string, Guid> ToGuidFormatter()
        {
            return s =>
            {
                if (Guid.TryParse(s, out var val))
                    return val;

                throw new ArgumentFormatException($"Value '{s}' can't parsed to {typeof(Guid)}.");
            };
        }


    }
}
