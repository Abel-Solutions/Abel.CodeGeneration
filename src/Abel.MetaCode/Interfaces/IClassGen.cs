using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IClassGen : ICodeGen
	{
		new IClassGen AddLine();

		new IClassGen AddLine(string line);

		IClassGen AddConstructor(Action<ICodeGen> action);

		IWithConstructor AddConstructor();

		IWithMethod AddMethod(string methodName);
	}
}