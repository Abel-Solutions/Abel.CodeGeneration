using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abel.CodeGeneration.Extensions;
using Abel.CodeGeneration.Interfaces;

namespace Abel.CodeGeneration.Generators
{
	public abstract class With<TGenerator, TWith>
		where TGenerator : IGenerator<TGenerator>
	{
		protected abstract string Line { get; }

		protected readonly TGenerator Generator;
		protected readonly string Name;

		protected string ReturnTypeName = "object";

		private TWith This => (TWith)(object)this;

		private readonly IList<string> _modifiers = new List<string>();
		private readonly IList<string> _genericTypeNames = new List<string>();
		private readonly IList<string> _parentNames = new List<string>();
		private readonly IList<string> _constraints = new List<string>();
		private readonly IList<string> _parameters = new List<string>();

		protected With(string name, TGenerator generator)
		{
			Name = name;
			Generator = generator;
		}

		public TWith WithParameters(params string[] parameters)
		{
			_parameters.AddRange(parameters);
			return This;
		}

		public TWith WithParameter(string parameter) => WithParameters(parameter);

		public TWith WithParameters(params ParameterInfo[] parameters) =>
			WithParameters(parameters.Select(p => $"{p.ParameterType.Name} {p.Name}").ToArray());

		public TWith WithParameter(ParameterInfo parameter) => WithParameters(parameter);

		public TWith WithModifiers(params string[] modifiers)
		{
			_modifiers.AddRange(modifiers.SelectMany(m => m.Split(" ")));
			return This;
		}

		public TWith WithModifier(string modifier) => WithModifiers(modifier);

		public TWith WithParents(params string[] parentNames)
		{
			_parentNames.AddRange(parentNames);
			return This;
		}

		public TWith WithParent(string parentName) => WithParents(parentName);

		public TWith WithParent<T>() => WithParent(typeof(T).Name);

		public TWith WithGenericType(string typeName)
		{
			_genericTypeNames.Add(typeName);
			return This;
		}

		public TWith WithGenericType(string typeName, string constraintTypeName)
		{
			_constraints.Add($" where {typeName} : {constraintTypeName}");
			return WithGenericType(typeName);
		}

		public TWith WithReturnType(string typeName)
		{
			ReturnTypeName = typeName;
			return This;
		}

		public TWith WithReturnType(Type type) => WithReturnType(type.Name);

		public TWith WithReturnType<TResult>() => WithReturnType(typeof(TResult));

		public TGenerator WithContent<T>(T generator, Action<T> action) =>
			Generator.AddScoped(Line, generator, action);

		protected string Modifiers => _modifiers.Any() ?
			string.Join(" ", _modifiers.Distinct()) :
			"public";

		protected string Generics => _genericTypeNames.Any() ?
			$"<{string.Join(", ", _genericTypeNames)}>"
			: null;

		protected string Parents => _parentNames.Any() ?
			$" : {string.Join(", ", _parentNames)}" :
			null;

		protected string Constraints => _constraints.Any() ?
			string.Join(string.Empty, _constraints) :
			null;

		protected string Parameters => string.Join(", ", _parameters);
	}
}
