using FluentAssertions;
using Xunit;

namespace MetaCode.Tests
{
	public class MockerTests
	{
		[Fact]
		public void Mocker_Object_MethodsAreMocked()
		{
			var mock = new Mocker<ISomething>();
			mock.Setup(m => m.ToString(2), "2");
			mock.Setup(m => m.GetDouble(2), 4);

			mock.Object.ToString(2).Should().Be("2");
			mock.Object.GetDouble(2).Should().Be(4);
		}
	}
}
