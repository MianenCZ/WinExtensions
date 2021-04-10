# WinExtensions
.NET applications for Windows Subsystem for Linux.



## `WinExtension.GetOpt`

`GetOpt` is a library to simply define, parse and use arguments and options from applications `args`.
Using fluent syntax you can define your own options with automatic parsing, documentation and storage.

```cs
public class Opts
{
    public string FileName { get; set; }
    public bool Delete { get; set; }
}

public RenameFileGetOpt()
{
    this.ApplicationName = "winrn";

    this.AddArg(x => x.FileName, _ => _)
        .ExcludeStartingWithComma()
        .IsVariadic(false)
        .IsRequired(true)
        .WithName("From")
        .HasDescription("Defines original pattern to rename");

    this.AddOpt(x => x.Recursive)
        .HasShortName("r")
        .HasLongName("recursive")
        .HasDescription("Search subdirectories for files to rename");

    this.AddOpt(x => x.HasStartDirectory)
        .WithRequiredArgument(x => x.StartDirectory).WithName("DIR")
        .HasShortName("d")
        .HasDescription("Define start directory");

    this.AddOpt(x => x.RenameDirectories)
        .HasShortName("D")
        .HasDescription("Rename also directories");

    this.AddHelp(true);
}

```


## `WinExtensions.RenameFiles`
