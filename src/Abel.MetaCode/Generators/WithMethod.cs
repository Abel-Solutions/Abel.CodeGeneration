using System;
using System.Linq;
using System.Reflection;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithMethod : With<IClassGenerator>, IWithMethod
	{
		private string _returnTypeName = "void";

		public WithMethod(string name, IClassGenerator classGenerator)
			: base(name, classGenerator)
		{
		}

		public new IWithMethod WithModifiers(params string[] modifiers) => (IWithMethod)base.WithModifiers(modifiers);

		public IWithMethod WithModifier(string modifier) => WithModifiers(modifier);

		public IWithMethod WithReturnType(string typeName)
		{
			_returnTypeName = typeName;
			return this;
		}

		public IWithMethod WithReturnType(Type type) => WithReturnType(type.Name);

		public IWithMethod WithReturnType<TResult>() => WithReturnType(typeof(TResult));

		public new IWithMethod WithParameters(params string[] parameters) => (IWithMethod)base.WithParameters(parameters);

		public IWithMethod WithParameters(params ParameterInfo[] parameters) =>
			WithParameters(parameters.Select(p => $"{p.ParameterType.Name} {p.Name}").ToArray());

		public IWithMethod WithParameter(string parameter) => WithParameters(parameter);

		public IWithMethod WithParameter(ParameterInfo parameter) => WithParameters(parameter);

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			WithContent(action, _generator.ToMethodGenerator());

		protected override string Line() => $"{Modifiers()} {_returnTypeName} {_name}({Parameters()})"; // todo properties
	}
}
