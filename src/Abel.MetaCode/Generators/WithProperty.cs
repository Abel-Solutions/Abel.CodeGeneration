using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithProperty : With<IClassGenerator>, IWithProperty
	{
		private string _returnTypeName = "object";

		public WithProperty(string name, IClassGenerator classGenerator)
			: base(name, classGenerator)
		{
		}

		public new IWithProperty WithModifiers(params string[] modifiers) => (IWithProperty)base.WithModifiers(modifiers);

		public IWithProperty WithModifier(string modifier) => WithModifiers(modifier);

		public IWithProperty WithReturnType(string typeName)
		{
			_returnTypeName = typeName;
			return this;
		}

		public IWithProperty WithReturnType(Type type) => WithReturnType(type.Name);

		public IWithProperty WithReturnType<TResult>() => WithReturnType(typeof(TResult));

		public IClassGenerator WithContent(Action<IPropertyGenerator> action) =>
			WithContent(action, _generator.ToPropertyGenerator());

		protected override string Line() => $"{Modifiers()} {_returnTypeName} {_name}";
	}
}
