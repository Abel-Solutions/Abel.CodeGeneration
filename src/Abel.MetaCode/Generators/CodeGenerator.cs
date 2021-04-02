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

		public ICodeGenerator AddLine() => AddLine(this);

		public ICodeGenerator AddLine(string line) => AddLine(line, this);

		public ICodeGenerator AddLines(IEnumerable<string> lines) => AddLines(lines, this);

		public ICodeGenerator AddScoped(string line, IClassGenerator generator, Action<IClassGenerator> action) => 
			AddScoped(line, generator, action, this); // todo simplify more?

		public ICodeGenerator Using(string namespaceName) => AddLine($"using {namespaceName};");

		public ICodeGenerator AddUsings(IEnumerable<string> namespaceNames) =>
			AddLines(namespaceNames.Select(namespaceName => $"using {namespaceName};"))
				.AddLine();

		public ICodeGenerator AddUsings(params string[] namespaceNames) => AddUsings(namespaceNames.ToList());

		public ICodeGenerator AddNamespace(string namespaceName, Action<ICodeGenerator> action) =>
			AddScoped($"namespace {namespaceName}", this, action, this);

		public ICodeGenerator AddClass(string className, Action<IClassGenerator> action) =>
			AddScoped($"public class {className}", ToClassGenerator(className), action, this);

		public IWithClass AddClass(string className) =>
			new WithClass(className, this);

		public string Generate() => CodeWriter.ToString();
	}
}
