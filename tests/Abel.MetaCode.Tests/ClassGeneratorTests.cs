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

		private static string RemoveSpecialChars(string text) =>
			text
				.Replace("\t", "")
				.Replace(Environment.NewLine, "");
	}
}
