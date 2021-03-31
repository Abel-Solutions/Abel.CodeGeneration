using System;
using System.Collections.Generic;
using System.Linq;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithConstructor : IWithConstructor
	{
		private readonly string _name;
		private readonly IClassGenerator _classGenerator;

		private readonly IList<string> _modifiers = new List<string>();
		private readonly IList<string> _parameters = new List<string>();

		public WithConstructor(string name, IClassGenerator classGenerator)
		{
			_name = name;
			_classGenerator = classGenerator;
		}

		public IWithConstructor WithModifiers(params string[] modifiers)
		{
			_modifiers.AddRange(modifiers.SelectMany(m => m.Split(" ")));
			return this;
		}

		public IWithConstructor WithModifier(string modifier) => WithModifiers(modifier);

		public IWithConstructor WithParameters(params string[] parameters)
		{
			_parameters.AddRange(parameters);
			return this;
		}

		public IWithConstructor WithParameter(string parameter) => WithParameters(parameter);

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			_classGenerator.AddScoped(Line(), action);

		private string Line() => $"{Modifiers()} {_name}({Parameters()})";

		private string Modifiers() => _modifiers.Any() ?
			string.Join(" ", _modifiers.Distinct()) :
			"public";

		private string Parameters() => string.Join(", ", _parameters);
	}
}
