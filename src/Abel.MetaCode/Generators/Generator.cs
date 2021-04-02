using System.Collections.Generic;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public abstract class Generator<TGenerator>
	{
		protected readonly ICodeWriter _codeWriter; // todo why protected?

		protected Generator(ICodeWriter codeWriter) => _codeWriter = codeWriter;

		public TGenerator AddLine(TGenerator generator) => AddLine(string.Empty, generator);

		public TGenerator AddLine(string line, TGenerator generator)
		{
			_codeWriter.WriteLine(line);
			return generator;
		}

		public TGenerator AddLines(IEnumerable<string> lines, TGenerator generator)
		{
			_codeWriter.WriteLines(lines);
			return generator;
		}
	}
}
