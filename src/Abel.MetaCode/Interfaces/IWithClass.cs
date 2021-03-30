using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithClass
	{
		IWithClass WithParent(string parentName);

		IWithClass WithModifiers(string modifiers);
		
		ICodeGenerator WithContent(Action<IClassGenerator> action);
	}
}