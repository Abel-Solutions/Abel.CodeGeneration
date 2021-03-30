using System;
using System.Collections.Generic;
using System.Text;
using Abel.MetaCode.Extensions;

namespace Abel.MetaCode
{
	public class CodeWriter
	{
		private int _indents;

		private readonly StringBuilder _sb = new StringBuilder();

		public void WriteLine(string line) => _sb.AppendLine(new string('\t', _indents) + line);

		public void WriteLines(IEnumerable<string> lines) => lines.ForEach(WriteLine);

		public void WriteScoped<TGenerator>(string line, TGenerator generator, Action<TGenerator> action)
		{
			WriteLine(line);
			WriteLine("{");
			_indents++;
			action(generator);
			_indents--;
			WriteLine("}");
		}

		public override string ToString() => _sb.ToString();
	}
}
