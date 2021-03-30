using System;
using System.Linq;
using System.Reflection;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class WithMethod : IWithMethod
	{
		private readonly string _name;
		private readonly ICodeGenerator _codeGenerator;

		private string _modifiers = "public";
		private string _returnTypeName = "void";
		private string _parameters = string.Empty;

		private string Line => $"{_modifiers} {_returnTypeName} {_name}({_parameters})";

		public WithMethod(string name, ICodeGenerator codeGenerator)
		{
			_name = name;
			_codeGenerator = codeGenerator;
		}

		public IWithMethod WithModifiers(string modifiers)
		{
			_modifiers = modifiers;
			return this;
		}

		public IWithMethod WithReturnType(string returnTypeName)
		{
			_returnTypeName = returnTypeName;
			return this;
		}

		public IWithMethod WithParameters(string parameters)
		{
			_parameters = parameters;
			return this;
		}

		public IWithMethod WithParameters(ParameterInfo[] parameters) =>
			WithParameters(string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}")));

		public ICodeGenerator WithContent(Action<ICodeGenerator> action) =>
			_codeGenerator.AddScoped(Line, action);
	}
}
