using System;
using FluentAssertions;
using Xunit;

namespace MetaCode.Tests
{
	public class MockerTests
	{
		[Fact]
		public void Mocker_Setup_CodeIsCorrect()
		{
			var mock = new Mocker<ISomething>();
			mock.Setup(m => m.ToString(), "2");

			var code = mock.GenerateCode();

			RemoveSpecialChars(code).Should().Be(
				"using System;" +
				"using System.Collections.Generic;" +
				"namespace MetaCode.Tests" +
				"{" +
				"public class ISomethingProxy : ISomething" +
				"{" +
				"IDictionary<string, Func<object>> _methods;" +
				"public ISomethingProxy(IDictionary<string, Func<object>> methods)" +
				"{" +
				"_methods = methods;" +
				"}" +
				"public Int32 GetDouble(Int32 number)" +
				"{" +
				"return (Int32)_methods[\"GetDouble\"]();" +
				"}" +
				"public String ToString(Int32 number)" +
				"{" +
				"return (String)_methods[\"ToString\"]();" +
				"}" +
				"}" +
				"}");
		}

		[Fact]
		public void Mocker_Object_ObjectIsCorrect()
		{
			var mock = new Mocker<ISomething>();
			mock.Setup(m => m.ToString(2), "2");
			mock.Setup(m => m.GetDouble(2), 4);

			mock.Object.ToString(2).Should().Be("2");
			mock.Object.GetDouble(2).Should().Be(4);
		}

		private static string RemoveSpecialChars(string text) =>
			text.Replace("\t", "").Replace(Environment.NewLine, "");
	}
}
