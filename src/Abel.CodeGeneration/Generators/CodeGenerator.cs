using System;
using System.Collections.Generic;
using System.Linq;
using Abel.CodeGeneration.Interfaces;

namespace Abel.CodeGeneration.Generators
{
	public class CodeGenerator : Generator<ICodeGenerator>, ICodeGenerator
	{
		private readonly CodeWriter _codeWriter;

		public CodeGenerator()
			: this(new CodeWriter())
		{
		}

		internal CodeGenerator(CodeWriter codeWriter)
			: base(codeWriter) =>
			_codeWriter = codeWriter;

		public ICodeGenerator Using(string namespaceName) => AddLine($"using {namespaceName};");

		public ICodeGenerator AddUsings(IEnumerable<string> namespaceNames) =>
			AddLines(namespaceNames.Select(namespaceName => $"using {namespaceName};"))
				.AddLine();

		public ICodeGenerator AddUsings(params string[] namespaceNames) => AddUsings(namespaceNames.ToList());

		public ICodeGenerator AddNamespace(string namespaceName, Action<ICodeGenerator> action) =>
			AddScoped($"namespace {namespaceName}", action);

		public ICodeGenerator AddClass(string className, Action<IClassGenerator> action) =>
			AddScoped($"public class {className}", ToClassGenerator(className), action);

		public IWithClass AddClass(string className) =>
			new WithClass(className, this);

		public string Generate() => _codeWriter.ToString();
	}
}
