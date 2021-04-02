using System;
using System.Collections.Generic;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithConstructor : With<IClassGenerator>, IWithConstructor
	{
		private readonly IList<string> _modifiers = new List<string>();
		private readonly IList<string> _parameters = new List<string>();

		public WithConstructor(string name, IClassGenerator classGenerator)
			: base(name, classGenerator)
		{
		}

		public new IWithConstructor WithModifiers(params string[] modifiers) => (IWithConstructor)base.WithModifiers(modifiers);

		public IWithConstructor WithModifier(string modifier) => WithModifiers(modifier);

		public IWithConstructor WithParameters(params string[] parameters)
		{
			_parameters.AddRange(parameters);
			return this;
		}

		public IWithConstructor WithParameter(string parameter) => WithParameters(parameter);

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			WithContent(action, _generator.ToMethodGenerator());

		protected override string Line() => $"{Modifiers()} {_name}({Parameters()})";

		private string Parameters() => string.Join(", ", _parameters);
	}
}
