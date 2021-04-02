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
		public void AcceptanceTest()
		{
			var code = _codeGenerator
				.Using("System")
				.Using("System.Text")
				.AddLine()
				.AddNamespace("MetaCode", nspace => nspace
					.AddClass("Lol")
					.WithModifier("private")
					.WithGenericType("T", "Object")
					.WithParent<object>()
					.WithContent(@class => @class
						.AddConstructor("string lol", ctor => ctor
							.AddLine("Console.WriteLine(lol);"))
						.AddMethod<int>("GetInt", method => method
							.AddLine("return 1337;"))
						.AddMethod("Main")
						.WithModifiers("public static")
						.WithParameters("string[] args")
						.WithContent(method => method
							.AddLine("Console.WriteLine(\"foo\");"))))
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"using System;" +
				"using System.Text;" +
				"namespace MetaCode" +
				"{" +
				"private class Lol<T> : Object where T : Object" +
				"{" +
				"public Lol(string lol)" +
				"{" +
				"Console.WriteLine(lol);" +
				"}" +
				"public Int32 GetInt()" +
				"{" +
				"return 1337;" +
				"}" +
				"public static void Main(string[] args)" +
				"{" +
				"Console.WriteLine(\"foo\");" +
				"}" +
				"}" +
				"}");
		}

		[Fact]
		public void AddNamespace_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddNamespace("Lol", nspace => { })
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"namespace Lol" +
				"{" +
				"}");
		}

		[Fact]
		public void AddClass_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddClass("Lol", @class => { })
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public class Lol" +
				"{" +
				"}");
		}

		[Fact]
		public void AddNamespace_AddClass_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddNamespace("Foo", nspace => nspace
					.AddClass("Bar", @class => { }))
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"namespace Foo" +
				"{" +
				"public class Bar" +
				"{" +
				"}" +
				"}");
		}

		[Fact]
		public void AddClass_GenericType_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddClass("Lol")
				.WithGenericType("T")
				.WithContent(@class => { })
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public class Lol<T>" +
				"{" +
				"}");
		}

		[Fact]
		public void AddClass_GenericTypeWithConstraint_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddClass("Lol")
				.WithGenericType("T", "object")
				.WithContent(@class => { })
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public class Lol<T> where T : object" +
				"{" +
				"}");
		}

		[Fact]
		public void AddClass_Parent_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddClass("Lol")
				.WithParent("object")
				.WithContent(@class => { })
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public class Lol : object" +
				"{" +
				"}");
		}

		[Fact]
		public void AddClass_GenericParent_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddClass("Lol")
				.WithParent<object>()
				.WithContent(@class => { })
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public class Lol : Object" +
				"{" +
				"}");
		}

		[Fact]
		public void AddClass_Parents_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddClass("Lol")
				.WithParents("object, ISomething")
				.WithContent(@class => { })
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public class Lol : object, ISomething" +
				"{" +
				"}");
		}

		[Fact]
		public void AddClass_Modifier_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddClass("Lol")
				.WithModifier("private")
				.WithContent(@class => { })
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private class Lol" +
				"{" +
				"}");
		}

		[Fact]
		public void AddClass_Modifiers_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddClass("Lol")
				.WithModifiers("private", "static")
				.WithContent(@class => { })
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private static class Lol" +
				"{" +
				"}");
		}

		[Fact]
		public void AddClass_ModifierWithSpaces_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddClass("Lol")
				.WithModifier("private static")
				.WithContent(@class => { })
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private static class Lol" +
				"{" +
				"}");
		}

		[Fact]
		public void AddClass_ModifiersWithSpaces_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddClass("Lol")
				.WithModifiers("private static")
				.WithContent(@class => { })
				.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private static class Lol" +
				"{" +
				"}");
		}

		private static string RemoveSpecialChars(string text) =>
			text
				.Replace("\t", "")
				.Replace(Environment.NewLine, "");
	}
}
