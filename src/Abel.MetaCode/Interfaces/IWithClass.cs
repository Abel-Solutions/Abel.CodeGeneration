using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithClass
	{
		IWithClass WithParents(params string[] parentNames);

		IWithClass WithParent<T>();

		IWithClass WithGenericTypes(params string[] typeNames);
		
		IWithClass WithGenericType<T>();

		IWithClass WithModifiers(params string[] modifiers);
		
		ICodeGenerator WithContent(Action<IClassGenerator> action);
	}
}