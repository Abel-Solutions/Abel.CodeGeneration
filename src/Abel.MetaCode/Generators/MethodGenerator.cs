using System.Collections.Generic;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class MethodGenerator : IMethodGenerator
	{
		private readonly ICodeWriter _codeWriter;

		public MethodGenerator(ICodeWriter codeWriter) => _codeWriter = codeWriter;

		public IMethodGenerator AddLine() => AddLine(string.Empty);

		public IMethodGenerator AddLine(string line)
		{
			_codeWriter.WriteLine(line);
			return this;
		}

		public IMethodGenerator AddLines(IEnumerable<string> lines)
		{
			_codeWriter.WriteLines(lines);
			return this;
		}
	}
}
