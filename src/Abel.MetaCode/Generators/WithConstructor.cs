using System;
using System.Collections.Generic;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithConstructor : With<IClassGenerator>, IWithConstructor
	{
		public WithConstructor(string name, IClassGenerator classGenerator)
			: base(name, classGenerator)
		{
		}

		public new IWithConstructor WithModifiers(params string[] modifiers) => (IWithConstructor)base.WithModifiers(modifiers);

		public IWithConstructor WithModifier(string modifier) => WithModifiers(modifier);

		public new IWithConstructor WithParameters(params string[] parameters) => (IWithConstructor)base.WithParameters(parameters);

		public IWithConstructor WithParameter(string parameter) => WithParameters(parameter);

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			WithContent(action, _generator.ToMethodGenerator());

		protected override string Line() => $"{Modifiers()} {_name}({Parameters()})";
	}
}
