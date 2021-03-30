using System;
using System.Collections.Generic;
using System.Text;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class CodeGenerator : MethodGenerator, ICodeGenerator // todo separate?
	{
		private readonly StringBuilder _sb;

		public CodeGenerator()
			: this(new StringBuilder())
		{
		}

		public CodeGenerator(StringBuilder sb)
			: base(sb) =>
			_sb = sb;

		public new ICodeGenerator AddLine() =>
			(ICodeGenerator)base.AddLine(string.Empty);

		public new ICodeGenerator AddLine(string line) =>
			(ICodeGenerator)base.AddLine(line);

		public new ICodeGenerator AddLines(IEnumerable<string> lines) =>
			(ICodeGenerator)base.AddLines(lines);

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
			AddLine(line);
			AddLine("{");
			_indents++;
			action(generator);
			_indents--;
			AddLine("}");
			return this;
		}

		public IWithClass AddClass(string className) =>
			new WithClass(className, this);

		public string Generate() => _sb.ToString();

		public IClassGenerator ToClassGenerator(string className) => new ClassGenerator(className, _sb);
	}
}
