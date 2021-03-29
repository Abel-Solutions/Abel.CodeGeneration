using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWith
	{
		IWith WithModifiers(string modifiers);

		ICodeGen WithContent(Action<ICodeGen> action);
	}
}
