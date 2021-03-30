using System;
using System.IO;
using System.Text;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Generators;
using Abel.MetaCode.Interfaces;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Abel.MetaCode.Tests
{
	public class CompilerTests
	{
		private readonly ICodeGenerator _codeGenerator = new CodeGenerator();
		private readonly ICompiler _compiler = new Compiler();

		[Fact]
		public void Compiler_Code_CodeIsRunCorrectly()
		{
			var code = _codeGenerator
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
	}
}
