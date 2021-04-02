using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithConstructor : With<IClassGenerator>, IWithConstructor
	{
		protected override string Line => $"{Modifiers} {Name}({Parameters})";

		public WithConstructor(string name, IClassGenerator classGenerator)
			: base(name, classGenerator)
		{
		}

		public new IWithConstructor WithModifiers(params string[] modifiers) => (IWithConstructor)base.WithModifiers(modifiers);

		public IWithConstructor WithModifier(string modifier) => WithModifiers(modifier);

		public new IWithConstructor WithParameters(params string[] parameters) => (IWithConstructor)base.WithParameters(parameters);

		public IWithConstructor WithParameter(string parameter) => WithParameters(parameter);

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			WithContent(action, Generator.ToMethodGenerator());
	}
}
