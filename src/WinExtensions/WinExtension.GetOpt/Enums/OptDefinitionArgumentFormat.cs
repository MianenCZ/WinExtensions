using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WinExtension.GetOpt.Enums
{
    [Flags]
    public enum OptDefinitionArgumentFormat
    {
        None = 0,
        NextArg = 1 << 0,
        EqualSign = 1 << 1,
        Close = 1 << 2,
        Parentheses = 1<<3,
        SquareBrackets = 1<<4,
        Braces = 1<<5,
        AngleBrackets = 1<<6
    }
}
