using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithProperty
	{
		IWithProperty WithModifier(string modifier);

		IWithProperty WithModifiers(params string[] modifiers);

		IWithProperty WithReturnType(string typeName);

		IWithProperty WithReturnType(Type type);

		IWithProperty WithReturnType<TResult>();

		IClassGenerator WithContent(Action<IPropertyGenerator> action);
	}
}
