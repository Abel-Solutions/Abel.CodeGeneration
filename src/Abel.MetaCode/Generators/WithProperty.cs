using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithProperty : With<IClassGenerator>, IWithProperty
	{
		public WithProperty(string name, IClassGenerator classGenerator)
			: base(name, classGenerator) =>
			ReturnTypeName = "object";

		public new IWithProperty WithModifiers(params string[] modifiers) => (IWithProperty)base.WithModifiers(modifiers);

		public IWithProperty WithModifier(string modifier) => WithModifiers(modifier);

		public new IWithProperty WithReturnType(string typeName) => (IWithProperty)base.WithReturnType(typeName);

		public IWithProperty WithReturnType(Type type) => WithReturnType(type.Name);

		public IWithProperty WithReturnType<TResult>() => WithReturnType(typeof(TResult));

		public IClassGenerator WithContent(Action<IPropertyGenerator> action) =>
			WithContent(action, Generator.ToPropertyGenerator());

		protected override string Line() => $"{Modifiers()} {ReturnTypeName} {Name}";
	}
}
