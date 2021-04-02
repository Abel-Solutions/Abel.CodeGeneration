using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithClass : With<ICodeGenerator>, IWithClass
	{
		protected override string Line => $"{Modifiers} class {Name}{Generics}{Parents}{Constraints}";

		public WithClass(string name, ICodeGenerator generator)
			: base(name, generator)
		{
		}

		public IWithClass WithModifiers(params string[] modifiers) =>
			WithModifiers(modifiers, this);

		public IWithClass WithModifier(string modifier) =>
			WithModifiers(modifier);

		public IWithClass WithParents(params string[] parentNames) =>
			WithParents(parentNames, this);

		public IWithClass WithParent(string parentName) =>
			WithParents(parentName);

		public IWithClass WithParent<T>() =>
			WithParent(typeof(T).Name);

		public IWithClass WithGenericType(string typeName) =>
			WithGenericType(typeName, this);

		public IWithClass WithGenericType(string typeName, string constraintTypeName) =>
			WithGenericType(typeName, constraintTypeName, this);

		public ICodeGenerator WithContent(Action<IClassGenerator> action) =>
			WithContent(action, Generator.ToClassGenerator(Name));
	}
}
