using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinExtension.Common.Extensions;

namespace WinExtension.GetOpt.Dtos
{
    public class ArgDefinition<T> where T: new()
    {
        internal Func<T, string, T> Setter;
        internal bool IncludeStartingWithComma = false;
        internal Action<T> OnMatched = null;

        internal ArgDefinition()
        {
        }

        public string ArgName { get; set; }
        public bool IsRequired { get; set; } = false;
        public bool IsVariadic { get; set; } = false;
        public string Description { get; set; } = "";
        public string Help { get; set; } = null;

        public string VerboseName => this.ArgName.ToMacroCase();
    }
}
