using System;
using System.Linq;
using FluentAssertions;
using WinExtension.GetOpt;
using WinExtension.GetOpt.Enums;
using WinExtension.GetOpt.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace WinExtensions.GetOpt.Tests
{
    public class Opts
    {
        public bool isA { get; set; }

        public string aArg { get; set; }

        public string arg { get; set; }
    }

    public class Opt2
    {
        public bool a { get; set; }
        public bool b { get; set; }
        public string b_file { get; set; }
        public bool c { get; set; }
        public string c_file { get; set; }
    }

    public class Args
    {
        public string fileName { get; set; }
        public int lineCount { get; set; }

        public int[] arr { get; set; }
    }


    public class PrintTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public PrintTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        #region [Theory]

        [Theory]
        [InlineData(OptDefinitionArgumentFormat.NextArg, "Usage: Application -b[ TMP]")]
        [InlineData(OptDefinitionArgumentFormat.EqualSign, "Usage: Application -b[=TMP]")]
        [InlineData(OptDefinitionArgumentFormat.Close, "Usage: Application -b[TMP]")]
        [InlineData(OptDefinitionArgumentFormat.Parentheses, "Usage: Application -b[(TMP)]")]
        [InlineData(OptDefinitionArgumentFormat.SquareBrackets, "Usage: Application -b[[TMP]]")]
        [InlineData(OptDefinitionArgumentFormat.Braces, "Usage: Application -b[{TMP}]")]
        [InlineData(OptDefinitionArgumentFormat.AngleBrackets, "Usage: Application -b[<TMP>]")]

        #endregion

        public void OptionalArgumentShort(OptDefinitionArgumentFormat format, string excepted)
        {
            GetOptBase<Opt2> getOpt = new GetOptBase<Opt2>();
            getOpt.AddOpt(o => o.b)
                  .HasShortName("b")
                  .WithOptionalArgument(o => o.b_file).FormattedAs(format).WithName("tmp")
                  .HasDescription("file TMP is optional and will be passed via b option")
                  .IsRequired();

            var res = getOpt.GenerateUsage();
            _testOutputHelper.WriteLine(res);
            res.Should().Contain(excepted);
        }

        #region [Theory]

        [Theory]
        [InlineData(OptDefinitionArgumentFormat.NextArg, "Usage: Application --big-file[ TMP]")]
        [InlineData(OptDefinitionArgumentFormat.EqualSign, "Usage: Application --big-file[=TMP]")]
        [InlineData(OptDefinitionArgumentFormat.Close, "Usage: Application --big-file[TMP]")]
        [InlineData(OptDefinitionArgumentFormat.Parentheses, "Usage: Application --big-file[(TMP)]")]
        [InlineData(OptDefinitionArgumentFormat.SquareBrackets, "Usage: Application --big-file[[TMP]]")]
        [InlineData(OptDefinitionArgumentFormat.Braces, "Usage: Application --big-file[{TMP}]")]
        [InlineData(OptDefinitionArgumentFormat.AngleBrackets, "Usage: Application --big-file[<TMP>]")]

        #endregion

        public void OptionalArgumentLong(OptDefinitionArgumentFormat format, string excepted)
        {
            GetOptBase<Opt2> getOpt = new GetOptBase<Opt2>();
            getOpt.AddOpt(o => o.b)
                  .HasLongName("big-file")
                  .WithOptionalArgument(o => o.b_file).FormattedAs(format).WithName("tmp")
                  .HasDescription("file TMP is optional and will be passed via b option")
                  .IsRequired();

            var res = getOpt.GenerateUsage();
            _testOutputHelper.WriteLine(res);
            res.Should().Contain(excepted);
        }

        #region [Theory]

        [Theory]
        [InlineData(OptDefinitionArgumentFormat.NextArg, "Usage: Application -b TMP")]
        [InlineData(OptDefinitionArgumentFormat.EqualSign, "Usage: Application -b=TMP")]
        [InlineData(OptDefinitionArgumentFormat.Close, "Usage: Application -bTMP")]
        [InlineData(OptDefinitionArgumentFormat.Parentheses, "Usage: Application -b(TMP)")]
        [InlineData(OptDefinitionArgumentFormat.SquareBrackets, "Usage: Application -b[TMP]")]
        [InlineData(OptDefinitionArgumentFormat.Braces, "Usage: Application -b{TMP}")]
        [InlineData(OptDefinitionArgumentFormat.AngleBrackets, "Usage: Application -b<TMP>")]

        #endregion

        public void RequiredArgumentShort(OptDefinitionArgumentFormat format, string excepted)
        {
            GetOptBase<Opt2> getOpt = new GetOptBase<Opt2>();
            getOpt.AddOpt(o => o.b)
                  .HasShortName("b")
                  .WithRequiredArgument(o => o.b_file).FormattedAs(format).WithName("tmp")
                  .HasDescription("file TMP is required and will be passed via b option")
                  .IsRequired();

            var res = getOpt.GenerateUsage();
            _testOutputHelper.WriteLine(res);
            res.Should().Contain(excepted);
        }

        #region [Theory]

        [Theory]
        [InlineData(OptDefinitionArgumentFormat.NextArg, "Usage: Application --big-file TMP")]
        [InlineData(OptDefinitionArgumentFormat.EqualSign, "Usage: Application --big-file=TMP")]
        [InlineData(OptDefinitionArgumentFormat.Close, "Usage: Application --big-fileTMP")]
        [InlineData(OptDefinitionArgumentFormat.Parentheses, "Usage: Application --big-file(TMP)")]
        [InlineData(OptDefinitionArgumentFormat.SquareBrackets, "Usage: Application --big-file[TMP]")]
        [InlineData(OptDefinitionArgumentFormat.Braces, "Usage: Application --big-file{TMP}")]
        [InlineData(OptDefinitionArgumentFormat.AngleBrackets, "Usage: Application --big-file<TMP>")]

        #endregion

        public void RequiredArgumentLong(OptDefinitionArgumentFormat format, string excepted)
        {
            GetOptBase<Opt2> getOpt = new GetOptBase<Opt2>();
            getOpt.AddOpt(o => o.b)
                  .HasLongName("big-file")
                  .WithRequiredArgument(o => o.b_file).FormattedAs(format).WithName("tmp")
                  .HasDescription("file TMP is required and will be passed via b option")
                  .IsRequired();

            var res = getOpt.GenerateUsage();
            _testOutputHelper.WriteLine(res);
            res.Should().Contain(excepted);
        }
    }

    public class SendBox
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public SendBox(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void _Sandbox()
        {
            GetOptBase<Opts> getOpt = new GetOptBase<Opts>();
            getOpt.ApplicationName = "TestApp.exe";
            getOpt.TopDescription =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer eget sodales nibh. Sed a justo id velit tristique iaculis. Aenean mauris ante, luctus eget elit eget, accumsan aliquet erat.";
            getOpt.BootomDescription =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer eget sodales nibh. Sed a justo id velit tristique iaculis. Aenean mauris ante, luctus eget elit eget, accumsan aliquet erat. Quisque posuere sagittis ex, sed porttitor ex elementum vitae. Nulla viverra velit vel sapien dictum ultrices. Sed neque nibh, gravida nec mattis ut, bibendum quis lectus. Ut euismod tempor turpis. Nullam ac luctus libero, in vestibulum enim. Nullam maximus posuere metus sed tincidunt. Curabitur eget est quam. Aliquam ac ligula consectetur, pharetra elit at, viverra lectus. Nunc venenatis iaculis risus, id molestie mi malesuada id. Maecenas placerat blandit lacus, ut fermentum mi maximus nec. Nulla vehicula dapibus quam.";


            getOpt.AddOpt(o => o.isA)
                  .HasShortName("a")
                  .HasLongName("long")
                  .IsRequired()
                  .IsIncompatibleWith("--longc")
                  .Requires("-b")
                  .WithOptionalArgument(o => o.aArg)
                  .HasDescription("I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. ");

            getOpt.AddOpt(o => o.isA)
                  .HasShortName("b")
                  .HasLongName("Some really fucked up name of long opt. Really fucked up ")
                  .IsRequired()
                  .Requires("-a")
                  .IsIncompatibleWith("--longc")
                  .WithOptionalArgument(o => o.aArg).WithName("Some really fucked up na of argument")
                  .HasDescription("I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. ");

            getOpt.AddOpt(o => o.isA)
                  .HasLongName("--longc")
                  .IsRequired()
                  .WithOptionalArgument(o => o.aArg)
                  .HasDescription(
                      "I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. I am some a longed option. ")
                  .IsIncompatibleWith("-a", "-b");
            getOpt.AddArg(o => o.arg, s => s)
                  .IsRequired()
                  .HasDescription("prcat")
                  .HasHelpText("prcat help")
                  .IsVariadic();


            _testOutputHelper.WriteLine(getOpt.GenerateUsage());
        }
    }


    public class UsageTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public UsageTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }



        [Fact]
        public void OptionalArg()
        {
            GetOptBase<Opts> getOpt = new GetOptBase<Opts>();
            getOpt.AddOpt(o => o.isA)
                  .HasShortName("a");
            _testOutputHelper.WriteLine(getOpt.GenerateUsage());

            getOpt.GenerateUsage().Should().Contain("Usage: Application [OPTIONS]...");

        }

        [Fact]
        public void RequiredArg()
        {
            GetOptBase<Opts> getOpt = new GetOptBase<Opts>();
            getOpt.AddOpt(o => o.isA)
                  .HasShortName("a")
                  .IsRequired();
            _testOutputHelper.WriteLine(getOpt.GenerateUsage());

            getOpt.GenerateUsage().Should().Contain("Usage: Application -a");

        }

        [Fact]
        public void RequiredAndOptionalArg()
        {
            GetOptBase<Opts> getOpt = new GetOptBase<Opts>();
            getOpt.AddOpt(o => o.isA)
                  .HasShortName("a")
                  .IsRequired();
            getOpt.AddOpt(o => o.isA)
                  .HasShortName("b");
            _testOutputHelper.WriteLine(getOpt.GenerateUsage());

            getOpt.GenerateUsage().Should().Contain("Usage: Application -a [OPTIONS]...");

        }
    }

    public class ArgTests
    {
        #region [Theory]

        [Theory]
        [InlineData("IamFile", "150", "1,1,1,1")]
        [InlineData("null", "0", "1")]
        [InlineData("", "0", "1,1,1,1,1,1,1,1,1")]
        [InlineData("aoscnoasnvoianva", "-42", "0")]

        #endregion

        public void BasicOptTests(params string[] args)
        {
            GetOptBase<Args> getOpt = new();
            getOpt.AddArg(x => x.fileName)
                  .IncludeStartingWithComma();
            getOpt.AddArg(x => x.lineCount)
                  .IncludeStartingWithComma();
            getOpt.AddArg(
                      x => x.arr,
                      s => s.Split(',')
                            .Select(x => int.Parse(x))
                            .ToArray())
                  .IncludeStartingWithComma();


            var res = getOpt.GetOpts(args);
            res.fileName.Should().Be(args[0]);
            res.lineCount.Should().Be(int.Parse(args[1]));
            res.arr.Length.Should().BeGreaterOrEqualTo(1);
        }
    }

    public class OptTests
    {
        #region [Theory]
        [Theory]
        [InlineData(new string[] { }, false, false, false)]
        [InlineData(new[]{ "-a"}, true, false, false)]
        [InlineData(new[]{ "-b"}, false, true, false)]
        [InlineData(new[]{ "-c"}, false, false, true)]
        [InlineData(new[]{ "-a", "-b" }, true, true, false)]
        [InlineData(new[]{ "-b", "-c" }, false, true, true)]
        [InlineData(new[]{ "-a", "-c" }, true, false, true)]
        [InlineData(new[]{ "-a", "-b", "-c" }, true, true, true)]
        #endregion
        public void BasicOptTests(string[] args, bool aExp, bool bExp, bool cExp)
        {
            GetOptBase<Opt2> getOpt = new GetOptBase<Opt2>();
            getOpt.AddOpt(o => o.a)
                  .HasShortName("a");
            getOpt.AddOpt(o => o.b)
                  .HasShortName("b");
            getOpt.AddOpt(o => o.c)
                  .HasShortName("c");


            Opt2 opts = getOpt.GetOpts(args);
            opts.a.Should().Be(aExp);
            opts.b.Should().Be(bExp);
            opts.c.Should().Be(cExp);
        }

        #region [Theory]
        [Theory]
        [InlineData(new string[] { }, false, false, false)]
        [InlineData(new[] { "--A" }, true, false, false)]
        [InlineData(new[] { "--B" }, false, true, false)]
        [InlineData(new[] { "--C" }, false, false, true)]
        [InlineData(new[] { "--A", "--B" }, true, true, false)]
        [InlineData(new[] { "--B", "--C" }, false, true, true)]
        [InlineData(new[] { "--A", "--C" }, true, false, true)]
        [InlineData(new[] { "--A", "--B", "--C" }, true, true, true)]
        #endregion
        public void LongOptTests(string[] args, bool aExp, bool bExp, bool cExp)
        {
            GetOptBase<Opt2> getOpt = new GetOptBase<Opt2>();
            getOpt.AddOpt(o => o.a)
                  .HasLongName("A");
            getOpt.AddOpt(o => o.b)
                  .HasLongName("B");
            getOpt.AddOpt(o => o.c)
                  .HasLongName("C");


            Opt2 opts = getOpt.GetOpts(args);
            opts.a.Should().Be(aExp);
            opts.b.Should().Be(bExp);
            opts.c.Should().Be(cExp);
        }

        #region [Theory]
        [Theory]
        [InlineData(new string[] { }, false, false, false)]
        [InlineData(new[] { "--A" }, true, false, false)]
        [InlineData(new[] { "--B" }, false, true, false)]
        [InlineData(new[] { "--C" }, false, false, true)]
        [InlineData(new[] { "-a" }, true, false, false)]
        [InlineData(new[] { "-b" }, false, true, false)]
        [InlineData(new[] { "-c" }, false, false, true)]
        [InlineData(new[] { "-a", "--B" }, true, true, false)]
        [InlineData(new[] { "-b", "--C" }, false, true, true)]
        [InlineData(new[] { "-a", "--C" }, true, false, true)]
        [InlineData(new[] { "--A", "-b" }, true, true, false)]
        [InlineData(new[] { "--B", "-c" }, false, true, true)]
        [InlineData(new[] { "--A", "-c" }, true, false, true)]
        [InlineData(new[] { "--A", "-b", "-c" }, true, true, true)]
        [InlineData(new[] { "-a", "--B", "-c" }, true, true, true)]
        [InlineData(new[] { "-a", "-b", "--C" }, true, true, true)]
        #endregion
        public void LongAndShortOptsTests(string[] args, bool aExp, bool bExp, bool cExp)
        {
            GetOptBase<Opt2> getOpt = new GetOptBase<Opt2>();
            getOpt.AddOpt(o => o.a)
                  .HasShortName("a")
                  .HasLongName("A");
            getOpt.AddOpt(o => o.b)
                  .HasShortName("b")
                  .HasLongName("B");
            getOpt.AddOpt(o => o.c)
                  .HasShortName("c")
                  .HasLongName("C");


            Opt2 opts = getOpt.GetOpts(args);
            opts.a.Should().Be(aExp);
            opts.b.Should().Be(bExp);
            opts.c.Should().Be(cExp);
        }

        #region [Theory]
        [Theory]
        [InlineData("-Q")]
        [InlineData("--a")]
        [InlineData("-a", "--a")]
        [InlineData("-a", "--short-long")]
        #endregion
        public void UnexpectedOptionExceptionTests(params string[] args)
        {
            //app file -42



            GetOptBase<Opt2> getOpt = new GetOptBase<Opt2>();
            getOpt.AddOpt(o => o.a)
                  .HasShortName("a");
            getOpt.AddOpt(o => o.b)
                  .HasShortName("b");
            getOpt.AddOpt(o => o.c)
                  .HasShortName("c");

            Xunit.Assert.Throws<UnexpectedOptionException>(() => getOpt.GetOpts(args));
        }


        #region [Theory]
        [Theory]
        [InlineData(OptDefinitionArgumentFormat.NextArg, "-b", "argument")]
        [InlineData(OptDefinitionArgumentFormat.EqualSign, "-b=argument")]
        [InlineData(OptDefinitionArgumentFormat.Close, "-bargument")]
        [InlineData(OptDefinitionArgumentFormat.Parentheses, "-b(argument)")]
        [InlineData(OptDefinitionArgumentFormat.SquareBrackets, "-b[argument]")]
        [InlineData(OptDefinitionArgumentFormat.Braces, "-b{argument}")]
        [InlineData(OptDefinitionArgumentFormat.AngleBrackets, "-b<argument>")]
        #endregion
        public void BasicArgOptTests(OptDefinitionArgumentFormat format, params string[] args)
        {
            GetOptBase<Opt2> getOpt = new GetOptBase<Opt2>();
            getOpt.AddOpt(o => o.b)
                  .HasShortName("b")
                  .HasLongName("big-file")
                  .WithRequiredArgument(o => o.b_file)
                  .FormattedAs(format);

            var res = getOpt.GetOpts(args);
            res.b.Should().Be(true);
            res.b_file.Should().Be("argument");
        }
    }
}
