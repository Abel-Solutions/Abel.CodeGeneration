using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithConstructor
	{
		IWithConstructor WithModifiers(string modifiers);

		IWithConstructor WithParameters(string parameters);

		IClassGenerator WithContent(Action<IMethodGenerator> action);
	}
}