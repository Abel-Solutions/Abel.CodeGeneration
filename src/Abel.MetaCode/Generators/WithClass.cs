using System;
using System.Collections.Generic;
using System.Linq;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithClass : IWithClass
	{
		private readonly ICodeGenerator _codeGenerator;
		private readonly string _name;

		private readonly IList<string> _modifiers = new List<string> { "public" };
		private readonly IList<string> _genericTypeNames = new List<string>();
		private readonly IList<string> _parentNames = new List<string>();

		public WithClass(string name, ICodeGenerator codeGenerator)
		{
			_name = name;
			_codeGenerator = codeGenerator;
		}

		public IWithClass WithModifiers(params string[] modifiers)
		{
			_modifiers.AddRange(modifiers.SelectMany(m => m.Split(" ")));
			return this;
		}

		public IWithClass WithModifier(string modifier) => WithModifiers(modifier);

		public IWithClass WithParents(params string[] parentNames)
		{
			_parentNames.AddRange(parentNames);
			return this;
		}

		public IWithClass WithParent(string parentName) => WithParents(parentName);

		public IWithClass WithParent<T>() => WithParent(typeof(T).Name);

		public IWithClass WithGenericTypes(params string[] typeNames)
		{
			_genericTypeNames.AddRange(typeNames);
			return this;
		}

		public IWithClass WithGenericType(string typeName) => WithGenericTypes(typeName);

		public IWithClass WithGenericType<T>() => WithGenericTypes(typeof(T).Name);

		public ICodeGenerator WithContent(Action<IClassGenerator> action) =>
			_codeGenerator.AddScoped(Line(), _codeGenerator.ToClassGenerator(_name), action);

		private string Line() => $"{Modifiers()} class {_name}{Generics()}{Parents()}";

		private string Modifiers() => string.Join(" ", _modifiers.Distinct());

		private string Generics() => _genericTypeNames.Any() ?
			$"<{string.Join(", ", _genericTypeNames)}>"
			: null;

		private string Parents() => _parentNames.Any() ?
			$" : {string.Join(", ", _parentNames)}" :
			null;
	}
}
