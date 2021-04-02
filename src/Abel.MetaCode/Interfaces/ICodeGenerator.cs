using System;
using System.Collections.Generic;

namespace Abel.MetaCode.Interfaces
{
	public interface ICodeGenerator : IGenerator
	{
		ICodeGenerator AddLine();

		ICodeGenerator AddLine(string line);

		ICodeGenerator AddLines(IEnumerable<string> lines);

		ICodeGenerator Using(string namespaceName);

		ICodeGenerator AddUsings(IEnumerable<string> namespaceNames);

		ICodeGenerator AddUsings(params string[] namespaceNames);

		ICodeGenerator AddNamespace(string namespaceName, Action<ICodeGenerator> action);

		ICodeGenerator AddClass(string className, Action<IClassGenerator> action);

		IWithClass AddClass(string className);

		string Generate();
	}
}