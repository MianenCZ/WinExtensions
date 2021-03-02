using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinExtension.Common.Helpers;
using WinExtension.GetOpt.Dtos;
using WinExtension.GetOpt.Exceptions;

namespace WinExtension.GetOpt
{
    public class ArgDefinitionFluentBuilder<TTarget, TProp> : IArgDefinitionFluentBuilder<TTarget, TProp> where TTarget: new()
    {
        private readonly ArgDefinition<TTarget> _arg;
        private readonly Expression<Func<TTarget, TProp>> _selector;
        private Func<string, TProp> _formatter = null;


        public ArgDefinitionFluentBuilder(
            ArgDefinition<TTarget> arg, 
            Expression<Func<TTarget, TProp>> selector,
            Func<string, TProp> formatter)
        {
            this._arg = arg;
            this._selector = selector;
            this._formatter = formatter;

            this._arg.Setter = (target, s) => target.SetPropertyValue(_selector, _formatter(s));
        }


        public IArgDefinitionFluentBuilder<TTarget, TProp> WithName(string name)
        {
            this._arg.ArgName = name;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> IsRequired(bool isRequired = true)
        {
            this._arg.IsRequired = isRequired;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> IsVariadic(bool isVariadic = true)
        {
            this._arg.IsVariadic = isVariadic;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> HasDescription(string description)
        {
            this._arg.Description = description;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> HasHelpText(string help)
        {
            this._arg.Help = help;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> WithCustomFormatter(Func<string, TProp> formatter)
        {
            this._formatter = formatter;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> IncludeStartingWithComma()
        {
            this._arg.IncludeStartingWithComma = true;
            return this;
        }

        public IArgDefinitionFluentBuilder<TTarget, TProp> ExcludeStartingWithComma()
        {
            this._arg.IncludeStartingWithComma = false;
            return this;
        }
    }
}
