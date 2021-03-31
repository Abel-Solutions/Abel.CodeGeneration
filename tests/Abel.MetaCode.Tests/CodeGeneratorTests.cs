using System;
using Abel.MetaCode.Generators;
using Abel.MetaCode.Interfaces;
using FluentAssertions;
using Xunit;

namespace Abel.MetaCode.Tests
{
	public class CodeGeneratorTests
	{
		private readonly ICodeGenerator _codeGenerator = new CodeGenerator();

		[Fact]
		public void CodeGen_AddCode_GeneratedCodeIsCorrect()
		{
			var code = _codeGenerator
				.Using("System")
				.Using("System.Text")
				.AddLine()
				.AddNamespace("MetaCode", nspace => nspace
					.AddClass("Lol")
					.WithParent("object")
					.WithContent(@class => @class
						.AddConstructor("string lol", ctor => ctor
							.AddLine("Console.WriteLine(lol);"))
						.AddMethod<int>("GetInt", method => method
							.AddLine("return 1337;"))))
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"using System;" +
				"using System.Text;" +
				"namespace MetaCode" +
				"{" +
				"public class Lol : object" +
				"{" +
				"public Lol(string lol)" +
				"{" +
				"Console.WriteLine(lol);" +
				"}" +
				"public Int32 GetInt()" +
				"{" +
				"return 1337;" +
				"}" +
				"}" +
				"}");
		}

		private static string RemoveSpecialChars(string text) =>
			text
				.Replace("\t", "")
				.Replace(Environment.NewLine, "");
	}
}
