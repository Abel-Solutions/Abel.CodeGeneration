using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithConstructor
	{
		IWithConstructor WithModifiers(params string[] modifiers);

		IWithConstructor WithParameters(params string[] parameters);

		IClassGenerator WithContent(Action<IMethodGenerator> action);
	}
}