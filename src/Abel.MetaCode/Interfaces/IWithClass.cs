using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithClass : IWith
	{
		IWithClass WithParent(string parentName);

		ICodeGenerator WithContent(Action<IClassGenerator> action);
	}
}