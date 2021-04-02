using System;
using System.Collections.Generic;
using System.Linq;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithProperty : IWithProperty
	{
		private readonly string _name;
		private readonly IClassGenerator _classGenerator;

		private readonly IList<string> _modifiers = new List<string>();

		private string _returnTypeName = "object";

		public WithProperty(string name, IClassGenerator classGenerator)
		{
			_name = name;
			_classGenerator = classGenerator;
		}

		public IWithProperty WithModifiers(params string[] modifiers)
		{
			_modifiers.AddRange(modifiers.SelectMany(m => m.Split(" ")));
			return this;
		}

		public IWithProperty WithModifier(string modifier) => WithModifiers(modifier);

		public IWithProperty WithReturnType(string typeName)
		{
			_returnTypeName = typeName;
			return this;
		}

		public IWithProperty WithReturnType(Type type) => WithReturnType(type.Name);

		public IWithProperty WithReturnType<TResult>() => WithReturnType(typeof(TResult));

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			_classGenerator.AddScoped(Line(), action);

		private string Line() => $"{Modifiers()} {_returnTypeName} {_name}";

		private string Modifiers() => _modifiers.Any() ?
			string.Join(" ", _modifiers.Distinct()) :
			"public";
	}
}
