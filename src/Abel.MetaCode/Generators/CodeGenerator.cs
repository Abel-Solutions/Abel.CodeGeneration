using System;
using System.Collections.Generic;
using System.Linq;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class CodeGenerator : Generator, ICodeGenerator
	{
		public CodeGenerator()
			: base(new CodeWriter())
		{
		}

		public new ICodeGenerator AddLine() => (ICodeGenerator)base.AddLine();

		public new ICodeGenerator AddLine(string line) => (ICodeGenerator)base.AddLine(line);

		public new ICodeGenerator AddLines(IEnumerable<string> lines) => (ICodeGenerator)base.AddLines(lines);

		public ICodeGenerator Using(string namespaceName) => AddLine($"using {namespaceName};");

		public ICodeGenerator AddUsings(IEnumerable<string> namespaceNames) =>
			AddLines(namespaceNames.Select(namespaceName => $"using {namespaceName};"))
				.AddLine();

		public ICodeGenerator AddUsings(params string[] namespaceNames) => AddUsings(namespaceNames.ToList());

		public ICodeGenerator AddNamespace(string namespaceName, Action<ICodeGenerator> action) =>
			(ICodeGenerator)AddScoped($"namespace {namespaceName}", this, action);

		public ICodeGenerator AddClass(string className, Action<IClassGenerator> action) =>
			(ICodeGenerator)AddScoped($"public class {className}", ToClassGenerator(className), action);

		public IWithClass AddClass(string className) =>
			new WithClass(className, this);

		public string Generate() => _codeWriter.ToString();
	}
}
