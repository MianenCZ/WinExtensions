using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WinExtension.Common.Extensions;
using WinExtension.GetOpt.Enums;

namespace WinExtension.GetOpt.Dtos
{
    public class OptDefinition<T> where T : new()
    {
        internal Expression<Func<T, bool>> Selector;
        internal HashSet<string> IncompatibleWith = new();
        internal HashSet<string> Requires = new();

        internal OptDefinition(Expression<Func<T, bool>> selector)
        {
            this.Selector = selector;
        }

        public string ShortOpt { get; internal set; }
        public string LongOpt { get; internal set; }
        public bool IsRequired { get; internal set; } = false;
        public OptDefinitionArgument Argument { get; internal set; } = OptDefinitionArgument.None;
        public string ArgumentName { get; internal set; }
        public Expression<Func<T, string>> ArgumentSelector { get; internal set; }
        public string Description { get; internal set; }
        public OptDefinitionArgumentFormat ArgumentFormat { get; internal set; } = OptDefinitionArgumentFormat.None;
        public string Verbose => (ShortOpt is not null) ? $"-{ShortOpt}" : $"--{LongOpt}";

        public string VerboseArg =>
            this.Argument switch
            {
                OptDefinitionArgument.Required => $"{VerboseArgFormat}",
                OptDefinitionArgument.Optional => $"[{VerboseArgFormat}]",
                OptDefinitionArgument.None     => "",
                var _                          => throw new NotImplementedException($"{typeof(OptDefinitionArgument)}"),
            };
        public string VerboseArgFormat =>
            this.ArgumentFormat switch
            {
                OptDefinitionArgumentFormat.None           => throw new ArgumentOutOfRangeException(),
                OptDefinitionArgumentFormat.NextArg        => $" {this.ArgumentName.ToMacroCase()}",
                OptDefinitionArgumentFormat.EqualSign      => $"={this.ArgumentName.ToMacroCase()}",
                OptDefinitionArgumentFormat.Close          => $"{this.ArgumentName.ToMacroCase()}",
                OptDefinitionArgumentFormat.Parentheses    => $"({this.ArgumentName.ToMacroCase()})",
                OptDefinitionArgumentFormat.SquareBrackets => $"[{this.ArgumentName.ToMacroCase()}]",
                OptDefinitionArgumentFormat.Braces         => $"{{{this.ArgumentName.ToMacroCase()}}}",
                OptDefinitionArgumentFormat.AngleBrackets  => $"<{this.ArgumentName.ToMacroCase()}>",
                _                                          => throw new ArgumentOutOfRangeException()
            };
    }
}
