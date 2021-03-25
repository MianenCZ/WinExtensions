using System;
using System.Linq.Expressions;
using WinExtension.Common.Helpers;
using WinExtension.GetOpt.Dtos;
using WinExtension.GetOpt.Enums;
using WinExtension.GetOpt.Fluent;

namespace WinExtension.GetOpt
{
    internal class OptDefinitionFluentBuilder<T> : IOptDefinitionFluentBuilder<T> where T : new()
    {
        protected readonly OptDefinition<T> _opt;

        public OptDefinitionFluentBuilder(OptDefinition<T> opt)
        {
            _opt = opt;
        }

        public IOptDefinitionFluentBuilder<T> HasShortName(string shortName)
        {
            _opt.ShortOpt = shortName;
            return this;
        }

        public IOptDefinitionFluentBuilder<T> HasLongName(string longName)
        {
            _opt.LongOpt = longName;
            return this;
        }

        public IOptDefinitionFluentBuilder<T> IsRequired(bool isRequired = true)
        {
            _opt.IsRequired = isRequired;
            return this;
        }

        public IOptWithArgumentDefinitionFluentBuilder<T> WithOptionalArgument(
            Expression<Func<T, string>> selector)
        {
            _opt.ArgumentSelector = selector;
            _opt.ArgumentName = PropertyHelper<T>.GetName(selector);
            _opt.Argument = OptDefinitionArgument.Optional;
            _opt.ArgumentFormat = OptDefinitionArgumentFormat.EqualSign;
            return new OptWithArgumentDefinitionFluentBuilder<T>(this._opt);
        }

        public IOptWithArgumentDefinitionFluentBuilder<T> WithRequiredArgument(
            Expression<Func<T, string>> selector)
        {
            _opt.ArgumentSelector = selector;
            _opt.ArgumentName =  PropertyHelper<T>.GetName(selector);
            _opt.Argument = OptDefinitionArgument.Required;
            _opt.ArgumentFormat = OptDefinitionArgumentFormat.EqualSign;
            return new OptWithArgumentDefinitionFluentBuilder<T>(this._opt);
        }

        public IOptDefinitionFluentBuilder<T> WithoutArgument()
        {
            _opt.ArgumentSelector = null;
            _opt.ArgumentName = null;
            _opt.Argument = OptDefinitionArgument.None;
            return this;
        }

        public IOptDefinitionFluentBuilder<T> HasDescription(string description)
        {
            _opt.Description = description;
            return this;
        }

        public IOptDefinitionFluentBuilder<T> IsIncompatibleWith(params string[] incompatibilities)
        {
            foreach (var incompatibility in incompatibilities)
            {
                _opt.IncompatibleWith.Add(incompatibility);
            }

            return this;
        }

        public IOptDefinitionFluentBuilder<T> Requires(params string[] requirements)
        {
            foreach (var req in requirements)
            {
                _opt.Requires.Add(req);
            }

            return this;
        }
    }
}
