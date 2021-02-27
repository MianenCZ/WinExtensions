using System;
using System.Linq.Expressions;
using WinExtension.Common.Helpers;
using WinExtension.GetOpt.Dtos;
using WinExtension.GetOpt.Enums;

namespace WinExtension.GetOpt
{
    public interface IOptDefinitionFluentBuilder<T> where T : new()
    {
        public IOptDefinitionFluentBuilder<T> HasShortName(string shortName);
        public IOptDefinitionFluentBuilder<T> HasLongName(string longName);
        public IOptDefinitionFluentBuilder<T> IsRequired(bool isRequired = true);
        public IOptWithArgumentDefinitionFluentBuilder<T> WithOptionalArgument( Expression<Func<T, string>> selector);
        public IOptWithArgumentDefinitionFluentBuilder<T> WithRequiredArgument( Expression<Func<T, string>> selector);
        public IOptDefinitionFluentBuilder<T> WithoutArgument();
        public IOptDefinitionFluentBuilder<T> HasDescription(string description);
        public IOptDefinitionFluentBuilder<T> IsIncompatibleWith(params string[] incompatibilities);
        public IOptDefinitionFluentBuilder<T> Requires(params string[] requirements);
    }
}
