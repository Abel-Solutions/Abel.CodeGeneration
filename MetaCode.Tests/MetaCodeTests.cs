using System;
using System.IO;
using System.Text;
using FluentAssertions;
using MetaCode.DynamicRun;
using Xunit;

namespace MetaCode.Tests
{
	public class MetaCodeTests
	{
		private static string NewLine => Environment.NewLine;

		private readonly ICodeGen _codeGen;
		private readonly ICodeRunner _codeRunner;

		public MetaCodeTests()
		{
			_codeGen = new CodeGen();
			_codeRunner = new CodeRunner(new Compiler(), new Runner()); // todo DI?
		}

		[Fact]
		public void CodeGen_AddCode_GeneratedCodeIsCorrect()
		{
			var code = _codeGen
				.AddLine("using System;")
				.AddLine("using System.Text;")
				.AddLine()
				.AddScoped("namespace MetaCode", nspace =>
				{
					nspace.AddScoped("public class Lol", cl =>
					{
						cl.AddScoped("public static void Main()", method =>
						{
							method.AddLine("Console.WriteLine(\"foo\");");
						});
					});
				})
				.Generate();

			code.Should().Be($"using System;{NewLine}" +
							 $"using System.Text;{NewLine}" +
							 NewLine +
							 $"namespace MetaCode{NewLine}" +
							 $"{{{NewLine}" +
							 $"\tpublic class Lol{NewLine}" +
							 $"\t{{{NewLine}" +
							 $"\t\tpublic static void Main(){NewLine}" +
							 $"\t\t{{{NewLine}" +
							 $"\t\t\tConsole.WriteLine(\"foo\");{NewLine}" +
							 $"\t\t}}{NewLine}" +
							 $"\t}}{NewLine}" +
							 $"}}{NewLine}");
		}

		[Fact]
		public void CodeRunner_Code_CodeIsRunCorrectly()
		{
			var code = _codeGen
				.AddLine("using System;")
				.AddLine("using System.Text;")
				.AddLine()
				.AddScoped("namespace MetaCode", nspace =>
				{
					nspace.AddScoped("public class Lol", cl =>
					{
						cl.AddScoped("public static void Main()", method =>
						{
							method.AddLine("Console.WriteLine(\"foo\");");
						});
					});
				})
				.Generate();

			var sb = new StringBuilder();
			Console.SetOut(new StringWriter(sb));

			_codeRunner.Run(code);

			sb.ToString().Should().Be($"foo{NewLine}");
		}
	}
}
