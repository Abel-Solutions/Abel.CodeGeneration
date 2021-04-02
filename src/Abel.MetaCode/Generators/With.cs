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
		protected readonly TGenerator _generator;
		protected readonly string _name;

		private readonly IList<string> _modifiers = new List<string>();
		private readonly IList<string> _genericTypeNames = new List<string>();
		private readonly IList<string> _parentNames = new List<string>();
		private readonly IList<string> _constraints = new List<string>();
		private readonly IList<string> _parameters = new List<string>();

		protected With(string name, TGenerator generator)
		{
			_name = name;
			_generator = generator;
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

		public TGenerator WithContent<T>(Action<T> action, T generator)
		{
			_generator.AddScoped(Line(), generator, action);
			return _generator;
		}

		protected abstract string Line();

		protected string Modifiers() => _modifiers.Any() ?
			string.Join(" ", _modifiers.Distinct()) :
			"public";

		protected string Generics() => _genericTypeNames.Any() ?
			$"<{string.Join(", ", _genericTypeNames)}>"
			: null;

		protected string Parents() => _parentNames.Any() ?
			$" : {string.Join(", ", _parentNames)}" :
			null;

		protected string Constraints() => _constraints.Any() ?
			string.Join(string.Empty, _constraints) :
			null;

		protected string Parameters() => string.Join(", ", _parameters);
	}
}
