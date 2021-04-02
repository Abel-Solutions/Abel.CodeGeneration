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

		public IWithConstructor WithModifiers(params string[] modifiers) =>
			WithModifiers(modifiers, this);

		public IWithConstructor WithModifier(string modifier) =>
			WithModifiers(modifier);

		public IWithConstructor WithParameters(params string[] parameters) =>
			WithParameters(parameters, this);

		public IWithConstructor WithParameter(string parameter) =>
			WithParameters(parameter);

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			Generator.AddScoped(Line, Generator.ToMethodGenerator(), action);
	}
}
