using System;
using System.Linq;
using System.Reflection;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class WithMethod : IWithMethod
	{
		private readonly string _name;
		private readonly IClassGenerator _classGenerator;

		private string _modifiers = "public";
		private string _returnTypeName = "void";
		private string _parameters = string.Empty;

		private string Line => $"{_modifiers} {_returnTypeName} {_name}({_parameters})";

		public WithMethod(string name, IClassGenerator classGenerator)
		{
			_name = name;
			_classGenerator = classGenerator;
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

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			_classGenerator.AddScoped(Line, action);
	}
}
