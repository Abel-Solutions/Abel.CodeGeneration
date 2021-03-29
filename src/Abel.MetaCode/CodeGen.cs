using System;
using System.Collections.Generic;
using System.Text;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class CodeGen : ICodeGen
	{
		private int _indents;

		private readonly StringBuilder _sb = new StringBuilder();

		public ICodeGen AddLine() => AddLine(string.Empty);

		public ICodeGen AddLine(string line)
		{
			_sb.AppendLine(new string('\t', _indents) + line);
			return this;
		}

		public ICodeGen AddLines(IEnumerable<string> lines)
		{
			lines.ForEach(line => AddLine(line));
			return this;
		}

		public ICodeGen Using(string namespaceName) => AddLine($"using {namespaceName};");

		public ICodeGen AddUsings(IEnumerable<string> namespaceNames)
		{
			namespaceNames.ForEach(namespaceName => Using(namespaceName));
			AddLine();
			return this;
		}

		public ICodeGen AddScoped(string line, Action<ICodeGen> action)
		{
			AddLine(line);
			AddLine("{");
			_indents++;
			action(this);
			_indents--;
			AddLine("}");
			return this;
		}

		public ICodeGen AddNamespace(string namespaceName, Action<ICodeGen> action) =>
			AddScoped($"namespace {namespaceName}", action);

		public ICodeGen AddClass(string className, Action<ICodeGen> action) =>
			AddScoped($"public class {className}", action);

		public ICodeGen AddConstructor(string className, Action<ICodeGen> action) =>
			AddScoped($"public {className}()", action);

		public IWithClass AddClass(string className) =>
			new WithClass(className, this);

		public IWithConstructor AddConstructor(string className) =>
			new WithConstructor(className, this); // todo put in class

		public IWithMethod AddMethod(string methodName) =>
			new WithMethod(methodName, this);

		public string Generate() => _sb.ToString();
	}
}
