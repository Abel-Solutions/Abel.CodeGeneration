using System;
using System.Collections.Generic;

namespace Abel.CodeGeneration.Interfaces
{
	public interface ICodeGenerator : IGenerator<ICodeGenerator>
	{
		ICodeGenerator Using(string namespaceName);

		ICodeGenerator AddUsings(IEnumerable<string> namespaceNames);

		ICodeGenerator AddUsings(params string[] namespaceNames);

		ICodeGenerator AddNamespace(string namespaceName, Action<ICodeGenerator> action);

		ICodeGenerator AddClass(string className, Action<IClassGenerator> action);

		IWithClass AddClass(string className);

		IClassGenerator ToClassGenerator(string className);

		string Generate();
	}
}