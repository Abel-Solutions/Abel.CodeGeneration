using System;
using System.Collections.Generic;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class CodeGenerator : ICodeGenerator
	{
		private readonly ICodeWriter _codeWriter = new CodeWriter();

		public ICodeGenerator AddLine() => AddLine(string.Empty);

		public ICodeGenerator AddLine(string line)
		{
			_codeWriter.WriteLine(line);
			return this;
		}

		public ICodeGenerator AddLines(IEnumerable<string> lines)
		{
			_codeWriter.WriteLines(lines);
			return this;
		}

		public ICodeGenerator Using(string namespaceName) => AddLine($"using {namespaceName};");

		public ICodeGenerator AddUsings(IEnumerable<string> namespaceNames)
		{
			namespaceNames.ForEach(namespaceName => Using(namespaceName));
			AddLine();
			return this;
		}

		public ICodeGenerator AddNamespace(string namespaceName, Action<ICodeGenerator> action) =>
			AddScoped($"namespace {namespaceName}", this, action);

		public ICodeGenerator AddClass(string className, Action<IClassGenerator> action) =>
			AddScoped($"public class {className}", ToClassGenerator(className), action);

		public ICodeGenerator AddScoped<TGenerator>(string line, TGenerator generator, Action<TGenerator> action)
		{
			_codeWriter.WriteScoped(line, generator, action);
			return this;
		}

		public IWithClass AddClass(string className) =>
			new WithClass(className, this);

		public string Generate() => _codeWriter.ToString();

		public IClassGenerator ToClassGenerator(string className) => new ClassGenerator(className, _codeWriter);
	}
}
