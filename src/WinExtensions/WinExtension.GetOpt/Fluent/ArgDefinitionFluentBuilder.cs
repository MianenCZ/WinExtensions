using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinExtension.GetOpt.Dtos;

namespace WinExtension.GetOpt
{
    public class ArgDefinitionFluentBuilder<T> : IArgDefinitionFluentBuilder<T> where T: new()
    {
        public readonly ArgDefinition<T> _arg;
        public ArgDefinitionFluentBuilder(ArgDefinition<T> arg)
        {
            this._arg = arg;
        }


        public IArgDefinitionFluentBuilder<T> WithName(string name)
        {
            this._arg.ArgName = name;
            return this;
        }

        public IArgDefinitionFluentBuilder<T> IsRequired(bool isRequired = true)
        {
            this._arg.IsRequired = isRequired;
            return this;
        }

        public IArgDefinitionFluentBuilder<T> IsVariadic(bool isVariadic = true)
        {
            this._arg.IsVariadic = isVariadic;
            return this;
        }

        public IArgDefinitionFluentBuilder<T> HasDescription(string description)
        {
            this._arg.Description = description;
            return this;
        }

        public IArgDefinitionFluentBuilder<T> HasHelpText(string help)
        {
            this._arg.Help = help;
            return this;
        }
    }
}
