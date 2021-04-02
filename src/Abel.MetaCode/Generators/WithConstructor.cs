using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithConstructor : With<IClassGenerator, IWithConstructor>, IWithConstructor
	{
		protected override string Line => $"{Modifiers} {Name}({Parameters})";

		public WithConstructor(string name, IClassGenerator classGenerator)
			: base(name, classGenerator)
		{
		}

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			WithContent(Generator.ToMethodGenerator(), action);
	}
}
