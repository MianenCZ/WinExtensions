using System;
using System.Linq;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using WinExtension.Common.Helpers;
using WinExtension.GetOpt;

namespace ConsoleApplication3
{

    public class Cmd
    {
        public bool i { get; set; }
        public string in_file { get; set; }

        public bool o { get; set; }
        public string out_file { get; set; }

        public Guid code { get; set; }
    }

    class Program
    {
        // app.exe -i file.in -o file.out 66f6eafd-7b49-48e4-9ee2-f979b272a32b
        static void Main(string[] args)
        {
            var getOpt = CreateGetOpt();
            Console.WriteLine(getOpt.GenerateUsage());
        }

        static GetOptBase<Cmd> CreateGetOpt()
        {
            GetOptBase<Cmd> getOpt = new();
            getOpt.AddOpt(_ => _.i)
                  .HasShortName("i")
                  .HasLongName("in")
                  .WithRequiredArgument(_ => _.in_file)
                  .WithName("input file");

            getOpt.AddOpt(_ => _.o)
                  .HasShortName("o")
                  .HasLongName("out")
                  .WithRequiredArgument(_ => _.out_file)
                  .WithName("output file");

            getOpt.AddArg(_ => _.code, s => Guid.Parse(s))
                  .WithName("Code");
            return getOpt;
        }
    }

    
}