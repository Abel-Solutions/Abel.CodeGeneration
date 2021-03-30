using System;
using System.Collections.Generic;
using System.Text;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class CodeGenerator : ICodeGenerator
	{
		private int _indents;

		private readonly StringBuilder _sb;

		public CodeGenerator()
			: this(new StringBuilder())
		{
		}

		public CodeGenerator(StringBuilder sb) => _sb = sb;

		public ICodeGenerator AddLine() => AddLine(string.Empty);

		public ICodeGenerator AddLine(string line)
		{
			_sb.AppendLine(new string('\t', _indents) + line);
			return this;
		}

		public ICodeGenerator AddLines(IEnumerable<string> lines)
		{
			lines.ForEach(line => AddLine(line));
			return this;
		}

		public ICodeGenerator Using(string namespaceName) => AddLine($"using {namespaceName};");

		public ICodeGenerator AddUsings(IEnumerable<string> namespaceNames)
		{
			namespaceNames.ForEach(namespaceName => Using(namespaceName));
			AddLine();
			return this;
		}

		public ICodeGenerator AddScoped(string line, Action<ICodeGenerator> action)
		{
			AddLine(line);
			AddLine("{");
			_indents++;
			action(this);
			_indents--;
			AddLine("}");
			return this;
		}

		public ICodeGenerator AddNamespace(string namespaceName, Action<ICodeGenerator> action) =>
			AddScoped($"namespace {namespaceName}", action);

		public ICodeGenerator AddClass(string className, Action<IClassGenerator> action) =>
			AddScoped($"public class {className}", gen => action(ToClassGenerator(className)));

		public IWithClass AddClass(string className) =>
			new WithClass(className, this);

		public string Generate() => _sb.ToString();

		public IClassGenerator ToClassGenerator(string className) => new ClassGenerator(className, _sb);
	}
}
