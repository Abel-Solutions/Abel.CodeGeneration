using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IClassGen : ICodeGen
	{
		new IClassGen AddLine();

		new IClassGen AddLine(string line);

		IClassGen AddScoped(string line, Action<IClassGen> action);

		IClassGen AddConstructor(Action<ICodeGen> action);

		IWithConstructor AddConstructor();

		IWithMethod AddMethod(string methodName);
	}
}