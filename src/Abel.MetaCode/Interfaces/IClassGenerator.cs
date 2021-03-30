using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IClassGenerator : ICodeGenerator
	{
		new IClassGenerator AddLine();

		new IClassGenerator AddLine(string line);

		IClassGenerator AddConstructor(Action<ICodeGenerator> action);

		IWithConstructor AddConstructor();

		IWithMethod AddMethod(string methodName);
	}
}