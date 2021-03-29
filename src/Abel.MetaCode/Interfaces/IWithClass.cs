using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithClass : IWith
	{
		IWithClass WithParent(string parentName);

		IClassGen WithContent(Action<IClassGen> action);
	}
}