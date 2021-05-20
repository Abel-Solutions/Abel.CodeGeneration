using System;
using System.Reflection;

namespace Abel.CodeGeneration.Interfaces
{
	public interface IWithMethod
	{
		IWithMethod WithModifier(string modifier);

		IWithMethod WithModifiers(params string[] modifiers);

		IWithMethod WithReturnType(string typeName);

		IWithMethod WithReturnType(Type type);

		IWithMethod WithReturnType<TResult>();

		IWithMethod WithParameter(string parameter);

		IWithMethod WithParameters(params string[] parameters);

		IWithMethod WithParameter(ParameterInfo parameter);

		IWithMethod WithParameters(params ParameterInfo[] parameters);

		IClassGenerator WithContent(Action<IMethodGenerator> action);
	}
}
