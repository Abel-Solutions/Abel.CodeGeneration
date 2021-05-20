using Abel.MetaCode.Generators;
using Abel.MetaCode.Interfaces;
using Abel.MetaCode.Tests.Extensions;
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
                .AddConstructor(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"public Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddConstructor_ParameterShorthand_CodeIsCorrect()
		{
			_classGenerator
                .AddConstructor("string lol", _ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"private static Lol()" +
				"{" +
				"}");
		}

		[Fact]
		public void AddMethod_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol", _ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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
				.WithContent(_ => { });

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
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

			code.RemoveSpecialCharacters().Should().Be(
				"public async Task<int> Lol()" +
				"{" +
				"await Task.Delay(100);" +
				"return 3;" +
				"}");
		}

		[Fact]
		public void AddMethod_Expression_CodeIsCorrect()
		{
			_classGenerator
				.AddMethod("Lol", 3);

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be("public Int32 Lol() => 3;");
		}

		[Fact]
		public void AddProperty_Expression_CodeIsCorrect()
		{
			_classGenerator
				.AddProperty("Lol", 3);

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be("public Int32 Lol => 3;");
		}

		[Fact]
		public void AddProperty_Line_CodeIsCorrect()
		{
			_classGenerator
				.AddProperty("Lol")
				.WithContent(prop => prop
				.AddLine("get => 3;"));

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"public object Lol" +
				"{" +
				"get => 3;" +
				"}");
		}

		[Fact]
		public void AddProperty_ReturnTypeGeneric_CodeIsCorrect()
		{
			_classGenerator
				.AddProperty("Lol")
				.WithReturnType<int>()
				.WithContent(prop => prop
					.AddLine("get => 3;"));

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"public Int32 Lol" +
				"{" +
				"get => 3;" +
				"}");
		}

		[Fact]
		public void AddProperty_ReturnType_CodeIsCorrect()
		{
			_classGenerator
				.AddProperty("Lol")
				.WithReturnType("int")
				.WithContent(prop => prop
					.AddLine("get => 3;"));

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"public int Lol" +
				"{" +
				"get => 3;" +
				"}");
		}

		[Fact]
		public void AddProperty_Modifier_CodeIsCorrect()
		{
			_classGenerator
				.AddProperty<int>("Lol")
				.WithModifier("private")
				.WithContent(prop => prop
					.AddLine("get => 3;"));

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"private Int32 Lol" +
				"{" +
				"get => 3;" +
				"}");
		}

		[Fact]
		public void AddProperty_GetAndSetLines_CodeIsCorrect()
		{
			_classGenerator
				.AddProperty<int>("Lol")
				.WithContent(prop => prop
					.AddLine("get => 3;")
					.AddLine("set => value = 3;"));

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"public Int32 Lol" +
				"{" +
				"get => 3;" +
				"set => value = 3;" +
				"}");
		}

		[Fact]
		public void AddProperty_GetAndSet_CodeIsCorrect()
		{
			_classGenerator
				.AddProperty<int>("Lol")
				.WithContent(prop => prop
					.Get(3)
					.Set(3));

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"public Int32 Lol" +
				"{" +
				"get => 3;" +
				"set => value = 3;" +
				"}");
		}

		[Fact]
		public void AddProperty_GetAndSetMethods_CodeIsCorrect()
		{
			_classGenerator
				.AddProperty<int>("Lol")
				.WithContent(prop => prop
					.Get(method => method
						.AddLine("return 3;"))
					.Set(method => method
						.AddLine("value = 3;")));

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"public Int32 Lol" +
				"{" +
				"get" +
				"{" +
				"return 3;" +
				"}" +
				"set" +
				"{" +
				"value = 3;" +
				"}" +
				"}");
		}
	}
}
