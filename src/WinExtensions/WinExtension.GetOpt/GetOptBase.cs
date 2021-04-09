using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using WinExtension.Common.Extensions;
using WinExtension.Common.Helpers;
using WinExtension.GetOpt.Dtos;
using WinExtension.GetOpt.Enums;
using WinExtension.GetOpt.Exceptions;
using WinExtension.GetOpt.Helpers;

namespace WinExtension.GetOpt
{
    public class GetOptBase<TTarget> where TTarget : new()
    {
        internal readonly List<OptDefinition<TTarget>> _opts;
        internal readonly List<ArgDefinition<TTarget>> _args;

        public bool AllowMixedArgsOpts { get; set; } = true;
        public string ApplicationName { get; set; } = "Application";
        public string TopDescription { get; set; }
        public string BottomDescription { get; set; }

        public MultiCharShort MultiCharShort { get; set; }

        public GetOptBase()
        {
            _opts = new();
            _args = new();
        }

        public IOptDefinitionFluentBuilder<TTarget> AddOpt(Expression<Func<TTarget, bool>> selector)
        {
            var opt = new OptDefinition<TTarget>(selector);
            this._opts.Add(opt);
            return new OptDefinitionFluentBuilder<TTarget>(opt);
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> AddArg<TProp>(
            Expression<Func<TTarget, TProp>> selector,
            Func<string, TProp> formatter)
        {
            var arg = new ArgDefinition<TTarget>();
            arg.ArgName = PropertyHelper<TTarget>.GetName(selector);
            this._args.Add(arg);

            return new ArgDefinitionFluentBuilder<TTarget, TProp>(arg, selector, formatter);
        }

        public IOptDefinitionFluentBuilder<TTarget> AddHelp(bool ExitOnHelp = true)
        {
            return this.AddOpt(null)
                .HasLongName("help")
                .HasShortName("h")
                .HasDescription("Show this help")
                .WithOptionalArgument(null)
                .FormattedAs(OptDefinitionArgumentFormat.NextArg)
                .AddRawTrigger((s, s1) =>
                {

                    if (s1 is null)
                    {
                        Console.WriteLine(this.GenerateUsage());
                    }
                    else
                    {
                        StringBuilder bld = new StringBuilder();
                        var opts = this._opts.Where(x => x.LongOpt == s1 || x.ShortOpt == s1);

                        if (!opts.Any())
                        {
                            Console.WriteLine(this.GenerateUsage());
                        }
                        else
                        {
                            Console.WriteLine(this.FormatUsage().ToString().Align(80));
                            foreach (var opt in opts)
                            {
                                FormatOption(bld, opt);
                            }
                            Console.WriteLine(bld.ToString().Align(80));
                        }
                    }
                    
                    if(ExitOnHelp)
                        Environment.Exit(0);
                });
        }

        private static readonly string Spaces32 = new(' ', 32);
        private static readonly string Spaces30 = new(' ', 30);
        private static readonly string Spaces7 = new(' ', 7);

        public string GenerateUsage()
        {
            StringBuilder bld = new();
            //Generate Usage

            var usageBld = FormatUsage();

            bld.Append(usageBld.ToString().Align(80).Indent(Spaces7, false));
            bld.Append(Environment.NewLine);
            bld.AppendLine(this.TopDescription.Align(80));
            bld.Append(Environment.NewLine);

            //Generate Options
            bld.AppendLine("Options:");
            foreach (OptDefinition<TTarget> opt in this._opts.OrderBy(x => (x.ShortOpt is not null) ? x.ShortOpt : x.LongOpt))
            {
                FormatOption(bld, opt);
            }

            bld.Append(Environment.NewLine);
            //Generate arguments
            bld.AppendLine("Arguments:");
            foreach (ArgDefinition<TTarget> arg in this._args)
            {
                FormatArgument(bld, arg);
            }

            bld.Append(Environment.NewLine);
            bld.Append(this.BottomDescription.Align(80));
            return bld.ToString();
        }

        private StringBuilder FormatUsage()
        {
            StringBuilder usageBld = new();
            usageBld.AppendFormat("Usage: {0} ", this.ApplicationName);
            foreach (var arg in this._opts.Where(x => x.IsRequired))
            {
                usageBld.AppendFormat("{0}{1} ", arg.Verbose, arg.VerboseArg);
            }

            if (this._opts.Any(x => !x.IsRequired))
            {
                usageBld.Append("[OPTIONS]... ");
            }

            foreach (var arg in this._args)
            {
                if (arg.IsRequired)
                    usageBld.AppendFormat("{0}", arg.ArgName.ToMacroCase());
                else
                    usageBld.AppendFormat("[{0}]", arg.ArgName.ToMacroCase());
                if (arg.IsVariadic)
                    usageBld.Append("...");
                usageBld.Append(' ');
            }

            return usageBld;
        }

        private static void FormatOption(StringBuilder bld, OptDefinition<TTarget> opt)
        {
            StringBuilder leftBld = new StringBuilder(30);
            if (opt.ShortOpt is not null)
                leftBld.AppendFormat("  -{0}", opt.ShortOpt);
            else
                leftBld.AppendFormat("    ");
            if (opt.LongOpt is not null)
                leftBld.AppendFormat("  --{0}", opt.LongOpt);
            leftBld.Append(opt.VerboseArg);
            string left = leftBld.ToString();
            if (left.Length < 30)
            {
                bld.Append(left.PadRight(30));
            }
            else
            {
                bld.AppendLine(left);
                bld.Append(Spaces30);
            }

            string right = opt.Description.Align(48).Indent(Spaces32, false);

            bld.Append(right);
            bld.Append(Environment.NewLine);
            if (opt.IncompatibleWith.Any())
            {
                string str = $"Incompatible with {string.Join(", ", opt.IncompatibleWith)}.";
                bld.Append(str.Align(48).Indent(Spaces32, true));
                bld.Append(Environment.NewLine);
            }

            if (opt.Requires.Any())
            {
                string str = $"Requires {string.Join(", ", opt.Requires)}.";
                bld.Append(str.Align(48).Indent(Spaces32, true));
                bld.Append(Environment.NewLine);
            }
        }

        private static void FormatArgument(StringBuilder bld, ArgDefinition<TTarget> arg)
        {
            string left = $"  {arg.ArgName.ToMacroCase()}";
            if (left.Length < 30)
            {
                bld.Append(left.PadRight(30));
            }
            else
            {
                bld.AppendLine(left);
                bld.Append(Spaces30);
            }

            string right = arg.Description.Align(48).Indent(Spaces32, false);
            bld.Append(right);
            bld.Append(Environment.NewLine);
        }

        public TTarget GetOpts(params string[] args)
        {
            try
            {
                return GetOptsExecute(args);
            }
            catch (UnexpectedOptionException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private TTarget GetOptsExecute(string[] args)
        {
            if (args is null)
                throw new ArgumentNullException(nameof(args));
            if (args.Any(x => x is null))
                throw new ArgumentNullException(nameof(args) + "[]");


            Dictionary<string, OptDefinition<TTarget>> shorts =
                _opts
                    .Where(x => x.ShortOpt is not null)
                    .ToDictionary(x => x.ShortOpt);
            Dictionary<string, OptDefinition<TTarget>> longs =
                _opts
                    .Where(x => x.LongOpt is not null)
                    .ToDictionary(x => x.LongOpt);

            TTarget getOptResult = new TTarget();
            int arg_count = 0;

            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                bool commaStartError = false;

                //If is opt
                if (ArgumentParsingHelper.IsOpt(args[i]))
                {
                    (OptDefinition<TTarget> optFound, string argMatch) = FindOpt(arg);
                    var tmp= FindOpt(arg);


                    if (optFound is not null)
                    {
                        if (optFound.Argument != OptDefinitionArgument.None)
                        {
                            string argFound = optFound.ParseArgument(args, ref i, argMatch);
                            if (argFound is not null)
                            {
                                if (optFound.Selector is not null)
                                    getOptResult.SetPropertyValue(optFound.ArgumentSelector, argFound);
                            }

                            optFound.OnRawMatched?.Invoke(argMatch, argFound);
                        }

                        //Store value into result
                        if(optFound.Selector is not null)
                            getOptResult.SetPropertyValue(optFound.Selector, true);

                        optFound.OnMatched?.Invoke(getOptResult);
                        continue;
                    }
                    else
                    {
                        commaStartError = true;
                    }

                }

                // The it is an argument
                if (this._args.Count > arg_count)
                {
                    ArgDefinition<TTarget> argFound = this._args[arg_count];
                    if (!argFound.IncludeStartingWithComma && commaStartError)
                    {
                        // arg is of format -... but argument does not accept it
                        throw new UnexpectedOptionException(arg);
                    }
                    //Call setter to store value
                    argFound.Setter(getOptResult, args[i]);
                    arg_count++;
                }
                else
                {
                    if(commaStartError)
                        throw new UnexpectedOptionException(arg);
                    throw new NotImplementedException();
                }

            }

            return getOptResult;

            //matches options against long and the against short set of options
            (OptDefinition<TTarget> optFound, string argMatch) FindOpt(string arg)
            {
                OptDefinition<TTarget> optFound;

                if (arg.StartsWith("--"))
                {
                    optFound = longs.FirstOrDefault(x => arg.Substring(2).StartsWith(x.Key)).Value;
                    if (optFound is not null)
                        return (optFound, $"--{optFound.LongOpt}");
                }
                else if (arg.StartsWith("-"))
                {
                    optFound = shorts.FirstOrDefault(x => arg.Substring(1).StartsWith(x.Key)).Value;
                    if (optFound is not null)
                        return (optFound, $"-{optFound.ShortOpt}");
                }
                return (null, null);
            }
        }
    }
}
