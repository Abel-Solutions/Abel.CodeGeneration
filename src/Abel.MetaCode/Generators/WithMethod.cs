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

		private string _modifiers = "public";
		private string _returnTypeName = "void";

		private readonly IList<string> _parameters = new List<string>();

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

		public IWithMethod WithParameters(params ParameterInfo[] parameters)
		{
			WithParameters(parameters.Select(p => $"{p.ParameterType.Name} {p.Name}").ToArray());
			return this;
		}

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			_classGenerator.AddScoped(Line(), action);

		private string Line() => $"{_modifiers} {_returnTypeName} {_name}({Parameters()})";

		private string Parameters() => string.Join(", ", _parameters);
	}
}
