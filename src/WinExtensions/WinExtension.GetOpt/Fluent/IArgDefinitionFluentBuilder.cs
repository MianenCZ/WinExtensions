using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinExtension.GetOpt
{
    public interface IArgDefinitionFluentBuilder<T> where T : new()
    {
        public IArgDefinitionFluentBuilder<T> WithName(string name);

        public IArgDefinitionFluentBuilder<T> IsRequired(bool isRequired = true);

        public IArgDefinitionFluentBuilder<T> IsVariadic(bool isVariadic = true);

        public IArgDefinitionFluentBuilder<T> HasDescription(string description);

        public IArgDefinitionFluentBuilder<T> HasHelpText(string help);
    }
}
