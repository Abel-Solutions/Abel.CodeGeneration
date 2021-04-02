using System;
using System.Collections.Generic;
using System.Linq;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public abstract class With<TGenerator> : IWith
		where TGenerator : IGenerator
	{
		protected readonly TGenerator Generator;
		protected readonly string Name;

		protected string ReturnTypeName = "object";

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

		public IWith WithParameters(params string[] parameters)
		{
			_parameters.AddRange(parameters);
			return this;
		}

		public IWith WithModifiers(string[] modifiers)
		{
			_modifiers.AddRange(modifiers.SelectMany(m => m.Split(" ")));
			return this;
		}

		public IWith WithParents(string[] parentNames)
		{
			_parentNames.AddRange(parentNames);
			return this;
		}

		public IWith WithGenericType(string typeName)
		{
			_genericTypeNames.Add(typeName);
			return this;
		}

		public IWith WithGenericType(string typeName, string constraintTypeName)
		{
			_constraints.Add($" where {typeName} : {constraintTypeName}");
			return WithGenericType(typeName);
		}

		public IWith WithReturnType(string typeName)
		{
			ReturnTypeName = typeName;
			return this;
		}

		public TGenerator WithContent<T>(Action<T> action, T generator)
		{
			Generator.AddScoped(Line, generator, action);
			return Generator;
		}

		protected abstract string Line { get; }

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
