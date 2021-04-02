using System;
using Abel.MetaCode.Generators;
using Abel.MetaCode.Interfaces;
using FluentAssertions;
using Xunit;

namespace Abel.MetaCode.Tests
{
	public class ClassGeneratorTests
	{
		private readonly ICodeGenerator _codeGenerator = new CodeGenerator();
		private readonly IClassGenerator _classGenerator;

		public ClassGeneratorTests() => _classGenerator = _codeGenerator.ToClassGenerator("Lol");

		[Fact]
		public void AddConstructor_CodeIsCorrect()
		{
			_classGenerator
				.AddConstructor(ctor => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddConstructor_ParameterShorthand_CodeIsCorrect()
		{
			_classGenerator
				.AddConstructor("string lol", ctor => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public Lol(string lol)" +
				"{" +
				"}");
		}

		[Fact]
		public void AddConstructor_Parameter_CodeIsCorrect()
		{
			_classGenerator
				.AddConstructor()
				.WithParameter("int foo")
				.WithContent(ctor => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public Lol(int foo)" +
				"{" +
				"}");
		}

		[Fact]
		public void AddConstructor_Parameters_CodeIsCorrect()
		{
			_classGenerator
				.AddConstructor()
				.WithParameters("int foo", "string bar")
				.WithContent(ctor => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public Lol(int foo, string bar)" +
				"{" +
				"}");
		}

		[Fact]
		public void AddConstructor_Modifier_CodeIsCorrect()
		{
			_classGenerator
				.AddConstructor()
				.WithModifier("private")
				.WithContent(ctor => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddConstructor_Modifiers_CodeIsCorrect()
		{
			_classGenerator
				.AddConstructor()
				.WithModifiers("private", "static")
				.WithContent(ctor => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private static Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddConstructor_ModifierWithSpace_CodeIsCorrect()
		{
			_classGenerator
				.AddConstructor()
				.WithModifier("private static")
				.WithContent(ctor => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private static Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddConstructor_ModifiersWithSpace_CodeIsCorrect()
		{
			_classGenerator
				.AddConstructor()
				.WithModifiers("private static")
				.WithContent(ctor => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private static Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddMethod_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol", method => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public void Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddMethod_Modifier_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol")
				.WithModifier("private")
				.WithContent(method => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private void Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddMethod_Modifiers_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol")
				.WithModifiers("private", "static")
				.WithContent(method => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private static void Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddMethod_ModifierWithSpace_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol")
				.WithModifier("private static")
				.WithContent(method => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private static void Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddMethod_ModifiersWithSpace_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol")
				.WithModifiers("private static")
				.WithContent(method => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"private static void Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddMethod_Parameter_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol")
				.WithParameter("string lol")
				.WithContent(method => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public void Lol(string lol)" +
				"{" +
				"}");
		}

		[Fact]
		public void AddMethod_Parameters_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol")
				.WithParameter("string lol, int foo")
				.WithContent(method => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public void Lol(string lol, int foo)" +
				"{" +
				"}");
		}

		[Fact]
		public void AddMethod_ReturnType_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol")
				.WithReturnType("string")
				.WithContent(method => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public string Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddMethod_ReturnTypeGeneric_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol")
				.WithReturnType<string>()
				.WithContent(method => { });

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public String Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddMethod_AsyncAwait_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol")
				.WithModifier("public async")
				.WithReturnType("Task<int>")
				.WithContent(method => method
					.AddLine("await Task.Delay(100);")
					.AddLine("return 3;"));

			var code = _codeGenerator.Generate();

			RemoveSpecialChars(code).Should().Be(
				"public async Task<int> Lol()" +
				"{" +
				"await Task.Delay(100);" +
				"return 3;" +
				"}");
		}

		private static string RemoveSpecialChars(string text) => text
			.Replace("\t", "")
			.Replace(Environment.NewLine, "");
	}
}
