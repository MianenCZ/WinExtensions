using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinExtension.GetOpt.Dtos;
using WinExtension.GetOpt.Enums;
using WinExtension.GetOpt.Exceptions;

namespace WinExtension.GetOpt.Helpers
{
    public static class ArgumentParsingHelper
    {
        public static bool IsOpt(string str)
        {
            return str.StartsWith("-");
        }

        public static string ParseArgument<T>(
            this OptDefinition<T> opt,
            string[] args,
            ref int i,
            string match) where T : new()
        {
            string arg = args[i];
            switch (opt.ArgumentFormat)
            {
                case OptDefinitionArgumentFormat.None:
                {
                    throw new GetOptException($"{OptDefinitionArgumentFormat.None} can't be parsed");
                }
                case OptDefinitionArgumentFormat.NextArg:
                {
                    if (args.Length <= i + 1)
                        throw new MissingArgumentException(opt.ArgumentName);
                    return args[++i];
                }
                case OptDefinitionArgumentFormat.EqualSign:
                {
                    var r = arg.Substring(match.Length);
                    if (r.StartsWith("="))
                        return r.Substring(1);
                    throw new ArgumentFormatException(
                        $"value '{args[i]}' can't be matched as '{opt.ArgumentName}' {OptDefinitionArgumentFormat.EqualSign}");
                }
                case OptDefinitionArgumentFormat.Close:
                {
                    return arg.Substring(match.Length);
                }
                case OptDefinitionArgumentFormat.Parentheses:
                {
                    var r = arg.Substring(match.Length);
                    if (r.StartsWith("(") && r.EndsWith(")"))
                        return r.Substring(1, r.Length - 2);
                    throw new ArgumentFormatException(
                        $"value '{args[i]}' can't be matched as '{opt.ArgumentName}' {OptDefinitionArgumentFormat.Parentheses}");
                }
                case OptDefinitionArgumentFormat.SquareBrackets:
                {
                    var r = arg.Substring(match.Length);
                    if (r.StartsWith("[") && r.EndsWith("]"))
                        return r.Substring(1, r.Length - 2);
                    throw new ArgumentFormatException(
                        $"value '{args[i]}' can't be matched as '{opt.ArgumentName}' {OptDefinitionArgumentFormat.SquareBrackets}");
                }
                case OptDefinitionArgumentFormat.Braces:
                {
                    var r = arg.Substring(match.Length);
                    if (r.StartsWith("{") && r.EndsWith("}"))
                        return r.Substring(1, r.Length - 2);
                    throw new ArgumentFormatException(
                        $"value '{args[i]}' can't be matched as '{opt.ArgumentName}' {OptDefinitionArgumentFormat.Braces}");
                }
                case OptDefinitionArgumentFormat.AngleBrackets:
                {
                    var r = arg.Substring(match.Length);
                    if (r.StartsWith("<") && r.EndsWith(">"))
                        return r.Substring(1, r.Length - 2);
                    throw new ArgumentFormatException(
                        $"value '{args[i]}' can't be matched as '{opt.ArgumentName}' {OptDefinitionArgumentFormat.AngleBrackets}");
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
