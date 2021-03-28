using System;
using System.Collections.Generic;
using System.Text;
using MetaCode.Extensions;

namespace MetaCode
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

		public string Generate() => _sb.ToString();

		//public ICodeGen AddScopes<T>(IEnumerable<T> enumerable, Func<T, string> func, Action<ICodeGen> action)
		//{
		//	enumerable.ForEach(element => AddScoped(func(element), action));
		//	return this;
		//}
	}
}
