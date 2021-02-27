using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinExtension.GetOpt.Exceptions
{
    public class UnexpectedOptionException : GetOptException
    {
        public UnexpectedOptionException() : base("Unexpected option found.") { }
        public UnexpectedOptionException(string message) : base(message) { }
        public UnexpectedOptionException(string message, Exception inner) : base(message, inner) { }
    }
}
