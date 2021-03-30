using System;
using System.Collections.Generic;
using System.Text;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class ClassGenerator : IClassGenerator
	{
		private readonly string _name;
		private readonly StringBuilder _sb;

		public ClassGenerator(string name, StringBuilder sb)
		{
			_name = name;
			_sb = sb;
		}

		public IClassGenerator AddLine() => AddLine(string.Empty);

		public IClassGenerator AddLine(string line)
		{
			_sb.AppendLine(new string('\t', 0 /* todo */) + line);
			return this;
		}

		public IClassGenerator AddLines(IEnumerable<string> lines)
		{
			lines.ForEach(line => AddLine(line));
			return this;
		}

		public IClassGenerator AddConstructor(Action<IMethodGenerator> action) =>
			AddScoped($"public {_name}()", action);

		public IClassGenerator AddScoped(string line, Action<IMethodGenerator> action)
		{
			AddLine(line);
			AddLine("{");
			//_indents++; todo
			action(ToMethodGenerator());
			//_indents--;
			AddLine("}");
			return this;
		}

		public IWithConstructor AddConstructor() =>
			new WithConstructor(_name, this);

		public IWithMethod AddMethod(string methodName) =>
			new WithMethod(methodName, this);

		public IMethodGenerator ToMethodGenerator() => new MethodGenerator(_sb);
	}
}
