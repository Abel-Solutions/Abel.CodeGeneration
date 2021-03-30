using System;
using System.Collections.Generic;

namespace Abel.MetaCode.Interfaces
{
	public interface ICodeGenerator
	{
		ICodeGenerator AddLine();

		ICodeGenerator AddLine(string line);

		ICodeGenerator AddLines(IEnumerable<string> lines);

		ICodeGenerator Using(string namespaceName);

		ICodeGenerator AddUsings(IEnumerable<string> namespaceNames);

		ICodeGenerator AddScoped(string line, Action<ICodeGenerator> action);

		ICodeGenerator AddNamespace(string namespaceName, Action<ICodeGenerator> action);

		ICodeGenerator AddClass(string className, Action<IClassGenerator> action);

		IWithClass AddClass(string className);

		IClassGenerator ToClassGenerator(string className);

		string Generate();
	}
}