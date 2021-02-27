using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinExtension.GetOpt.Dtos;
using WinExtension.GetOpt.Enums;

namespace WinExtension.GetOpt.Fluent
{
    internal class OptWithArgumentDefinitionFluentBuilder<T> :
        OptDefinitionFluentBuilder<T>,
        IOptWithArgumentDefinitionFluentBuilder<T>
        where T : new()
    {
        public OptWithArgumentDefinitionFluentBuilder(OptDefinition<T> opt) : base(opt)
        {

        }

        public IOptWithArgumentDefinitionFluentBuilder<T> FormattedAs(OptDefinitionArgumentFormat format)
        {
            this._opt.ArgumentFormat = format;
            return this;
        }

        public IOptWithArgumentDefinitionFluentBuilder<T> WithName(string name)
        {
            this._opt.ArgumentName = name;
            return this;
        }
    }
}
