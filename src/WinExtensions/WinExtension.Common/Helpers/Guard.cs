using System;
using System.Collections.Generic;
using System.Text;

namespace WinExtension.Common.Helpers
{
    public static class Guard
    {
        public static void IsNotNull(object? obj, string name)
        {
            if (obj is null)
                throw new ArgumentNullException(name);
        }
    }
}
