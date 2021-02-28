using System;
using System.Linq;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            var mi = typeof(A).GetMethod("Foo");

            Console.WriteLine("Regular method");
            Time(() => new A().Foo(1, 2));
            Console.WriteLine();

            Console.WriteLine("Invoke");
            Time(() => mi.Invoke(new A(), new object[] { 1, 2 }));
            Console.WriteLine();

            Console.WriteLine("Cached Delegate");
            var cache = Compile<Action<A, int, int>>(mi);
            Time(() => cache.Invoke(new A(), 1, 2));
            Console.WriteLine();

            Console.WriteLine("Dynamic call site caching");
            Time(() => { dynamic d = new A(); d.Foo(1, 2); });
            Console.WriteLine();

            Console.WriteLine("Open Delegate");
            var od = CreateOpenDelegate(mi);
            Time(() => od.Invoke(new A(), 1, 2));
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

        private static TDelegate Compile<TDelegate>(MethodInfo mi)
        {
            ParameterExpression @this = null;
            if (!mi.IsStatic)
            {
                @this = Expression.Parameter(mi.DeclaringType, "this");
            }

            var parameters = new List<ParameterExpression>();
            if (@this != null)
            {
                parameters.Add(@this);
            }

            foreach (var parameter in mi.GetParameters())
            {
                parameters.Add(Expression.Parameter(parameter.ParameterType, parameter.Name));
            }

            Expression call = null;
            if (@this != null)
            {
                call = Expression.Call(@this, mi, parameters.Skip(1));
            }
            else
            {
                call = Expression.Call(mi, parameters);
            }

            return Expression.Lambda<TDelegate>(call, parameters).Compile();
        }

        private static Action<A, int, int> CreateOpenDelegate(MethodInfo mi)
        {
            var openDelegate = Delegate.CreateDelegate(
                typeof(Action<A, int, int>),
                null,
                mi,
                true);

            return (Action<A, int, int>)openDelegate;
        }
    }

    public class A
    {
        public void Foo(int a, int b)
        {
        }
    }
}