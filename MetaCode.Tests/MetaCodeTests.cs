using System;
using System.IO;
using System.Text;
using FluentAssertions;
using MetaCode.Extensions;
using MetaCode.Interfaces;
using Microsoft.CodeAnalysis;
using Xunit;

namespace MetaCode.Tests
{
	public class MetaCodeTests
	{
		private readonly ICodeGen _codeGen = new CodeGen();
		private readonly ICompiler _compiler = new Compiler();

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

			RemoveSpecialChars(code).Should().Be(
				"using System;" +
				"using System.Text;" +
				"namespace MetaCode" +
				"{" +
				"public class Lol" +
				"{" +
				"public static void Main()" +
				"{" +
				"Console.WriteLine(\"foo\");" +
				"}" +
				"}" +
				"}");
		}

		[Fact]
		public void Compiler_Code_CodeIsRunCorrectly()
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

			_compiler
				.Compile(code, OutputKind.ConsoleApplication)
				.Execute();

			sb.ToString().Should().Be($"foo{Environment.NewLine}");
		}

		private static string RemoveSpecialChars(string text) =>
			text
				.Replace("\t", "")
				.Replace(Environment.NewLine, "");
	}
}
