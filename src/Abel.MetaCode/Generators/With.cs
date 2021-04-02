using System.Collections.Generic;
using System.Linq;
using Abel.MetaCode.Extensions;

namespace Abel.MetaCode.Generators
{
	public abstract class With<TGenerator>
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

		public TWith WithParameters<TWith>(string[] parameters, TWith with)
		{
			_parameters.AddRange(parameters);
			return with;
		}

		public TWith WithModifiers<TWith>(string[] modifiers, TWith with)
		{
			_modifiers.AddRange(modifiers.SelectMany(m => m.Split(" ")));
			return with;
		}

		public TWith WithParents<TWith>(string[] parentNames, TWith with)
		{
			_parentNames.AddRange(parentNames);
			return with;
		}

		public TWith WithGenericType<TWith>(string typeName, TWith with)
		{
			_genericTypeNames.Add(typeName);
			return with;
		}

		public TWith WithGenericType<TWith>(string typeName, string constraintTypeName, TWith with)
		{
			_constraints.Add($" where {typeName} : {constraintTypeName}");
			return WithGenericType(typeName, with);
		}

		public TWith WithReturnType<TWith>(string typeName, TWith with)
		{
			ReturnTypeName = typeName;
			return with;
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
