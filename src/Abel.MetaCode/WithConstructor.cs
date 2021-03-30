using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class WithConstructor : IWithConstructor
	{
		private readonly string _name;
		private readonly ICodeGenerator _codeGenerator;
		
		private string _modifiers = "public";
		private string _parameters = string.Empty;

		private string Line => $"{_modifiers} {_name}({_parameters})";
		
		public WithConstructor(string name, ICodeGenerator codeGenerator)
		{
			_name = name;
			_codeGenerator = codeGenerator;
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

		public ICodeGenerator WithContent(Action<ICodeGenerator> action) =>
			_codeGenerator.AddScoped(Line, action);
	}
}
