using System;
using System.IO;
using System.Text;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Abel.MetaCode.Tests
{
	public class MetaCodeTests
	{
		private readonly ICodeGen _codeGen = new CodeGen();
		private readonly ICompiler _compiler = new Compiler();

		[Fact]
		public void CodeGen_AddCode_GeneratedCodeIsCorrect()
		{
			var code = _codeGen
				.Using("System")
				.Using("System.Text")
				.AddLine()
				.AddNamespace("MetaCode", nspace => nspace
					.AddClass("Lol", cl => cl
						.AddConstructor(ctor => ctor
							.AddLine("Console.WriteLine(\"foo\");"))
						.AddMethod("Main")
							.WithModifiers("public static")
							.WithContent(method => method
								.AddLine("Console.WriteLine(\"bar\");"))))
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"using System;" +
				"using System.Text;" +
				"namespace MetaCode" +
				"{" +
				"public class Lol" +
				"{" +
				"public Lol()" +
				"{" +
				"Console.WriteLine(\"foo\");" +
				"}" +
				"public static void Main()" +
				"{" +
				"Console.WriteLine(\"bar\");" +
				"}" +
				"}" +
				"}");
		}

		[Fact]
		public void Compiler_Code_CodeIsRunCorrectly()
		{
			var code = _codeGen
				.Using("System")
				.Using("System.Text")
				.AddLine()
				.AddNamespace("MetaCode", nspace => nspace
					.AddClass("Lol", cl => cl
						.AddMethod("Main")
							.WithModifiers("public static")
							.WithContent(method => method
								.AddLine("Console.Write(\"foo\");"))))
				.Generate();

			var sb = new StringBuilder();
			Console.SetOut(new StringWriter(sb));

			_compiler
				.Compile(code, OutputKind.ConsoleApplication)
				.Execute();

			sb.ToString().Should().Be("foo");
		}

		private static string RemoveSpecialChars(string text) =>
			text
				.Replace("\t", "")
				.Replace(Environment.NewLine, "");
	}
}
