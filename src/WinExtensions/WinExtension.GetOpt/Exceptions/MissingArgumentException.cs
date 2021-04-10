using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinExtension.GetOpt.Exceptions
{
    public class MissingArgumentException : GetOptException
    {
        public MissingArgumentException() : base("Argument is missing.") { }
        public MissingArgumentException(string message) : base(message) { }
        public MissingArgumentException(string message, Exception inner) : base(message, inner) { }
    }
}
