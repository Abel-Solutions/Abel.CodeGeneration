using System;
using System.Collections.Generic;
using System.Linq;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithClass : IWithClass
	{
		private readonly ICodeGenerator _codeGenerator;
		private readonly string _name;

		private string _modifiers = "public";

		private readonly IList<string> _parentNames = new List<string>();

		private readonly IList<string> _genericTypeNames = new List<string>();

		public WithClass(string name, ICodeGenerator codeGenerator)
		{
			_name = name;
			_codeGenerator = codeGenerator;
		}

		public IWithClass WithModifiers(string modifiers)
		{
			_modifiers = modifiers;
			return this;
		}

		public IWithClass WithParent(string parentName)
		{
			_parentNames.Add(parentName);
			return this;
		}

		public IWithClass WithParent<T>() => WithParent(typeof(T).Name);

		public IWithClass WithGenericType(string typeName)
		{
			_genericTypeNames.Add(typeName);
			return this;
		}

		public IWithClass WithGenericType<T>() => WithGenericType(typeof(T).Name);

		public ICodeGenerator WithContent(Action<IClassGenerator> action) =>
			_codeGenerator.AddScoped(Line(), _codeGenerator.ToClassGenerator(_name), action);

		private string Line() =>
			$"{_modifiers} class {_name}{Generics()}{Parents()}";

		private string Generics() =>
			_genericTypeNames.Any() ?
				$"<{string.Join(", ", _genericTypeNames)}>"
				: null;

		private string Parents() =>
			_parentNames.Any() ?
				$" : {string.Join(", ", _parentNames)}" :
				null;
	}
}
