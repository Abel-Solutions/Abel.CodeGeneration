using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithClass : IWith
	{
		IWithClass WithParent(string parentName);

		ICodeGen WithContent(Action<IClassGen> action);
	}
}