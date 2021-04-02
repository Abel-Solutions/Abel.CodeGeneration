using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithProperty : With<IClassGenerator>, IWithProperty
	{
		protected override string Line => $"{Modifiers} {ReturnTypeName} {Name}";

		public WithProperty(string name, IClassGenerator classGenerator)
			: base(name, classGenerator) =>
			ReturnTypeName = "object";

		public IWithProperty WithModifiers(params string[] modifiers) =>
			WithModifiers(modifiers, this);

		public IWithProperty WithModifier(string modifier) =>
			WithModifiers(modifier);

		public IWithProperty WithReturnType(string typeName) =>
			WithReturnType(typeName, this);

		public IWithProperty WithReturnType(Type type) =>
			WithReturnType(type.Name);

		public IWithProperty WithReturnType<TResult>() =>
			WithReturnType(typeof(TResult));

		public IClassGenerator WithContent(Action<IPropertyGenerator> action) =>
			WithContent(Generator.ToPropertyGenerator(), action);
	}
}
