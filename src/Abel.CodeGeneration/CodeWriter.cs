using System;
using System.Collections.Generic;
using System.Text;
using Abel.CodeGeneration.Extensions;
using Abel.CodeGeneration.Interfaces;

namespace Abel.CodeGeneration
{
	public class CodeWriter : ICodeWriter
	{
		private int _indents;

		private readonly StringBuilder _sb = new();

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
