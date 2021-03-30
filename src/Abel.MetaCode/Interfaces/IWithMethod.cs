using System;
using System.Reflection;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithMethod
	{
		IWithMethod WithModifiers(string modifiers);

		IWithMethod WithReturnType(string returnTypeName);

		IWithMethod WithParameters(string parameters);

		IWithMethod WithParameters(ParameterInfo[] parameters);

		IClassGenerator WithContent(Action<IMethodGenerator> action);
	}
}
