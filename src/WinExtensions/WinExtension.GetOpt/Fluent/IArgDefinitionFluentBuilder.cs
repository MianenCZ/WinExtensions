using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinExtension.GetOpt
{
    public interface IArgDefinitionFluentBuilder<T, TProp> where T : new()
    {
        public IArgDefinitionFluentBuilder<T, TProp> WithName(string name);

        public IArgDefinitionFluentBuilder<T, TProp> IsRequired(bool isRequired = true);

        public IArgDefinitionFluentBuilder<T, TProp> IsVariadic(bool isVariadic = true);

        public IArgDefinitionFluentBuilder<T, TProp> HasDescription(string description);

        public IArgDefinitionFluentBuilder<T, TProp> HasHelpText(string help);
    }
}
