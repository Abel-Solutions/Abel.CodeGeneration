using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithClass
	{
		IWithClass WithParent(string parentName);

		IWithClass WithParent<T>();

		IWithClass WithGenericType<T>();

		IWithClass WithGenericType(string typeName);

		IWithClass WithModifiers(params string[] modifiers);
		
		ICodeGenerator WithContent(Action<IClassGenerator> action);
	}
}