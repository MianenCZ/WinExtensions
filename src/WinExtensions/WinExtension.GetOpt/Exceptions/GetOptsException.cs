using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinExtension.GetOpt.Exceptions
{
    public class GetOptException : Exception
    {
        public GetOptException() : base() { }
        public GetOptException(string message) : base(message) { }
        public GetOptException(string message, Exception inner) : base(message, inner) { }
    }
}
