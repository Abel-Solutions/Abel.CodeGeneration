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
				.AddNamespace("MetaCode", nspace =>
				{
					nspace.AddClass("Lol", cl =>
					{
						cl.AddMethod("Main", "public static", method =>
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
				.Using("System")
				.Using("System.Text")
				.AddLine()
				.AddNamespace("MetaCode", nspace =>
				{
					nspace.AddClass("Lol", cl =>
					{
						cl.AddMethod("Main", "public static", method =>
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
