using System;
using System.IO;
using System.Runtime.CompilerServices;
using WinExtension.GetOpt;
using WinExtension.Common.Helpers;
using WinExtension.Common.Extensions;

namespace WinExtensions.RenameFiles
{
    class Program
    {
        private static RenameFileGetOpt.Opts _opts;

        static void Main(string[] args)
        {
            RenameFileGetOpt getOpts = new RenameFileGetOpt();
            _opts = getOpts.GetOpts(args);
            
            RenameInDir(_opts.StartDirectory);
        }

        static void RenameInDir(string dir)
        {
            string[] filePaths = Directory.GetFiles(dir);
            foreach (string filePath in filePaths)
            {
                string fileName = filePath.FileName();
                string newName = fileName.Replace(_opts.From, _opts.To);

                if (newName != fileName)
                {
                    string newPath = PathHelper.Build(filePath.DirectoryPath(), newName);
                    File.Move(filePath, newPath);
                    Console.WriteLine(filePath);
                    Console.WriteLine($"\t-> {newPath}");
                }
            }

            if (_opts.RenameDirectories)
            {
                string[] dirPaths = Directory.GetDirectories(dir);

                foreach (string dirPath in dirPaths)
                {
                    string dirName = dirPath.FileName();
                    string newName = dirName.Replace(_opts.From, _opts.To);

                    if (newName != dirName)
                    {
                        string newPath = PathHelper.Build(dirPath.DirectoryPath(), newName);
                        Directory.Move(dirPath, newPath);
                        Console.WriteLine(dirPath);
                        Console.WriteLine($"\t-> {newPath}");
                    }
                }
            }

            if (_opts.Recursive)
            {
                string[] dirPaths = Directory.GetDirectories(dir);
                foreach (string dirPath in dirPaths)
                {
                    RenameInDir(dirPath);
                }
            }
        }
    }

    public class RenameFileGetOpt : GetOptBase<RenameFileGetOpt.Opts>
    {
        public class Opts
        {
            public string From { get; set; }
            public string To { get; set; }
            public bool Recursive { get; set; }

            public bool HasStartDirectory { get; set; }
            public string StartDirectory { get; set; } = Environment.CurrentDirectory;

            public bool RenameDirectories { get; set; }
        }

        public RenameFileGetOpt()
        {
            this.AddArg(x => x.From, _ => _)
                .ExcludeStartingWithComma()
                .IsVariadic(false)
                .IsRequired(true)
                .WithName("From");

            this.AddArg(x => x.To, _ => _)
                .ExcludeStartingWithComma()
                .IsVariadic(false)
                .IsRequired(true)
                .WithName("To");

            this.AddOpt(x => x.Recursive)
                .HasShortName("r");

            this.AddOpt(x => x.HasStartDirectory)
                .WithRequiredArgument(x => x.StartDirectory)
                .HasShortName("d");

            this.AddOpt(x => x.RenameDirectories)
                .HasShortName("D");
        }
    }
}
