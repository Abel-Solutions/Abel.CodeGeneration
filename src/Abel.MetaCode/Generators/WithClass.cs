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

		public new IWithClass WithModifiers(params string[] modifiers) => (IWithClass)base.WithModifiers(modifiers);

		public IWithClass WithModifier(string modifier) => WithModifiers(modifier);

		public new IWithClass WithParents(params string[] parentNames) => (IWithClass)base.WithParents(parentNames);

		public IWithClass WithParent(string parentName) => WithParents(parentName);

		public IWithClass WithParent<T>() => WithParent(typeof(T).Name);

		public new IWithClass WithGenericType(string typeName) => (IWithClass)base.WithGenericType(typeName);

		public new IWithClass WithGenericType(string typeName, string constraintTypeName) =>
			(IWithClass)base.WithGenericType(typeName, constraintTypeName);

		public ICodeGenerator WithContent(Action<IClassGenerator> action) =>
			WithContent(action, Generator.ToClassGenerator(Name));
	}
}
