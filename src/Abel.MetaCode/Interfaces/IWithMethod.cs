using System;
using System.Reflection;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithMethod
	{
		IWithMethod WithModifiers(params string[] modifiers);

		IWithMethod WithReturnType(string typeName);

		IWithMethod WithReturnType(Type type);

		IWithMethod WithReturnType<TResult>();

		IWithMethod WithParameters(params string[] parameters);

		IWithMethod WithParameters(params ParameterInfo[] parameters);

		IClassGenerator WithContent(Action<IMethodGenerator> action);
	}
}
