using System;
using System.Collections.Generic;

namespace Abel.MetaCode.Interfaces
{
	public interface ICodeGen
	{
		ICodeGen AddLine();

		ICodeGen AddLine(string line);

		ICodeGen AddLines(IEnumerable<string> lines);

		ICodeGen Using(string namespaceName);

		ICodeGen AddUsings(IEnumerable<string> namespaceNames);

		ICodeGen AddScoped(string line, Action<ICodeGen> action);

		ICodeGen AddNamespace(string namespaceName, Action<ICodeGen> action);

		ICodeGen AddClass(string className, Action<IClassGen> action);

		IWithClass AddClass(string className);

		string Generate();
	}
}