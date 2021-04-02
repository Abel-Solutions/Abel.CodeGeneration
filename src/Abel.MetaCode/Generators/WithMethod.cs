using System;
using System.Linq;
using System.Reflection;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithMethod : With<IClassGenerator>, IWithMethod
	{
		protected override string Line => $"{Modifiers} {ReturnTypeName} {Name}({Parameters})";

		public WithMethod(string name, IClassGenerator classGenerator)
			: base(name, classGenerator) =>
			ReturnTypeName = "void";

		public new IWithMethod WithModifiers(params string[] modifiers) => (IWithMethod)base.WithModifiers(modifiers);

		public IWithMethod WithModifier(string modifier) => WithModifiers(modifier);

		public new IWithMethod WithReturnType(string typeName) => (IWithMethod)base.WithReturnType(typeName);

		public IWithMethod WithReturnType(Type type) => WithReturnType(type.Name);

		public IWithMethod WithReturnType<TResult>() => WithReturnType(typeof(TResult));

		public new IWithMethod WithParameters(params string[] parameters) => (IWithMethod)base.WithParameters(parameters);

		public IWithMethod WithParameters(params ParameterInfo[] parameters) =>
			WithParameters(parameters.Select(p => $"{p.ParameterType.Name} {p.Name}").ToArray());

		public IWithMethod WithParameter(string parameter) => WithParameters(parameter);

		public IWithMethod WithParameter(ParameterInfo parameter) => WithParameters(parameter);

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			WithContent(action, Generator.ToMethodGenerator());
	}
}
