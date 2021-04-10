using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinExtension.GetOpt
{
    public interface IArgDefinitionFluentBuilder<TTarget, TProp> where TTarget : new()
    {
        public IArgDefinitionFluentBuilder<TTarget, TProp> WithName(string name);

        public IArgDefinitionFluentBuilder<TTarget, TProp> IsRequired(bool isRequired = true);

        public IArgDefinitionFluentBuilder<TTarget, TProp> IsVariadic(bool isVariadic = true);

        public IArgDefinitionFluentBuilder<TTarget, TProp> HasDescription(string description);

        public IArgDefinitionFluentBuilder<TTarget, TProp> HasHelpText(string help);

        public IArgDefinitionFluentBuilder<TTarget, TProp> IncludeStartingWithComma();

        public IArgDefinitionFluentBuilder<TTarget, TProp> ExcludeStartingWithComma();

        public IArgDefinitionFluentBuilder<TTarget, TProp> AddOnMatchedTrigger(Action<TTarget> trigger);
    }
}
