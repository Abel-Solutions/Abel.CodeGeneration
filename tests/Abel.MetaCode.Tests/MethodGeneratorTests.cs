using Abel.MetaCode.Generators;
using Abel.MetaCode.Interfaces;
using Abel.MetaCode.Tests.Extensions;
using FluentAssertions;
using Xunit;

namespace Abel.MetaCode.Tests
{
	public class MethodGeneratorTests
	{
		private readonly ICodeGenerator _codeGenerator = new CodeGenerator();
		private readonly IMethodGenerator _methodGenerator;

		public MethodGeneratorTests() => _methodGenerator = _codeGenerator
			.ToClassGenerator("Lol")
			.ToMethodGenerator();

		[Fact]
		public void AddScoped_foreach_CodeIsCorrect()
		{
			_methodGenerator.AddScoped("foreach(var e in list)", method => method
				.AddLine("Console.WriteLine(e);"));

			var code = _codeGenerator.Generate();

			code.RemoveSpecialCharacters().Should().Be(
				"foreach(var e in list)" +
				"{" +
				"Console.WriteLine(e);" +
				"}");
		}
	}
}
