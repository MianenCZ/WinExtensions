using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinExtension.GetOpt.Exceptions
{
    public class ArgumentFormatException : GetOptException
    {
        public ArgumentFormatException() : base("UnexpectedFormat") { }
        public ArgumentFormatException(string message) : base(message) { }
        public ArgumentFormatException(string message, Exception inner) : base(message, inner) { }
    }
}
