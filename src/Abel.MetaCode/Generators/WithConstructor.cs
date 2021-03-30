using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithConstructor : IWithConstructor
	{
		private readonly string _name;
		private readonly IClassGenerator _classGenerator;
		
		private string _modifiers = "public";
		private string _parameters = string.Empty;

		private string Line => $"{_modifiers} {_name}({_parameters})";
		
		public WithConstructor(string name, IClassGenerator classGenerator)
		{
			_name = name;
			_classGenerator = classGenerator;
		}

		public IWithConstructor WithModifiers(string modifiers)
		{
			_modifiers = modifiers;
			return this;
		}

		public IWithConstructor WithParameters(string parameters)
		{
			_parameters = parameters;
			return this;
		}

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			_classGenerator.AddScoped(Line, action);
	}
}
