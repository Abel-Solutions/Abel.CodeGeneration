using System;
using System.Collections.Generic;

namespace Abel.MetaCode.Interfaces
{
	public interface IClassGenerator : ICodeGenerator
	{
		new IClassGenerator AddLine();

		new IClassGenerator AddLine(string line);

		new IClassGenerator AddLines(IEnumerable<string> lines);

		IClassGenerator AddConstructor(Action<ICodeGenerator> action);

		IWithConstructor AddConstructor();

		IWithMethod AddMethod(string methodName);
	}
}