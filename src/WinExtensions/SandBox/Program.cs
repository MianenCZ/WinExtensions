using System;
using System.Linq;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using WinExtension.Common.Helpers;

namespace ConsoleApplication3
{
    class Program
    {
        static A data = new A();

        static void Main(string[] args)
        {
            var mi = typeof(A).GetMethod("Foo");

            Console.WriteLine("Regular method");
            Time(() => data.Property = 2);
            Console.WriteLine();

            Console.WriteLine("PropertyInfo");
            Time(() => Set<A, int>(_ => _.Property, 1));
            Console.WriteLine();

            Console.WriteLine("Expression");
            Time(() => data.SetPropertyValue(_ => _.Property, 1));
            Console.WriteLine();
            
        }

        private static void Time(Action a)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 10000000; i++)
            {
                a();
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }

        public static void Set<T, TValue>(Expression<Func<T, TValue>> setter, int value)
        {
            var prop = PropertyHelper<T>.GetProperty(setter);
            prop.SetValue(data, value);
        }

        public static void fce<TValue>(Func<string, TValue> form)
        {

        }
    }

    public class A
    {
        public int Property { get; set; }
    }
}