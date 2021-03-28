using System;
using System.Text;

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
	}
}
