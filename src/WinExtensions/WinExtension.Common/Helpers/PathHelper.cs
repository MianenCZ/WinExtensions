using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using WinExtension.Common.Extensions;


namespace WinExtension.Common.Helpers
{

    public static class PathHelper
    {
        public static string[] SplitToDirs(this string path)
        {
            return path.Split(Path.DirectorySeparatorChar);
        }

        public static string FileName(this string path)
        {
            return path.SplitToDirs().Last();
        }

        public static string DirectoryPath(this string filename)
        {
            return filename.SplitToDirs().ExceptLast(1).JoinToString(Path.DirectorySeparatorChar);
        }

        public static string Build(params string[] parts)
        {
            return string.Join(Path.DirectorySeparatorChar, parts);
        }
    }
}
