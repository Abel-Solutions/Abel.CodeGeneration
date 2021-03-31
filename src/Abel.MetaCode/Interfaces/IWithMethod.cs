using System;
using System.Reflection;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithMethod
	{
		IWithMethod WithModifiers(string modifiers);

		IWithMethod WithReturnType(string typeName);

		IWithMethod WithReturnType(Type type);

		IWithMethod WithReturnType<TResult>();

		IWithMethod WithParameters(string parameters);

		IWithMethod WithParameters(ParameterInfo[] parameters);

		IClassGenerator WithContent(Action<IMethodGenerator> action);
	}
}
