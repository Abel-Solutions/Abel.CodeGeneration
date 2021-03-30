using System.Collections.Generic;
using System.Text;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class MethodGenerator : IMethodGenerator
	{
		protected int _indents;

		private readonly StringBuilder _sb;

		public MethodGenerator(StringBuilder sb) => _sb = sb;

		public IMethodGenerator AddLine() => AddLine(string.Empty);

		public IMethodGenerator AddLine(string line)
		{
			_sb.AppendLine(new string('\t', _indents) + line);
			return this;
		}

		public IMethodGenerator AddLines(IEnumerable<string> lines)
		{
			lines.ForEach(line => AddLine(line));
			return this;
		}
	}
}
