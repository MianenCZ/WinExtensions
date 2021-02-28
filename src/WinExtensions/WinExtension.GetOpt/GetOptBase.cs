﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FS_Game.Common.Extensions;
using WinExtension.GetOpt.Dtos;
using System.Threading.Tasks;
using WinExtension.Common.Helpers;
using WinExtension.GetOpt.Enums;
using WinExtension.GetOpt.Exceptions;
using WinExtension.GetOpt.Helpers;

namespace WinExtension.GetOpt
{
    public class GetOptBase<TTarget> where TTarget : new()
    {
        private readonly List<OptDefinition<TTarget>> _opts;
        private readonly List<ArgDefinition<TTarget>> _args;

        public bool AllowMixedArgsOpts { get; set; } = true;
        public string ApplicationName { get; set; } = "Application";
        public string TopDescription { get; set; }
        public string BootomDescription { get; set; }

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
            Func<string, TProp> formater)
        {
            var prop = PropertyHelper<TTarget>.GetProperty(selector);

            var arg = new ArgDefinition<TTarget>(prop);
            arg.ArgName = PropertyHelper<TTarget>.GetName(selector);
            arg.formaterInfo = formater.Method;
            arg.formaterTarget = formater.Target;
            this._args.Add(arg);

            return new ArgDefinitionFluentBuilder<TTarget, TProp>(arg);
        }

        private static readonly string Spaces32 = new(' ', 32);
        private static readonly string Spaces30 = new(' ', 30);
        private static readonly string Spaces7 = new(' ', 7);

        public string GenerateUsage()
        {
            StringBuilder bld = new();
            //Generate Usage
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

            bld.Append(usageBld.ToString().Align(80).Indent(Spaces7, false));
            bld.Append(Environment.NewLine);
            bld.AppendLine(this.TopDescription.Align(80));
            bld.Append(Environment.NewLine);
            //Generate Options
            bld.AppendLine("Options:");
            foreach (var opt in this._opts.OrderBy(x => (x.ShortOpt is not null) ? x.ShortOpt : x.LongOpt))
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

            bld.Append(Environment.NewLine);
            //Generate arguments
            bld.AppendLine("Arguments:");
            foreach (var arg in this._args)
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

            bld.Append(Environment.NewLine);
            bld.Append(this.BootomDescription.Align(80));
            return bld.ToString();
        }


        public TTarget GetOpts(string[] args)
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

                if (ArgumentParsingHelper.IsOpt(args[i]))
                {
                    OptDefinition<TTarget> optFound = null;
                    string argMatch = null;
                    if (arg.StartsWith("--"))
                    {
                        optFound = longs.FirstOrDefault(x => arg.Substring(2).StartsWith(x.Key)).Value;
                        if (optFound is null)
                            throw new UnexpectedOptionException(arg);
                        argMatch = $"--{optFound.LongOpt}";
                    }
                    else if (arg.StartsWith("-"))
                    {
                        optFound = shorts.FirstOrDefault(x => arg.Substring(1).StartsWith(x.Key)).Value;
                        if (optFound is null)
                            throw new UnexpectedOptionException(arg);
                        argMatch = $"-{optFound.ShortOpt}";
                    }

                    if (optFound.Argument != OptDefinitionArgument.None)
                    {
                        string argFound = optFound.ParseArgument(args, ref i, argMatch);
                        PropertyHelper<TTarget>.GetProperty(optFound.ArgumentSelector).SetValue(getOptResult, argFound);
                    }


                    PropertyHelper<TTarget>.GetProperty(optFound.Selector).SetValue(getOptResult, true);
                }
                else
                {
                    if(this._args.Count > arg_count)
                    {
                        ArgDefinition<TTarget> argFound = this._args[arg_count];

                        object formated = argFound.formaterInfo.Invoke(argFound.formaterTarget, new object?[] { args[i] });

                        argFound.storageInfo.SetValue(getOptResult, formated);
                        arg_count++;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }

            }

            return getOptResult;
        }
    }
}
