using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WinExtension.GetOpt.Dtos
{
    public class ArgDefinition<T> where T: new()
    {
        internal Expression<Func<T, string>> Selector;

        internal ArgDefinition(Expression<Func<T, string>> selector)
        {
            this.Selector = selector;
        }

        public string ArgName { get; set; }
        public bool IsRequired { get; set; } = false;
        public bool IsVariadic { get; set; } = false;
        public string Description { get; set; } = "";
    }
}
