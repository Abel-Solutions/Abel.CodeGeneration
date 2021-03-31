using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithMethod : IWithMethod
	{
		private readonly string _name;
		private readonly IClassGenerator _classGenerator;

		private readonly IList<string> _modifiers = new List<string>();
		private readonly IList<string> _parameters = new List<string>();

		private string _returnTypeName = "void";

		public WithMethod(string name, IClassGenerator classGenerator)
		{
			_name = name;
			_classGenerator = classGenerator;
		}

		public IWithMethod WithModifiers(params string[] modifiers)
		{
			_modifiers.AddRange(modifiers.SelectMany(m => m.Split(" ")));
			return this;
		}

		public IWithMethod WithModifier(string modifier) => WithModifiers(modifier);

		public IWithMethod WithReturnType(string typeName)
		{
			_returnTypeName = typeName;
			return this;
		}

		public IWithMethod WithReturnType(Type type) => WithReturnType(type.Name);

		public IWithMethod WithReturnType<TResult>() => WithReturnType(typeof(TResult));

		public IWithMethod WithParameters(params string[] parameters)
		{
			_parameters.AddRange(parameters);
			return this;
		}

		public IWithMethod WithParameters(params ParameterInfo[] parameters) =>
			WithParameters(parameters.Select(p => $"{p.ParameterType.Name} {p.Name}").ToArray());

		public IWithMethod WithParameter(string parameter) => WithParameters(parameter);

		public IWithMethod WithParameter(ParameterInfo parameter) => WithParameters(parameter);

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			_classGenerator.AddScoped(Line(), action);

		private string Line() => $"{Modifiers()} {_returnTypeName} {_name}({Parameters()})";

		private string Modifiers() => _modifiers.Any() ?
			string.Join(" ", _modifiers.Distinct()) :
			"public";

		private string Parameters() => string.Join(", ", _parameters);
	}
}
