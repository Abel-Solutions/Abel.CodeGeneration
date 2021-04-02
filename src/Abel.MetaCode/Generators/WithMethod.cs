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

		public IWithMethod WithModifiers(params string[] modifiers) =>
			WithModifiers(modifiers, this);

		public IWithMethod WithModifier(string modifier) =>
			WithModifiers(modifier);

		public IWithMethod WithReturnType(string typeName) =>
			WithReturnType(typeName, this);

		public IWithMethod WithReturnType(Type type) =>
			WithReturnType(type.Name);

		public IWithMethod WithReturnType<TResult>() =>
			WithReturnType(typeof(TResult));

		public IWithMethod WithParameters(params string[] parameters) =>
			WithParameters(parameters, this);

		public IWithMethod WithParameters(params ParameterInfo[] parameters) =>
			WithParameters(parameters.Select(p => $"{p.ParameterType.Name} {p.Name}").ToArray());

		public IWithMethod WithParameter(string parameter) =>
			WithParameters(parameter);

		public IWithMethod WithParameter(ParameterInfo parameter) =>
			WithParameters(parameter);

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			WithContent(Generator.ToMethodGenerator(), action);
	}
}
