using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWith
	{
		IWith WithModifiers(string modifiers);

		ICodeGenerator WithContent(Action<ICodeGenerator> action);
	}
}
