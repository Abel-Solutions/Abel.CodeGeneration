using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithClass
	{
		IWithClass WithParent(string parentName);

		IWithClass WithParents(params string[] parentNames);

		IWithClass WithParent<T>();

		IWithClass WithGenericType(string typeName);

		IWithClass WithGenericType(string typeName, string constraintTypeName);

		IWithClass WithModifier(string modifier);

		IWithClass WithModifiers(params string[] modifiers);
		
		ICodeGenerator WithContent(Action<IClassGenerator> action);
	}
}