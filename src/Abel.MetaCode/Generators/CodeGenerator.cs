using System;
using System.Collections.Generic;
using System.Linq;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class CodeGenerator : Generator<ICodeGenerator>, ICodeGenerator
	{
		public CodeGenerator()
			: base(new CodeWriter())
		{
		}

		public ICodeGenerator AddLine() => AddLine(this);

		public ICodeGenerator AddLine(string line) => AddLine(line, this);

		public ICodeGenerator AddLines(IEnumerable<string> lines) => AddLines(lines, this);

		public ICodeGenerator Using(string namespaceName) => AddLine($"using {namespaceName};");

		public ICodeGenerator AddUsings(IEnumerable<string> namespaceNames) =>
			AddLines(namespaceNames.Select(namespaceName => $"using {namespaceName};"))
				.AddLine();

		public ICodeGenerator AddUsings(params string[] namespaceNames) => AddUsings(namespaceNames.ToList());

		public ICodeGenerator AddNamespace(string namespaceName, Action<ICodeGenerator> action) =>
			AddScoped($"namespace {namespaceName}", this, action);

		public ICodeGenerator AddClass(string className, Action<IClassGenerator> action) =>
			AddScoped($"public class {className}", ToClassGenerator(className), action);

		public ICodeGenerator AddScoped<TGenerator>(string line, TGenerator generator, Action<TGenerator> action) =>
			AddScoped(line, generator, action, this);

		public IWithClass AddClass(string className) =>
			new WithClass(className, this);

		public string Generate() => _codeWriter.ToString();
	}
}
