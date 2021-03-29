using System;
using System.Collections.Generic;
using System.Text;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class CodeGen : ICodeGen
	{
		protected int _indents; // todo

		private readonly StringBuilder _sb;

		public CodeGen()
			: this(new StringBuilder())
		{
		}

		public CodeGen(StringBuilder sb) => _sb = sb;

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

		public IClassGen AddClass(string className, Action<IClassGen> action)
		{
			var classGen = new ClassGen(className, _sb);
			AddScoped($"public class {className}", gen => action(classGen));
			return classGen;
		}

		public IWithClass AddClass(string className) =>
			new WithClass(className, this);

		public string Generate() => _sb.ToString();
	}
}
