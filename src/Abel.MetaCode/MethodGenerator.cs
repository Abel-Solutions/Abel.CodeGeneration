using System.Collections.Generic;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class MethodGenerator : IMethodGenerator
	{
		private readonly CodeWriter _codeWriter;

		public MethodGenerator(CodeWriter codeWriter) => _codeWriter = codeWriter;

		public IMethodGenerator AddLine() => AddLine(string.Empty);

		public IMethodGenerator AddLine(string line)
		{
			_codeWriter.WriteLine(line);
			return this;
		}

		public IMethodGenerator AddLines(IEnumerable<string> lines)
		{
			lines.ForEach(line => AddLine(line));
			return this;
		}
	}
}
