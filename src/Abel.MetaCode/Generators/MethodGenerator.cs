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

		public new IMethodGenerator AddLine() => (IMethodGenerator)base.AddLine();

		public new IMethodGenerator AddLine(string line) => (IMethodGenerator)base.AddLine(line);

		public new IMethodGenerator AddLines(IEnumerable<string> lines) => (IMethodGenerator)base.AddLines(lines);
	}
}
