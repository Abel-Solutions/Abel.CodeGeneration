using Xunit;

namespace Abel.CodeGeneration.Tests
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
						.AddProperty<int>("Woot")
						.WithModifier("private")
						.WithContent(property => property
							.Get(method => method
								.AddLine("return 3;"))
							.Set(method => method
								.AddLine("value = 3;")))
						.AddConstructor()
						.WithModifier("private")
						.WithParameter("string lol")
						.WithContent(ctor => ctor
							.AddLine("Console.WriteLine(lol);"))
						.AddMethod<int>("GetInt", method => method
							.AddLine("return 1337;"))
						.AddMethod("Main")
						.WithModifiers("public static")
						.WithParameters("string[] args")
						.WithContent(method => method
							.AddLine("Console.WriteLine(\"foo\");"))))
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"using System;" +
				"using System.Text;" +
				"namespace MetaCode" +
				"{" +
				"private class Lol<T> : Object where T : Object" +
				"{" +
				"private Int32 Woot" +
				"{" +
				"get" +
				"{" +
				"return 3;" +
				"}" +
				"set" +
				"{" +
				"value = 3;" +
				"}" +
				"}" +
				"private Lol(string lol)" +
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
		public void Using_CodeIsCorrect()
		{
			var code = _codeGenerator
				.Using("System")
				.Generate();

			code.RemoveSpecialCharacters().Should().Be("using System;");
		}

		[Fact]
		public void AddUsings_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddUsings("System", "System.Text")
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"using System;" +
				"using System.Text;");
		}

		[Fact]
		public void AddNamespace_NoContent_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddNamespace("Lol", _ => { })
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"namespace Lol" +
				"{" +
				"}");
		}

		[Fact]
		public void AddClass_NoContent_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddClass("Lol", _ => { })
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"public class Lol" +
				"{" +
				"}");
		}

		[Fact]
		public void AddNamespace_AndClass_CodeIsCorrect()
		{
			var code = _codeGenerator
				.AddNamespace("Foo", nspace => nspace
					.AddClass("Bar", _ => { }))
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { })
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { })
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { })
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { })
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { })
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { })
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { })
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { })
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { })
				.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"private static class Lol" +
				"{" +
				"}");
		}
	}
}
