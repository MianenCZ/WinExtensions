using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WinExtension.Panel.Providers;

namespace WinExtension.Panel
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SystemInformation.GetAllCounters();

            await Task.Delay(-1);
        }
    }
}
