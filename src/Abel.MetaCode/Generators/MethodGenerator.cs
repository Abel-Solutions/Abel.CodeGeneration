using System.Collections.Generic;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class MethodGenerator : Generator, IMethodGenerator
	{
		public MethodGenerator(ICodeWriter codeWriter)
			: base(codeWriter)
		{
		}

		public IMethodGenerator AddLine() => AddLine(this);

		public IMethodGenerator AddLine(string line) => AddLine(line, this);

		public IMethodGenerator AddLines(IEnumerable<string> lines) => AddLines(lines, this);
	}
}
