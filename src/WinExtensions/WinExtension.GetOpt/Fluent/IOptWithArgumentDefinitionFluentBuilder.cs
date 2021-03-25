using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinExtension.GetOpt.Enums;

namespace WinExtension.GetOpt
{
    public interface IOptWithArgumentDefinitionFluentBuilder<T> : IOptDefinitionFluentBuilder<T> where T: new()
    {
        public IOptWithArgumentDefinitionFluentBuilder<T> FormattedAs(OptDefinitionArgumentFormat format);
        public IOptWithArgumentDefinitionFluentBuilder<T> WithName(string name);
    }
}
